/*
 * DASD_Volume: Based on routines originally written in C by Roger Bowler and Malcom Beattie for support of the DASD files for Hercules.
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

namespace Hercules_DASD_Utility
{
    public class DASD_Volume
    {
      //* DASDLS.C    (c) Copyright Roger Bowler, 1999-2012                 */
      //*              Hercules DASD Utilities: DASD image loader           */
      //*                                                                   */
      //*   Released under "The Q Public License Version 1"                 */
      //*   (http://www.hercules-390.org/herclic.html) as modifications to  */
      //*   Hercules.                                                       */

      //*
      //* dasdls
      //*
      //* Copyright 2000-2009 by Malcolm Beattie
      //* Based on code copyright by Roger Bowler, 1999-2009
      //*

      //* DASD_Volume.cs
      //* 
      //* Copyright Don Tuel, 2017-2020
      //* Based on code copyright by Malcolm Beattie, 2000-2009
      //* Based on code copyright by Roger Bowler, 1999-2009
      //*

      static public List<DASDEntry> get_dasd_list(String fileName, String shadowFile, String pathName = null)
      {
          Int32 rc = DASD_Routines.Open_DASD_Image(fileName, shadowFile, pathName);

          if (rc < 0) { return null; }

          List<DASDEntry> dasdList = list_dasd();

          if (dasdList == null) { return null; }

          return dasdList;
      }

      static List<DASDEntry> list_dasd()
      {
          List<DASDEntry> dasdList = new List<DASDEntry>();

          for (int i = 0; i < Global.devblk_list.Count; i++)
          {
              DEVBLK tDev = Global.devblk_list[i]; tDev.Debug(nameof(tDev));

              build_dasd_list(tDev, ref dasdList);
          }
          return dasdList;
      }

      static void build_dasd_list(DEVBLK dev, ref List<DASDEntry> dasdList)
      {
          //String _CrLf = Environment.NewLine;

          DASD_DEVHDR devHdr = dev.devHdr; devHdr.Debug(nameof(devHdr));
          DASD_COMP_DEVHDR cDevHdr = dev.cmpDevHdr; cDevHdr.Debug(nameof(cDevHdr));
          LVL1TAB level1tab = dev.l1tab;

          dasdList.Add(new DASDEntry(dev.fileName, "DASD_DEVHDR", 0, 512, dev.filePath));
          dasdList.Add(new DASDEntry(dev.fileName, "DASD_COMP_DEVHDR", 512, 512, dev.filePath));
          dasdList.Add(new DASDEntry(dev.fileName, "LVL1TAB", 1024, level1tab.SIZE, dev.filePath));

          int n1tab = cDevHdr.numl1tab;
          for (int i = 0; i < n1tab; i++)
          {
              if (level1tab.offsets[i] > 1024)
              {
                  dasdList.Add(new DASDEntry(dev.fileName, "LVL2TAB[" + i + "]", level1tab.offsets[i], 2048, dev.filePath));
              }
          }
      }

        static public List<TRACKEntry> get_track_list(String fileName, String shadowFile, String pathName = null)
      {
          Int32 rc = DASD_Routines.Open_DASD_Image(fileName, shadowFile, pathName);

          if (rc < 0) { return null; }

          List<TRACKEntry> trackList = list_tracks();

          if (trackList == null) { return null; }

          return trackList;
      }

      static List<TRACKEntry> list_tracks()
      {
          List<TRACKEntry> trackList = new List<TRACKEntry>();

          for (int i = 0; i < Global.devblk_list.Count; i++)
          {
              DEVBLK tDev = Global.devblk_list[i]; tDev.Debug(nameof(tDev));

              build_track_list(tDev, ref trackList);
          }
          return trackList;
      }

      static void build_track_list(DEVBLK dev, ref List<TRACKEntry> trackData)
      {
          String _CrLf = Environment.NewLine;

          DASD_DEVHDR devHdr = dev.devHdr; devHdr.Debug(nameof(devHdr));
          DASD_COMP_DEVHDR cDevHdr = dev.cmpDevHdr; cDevHdr.Debug(nameof(cDevHdr));
          LVL1TAB level1tab = dev.l1tab; level1tab.Debug(nameof(level1tab));
            
          long rc;

          int n1tab = cDevHdr.numl1tab;
          for (int i = 0; i < n1tab; i++)
          {
              int n2entries = dev.l2tab[i].entries.Length;
              LVL2ENTRY l2ent;
              for (int idx = 0; idx < n2entries; idx++)
              {
                  l2ent = dev.l2tab[i].entries[idx];

                  if ((l2ent.offset > 512) && (l2ent.length > 0) && (l2ent.size > 0))
                  {
                      rc = dev.fileStream.Seek(l2ent.offset, 0);
                      if (rc > 0)
                      {
                          TRACKEntry dGroup = new TRACKEntry();
                          CKD_HDR ckdHdr = new CKD_HDR();
                          rc = dev.fileStream.Read(ckdHdr.buffer, 0, 5);
                          dGroup.pathname = dev.filePath;
                          dGroup.filename = dev.fileName;
                          dGroup.comp = ckdHdr.comp;
                          dGroup.CC = ckdHdr.CC.big_endian;
                          dGroup.HH = (Int16)ckdHdr.HH.big_endian;
                          dGroup.L1Tab = i;
                          dGroup.L2Tab = idx;
                          dGroup.offset = l2ent.offset;
                          dGroup.datasize = l2ent.length;

                          int dIdx = trackData.FindIndex(a => ((a.CC == dGroup.CC) && (a.HH == dGroup.HH)));
                          if (dIdx >= 0)
                          {
                              trackData.RemoveAt(dIdx);
                          }
                          trackData.Add(dGroup);
                      }
                  }
              }
          }
      }

      static public List<DSNEntry> get_DSN_list(String fileName, String shadowFile, String pathName = null)
      {
          Int32 rc = DASD_Routines.Open_DASD_Image(fileName, shadowFile, pathName);

          if (Global.devblk_list.Count < 1) { return null; }

          List<DSNEntry> DSNList = list_VTOC();

          if (DSNList == null) { return null; }

          return DSNList;
      }

    static List<DSNEntry> list_VTOC()
    {
        UInt16 klen = 0;
        Byte[] vol1data = new Byte[84];
        UInt16 dlen = 0;
        VOL1_LABEL v1label = null;
        String vol = "";
        Int32 cyl = -1;
        Int16 head = -1;
        Byte rec = 0;
        FORMAT4_DSCB f4dscb = null;

        List<DSNEntry> dataGroup = new List<DSNEntry>();

        for (int i = 0; i < Global.devblk_list.Count; i++)
        {
            Int32 rc = DASD_Routines.read_record(Global.devblk_list[i], 0, 0, 3, ref vol1data, ref klen, ref dlen);

            if (rc == 0)
            {
                v1label = new VOL1_LABEL(vol1data, klen);
                vol = v1label.vol1ser;
                cyl = v1label.vol1vtocp.CC.big_endian;
                head = (Int16)v1label.vol1vtocp.HH.big_endian;
                rec = v1label.vol1vtocp.R;
                f4dscb = new FORMAT4_DSCB();
            }

            Byte[] f4dscbB = new byte[FORMAT4_DSCB.SIZE];
            rc = DASD_Routines.read_record(Global.devblk_list[i], cyl, head, rec, ref f4dscbB, ref klen, ref dlen);

            if (rc == 0)
            {
                Array.Copy(f4dscbB, 0, f4dscb.buffer, 0, FORMAT4_DSCB.SIZE);
                build_DSN_list(Global.devblk_list[i], ref dataGroup, f4dscb.ds4vtoce);
            }

        }

        return dataGroup;
    }

      static void build_DSN_list(DEVBLK dev, ref List<DSNEntry> dataGroup, VTOCXTENT extent)
      {
          String _CrLf = Environment.NewLine;

          //Int16 cext = 0;
          Int32 ccyl = extent.xtbcyl.big_endian;
          Int16 chead = (Int16)extent.xtbtrk.big_endian;
          Int32 ecyl = extent.xtecyl.big_endian;
          Int16 ehead = (Int16)extent.xtetrk.big_endian;

          DASD_DEVHDR devHdr = dev.devHdr; devHdr.Debug(nameof(devHdr));
          DASD_COMP_DEVHDR cDevHdr = dev.cmpDevHdr; cDevHdr.Debug(nameof(cDevHdr));
          LVL1TAB level1tab = dev.l1tab;
          LVL2TAB[] level2Tab = dev.l2tab;

          long rc;
            
          do
          {
              Int32 ptr = 5;
              rc = DASD_Routines.read_track(ref dev, ccyl, chead);

              if (rc < 0) return;

              while (!DASD_Routines.end_of_track(dev.trkbuf, ptr))
              {
                  CKDDASD_RECHDR rechdr = new CKDDASD_RECHDR(dev.trkbuf, ptr); rechdr.Debug(nameof(rechdr));
                  Int16 kl = rechdr.K;
                  Int16 dl = (Int16)rechdr.LL.big_endian;
                  ptr += CKDDASD_RECHDR.SIZE;

                  if ((kl + dl) == FORMAT1_DSCB.SIZE)
                  {
                      FORMAT1_DSCB f1dscb = new FORMAT1_DSCB(dev.trkbuf, ptr); f1dscb.Debug(nameof(f1dscb));
                      if (f1dscb.ds1fmtid == FORMAT1_DSCB.DS1IDC)
                      {
                          DSNEntry dEntry = new DSNEntry();
                          dEntry.DSCB = f1dscb;
                          dEntry.pathname = dev.filePath;
                          dEntry.filename = dev.fileName;
                          dEntry.dev = dev;

                          dEntry.Debug(nameof(dEntry));

                          int dIdx = dataGroup.FindIndex(a => (a.DSN == f1dscb.ds1dsnam));
                          if (dIdx >= 0)
                          {
                              dataGroup.RemoveAt(dIdx);
                          }
                          dataGroup.Add(dEntry);
                      }
                  }

                  ptr += kl + dl;
              }
              chead++;
              if (chead >= dev.heads)
              {
                  ccyl++;
                  chead = 0;
              }
          } while (ccyl < ecyl || (ccyl == ecyl && chead <= ehead));
      }
    }
}
