/*
 * DASD_Routines: Based on routines originally written in C by Roger Bowler and Malcom Beattie for support of the DASD files for Hercules.
 *
 * Copyright (C) 2022, Donald L Tuel, All rights reserved.
 * 
 * This program is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License along with this program. If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using CustomExtensions;
using zlib;
using Hercules_DASD_Utility.Properties;


namespace Hercules_DASD_Utility
{
    public class DASD_Routines
    {
        private static List<string[]> symbolTable;

        #region "Read routines"
        /*-------------------------------------------------------------------*/
        /* Subroutine to read a track from the CKD DASD image                */
        /* Input:                                                            */
        /*      cif     -> CKD image file descriptor structure               */
        /*      cyl     Cylinder number                                      */
        /*      head    Head number                                          */
        /*      relTrk  Track relative to base track                         */
        /* Output:                                                           */
        /*      The track is read into trkbuf, and curcyl and curhead        */
        /*      are set to the cylinder and head number.                     */
        /*                                                                   */
        /* Return value is 0 if successful, -1 if error                      */
        /*-------------------------------------------------------------------*/
        public static int read_track(ref DEVBLK dev, Int32 cyl, Int16 head)
        {
	        int             rc;                     /* Return code               */
	        Int32           trk;                    /* Track number              */
	        Byte            unitstat = 0x00;        /* Unit status               */

            /* Exit if required track is already in buffer */
            if ((dev.curcyl == cyl) && (dev.curhead == head)) { return 0; }

            /* has the track been modified? */
            if (dev.trkmodif)
            {
                dev.trkmodif = false;

                trk = (dev.curcyl * dev.heads) + dev.curhead;
                rc = dev.devHnd.Write(dev, trk, 0, null, dev.trksz, ref unitstat);

                if (rc < 0)
                {
                //    fprintf(stderr, MSG(HHC00446, "E", SSID_TO_LCSS(cif->devblk.ssid), cif->devblk.devnum, cif->fname,
                //        unitstat));
                    return -1;
                }
            }

            trk = (Int32)(cyl * dev.heads) + head;
            rc = dev.devHnd.Read(ref dev, trk, ref unitstat);

            if (rc < 0)
            {
            //    fprintf(stderr, MSG(HHC00448, "E", SSID_TO_LCSS(cif->devblk.ssid), cif->devblk.devnum, cif->fname,
            //        unitstat));
                return -1;
            }

            /* Set current buf, cylinder and head */
            dev.curcyl = cyl;
            dev.curhead = head;

	        return 0;
        } /* end function read_track */

        /*-------------------------------------------------------------------*/
        /* Subroutine to read a record from the CKD DASD image               */
        /* Input:                                                            */
        /*      cif     CKD image file descriptor structure                  */
        /*      cyl     Cylinder number of requested block                   */
        /*      head    Head number of requested block                       */
        /*      rec     Record number of requested block                     */
        /* Output:                                                           */
        /*      key     Record key                                           */
        /*      keylen  Actual key length                                    */
        /*      data    Record data                                          */
        /*      datalen Actual data length                                   */
        /*                                                                   */
        /* Return value is 0 if successful, +1 if end of track, -1 if error  */
        /*-------------------------------------------------------------------*/
        public static int read_record(DEVBLK dev, Int32 cyl, Int16 head, Int16 rec,
            ref Byte[] data, ref UInt16 keylen, ref UInt16 datalen)
        {
            int rc;                     /* Return code               */
            Int32 ptr;                  /* -> Byte in track buffer   */
            CKDDASD_RECHDR rechdr;      /* -> Record header          */
            Byte kl;                    /* Key length                */
            UInt16 dl;                  /* Data length               */

            /* Read the required track into the track buffer if necessary */
            rc = read_track(ref dev, cyl, head);
            if (rc < 0) return -1;

            ptr = 5;

            while (true)
            {
                if (end_of_track(dev.trkbuf, ptr))
                {
                    return +1;
                }

                rechdr = new CKDDASD_RECHDR(dev.trkbuf, ptr);
                kl = rechdr.K;
                dl = (UInt16)rechdr.LL.big_endian;
                if ((kl + dl) > data.Length)
                {
                    data = new Byte[kl + dl];
                }

                if (rechdr.R == rec)
                {
                    if (data != null)
                    {
                        Array.Copy(dev.trkbuf, ptr + CKDDASD_RECHDR.SIZE, data, 0, kl + dl);
                        keylen = kl;
                        datalen = dl;
                    }
                    break;
                }

                ptr += CKDDASD_RECHDR.SIZE + kl + dl;
            }
            return 0;
        }

        //    /* Search for the requested record in the track buffer */
        //    ptr = cif->trkbuf;
        //    ptr += CKDDASD_TRKHDR_SIZE;

        //    while (1)
        //    {
        //        /* Exit with record not found if end of track */
        //        if (memcmp(ptr, eighthexFF, 8) == 0)
        //            return +1;

        //        /* Extract key length and data length from count field */
        //        rechdr = (CKDDASD_RECHDR*)ptr;
        //        kl = rechdr->klen;
        //        dl = (rechdr->dlen[0] << 8) | rechdr->dlen[1];

        //        /* Exit if requested record number found */
        //        if (rechdr->rec == rec)
        //            break;

        //        /* Issue progress message */
        ////      fprintf (stdout,
        ////              "Skipping CCHHR=%2.2X%2.2X%2.2X%2.2X"
        ////              "%2.2X KL=%2.2X DL=%2.2X%2.2X, 0n",
        ////              rechdr->cyl[0], rechdr->cyl[1],
        ////              rechdr->head[0], rechdr->head[1],
        ////              rechdr->rec, rechdr->klen,
        ////              rechdr->dlen[0], rechdr->dlen[1]);

        //        /* Point past count key and data to next block */
        //        ptr += CKDDASD_RECHDR_SIZE + kl + dl;
        //    }

        //    /* Return key and data pointers and lengths */
        //    if (keyptr != NULL) *keyptr = ptr + CKDDASD_RECHDR_SIZE;
        //    if (keylen != NULL) *keylen = kl;
        //    if (dataptr != NULL) *dataptr = ptr + CKDDASD_RECHDR_SIZE + kl;
        //    if (datalen != NULL) *datalen = dl;
        //    return 0;

        //} /* end function read_block */
        #endregion

        #region "Convert ASCIIZ to EBCDIC"
        /*-------------------------------------------------------------------*/
        /* Subroutine to convert an ASCIIZ byte array to an EBCDIC byte      */
        /* array. Returns the length of the EBCDIC array.                     */
        /*-------------------------------------------------------------------*/
        public static int asciiz_to_EBCDIC(ref Byte[] dest, int destlen, Byte[] src, int srcoffset, int srclen)
        {
            int idx;                    /* Result length             */

            //            set_codepage(NULL);

            for (idx = 0; idx < srclen && idx < destlen; idx++)
            { dest[idx] = Global.ascii_to_ebcdic[src[srcoffset + idx]]; }

            return idx;

        } /* end function asciiz_to_EBCDIC */

        /*-------------------------------------------------------------------*/
        /* Subroutine to convert an ASCIIZ string to an EBCDIC string.       */
        /* Removes trailing blanks and adds a terminating null.              */
        /* Returns the length of the EBCDIC string excluding terminating null*/
        /*-------------------------------------------------------------------*/
        public static int make_EBCDIC(ref Byte[] dest, int destlen, Byte[] src, int srcoffset, int srclen)
        {
            int idx;                    /* Result length             */

            //            set_codepage(NULL);

            for (idx = 0; idx < srclen && idx < destlen; idx++)
                dest[idx] = Global.ascii_to_ebcdic[src[srcoffset + idx]];
            while (idx > 0 && dest[idx - 1] == Global.ascii_to_ebcdic[(Byte)' ']) idx--;
            dest[idx] = 0x00;

            return idx;

        } /* end function make_asciiz */
        #endregion

        #region "Convert EBCDIC to ASCIIZ"
        /*-------------------------------------------------------------------*/
        /* Subroutine to convert an EBCDIC byte array to an ASCIIZ byte      */
        /* array. Returns the length of the ASCII array.                     */
        /*-------------------------------------------------------------------*/
        public static int EBCDIC_to_asciiz(ref Byte[] dest, int destlen, Byte[] src, int srcoffset, int srclen)
        {
            int idx;                    /* Result length             */

            //            set_codepage(NULL);

            for (idx = 0; idx < srclen && idx < destlen; idx++)
            { dest[idx] = Global.ebcdic_to_ascii[src[srcoffset + idx]]; }

            return idx;

        } /* end function EBCDIC_to_asciiz */

        /*-------------------------------------------------------------------*/
        /* Subroutine to convert an EBCDIC string to an ASCIIZ string.       */
        /* Removes trailing blanks and adds a terminating null.              */
        /* Returns the length of the ASCII string excluding terminating null */
        /*-------------------------------------------------------------------*/
        public static int make_asciiz(ref Byte[] dest, int destlen, Byte[] src, int srcoffset, int srclen)
        {
            int idx;                    /* Result length             */

            //            set_codepage(NULL);

            for (idx = 0; idx < srclen && idx < destlen - 1; idx++)
                dest[idx] = Global.ebcdic_to_ascii[src[srcoffset + idx]];
            while (idx > 0 && dest[idx - 1] == ' ') idx--;
            dest[idx] = 0x00;

            return idx;

        } /* end function make_asciiz */
        #endregion

        #region "Use guest host page for EBCDIC and ASCII conversion"
        public static Byte guest_to_host(Byte chr)
        {
            return Global.ebcdic_to_ascii[chr];
            //            return codepage_conv->g2h[chr];
        }
        public static Byte host_to_guest(Byte chr)
        {
            return Global.ascii_to_ebcdic[chr];
            //            return codepage_conv->g2h[chr];
        }
        #endregion

        //*-------------------------------------------------------------------*
        //* Subroutine to convert relative track to cylinder and head         *
        //* Input:                                                            *
        //*      tt      Relative track number                                *
        //*      noext   Number of extents in dataset                         *
        //*      extent  Dataset extent array                                 *
        //*      heads   Number of tracks per cylinder                        *
        //* Output:                                                           *
        //*      cyl     Cylinder number                                      *
        //*      head    Head number                                          *
        //*                                                                   *
        //* Return value is 0 if successful, or -1 if error                   *
        //*-------------------------------------------------------------------*
        public static int convert_tt(int tt, int noext, DSXTENT[] extent, int heads, 
            ref Int32 cyl, ref Int16 head)
        {
            int i;                       /* Extent sequence number    */
            int trk;                     /* Relative track number     */
            int bcyl;                    /* Extent begin cylinder     */
            int btrk;                    /* Extent begin head         */
            int ecyl;                    /* Extent end cylinder       */
            int etrk;                    /* Extent end head           */
            int start;                   /* Extent begin track        */
            int end;                     /* Extent end track          */
            int extsize;                 /* Extent size in tracks     */

            for (i = 0, trk = tt; i < noext; i++)
            {
                bcyl = extent[i].xtbcyl;
                btrk = extent[i].xtbtrk;
                ecyl = extent[i].xtecyl;
                etrk = extent[i].xtetrk;

                start = (bcyl * heads) + btrk;
                end = (ecyl * heads) + etrk;
                extsize = end - start + 1;

                if (trk < extsize)
                {
                    trk += start;
                    cyl = trk / heads;
                    head = (Int16)(trk % heads);
                    return 0;
                }
                
                trk -= extsize;

            } /* end for(i) */

            //    fprintf(stderr, MSG(HHC00450, "E", tt));
            return -1;

        } /* end function convert_tt */

        public static Char ByteToChar(Byte byt)
        {
            if ((byt > 0x1f) && (byt < 0xff)) { return (Char)byt; }
            return '.';
        }

        #region "Device Handler Routines"
        /* Device Initialisation      */
        public static Int32 Init(ref DEVBLK dev)
        {
            String fileName = dev.fileName;

            if (fileName.Contains(":")) { }
            else { fileName = dev.filePath + "\\" + fileName; }

            try
            {
                dev.fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error encountered");
                return -1;
            }

            long rc;

            rc = dev.fileStream.Seek(0, 0);
            if (rc < 0) { return -1; }
            rc = Init_DevBlk(ref dev);
            if (rc < 0) return (Int32)rc;
            dev.l1tab = new LVL1TAB(dev.cmpDevHdr.numl1tab);
            rc = dev.fileStream.Read(dev.l1tab.buffer, 0, dev.l1tab.SIZE);
            if (rc <= 0) { return -1; }

            int n1tab = dev.cmpDevHdr.numl1tab;
            dev.l2tab = new LVL2TAB[dev.cmpDevHdr.numl1tab];

            for (int i = 0; i < n1tab; i++)
            {
                dev.l2tab[i] = new LVL2TAB(dev.cmpDevHdr.numl2tab);
                int seekOffs = dev.l1tab.offsets[i];
                if (seekOffs > 0)
                {
                    rc = dev.fileStream.Seek(seekOffs, 0);
                    if (rc > 0)
                    {
                        rc = dev.fileStream.Read(dev.l2tab[i].buffer, 0, dev.l2tab[i].SIZE);
                        if (rc <= 0) { return -1; }
                    }
                }
            }

            if (dev.fileStream.Length > 0)
            {
                dev.trkbuf = new Byte[dev.trksz];
            }

            return 0;
        }


        /* Device Read                */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static Int32 Read(ref DEVBLK dev, Int32 track, ref Byte unitstat)
        {
            Int32 l1block = track / 256;
            Int32 l2block = track % 256;
            if (dev.l1tab == null)
            {
                return -1;
            }
            LVL2TAB l2tab = dev.l2tab[l1block]; l2tab.Debug(nameof(l2tab));
            LVL2ENTRY l2ent = l2tab.entries[l2block]; l2ent.Debug(nameof(l2ent));

            if (l2ent.offset <= 0)
            {
                return -1;
            }
            dev.fileStream.Seek(l2ent.offset, 0);
            CKD_HDR ckdHdr = new CKD_HDR();
            Byte[] buf = new Byte[l2ent.size];
            int rc = dev.fileStream.Read(buf, 0, l2ent.size);
            if (rc == l2ent.size)
            {
                Array.Copy(buf, 0, ckdHdr.buffer, 0, 5);
                if (ckdHdr.comp == 0)
                {
                    Array.Copy(buf, 0, dev.trkbuf, 0, buf.Length);
                    dev.bufused = buf.Length;
                    Array.Copy(Global.eighthexff, 0, dev.trkbuf, buf.Length, 8);
                    rc = buf.Length;
                }
                else
                {
                    Int32 udLen = dev.trksz;
                    Byte[] uncompData = new Byte[udLen];
                    Int32 dLen = buf.Length - 5;
                    Int32 dOffset = 5;

                    rc = uncompress(ref uncompData, ref udLen, ref buf, ref dOffset, ref dLen);

                    if (rc >= 0)
                    {
                        ckdHdr.comp = 0;
                        Array.Copy(ckdHdr.buffer, 0, dev.trkbuf, 0, 5);
                        Array.Copy(uncompData, 0, dev.trkbuf, 5, udLen);
                    }

                    dev.bufused = udLen + 5;
                    if (udLen < dev.trksz - 8)
                    {
                        Array.Copy(Global.eighthexff, 0, dev.trkbuf, udLen + 5, 8);
                    }

                    rc = dev.bufused;
                }
            }
            return rc;
        }


        /* Device Write               */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static Int32 Write(DEVBLK dev, Int32 rcd, Int32 off, Byte[] buf, Int32 len, ref Byte unitstat)
        {
            return 0;
        }
        /* Device Start channel pgm   */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static Boolean Start(DEVBLK dev) 
        {
            return true;
        }
        /* Device CCW execute         */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public void exec(DEVBLK dev, Byte code, Byte flags,
                                   Byte chained, UInt32 count,
                                   Byte prevcode, int ccwseq,
                                   Byte[] iobuf, Byte[] more,
                                   Byte[] unitstat, UInt32 residual)
        { }
        /* Device Close               */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void Close(DEVBLK dev) { }
        /* Device Query               */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void query(DEVBLK dev, Char[] devclass, int buflen, Char[] buffer) { }
        /* Extended Device Query      */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void ext_query(DEVBLK dev, Char[] devclass, int buflen, Char[] buffer) { }
        /* Device End channel pgm     */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void end(DEVBLK dev) { }
        /* Device Resume channel pgm  */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void resume(DEVBLK dev) { }
        /* Device Suspend channel pgm */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void suspend(DEVBLK dev) { }
        /* Device Halt                */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void halt(DEVBLK dev) { }
        /* Device Query used          */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void used(DEVBLK dev) { }
        /* Device Reserve             */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void reserve(DEVBLK dev) { }
        /* Device Release             */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void release(DEVBLK dev) { }
        /* Device Attention           */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void attention(DEVBLK dev) { }
        /* Immediate CCW Codes        */
        public static void immed() { }
        /* Signal Adapter Input       */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void siga_r(DEVBLK dev, UInt32 qmask) { }
        /* Signal Adapter Output      */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void siga_w(DEVBLK dev, UInt32 qmask) { }
        /* Signal Adapter Sync        */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void siga_s(DEVBLK dev, UInt32 oqmask, UInt32 iqmask) { }
        /* Signal Adapter Output Mult */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void siga_m(DEVBLK dev, UInt32 qmask) { }
        /* QDIO subsys query desc     */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void ssqd(DEVBLK dev, Byte[] desc) { }
        /* QDIO set subchan ind       */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void ssci(DEVBLK dev, Byte[] desc) { }
        /* Hercules suspend           */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void hsuspend(DEVBLK dev, Byte[] file) {  }
        /* Hercules resume            */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void hresume(DEVBLK dev, Byte[] file) { }
        #endregion

        public static Object dasd_lookup(int dtype, string name, UInt16 devt, UInt16 size)
        {
            switch (dtype)
            {
                case Global.DASD_CKDDEV:
                    foreach (CKDDEV ckd in Global.ckdtab)
                    {
                        if ((name == ckd._name) || ((devt == ckd._devt) || (devt == (ckd._devt & 0xff)))
                            && (size <= ckd._cyls + ckd._altcyls))
                        {
                            return ckd;
                        }
                    }
                    return null;

                case Global.DASD_FBADEV:
                    foreach (FBADEV fba in Global.fbatab)
                    {
                        if ((name == fba._name)
                            || ((devt == fba._devt) || (devt == (fba._devt & 0xff)))
                            && ((size <= fba._blks) || (fba._blks == 0)))
                        {
                            return fba;
                        }
                    }
                    return null;

                default:
                    break;
            }

            if (name == null) { return null; }

            foreach (var dev in Global.ckdtab)
            {
                if (name == dev._name) { return dev; }
            }

            foreach (var dev in Global.fbatab)
            {
                if (name == dev._name) { return dev; }
            }

            return null;
        }

        //*-------------------------------------------------------------------*
        //* Subroutine to open a CKD/FBA image file(s)                        *
        //* Input:                                                            *
        //*      fname    image file name                                     *
        //*      sfname   Shadow-File option string  (e.g. "sf=shadow_*.xxx") *
        //*      omode    Open mode: O_RDONLY or O_RDWR                       *
        //*      option   IMAGE_OPEN_NORMAL, IMAGE_OPEN_DASDCOPY, etc.        *
        //*                                                                   *
        //* The image file is opened, a track buffer is obtained,             *
        //* and a DASD image file descriptor structure is built.              *
        //* Return value is a pointer to the image file descriptor            *
        //* structure if successful, or NULL if unsuccessful.                 *
        //*-------------------------------------------------------------------*
        public static Int32 Open_DASD_Image(String fname, String sfname, String path = null, Int16 omode = 0, Int16 option = 0)
        {
            int rc;                     /* Return code               */
            Int32 rmtdev;               /* Possible remote device    */

            Close_DASD_Image();

            Global.devblk_list.Clear();

            if (path == null) { Global.devblk_list.Add(new DEVBLK(fname)); }
            else { Global.devblk_list.Add(new DEVBLK(path, fname)); }

            if (Global.devblk_list[0] == null)
            {
                return -1;
            }

            if ((sfname == null) || (sfname == "")) { }
            else
            {
                if (sfname.LastIndexOf('_') > 0)
                {
                    int lIdx = sfname.LastIndexOf('_');
                    string firstPart = sfname.Substring(0, lIdx);
                    string lastPart = sfname.Substring(lIdx + 2);

                    int count = 1;
                    while (count < 9)
                    {
                        string newShadow = firstPart + "_" + count.ToString() + lastPart;

                        if (File.Exists(newShadow))
                        {
                            Global.devblk_list.Add(new DEVBLK(newShadow));
                        }
                        else if (File.Exists(path + "\\" + newShadow))
                        {
                            Global.devblk_list.Add(new DEVBLK(path, newShadow));
                        }

                        count++;
                    }
                }
                else
                {
                    Global.devblk_list.Add(new DEVBLK(sfname));
                }
            }

            for (int i = 0; i < Global.devblk_list.Count; i++)
            {
                DEVBLK dev = Global.devblk_list[i];

                rc = dev.devHnd.Init(ref dev);
                //rc = Init(ref dev);

                if (rc < 0)
                {
                    dev.trkbuf = new byte[dev.trksz];

                    dev.devnum = ++Global.nextnum;

                    if (!dev.devHnd.Start(dev))
                    {
                        return -1;
                    }
                    return -1;
                }

                /* Initialize the devblk */
                if ((omode & Global.O_RDWR) == 0) { dev.rdonly = 1; }
                dev.batch = true;
                dev.dasdcopy = ((option & Global.IMAGE_OPEN_DASDCOPY) == Global.IMAGE_OPEN_DASDCOPY);
                dev.showdvol1 = ((option & Global.IMAGE_OPEN_DVOL1) == Global.IMAGE_OPEN_DVOL1);
                dev.quiet = ((option & Global.IMAGE_OPEN_QUIET) == Global.IMAGE_OPEN_QUIET);

                /* If the filename has a `:' then it may be a remote device */
                rmtdev = fname.IndexOf(':');
                if (rmtdev > -1)
                {
                    for (int idx = rmtdev + 1; idx < fname.Length; idx++)
                    {
                        if (!fname[idx].ToString().IsNumeric())
                        {
                            break;
                        }
                    }
                }

                /* Indicate that the track buffer is empty */
                dev.curcyl = -1;
                dev.curhead = -1;
                dev.trkmodif = false;
            }

            return 0;
        }

        static Int32 Init_DevBlk(ref DEVBLK dev)
        {
            int iLen;                   /* Record length             */
            DASD_DEVHDR devhdr;         /* CKD device header         */
            DASD_COMP_DEVHDR cDevHdr;   /* Compressed device header  */
            CKDDEV ckd;                 /* CKD DASD table entry      */
            String typname;

            typname = new string(' ', 12);

            if (dev.fileStream == null) { return -1; }

            if (dev.fileStream.Length >= 0)
            {
                devhdr = new DASD_DEVHDR();
                cDevHdr = new DASD_COMP_DEVHDR();
                Byte[] devhdrB = new Byte[DASD_DEVHDR.SIZE];
                Byte[] cDevHdrB = new Byte[DASD_COMP_DEVHDR.SIZE];

                iLen = ReadStream(dev.fileStream, ref devhdrB, DASD_DEVHDR.SIZE);
                if (iLen < 0) { return -1; }
                if (iLen < DASD_DEVHDR.SIZE) { return -1; }
                Array.Copy(devhdrB, 0, devhdr.buffer, 0, DASD_DEVHDR.SIZE);
                Array.Copy(devhdr.buffer, 0, dev.devHdr.buffer, 0, DASD_DEVHDR.SIZE); devhdr.Debug(nameof(devhdr));

                iLen = ReadStream(dev.fileStream, ref cDevHdrB, DASD_COMP_DEVHDR.SIZE);
                if (iLen < 0) { return -1; }
                if (iLen < DASD_COMP_DEVHDR.SIZE) { return -1; }
                Array.Copy(cDevHdrB, 0, cDevHdr.buffer, 0, DASD_COMP_DEVHDR.SIZE);
                Array.Copy(cDevHdr.buffer, 0, dev.cmpDevHdr.buffer, 0, DASD_COMP_DEVHDR.SIZE); cDevHdr.Debug(nameof(cDevHdr));

                if (devhdr.devid.StartsWith("CKD"))
                {
                    ckd = (CKDDEV)DASD_Routines.dasd_lookup(Global.DASD_CKDDEV, null, devhdr.devtype, 0); ckd.Debug(nameof(ckd));

                    if (ckd == null)
                    {
                        return -1;
                    }

                    dev.ckddev = ckd;
                    dev.devtype = ckd._devt;
                    dev.typname = typname;
                    dev.trksz = devhdr.trksize.little_endian;
                    dev.heads = devhdr.heads.little_endian;
                    dev.trkbuf = new byte[dev.trksz];

                    dev.devnum = ++Global.nextnum;

                    if (!dev.devHnd.Start(dev))
                    {
                        return -1;
                    }
                }
                else if (devhdr.devid.StartsWith("FBA"))
                {
                    FBADEV fba = (FBADEV)dasd_lookup(Global.DASD_FBADEV, null, Global.DEFAULT_FBA_TYPE, 0); fba.Debug(nameof(fba));

                    dev.devtype = fba._devt;
                    dev.fbadev = fba;
                    dev.typname = typname;
                    dev.trksz = fba._size;
                    dev.heads = cDevHdr.cyls.little_endian;
                    dev.trkbuf = new byte[dev.trksz * 120];

                    dev.devnum = ++Global.nextnum;

                    if (!dev.devHnd.Start(dev))
                    {
                        return -1;
                    }
                }
                else { return -1; }
            }

            return 0;
        }

        public static Boolean Close_DASD_Image()
        {
            for (int i = 0; i < Global.devblk_list.Count; i++)
            {
                Close(Global.devblk_list[i].fileStream);
            }
            return true;
        }

        public static Boolean Close_CKD_Image()
        {
            for (int i = 0; i < Global.devblk_list.Count; i++)
            {
                Close(Global.devblk_list[i].fileStream);
            }
            return true;
        }

        //public static Stream Open(String path, int oflag)
        //{
        //    Global.sReader = new FileStream(path, FileMode.Open, FileAccess.Read);

        //    return Global.sReader;
        //}

        public static int ReadStream(Stream stream, ref Byte[] buf, int len)
        {
            return stream.Read(buf, 0, len);
        }

        public static void Close(Stream stream)
        {
            if (stream != null)
            {
                stream.Close();
            }
        }

        public static Boolean end_of_track(Byte[] trkbuf, Int32 ptr)
        {
            if (ptr > trkbuf.Length - 8) { return true; }
            return ((trkbuf[ptr] == 0xff) && (trkbuf[ptr + 1] == 0xff)
                && (trkbuf[ptr + 2] == 0xff) && (trkbuf[ptr + 3] == 0xff)
                && (trkbuf[ptr + 4] == 0xff) && (trkbuf[ptr + 5] == 0xff)
                && (trkbuf[ptr + 6] == 0xff) && (trkbuf[ptr + 7] == 0xff));
        }

        public static Int32 uncompress(ref Byte[] dest, ref Int32 destLen,
            ref Byte[] source, ref Int32 sourceOffset, ref Int32 sourceLen)
        {
            Int32 rc;
            ZStream zs = new ZStream();
            zs.inflateInit();

            zs.next_in_index = sourceOffset;
            zs.next_in = source;
            zs.avail_in = sourceLen;
            zs.next_out = dest;
            zs.avail_out = destLen;

            rc = zs.inflate(0);

            source = zs.next_in;
            sourceLen = (Int32)zs.total_in;
            dest = zs.next_out;
            destLen = (Int32)zs.total_out;

            zs.inflateEnd();

            zs.free();

            return rc;
        }

        private static readonly int pdsDirBlkLen = 264;  // PDS directory block consists of 8 byte last member name, 2 byte field count, 254 bytes of directory entries

        public static List<MemberEntry> Build_MemberEntry_List(DSNEntry dEntry)
        {
            DEVBLK dev = null;

            Int32 ccyl = dEntry.dsextents[0].xtbcyl;
            Int16 chead = dEntry.dsextents[0].xtbtrk;
            Int32 ecyl = dEntry.dsextents[0].xtecyl;
            Int16 ehead = dEntry.dsextents[0].xtetrk;

            Int32 rc;
            Int32 trks = 0;

            List<MemberEntry> memList = new List<MemberEntry>();

            Boolean end_of_directory = false;

            for (int i = Global.devblk_list.Count - 1; i >= 0; i--)
            {
                dev = Global.devblk_list[i]; dev.Debug(nameof(dev));

                rc = DASD_Routines.read_track(ref dev, ccyl, chead);
                if (rc >= 0)
                {
                    break;
                }
            }

            do
            {
                Int32 ptr = 5;
                rc = DASD_Routines.read_track(ref dev, ccyl, chead);

                if (rc >= 0)
                {
                    trks++;

                    while (!DASD_Routines.end_of_track(dev.trkbuf, ptr))
                    {
                        CKDDASD_RECHDR rechdr = new CKDDASD_RECHDR(dev.trkbuf, ptr); rechdr.Debug(nameof(rechdr));
                        Int16 iK = rechdr.K;
                        Int16 iLL = rechdr.LL.big_endian;
                        //Int16 rec = rechdr.R;
                        Int32 bufLen = iK + iLL;

                        if (bufLen == 0)
                        {
                            end_of_directory = true;
                            break;
                        }

                        ptr += CKDDASD_RECHDR.SIZE;

                        Byte[] buf = new Byte[bufLen];
                        Array.Copy(dev.trkbuf, ptr, buf, 0, bufLen);

                        if (bufLen == pdsDirBlkLen)
                        {
                            Int32 dPtr = 10;
                            Byte[] EBCDICdata = new Byte[bufLen];
                            HWORD numBytesUsed = new HWORD(buf[8], buf[9]);
                            Int32 bytesUsed = numBytesUsed.big_endian;

                            DASD_Routines.EBCDIC_to_asciiz(ref EBCDICdata, bufLen, buf, 0, bufLen);
                            //String lastMember = new String(' ', 1).ByteArrayToString(EBCDICdata, 0, 8);

                            while (dPtr < bytesUsed)
                            {
                                if (DASD_Routines.end_of_track(buf, dPtr)) { break; }
                                MemberEntry memEntry = new MemberEntry();
                                String member = new String(' ', 1).ByteArrayToString(dPtr, 8, EBCDICdata).TrimEnd();
                                memEntry.member = member;

                                memEntry.TTR = new TTR();
                                Array.Copy(buf, dPtr + 8, memEntry.TTR.buffer, 0, 3);
                                Byte C = buf[dPtr + 11];
                                //Byte isAlias = (Byte)(C >> 7);
                                //Byte numPtrs = (Byte)((C >> 5) & 0x03);
                                Int16 numHWORDs = (Int16)(C & 0x1f);
                                Int32 numBytes = numHWORDs * 2;

                                if ((numHWORDs > 0) && ((dPtr + 12 + numBytes) < pdsDirBlkLen))
                                {
                                    Byte[] userData = new Byte[numBytes];
                                    memEntry.C = C;
                                    Array.Copy(buf, dPtr + 12, userData, 0, numBytes);
                                    memEntry.userData = userData;
                                }
                                memEntry.dEntry = dEntry; memEntry.Debug(nameof(memEntry));

                                memList.Add(memEntry);

                                //Byte[] dumpBuf = new byte[12 + numBytes];
                                //Array.Copy(buf, dPtr, dumpBuf, 0, 12 + numBytes);
                                dPtr = dPtr + 12 + numBytes;
                            }

                            if (DASD_Routines.end_of_track(buf, 0))
                            {
                                end_of_directory = true;
                                break;
                            }
                        }

                        ptr += bufLen;
                    }
                }

                chead++;
                if (chead >= Global.devblk_list[0].heads)
                {
                    ccyl++;
                    chead = 0;
                }
            } while ((ccyl < ecyl || (ccyl == ecyl && chead <= ehead)) && (!end_of_directory));

            return memList;
        }

        public static String Get_Member_Contents(MemberEntry memEntry)
        {
            DEVBLK dev = null;

            String retStr = "";
            String _NewLine = Environment.NewLine;
            Int32[] lrecLs = { 133, 132, 121, 80 };

            Int32 trk = memEntry.TTR.TT.big_endian;
            Int16 rec = memEntry.TTR.R;
            DSNEntry dEntry = memEntry.dEntry; dEntry.Debug(nameof(dEntry));

            Int32 rc;
            Int32 fnd = 0;
            Int32 numExt = 0;

            Boolean objDeck = false;
            Int32 recsProcessed = 0;

            foreach (DSXTENT dse in dEntry.dsextents)
            {
                dse.Debug(nameof(dse));
                if (dse.xttype != 0) { numExt++; }
            }

            Int32 ccyl = dEntry.dsextents[0].xtbcyl;
            Int16 chead = dEntry.dsextents[0].xtbtrk;

            for (int i = Global.devblk_list.Count - 1; i >= 0; i--)
            {
                dev = Global.devblk_list[i]; dev.Debug(nameof(dev));

                rc = DASD_Routines.read_track(ref dev, ccyl, chead);
                if (rc >= 0)
                {
                    fnd++;
                    break;
                }
            }

            if (fnd == 0) { return null; }

            Int32 cyl = -1;
            Int16 head = -1;
            String recFmt;

            if (dEntry.recordFormat.IndexOf('U') < 0)
            {
                recFmt = dEntry.recordFormat;
            }
            else
            {
                recFmt = dEntry.recordFormat.Substring(0, dEntry.recordFormat.IndexOf('U') + 1);
            }

            if (recFmt == "U" || Global.HexDumpOnly)
            {
                retStr = "{\\rtf1 ";
            }

            while (true)
            {
                rc = DASD_Routines.convert_tt(trk, numExt, dEntry.dsextents, dev.heads, ref cyl, ref head);
                if (rc < 0) { return null; }

                Byte[] data = new Byte[dev.bufused];
                UInt16 klen = 0;
                UInt16 dlen = 0;
                fnd = 0;

                for (int i = Global.devblk_list.Count - 1; i >= 0; i--)
                {
                    dev = Global.devblk_list[i];

                    rc = DASD_Routines.read_track(ref dev, cyl, head);
                    if (rc >= 0)
                    {
                        fnd++;
                        break;
                    }
                }

                if (fnd == 0) { return null; }

                rc = DASD_Routines.read_record(dev, cyl, head, rec, ref data, ref klen, ref dlen);
                if (rc < 0) { return null; }

                if (rc > 0)
                {
                    trk++;
                    rec = 1;
                    continue;
                }

                if (dlen == 0) { break; }

                Byte[] buf = new Byte[dlen];
                Array.Copy(data, 0, buf, 0, dlen);

                if (!objDeck && recsProcessed == 0)
                {
                    recsProcessed++;
                    // check if ESD record
                    if (buf[0] == 0x02 && buf[1] == 0xc5 && buf[2] == 0xe2 && buf[3] == 0xc4 && buf[4] == 0x40)
                    {
                        objDeck = true;
                        if (retStr == "")
                        {
                            retStr = "{\\rtf1 ";
                        }
                    }
                }

                if ((recFmt == "FB") || (recFmt == "F"))
                {
                    Int32 numLrecl;
                    Int32 lrecl = -1;

                    if (recFmt == "FB")
                    {
                        for (int i = 0; i < lrecLs.Length; i++)
                        {
                            if ((dlen % lrecLs[i]) == 0)
                            {
                                lrecl = lrecLs[i];
                                break;
                            }
                        }
                    }

                    if (lrecl == -1) { lrecl = dlen; }

                    numLrecl = dlen / lrecl;

                    for (int i = 0; i < numLrecl; i++)
                    {
                        if (Global.HexDumpOnly || objDeck)
                        {
                            Byte[] dumpBuf = new Byte[lrecl];
                            Array.Copy(buf, i * lrecl, dumpBuf, 0, lrecl);
                            retStr += Hex_Dump_EBCDIC(dumpBuf, lrecl, 32);
                        }
                        else
                        {
                            Byte[] bufAscii = new Byte[lrecl];
                            DASD_Routines.EBCDIC_to_asciiz(ref bufAscii, lrecl, buf, i * lrecl, lrecl);
                            retStr += new string(' ', 1).ByteArrayToString(bufAscii) + _NewLine;
                        }
                    }
                }
                else if (recFmt == "VB")
                {
                    Byte[] bdw = new Byte[4];
                    Array.Copy(buf, 0, bdw, 0, 4);
                    Int32 bLL = (bdw[0] << 8) | bdw[1];
                    if (bLL == dlen)
                    {
                        Int32 ptr = 4;
                        while (ptr < dlen)
                        {
                            Byte[] rdw = new Byte[4];
                            Array.Copy(buf, ptr, rdw, 0, 4);
                            Int32 rLL = (rdw[0] << 8) | rdw[1];
                            if (!Global.HexDumpOnly)
                            {
                                Byte[] bufAscii = new Byte[rLL - 4];
                                DASD_Routines.EBCDIC_to_asciiz(ref bufAscii, rLL - 4, buf, ptr + 4, rLL - 4);
                                retStr += new string(' ', 1).ByteArrayToString(bufAscii) + _NewLine;
                            }
                            else
                            {
                                Byte[] dumpBuf = new Byte[rLL - 4];
                                Array.Copy(buf, ptr, dumpBuf, 0, rLL - 4);
                                retStr += Hex_Dump_EBCDIC(dumpBuf, rLL - 4, 32);
                            }

                            ptr += rLL;
                        }
                    }
                    else
                    {
                        retStr += Hex_Dump_EBCDIC(buf, dlen, 32);
                    }
                }
                else
                {
                    retStr += Hex_Dump_EBCDIC(buf, dlen, 32);
                }

                rec++;
            }

            //rtfStr = rtfStr + _rtfEnd;

            //if (((recFmt == "FB") || (recFmt == "F") || (recFmt == "VB")) && (!Global.HexDumpOnly))
            //{
            //    return retStr;
            //}
            //else
            //{
            //    return retStr; // rtfStr;
            //}

            return retStr;
        }

        public static Byte[] Get_Member_Buffer(MemberEntry memEntry)
        {
            DEVBLK dev = null;
            List<Byte> buf = new List<byte>();

            Int32 trk = memEntry.TTR.TT.big_endian;
            Int16 rec = memEntry.TTR.R;
            DSNEntry dEntry = memEntry.dEntry; dEntry.Debug(nameof(dEntry));

            Int32 rc;
            Int32 fnd = 0;
            Int32 numExt = 0;

            foreach (DSXTENT dse in dEntry.dsextents)
            {
                dse.Debug(nameof(dse));
                if (dse.xttype != 0) { numExt++; }
            }

            Int32 ccyl = dEntry.dsextents[0].xtbcyl;
            Int16 chead = dEntry.dsextents[0].xtbtrk;

            for (int i = Global.devblk_list.Count - 1; i >= 0; i--)
            {
                dev = Global.devblk_list[i]; dev.Debug(nameof(dev));

                rc = DASD_Routines.read_track(ref dev, ccyl, chead);
                if (rc >= 0)
                {
                    fnd++;
                    break;
                }
            }

            if (fnd == 0) { return null; }

            Int32 cyl = -1;
            Int16 head = -1;

            while (true)
            {
                rc = DASD_Routines.convert_tt(trk, numExt, dEntry.dsextents, dev.heads, ref cyl, ref head);
                if (rc < 0) { return null; }

                Byte[] data = new Byte[dev.bufused];
                UInt16 klen = 0;
                UInt16 dlen = 0;
                fnd = 0;

                for (int i = Global.devblk_list.Count - 1; i >= 0; i--)
                {
                    dev = Global.devblk_list[i];

                    rc = DASD_Routines.read_track(ref dev, cyl, head);
                    if (rc >= 0)
                    {
                        fnd++;
                        break;
                    }
                }

                if (fnd == 0) { return null; }

                rc = DASD_Routines.read_record(dev, cyl, head, rec, ref data, ref klen, ref dlen);
                if (rc < 0) { return null; }

                if (rc > 0)
                {
                    trk++;
                    rec = 1;
                    continue;
                }

                if (dlen == 0) { break; }

                if (dlen == data.Length)
                {
                    buf.AddRange(data);
                }
                else
                {
                    Byte[] temp = new Byte[dlen];
                    Array.Copy(data, 0, temp, 0, dlen);
                    buf.AddRange(temp);
                }

                rec++;
            }

            return buf.ToArray();
        }

        public static Global.SaveResults Save_MemberEntry(MemberEntry memEntry, String extension = null)
        {
            String retStr = DASD_Routines.Get_Member_Contents(memEntry);

            if (retStr.StartsWith("{\\rtf1"))
            {
                RichTextBox richText = new RichTextBox { Rtf = retStr };
                retStr = richText.Text;
                richText.Dispose();
            }

            if (!retStr.Equals(null))
            {
                if (extension == null)
                {
                    SaveFileDialog dlg = new SaveFileDialog
                    {
                        FileName = memEntry.member,
                        Filter = Global.filter,
                        InitialDirectory = Global.folder
                    };
                    DialogResult dr = dlg.ShowDialog();

                    if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        Global.folder = Path.GetDirectoryName(dlg.FileName);
                        Settings.Default.UnloadPath = Global.folder;
                        Settings.Default.Save();
                        return Save_Text(dlg.FileName, retStr);
                    }

                    return Global.SaveResults.Cancel;
                }
                else
                {
                    if (Global.folder == "")
                    {
                        SelectUnloadFolder();
                    }
                    String fileName = Global.folder + "\\" + memEntry.member + "." + extension;
                    return Save_Text(fileName, retStr);
                }
            }

            return Global.SaveResults.Fail;
        }

        public static Global.SaveResults Binary_Save_MemberEntry(MemberEntry memEntry, String extension = null)
        {
            byte[] binaryData = DASD_Routines.Get_Member_Buffer(memEntry);

            if (extension == null)
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = memEntry.member,
                    Filter = Global.filter,
                    InitialDirectory = Global.folder
                };
                DialogResult dr = dlg.ShowDialog();

                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    Global.folder = Path.GetDirectoryName(dlg.FileName);
                    Settings.Default.UnloadPath = Global.folder;
                    Settings.Default.Save();
                    return Save_Binary(dlg.FileName, binaryData);
                }

                return Global.SaveResults.Cancel;
            }
            else
            {
                if (Global.folder == "")
                {
                    SelectUnloadFolder();
                }
                String fileName = Global.folder + "\\" + memEntry.member + "." + extension;
                return Save_Binary(fileName, binaryData);
            }
        }

        public static Global.SaveResults Save_Text(String fileName, String textOut, Boolean silent = false)
        {
            StreamWriter sw;
            try
            {
                sw = new StreamWriter(fileName);
                sw.Write(textOut.ToCharArray());
                sw.Close();
            }
            catch (Exception ex)
            {
                if (!silent) { MessageBox.Show(ex.Message); }
                return Global.SaveResults.Fail;
            }

            return Global.SaveResults.Success;
        }

        public static Global.SaveResults Save_Binary(String fileName, Byte[] outBuf, Boolean silent = false)
        {
            
            //StreamWriter sw;
            try
            {
                File.WriteAllBytes(fileName, outBuf);
                //sw = new StreamWriter(fileName);
                //sw.Write(outBuf.BytesToChars());
                //sw.Close();
            }
            catch (Exception ex)
            {
                if (!silent) { MessageBox.Show(ex.Message); }
                return Global.SaveResults.Fail;
            }

            return Global.SaveResults.Success;
        }

        public static Boolean Open_DASD(String filename, ListView lv)
        {
            StreamReader rdr = new StreamReader(filename);
            if (rdr.BaseStream.Length < 1024) { return false; }

            DASD_DEVHDR dev = new DASD_DEVHDR();
            Int32 rc = rdr.BaseStream.Read(dev.buffer, 0, 512);
            if (rc != 512) { return false; }

            if (dev.devid.StartsWith("CKD_") || dev.devid.StartsWith("FBA_"))
            {
                lv.BeginUpdate();
                lv.Items.Clear();

                String[] fileParts = filename.Split('\\');
                String[] volume = fileParts[fileParts.Length - 1].Split('.');

                ListViewItem lvi1 = new ListViewItem(volume[0]);

                if (dev.devid.StartsWith("CKD_"))
                {
                    CKDDEV ckd = (CKDDEV)DASD_Routines.dasd_lookup(Global.DASD_CKDDEV, null, dev.devtype, 0);
                    lvi1.SubItems.Add(ckd._name);
                }
                else if (dev.devid.StartsWith("FBA_"))
                {
                    FBADEV fba = (FBADEV)DASD_Routines.dasd_lookup(Global.DASD_FBADEV, null, Global.DEFAULT_FBA_TYPE, 0);
                    lvi1.SubItems.Add(fba._name);
                }

                lvi1.SubItems.Add(filename);
                lvi1.SubItems.Add("");
                lv.Items.Add(lvi1);

                lv.EndUpdate();

                lv.Items[0].Selected = true;
            }
            else
            { return false; }

            return true;
        }

        public static void Open_Config(String filename, ListView lv)
        {
            Char[] anyDelim = { '/', '\\' };

            StreamReader rdr = new StreamReader(filename);
            String inLine = null;
            String filePath = filename.Substring(0, filename.LastIndexAnyOf(anyDelim));
            if (filePath.EndsWith("\\conf"))
            {
                filePath = filePath.Substring(0, filePath.Length - 5);
            }

            List<VolumeEntry> dasdList = new List<VolumeEntry>();

            inLine = LoadConfig(rdr, filePath, dasdList);

            dasdList.Sort(delegate (VolumeEntry x, VolumeEntry y)
            {
                return x.volume.CompareTo(y.volume);
            });

            lv.BeginUpdate();
            lv.Items.Clear();

            for (int i = 0; i < dasdList.Count; i++)
            {
                String fileName = dasdList[i].filename;
                String shadowName = dasdList[i].shadowfile;
                ListViewItem lvi1 = new ListViewItem(dasdList[i].volume) { Tag = dasdList[i] };
                lvi1.SubItems.Add(dasdList[i].device);
                lvi1.SubItems.Add(dasdList[i].filename);
                lvi1.SubItems.Add(dasdList[i].shadowfile);
                lv.Items.Add(lvi1);
            }

            lv.EndUpdate();
        }

        public static string LoadConfig(StreamReader rdr, string filePath, List<VolumeEntry> dasdList)
        {
            string inLine;
            symbolTable = new List<string[]>();

            do
            {
                inLine = rdr.ReadLine();

                if (inLine.Contains("${"))
                {
                    inLine = ResolveEnhancedSymbolicData(inLine);
                }

                if (inLine.Contains("$("))
                {
                    inLine = ResolveSymbolSubstitution(inLine);
                }

                String[] lineData = inLine.TrimEnd(' ').Split(' ');
                VolumeEntry dasd = new VolumeEntry();

                if (lineData.Length > 1)
                {
                    switch (lineData[0].ToUpper())
                    {
                        case "INCLUDE":
                            string fileName = filePath + "\\" + lineData[1];

                            if (File.Exists(fileName))
                            {
                                StreamReader incRdr = new StreamReader(fileName);
                                inLine = LoadConfig(incRdr, filePath, dasdList);
                            }

                            break;

                        case "DEFSYM":
                            if (lineData.Length > 2)
                            {
                                string[] symbolEntry = new string[2];
                                symbolEntry[0] = lineData[1];
                                symbolEntry[1] = lineData[2].Trim('"');
                                symbolTable.Add(symbolEntry);
                            }
                            
                            break;

                        default:
                            switch (lineData[1])
                            {
                                case "2305":    // Count Key Data (CKD) Devices
                                case "2311":
                                case "2314":
                                case "3330":
                                case "3340":
                                case "3350":
                                case "3375":
                                case "3380":
                                case "3390":
                                case "9345":
                                    if (lineData.Length > 2)
                                    {
                                        String[] fileParts = lineData[2].Split('/');
                                        String[] volume = fileParts[fileParts.Length - 1].Split('.');
                                        dasd.volume = volume[0].ToUpper();
                                        dasd.device = lineData[1];
                                        dasd.filename = lineData[2];
                                        dasd.pathname = filePath;

                                        if (lineData.Length > 4)
                                        {
                                            String[] sfParts = lineData[lineData.Length - 1].Split('=');
                                            dasd.shadowfile = sfParts[1];
                                        }
                                        dasd.Debug(nameof(dasd));

                                        dasdList.Add(dasd);
                                    }

                                    break;

                                case "3310":    // Fixed Block Architecture (FBA) Devices
                                case "3370":
                                case "9332":
                                case "9335":
                                case "9336":
                                    if (lineData.Length > 2)
                                    {
                                        String[] fileParts = lineData[2].Split('/');
                                        String[] volume = fileParts[fileParts.Length - 1].Split('.');
                                        dasd.volume = volume[0].ToUpper();
                                        dasd.device = lineData[1];
                                        dasd.filename = lineData[2];
                                        dasd.pathname = filePath;

                                        if (lineData.Length > 4)
                                        {
                                            String[] sfParts = lineData[lineData.Length - 1].Split('=');
                                            dasd.shadowfile = sfParts[1];
                                        }
                                        dasd.Debug(nameof(dasd));

                                        dasdList.Add(dasd);
                                    }

                                    break;

                                default:
                                    break;
                            }

                            break;
                    }
                }
            } while (!rdr.EndOfStream);
            return inLine;
        }

        public static string ResolveSymbolSubstitution(string inLine)
        {
            string lineData = inLine;

            // TODO: This needs enhancement but will do for an initial pass
            // TODO: This code needs testing (none of my configs use DEFSYM)
            while (lineData.Contains("$("))
            {
                int iDLB = lineData.IndexOf("$(");
                string leftPart = lineData.Substring(0, iDLB);
                int iRB = lineData.IndexOf(")", iDLB);
                string rightPart = lineData.Substring(iRB + 1);

                string symName = lineData.Substring(iDLB + 2, (iRB - (iDLB + 2)));
                string symValue = null;
                foreach (string[] item in symbolTable)
                {
                    if (item[0] == symName)
                    {
                        symValue = item[1];
                        break;
                    }
                }
                if (string.IsNullOrEmpty(symValue))
                {
                    symValue = Environment.GetEnvironmentVariable(symName);
                }

                string middlePart;

                if (string.IsNullOrEmpty(symValue))
                {
                    middlePart = "";
                }
                else
                {
                    middlePart = symValue;
                }
                lineData = leftPart + middlePart + rightPart;
            }

            return lineData;
        }

        public static string ResolveEnhancedSymbolicData(string inLine)
        {
            string lineData = inLine;

            while (lineData.Contains("${"))
            {
                int iDLB = lineData.IndexOf("${");
                string leftPart = lineData.Substring(0, iDLB);
                int iRB = lineData.IndexOf("}", iDLB);
                string rightPart = lineData.Substring(iRB + 1);

                string envValue = "";
                string envName;
                string defaultPart;
                string middlePart;
                int iCE = -1;

                if (lineData.Contains(":="))
                {
                    iCE = lineData.IndexOf(":=", iDLB);
                    defaultPart = lineData.Substring(iCE + 2, (iRB - (iCE + 2)));
                }
                else if (lineData.Contains("="))
                {
                    iCE = lineData.IndexOf("=", iDLB);
                    defaultPart = lineData.Substring(iCE + 1, (iRB - (iCE + 1)));
                }
                else
                {
                    defaultPart = "";
                }
                if (iCE > 0)
                {
                    envName = lineData.Substring(iDLB + 2, iCE - (iDLB + 2));
                    envValue = Environment.GetEnvironmentVariable(envName);
                }
                if (string.IsNullOrEmpty(envValue))
                {
                    middlePart = defaultPart;
                }
                else
                {
                    middlePart = envValue;
                }
                lineData = leftPart + middlePart + rightPart;
            }

            return lineData;
        }

        public static String Process_Block_DSN(DSNEntry dEntry)
        {
            String rtfStr = Global._rtfHdr;
            DEVBLK dev = null;

            Int32 ccyl = dEntry.dsextents[0].xtbcyl;
            Int16 chead = dEntry.dsextents[0].xtbtrk;
            Int32 ecyl = dEntry.dsextents[0].xtecyl;
            Int16 ehead = dEntry.dsextents[0].xtetrk;

            Int32 rc;
            Int32 fnd = 0;
            Int32 trks = 0;

            if (dEntry.dsextents[0].xttype == 0)
            {
                rtfStr = rtfStr + " Invalid extent" + Global._rtfNL;
            }
            else
            {
                for (int i = Global.devblk_list.Count - 1; i >= 0; i--)
                {
                    dev = Global.devblk_list[i]; dev.Debug(nameof(dev));

                    rc = DASD_Routines.read_track(ref dev, ccyl, chead);
                    if (rc >= 0)
                    {
                        fnd++;
                        break;
                    }
                }
                do
                {
                    rc = DASD_Routines.read_track(ref dev, ccyl, chead);
                    if (rc >= 0)
                    {
                        fnd++;
                        trks++;
                        if (trks > 10) { break; }

                        rtfStr += " CC=" + ccyl.ToString("X4");
                        rtfStr += "  HH=" + chead.ToString("X2");
                        rtfStr += "  Length=" + dev.bufused.ToString("X4");
                        rtfStr += Global._rtfNL;

                        rtfStr += Hex_Dump_EBCDIC(dev.trkbuf, dev.bufused, 32);
                    }
                    chead++;
                    if (chead >= Global.devblk_list[0].heads)
                    {
                        ccyl++;
                        chead = 0;
                    }
                } while (ccyl < ecyl || (ccyl == ecyl && chead <= ehead));
            }
            return rtfStr;
        }

        public static String Process_Text_DSN(DSNEntry dEntry)
        {
            String rtfStr = Global._rtfHdr;
            DEVBLK dev = null;

            Int32 ccyl = dEntry.dsextents[0].xtbcyl;
            Int16 chead = dEntry.dsextents[0].xtbtrk;
            Int32 ecyl = dEntry.dsextents[0].xtecyl;
            Int16 ehead = dEntry.dsextents[0].xtetrk;

            Int32 rc;
            Int32 fnd = 0;
            //            Int32 trks = 0;

            String recFmt;
            if (dEntry.recordFormat.IndexOf('U') < 0)
            {
                recFmt = dEntry.recordFormat;
            }
            else
            {
                recFmt = dEntry.recordFormat.Substring(0, dEntry.recordFormat.IndexOf('U') + 1);
            }

            if (dEntry.dsextents[0].xttype == 0)
            {
                rtfStr = rtfStr + " Invalid extent" + Global._rtfNL;
            }
            else
            {
                for (int i = Global.devblk_list.Count - 1; i >= 0; i--)
                {
                    dev = Global.devblk_list[i]; dev.Debug(nameof(dev));

                    rc = DASD_Routines.read_track(ref dev, ccyl, chead);
                    if (rc >= 0)
                    {
                        fnd++;
                        break;
                    }
                }
                do
                {
                    rc = DASD_Routines.read_track(ref dev, ccyl, chead);
                    if (rc < 0)
                    {
                        break;
                    }

                    Int32 dlen = dev.bufused;

                    if ((recFmt == "FB") || (recFmt == "F "))
                    {
                        Int32 numLrecl;
                        Int32 lrecl = -1;

                        if ((dlen % 133) == 0)
                        {
                            lrecl = 133;
                        }
                        else if ((dlen % 132) == 0)
                        {
                            lrecl = 132;
                        }
                        else if ((dlen % 80) == 0)
                        {
                            lrecl = 80;
                        }

                        if (lrecl == -1)
                        {
                            for (int i = 2; i < (dlen / 2); i++)
                            {
                                if ((dlen % i) == 0)
                                {
                                    lrecl = i;
                                }
                            }
                        }

                        if (lrecl == -1) { lrecl = dlen; }

                        numLrecl = dlen / lrecl;

                        for (int i = 0; i < numLrecl; i++)
                        {
                            if (!Global.HexDumpOnly)
                            {
                                Byte[] bufAscii = new Byte[lrecl];
                                DASD_Routines.EBCDIC_to_asciiz(ref bufAscii, lrecl, dev.trkbuf, i * lrecl, lrecl);
                                rtfStr = rtfStr + new string(' ', 1).ByteArrayToString(bufAscii) + Global._rtfNL;
                            }
                            else
                            {
                                Byte[] dumpBuf = new Byte[lrecl];
                                Array.Copy(dev.trkbuf, i * lrecl, dumpBuf, 0, lrecl);
                                rtfStr += Hex_Dump_EBCDIC(dumpBuf, lrecl, 32);
                            }
                        }
                    }
                    else if (recFmt == "VB")
                    {
                        Byte[] bdw = new Byte[4];
                        Array.Copy(dev.trkbuf, 0, bdw, 0, 4);
                        Int32 bLL = (bdw[0] << 8) | bdw[1];
                        if (bLL == dlen)
                        {
                            Int32 ptr = 4;
                            while (ptr < dlen)
                            {
                                Byte[] rdw = new Byte[4];
                                Array.Copy(dev.trkbuf, ptr, rdw, 0, 4);
                                Int32 rLL = (rdw[0] << 8) | rdw[1];
                                if (!Global.HexDumpOnly)
                                {
                                    Byte[] bufAscii = new Byte[rLL - 4];
                                    DASD_Routines.EBCDIC_to_asciiz(ref bufAscii, rLL - 4, dev.trkbuf, ptr + 4, rLL - 4);
                                    rtfStr = rtfStr + new string(' ', 1).ByteArrayToString(bufAscii) + Global._rtfNL;
                                }
                                else
                                {
                                    Byte[] dumpBuf = new Byte[rLL - 4];
                                    Array.Copy(dev.trkbuf, ptr, dumpBuf, 0, rLL - 4);
                                    rtfStr += Hex_Dump_EBCDIC(dumpBuf, rLL - 4, 32);
                                }

                                ptr += rLL;
                            }
                        }
                        else
                        {
                            rtfStr += Hex_Dump_EBCDIC(dev.trkbuf, dlen, 32);
                        }
                    }
                    else
                    {
                        rtfStr += " CC=" + ccyl.ToString("X4");
                        rtfStr += "  HH=" + chead.ToString("X2");
                        rtfStr += "  Length=" + dev.bufused.ToString("X4");
                        rtfStr += Global._rtfNL;

                        rtfStr += Hex_Dump_EBCDIC(dev.trkbuf, dlen, 16);
                    }

                    chead++;
                    if (chead >= Global.devblk_list[0].heads)
                    {
                        ccyl++;
                        chead = 0;
                    }
                } while (ccyl < ecyl || (ccyl == ecyl && chead <= ehead));
            }
            return rtfStr;
        }

        public static String Fill_Member_ListView(DSNEntry dEntry, ListView lv)
        {
            String retStr = "";
            Char recFmt = dEntry.recordFormat[0];
            List<MemberEntry> memList = DASD_Routines.Build_MemberEntry_List(dEntry);

            Byte[] memName;

            lv.Columns.Clear();

            switch (recFmt)
            {
                case 'U':
                    lv.Columns.Add("Member", 76);
                    lv.Columns.Add(" SSI", 76);
                    lv.Columns.Add(" Size", 60);
                    lv.Columns.Add(" TTR", 60);
                    lv.Columns.Add("Alias of", 76);
                    lv.Columns.Add("AC", 30);
                    lv.Columns.Add("-- -------- -Attributes -- -- ---", 280);
                    lv.Columns.Add("ATR1", 50);
                    lv.Columns.Add("ATR2", 50);
                    lv.Columns.Add("FTB1", 50);
                    lv.Columns.Add("FTB2", 50);
                    lv.Columns.Add("FTB3", 50);
                    break;

                case 'F':
                case 'V':
                    lv.Columns.Add("Member", 76);
                    lv.Columns.Add(" TTR", 60);
                    lv.Columns.Add("VV.MM", 54);
                    lv.Columns.Add(" Created", 76);
                    lv.Columns.Add("     Changed", 150);
                    lv.Columns.Add(" Init", 54);
                    lv.Columns.Add(" Size", 54);
                    lv.Columns.Add("  Mod", 54);
                    lv.Columns.Add("  ID", 76);
                    break;

                default:
                    lv.Columns.Add("Member", 80);
                    lv.Columns.Add(" TTR", 60);
                    lv.Columns.Add(" C", 30);
                    lv.Columns.Add("User Data", 360);
                    break;
            }

            foreach (MemberEntry memEntry in memList)
            {
                ListViewItem lvi = new ListViewItem(memEntry.member);

                int numBytes = 0;

                if (memEntry.userData != null)
                {
                    numBytes = memEntry.userData.Length;
                }

                Byte[] dumpBuf = new byte[12 + numBytes];
                memName = new byte[8];
                DASD_Routines.asciiz_to_EBCDIC(ref memName, 8, memEntry.member.PadRight(8).ToByteArray(), 0, 8);
                Array.Copy(memName, 0, dumpBuf, 0, 8);
                Array.Copy(memEntry.TTR.buffer, 0, dumpBuf, 8, 3);
                dumpBuf[11] = memEntry.C;
                if (numBytes > 0)
                {
                    Array.Copy(memEntry.userData, 0, dumpBuf, 12, numBytes);
                }
                retStr += Hex_Dump_EBCDIC(dumpBuf, 12 + numBytes, 16);

                String TTR = memEntry.TTR.buffer.BytesToHexString();

                switch (recFmt)
                {
                    case 'U':   // if userdata present it probably contains loadlib statistics
                        String SSI = "";
                        String SIZE = "";
                        String ALIASOF = "";
                        String AC = "  ";
                        String ATTRIBUTES = "";
                        String FLAGS = "";
                        Byte PDS2ATR1 = 0x00;
                        Byte PDS2ATR2 = 0x00;
                        Byte PDS2FTB1 = 0x00;
                        Byte PDS2FTB2 = 0x00;
                        Byte PDS2FTB3 = 0x00;

                        if (memEntry.userData == null) { }
                        else
                        {
                            String FLVL = "   ";
                            String EP = " ".PadRight(9);
                            String OVLY = "   ";
                            String REFR = "   ";
                            String RENT = "   ";
                            String REUS = "   ";
                            String XXXX = "   ";
                            String TEST = "   ";
                            String AMOD = "   ";
                            Int16 PDS2NTTR = (Int16)((memEntry.C & 0x60) >> 5);
                            Int16 PDS2LUSR = (Int16)((memEntry.C & 0x1F) << 1);
                            PDS2ATR1 = memEntry.userData[8];
                            PDS2ATR2 = memEntry.userData[9];
                            SIZE = memEntry.userData.BytesToHexString(10, 3);
                            String PDS2FTBL = memEntry.userData.BytesToHexString(13, 2);
                            String PDS2EPA = memEntry.userData.BytesToHexString(15, 3);
                            PDS2EPA = PDS2EPA.TrimStart('0').PadLeft(1, '0');
                            PDS2FTB1 = memEntry.userData[18];
                            PDS2FTB2 = memEntry.userData[19];
                            PDS2FTB3 = memEntry.userData[20];

                            Byte PDSLRMOD = (Byte)((PDS2FTB2 & 0x10) >> 4);
                            Byte PDSMAMOD = (Byte)(PDS2FTB2 & 0x03);
                            Byte PDSAAMOD = (Byte)((PDS2FTB2 & 0x0C) >> 2);

                            Boolean PDS2ALIS = ((memEntry.C & 0x80) != 0);
                            Boolean PDS2RENT = ((PDS2ATR1 & 0x80) != 0);
                            Boolean PDS2REUS = ((PDS2ATR1 & 0x40) != 0);
                            Boolean PDS2OVLY = ((PDS2ATR1 & 0x20) != 0);
                            Boolean PDS2TEST = ((PDS2ATR1 & 0x10) != 0);
                            Boolean PDS2LOAD = ((PDS2ATR1 & 0x08) != 0);
                            Boolean PDS2SCTR = ((PDS2ATR1 & 0x04) != 0);
                            Boolean PDS2EXEC = ((PDS2ATR1 & 0x02) != 0);
                            Boolean PDS21BLK = ((PDS2ATR1 & 0x01) != 0);
                            Boolean PDS2FLVL = ((PDS2ATR2 & 0x80) != 0);
                            Boolean PDS2ORG0 = ((PDS2ATR2 & 0x40) != 0);
                            Boolean PDS2EP0 = ((PDS2ATR2 & 0x20) != 0);
                            Boolean PDS2NRLD = ((PDS2ATR2 & 0x10) != 0);
                            Boolean PDS2NREP = ((PDS2ATR2 & 0x08) != 0);
                            Boolean PDS2TSTN = ((PDS2ATR2 & 0x04) != 0);
                            Boolean PDS2LEF = ((PDS2ATR2 & 0x02) != 0);
                            Boolean PDS2REFR = ((PDS2ATR2 & 0x01) != 0);
                            Boolean PDSAOSLE = ((PDS2FTB1 & 0x80) != 0);
                            Boolean PDS2BIG = ((PDS2FTB1 & 0x40) != 0);
                            Boolean PDS2PAGA = ((PDS2FTB1 & 0x20) != 0);
                            Boolean PDS2SSI = ((PDS2FTB1 & 0x10) != 0);
                            Boolean PDSAPFLG = ((PDS2FTB1 & 0x08) != 0);
                            Boolean PDS2LFMT = ((PDS2FTB1 & 0x04) != 0);
                            Boolean PDS2SIGN = ((PDS2FTB1 & 0x02) != 0);
                            Boolean PDS2XATR = ((PDS2FTB1 & 0x01) != 0);
                            Boolean PDS2ALTP = ((PDS2FTB2 & 0x80) != 0);
                            Boolean PDS2NMIG = ((PDS2FTB3 & 0x80) != 0);
                            Boolean PDS2PRIM = ((PDS2FTB3 & 0x40) != 0);
                            Boolean PDS2PACK = ((PDS2FTB3 & 0x20) != 0);

                            if (PDS2PAGA)
                            {
                                ATTRIBUTES = "";
                            }
                            if (PDS2ALIS)       // is this an alias?
                            {
                                Byte[] bAlias = new Byte[8];
                                DASD_Routines.EBCDIC_to_asciiz(ref bAlias, 8, memEntry.userData, 24, 8);
                                ALIASOF = bAlias.CharsToString();
                                if (numBytes > 35)
                                {
                                    if (PDS2SSI) { SSI = memEntry.userData.BytesToHexString(32, 3).PadLeft(8, '0'); }
                                    AC = memEntry.userData.BytesToHexString(37, 1).PadRight(2);
                                }
                                else
                                {
                                    AC = memEntry.userData.BytesToHexString(33, 1).PadRight(2);
                                }
                            }
                            else
                            {
                                if (numBytes > 24)
                                {
                                    if (PDS2SSI) { SSI = memEntry.userData.BytesToHexString(21, 4).PadLeft(8, '0'); }
                                    AC = memEntry.userData.BytesToHexString(27, 1).PadRight(2);
                                }
                                else
                                {
                                    AC = memEntry.userData.BytesToHexString(22, 1).PadRight(2);
                                }
                            }
                            if (AC == "00" || AC == "01" || AC == "  ") { }
                            else { AC = "??"; }
                            if (PDS2FLVL) { FLVL = "FO "; }  // Can be processed only by F level of linkage editor
                            else { FLVL = "DC "; }
                            EP = "EP" + PDS2EPA.PadRight(7);
                            if (PDS2EXEC)
                            {
                                if (PDS2NREP) { EP = "NE".PadRight(9); }
                            }
                            else
                            { EP = "NX".PadRight(9); }
                            if (PDS2PAGA | PDS2OVLY | PDS2LOAD)
                            {
                                if (PDS2LOAD)
                                {
                                    if (!PDS2NREP) { EP = " ".PadRight(9); }
                                    AC = "  ";
                                    OVLY = "OL ";
                                }
                                else
                                {
                                    if (PDS2OVLY) { OVLY = "OV "; }
                                    else { if (PDS2PAGA) { OVLY = "PG "; } }
                                }
                            }
                            if (PDS2REFR) { REFR = "RF "; }
                            if (PDS2RENT) { RENT = "RN "; }
                            if (PDS2REUS) { REUS = "RU "; }
                            if (PDS2TEST) { TEST = "TS "; }
                            switch (PDSMAMOD)
                            {
                                case 0x01:
                                    AMOD = "A64";
                                    break;
                                case 0x02:
                                    AMOD = "A31";
                                    break;
                                case 0x03:
                                    AMOD = "ANY";
                                    break;
                                default:
                                    break;
                            }
                            ATTRIBUTES = FLVL + EP + OVLY + REFR + RENT + REUS + XXXX + TEST + AMOD;
                            FLAGS += PDS2ATR1.ToString("X2").PadRight(5);
                            FLAGS += PDS2ATR2.ToString("X2").PadRight(5);
                            FLAGS += PDS2FTB1.ToString("X2").PadRight(5);
                            FLAGS += PDS2FTB2.ToString("X2").PadRight(5);
                            FLAGS += PDS2FTB3.ToString("X2").PadRight(5);
                        }
                        lvi.SubItems.Add(SSI);
                        lvi.SubItems.Add(SIZE);
                        lvi.SubItems.Add(TTR);
                        lvi.SubItems.Add(ALIASOF);
                        lvi.SubItems.Add(AC);
                        lvi.SubItems.Add(ATTRIBUTES);
                        lvi.SubItems.Add(PDS2ATR1.ToString("X2"));
                        lvi.SubItems.Add(PDS2ATR2.ToString("X2"));
                        lvi.SubItems.Add(PDS2FTB1.ToString("X2"));
                        lvi.SubItems.Add(PDS2FTB2.ToString("X2"));
                        lvi.SubItems.Add(PDS2FTB3.ToString("X2"));
                        break;

                    case 'F':   // if userdata present it probably contains ISPF statistics
                    case 'V':
                        lvi.SubItems.Add(TTR);
                        if (memEntry.userData == null) { }
                        else
                        {
                            String temp;
                            temp = memEntry.userData[0].ToString().PadLeft(2, '0') + "." + memEntry.userData[1].ToString().PadLeft(2, '0');
                            lvi.SubItems.Add(temp);
                            if (memEntry.userData.Length > 4)
                            {
                                Byte[] julDate = new Byte[4];
                                Array.Copy(memEntry.userData, 4, julDate, 0, 4);
                                temp = JulianToGregorian(julDate);
                                lvi.SubItems.Add(temp);
                                Array.Copy(memEntry.userData, 8, julDate, 0, 4);
                                Byte[] modTime = new Byte[3];
                                Array.Copy(memEntry.userData, 12, modTime, 0, 2);
                                modTime[2] = memEntry.userData[3];
                                temp = JulianToGregorian(julDate) + " " + TimeToString(modTime);
                                lvi.SubItems.Add(temp);
                                temp = new HWORD(memEntry.userData[14], memEntry.userData[15]).big_endian.ToString();
                                lvi.SubItems.Add(temp.PadLeft(5));
                                temp = new HWORD(memEntry.userData[16], memEntry.userData[17]).big_endian.ToString();
                                lvi.SubItems.Add(temp.PadLeft(5));
                                temp = new HWORD(memEntry.userData[18], memEntry.userData[19]).big_endian.ToString();
                                lvi.SubItems.Add(temp.PadLeft(5));
                                Byte[] ID = new Byte[7];
                                DASD_Routines.EBCDIC_to_asciiz(ref ID, 7, memEntry.userData, 20, 7);
                                lvi.SubItems.Add(ID.CharsToString());
                            }
                        }
                        break;

                    default:
                        lvi.SubItems.Add(TTR);
                        lvi.SubItems.Add(memEntry.C.ToString("X2"));
                        if (numBytes > 0)
                        {
                            lvi.SubItems.Add(memEntry.userData.BytesToHexString());
                        }
                        break;
                }

                lvi.Tag = memEntry;

                lv.Items.Add(lvi);
            }

            return retStr;
        }

        public static string JulianToGregorian(byte[] julDate)
        {
            String retDate = null;

            if (julDate.Length == 4)
            {
                //                J   F   M   A   M   J   J   A   S   O   N   D
                int[] months = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                Int32 julYear = 1900;
                if (julDate[0] == 1) { julYear = 2000; }
                String temp = julDate[1].ToString("X2") + julDate[2].ToString("X2") + julDate[3].ToString("X2");
                Int32 julDay = Convert.ToInt32(temp.Substring(2, 3));
                julYear += Convert.ToInt32(temp.Substring(0, 2));

                if ((julYear & 0x03) != 0) { }
                else
                {
                    if ((julYear - ((julYear / 100) * 100)) == 0) { months[1] += 1; }
                    else
                    {
                        if ((julYear - ((julYear / 400) * 400)) == 0) { }
                        else { months[1] += 1; }
                    }
                }
                Int32 mm = 0;

                while (julDay > 0)
                {
                    julDay -= months[mm++];
                }

                julDay += months[mm - 1];

                retDate = julYear.ToString().Substring(2, 2) + "-" + mm.ToString().PadLeft(2, '0') + "-" + julDay.ToString().PadLeft(2, '0');
            }

            return retDate;
        }

        public static string TimeToString(byte[] inTime)
        {
            String retTime = null;

            if (inTime.Length == 3)
            {
                retTime = inTime[0].ToString("X2") + ":" + inTime[1].ToString("X2") + ":" + inTime[2].ToString("X2");
            }

            return retTime;
        }

        public static String Hex_Dump_Ascii(Byte[] buf, Int32 buflen, Int32 width)
        {
            String retStr = "";
            String retSStr = "";

            String retCStr = "  ";
            int k = buflen;
            int j;

            retStr += "{ Offset ";

            for (int i = 0; i < (width + 1); i++)
            {
                retStr += " " + i.ToString("X2");
            }
            retStr += " |";
            for (int i = 0; i < (width + 1); i++)
            {
                if ((i % 8) == 7)
                {
                    retStr += "+";
                }
                else
                {
                    retStr += ".";
                }
            }
            retStr += "|";
            retStr += Global._rtfNL;

            retSStr += " {\\b 000000} ";

            for (j = 0; j < k; j++)
            {
                retSStr += " " + buf[j].ToString("X2");
                Byte c = (Byte)DASD_Routines.ByteToChar(buf[j]);
                retCStr = retCStr + "\\'" + c.ToString("x2");
                if ((j & width) == width)
                {
                    retStr += retSStr + retCStr + Global._rtfNL;
                    if (j < (k - 1))
                    {
                        retSStr = " {\\b " + (j + 1).ToString("X6") + "} ";
                        retCStr = "  ";
                    }
                    else
                    {
                        retSStr = "";
                        retCStr = "";
                    }
                }
            }
            j--;
            j &= width;
            if (j > 0)
            {
                for (int jj = j; jj < width; jj++)
                {
                    retSStr += "   ";
                }
            }
            if (!((retSStr == "") && (retCStr == "")))
            {
                retStr += retSStr + retCStr + Global._rtfNL;
            }

            retStr += Global._rtfNL;

            return retStr;
        }

        public static String Hex_Dump_EBCDIC(Byte[] buf, Int32 buflen, Int32 width)
        {
            String retStr = "";
            String retSStr = "";
            Byte[] EBCDICdata = new Byte[buflen];
            Int32 widthM1 = width - 1;

            String retCStr = "  ";
            int k = buflen;
            int j;

            retStr += "{ Offset ";

            for (int i = 0; i < width; i++)
            {
                retStr += " " + i.ToString("X2");
            }
            retStr += " |";
            for (int i = 0; i < width; i++)
            {
                if ((i % 8) == 7)
                {
                    retStr += "+";
                }
                else
                {
                    retStr += ".";
                }
            }
            retStr += "|";
            retStr += Global._rtfNL;

            DASD_Routines.EBCDIC_to_asciiz(ref EBCDICdata, buflen, buf, 0, buflen);

            retSStr += " {\\b 000000} ";

            for (j = 0; j < k; j++)
            {
                retSStr += " " + buf[j].ToString("X2");
                Byte c = (Byte)DASD_Routines.ByteToChar(EBCDICdata[j]);
                retCStr = retCStr + "\\'" + c.ToString("x2");
                if ((j & widthM1) == widthM1)
                {
                    retStr += retSStr + retCStr + Global._rtfNL;
                    if (j < (k - 1))
                    {
                        retSStr = " {\\b " + (j + 1).ToString("X6") + "} ";
                        retCStr = "  ";
                    }
                    else
                    {
                        retSStr = "";
                        retCStr = "";
                    }
                }
            }
            j--;
            j &= widthM1;
            if (j > 0)
            {
                for (int jj = j; jj < widthM1; jj++)
                {
                    retSStr += "   ";
                }
            }
            if (!((retSStr == "") && (retCStr == "")))
            {
                retStr += retSStr + retCStr + Global._rtfNL;
            }

            retStr += Global._rtfNL;

            return retStr;
        }

        public static void SelectUnloadFolder(String selectedFolder = null)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.RootFolder = Environment.SpecialFolder.MyComputer;
            if (selectedFolder != null && selectedFolder != "")
            {
                dlg.SelectedPath = selectedFolder;
            }
            DialogResult dr = dlg.ShowDialog();
            if (dr == DialogResult.Cancel) { return; }
            Global.folder = dlg.SelectedPath;
            Settings.Default.UnloadPath = Global.folder;
            Settings.Default.Save();
        }
    }

}
