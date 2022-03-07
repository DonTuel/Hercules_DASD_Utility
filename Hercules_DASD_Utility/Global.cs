/*
 * Global: Global settings, values and debug routines.
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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using CustomExtensions;

namespace Hercules_DASD_Utility
{
    public static class Global
    {
        #region "Local Variables"
        public const Int32 O_RDONLY = 0x0000;   /* open for reading only */
        public const Int32 O_WRONLY = 0x0001;   /* open for writing only */
        public const Int32 O_RDWR = 0x0002;     /* open for reading and writing */
        public const Int32 O_APPEND = 0x0008;   /* writes done at eof */
        /* sequential/random access hints */
        public const Int32 O_RANDOM = 0x0010;  /* file access is primarily random */
        public const Int32 O_SEQUENTIAL = 0x0020;  /* file access is primarily sequential */
        /* Temporary file bit - file is deleted when last handle is closed */
        public const Int32 O_TEMPORARY = 0x0040;  /* temporary file bit */
        /* Open handle inherit bit */
        public const Int32 O_NOINHERIT = 0x0080;  /* child process doesn't inherit file */
        public const Int32 O_CREAT = 0x0100;    /* create and open file */
        public const Int32 O_TRUNC = 0x0200;    /* open and truncate */
        public const Int32 O_EXCL = 0x0400;     /* open only if file doesn't already exist */
        /* O_TEXT files have <cr><lf> sequences translated to <lf> on read()'s,
        ** and <lf> sequences translated to <cr><lf> on write()'s
        */
        /* temporary access hint */
        public const Int32 O_SHORT_LIVED = 0x1000;  /* temporary storage file, try not to flush */
        /* directory access hint */
        public const Int32 O_OBTAIN_DIR = 0x2000;  /* get information about a directory */
        public const Int32 O_TEXT = 0x4000;     /* file mode is text (translated) */
        public const Int32 O_BINARY = 0x8000;   /* file mode is binary (untranslated) */
        /* macro to translate the C 2.0 name used to force binary mode for files */
        public const Int32 O_RAW = 0x8000;
        public const Int32 O_WTEXT = 0x10000;   /* file mode is UTF16 (translated) */
        public const Int32 O_U16TEXT = 0x20000; /* file mode is UTF16 no BOM (translated) */
        public const Int32 O_U8TEXT = 0x40000;  /* file mode is UTF8  no BOM (translated) */

        public const Int16 IMAGE_OPEN_DASDCOPY = 0x01;
        public const Int16 IMAGE_OPEN_QUIET = 0x02;
        public const Int16 IMAGE_OPEN_DVOL1 = 0x04;

        public const Int16 DASD_CKDDEV = 1;       /* Lookup CKD device         */
        public const Int16 DASD_CKDCU = 2;        /* Lookup CKD control unit   */
        public const Int16 DASD_FBADEV = 3;       /* Lookup FBA device         */
        public const Int16 DASD_STDBLK = 4;       /* Lookup device standard block/physical */

        public const UInt16 DEFAULT_FBA_TYPE = 0x3370;

        public static UInt16 nextnum = 0;

        public static Boolean HexDumpOnly = false;

        public static Stream sReader;

        public static RichTextBox diag = null;
        public static Boolean genDiag = false;

        public static List<DEVBLK> devblk_list = new List<DEVBLK>();

        public static Byte[] eighthexff = { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

        public static Byte[]
        ascii_to_ebcdic = {
         /*         x0    x1    x2    x3    x4    x5    x6    x7    x8    x9    xA    xB    xC    xD    xE    xF */
         /* 0x */ 0x00, 0x01, 0x02, 0x03, 0x37, 0x2D, 0x2E, 0x2F, 0x16, 0x05, 0x25, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
         /* 1x */ 0x10, 0x11, 0x12, 0x13, 0x3C, 0x3D, 0x32, 0x26, 0x18, 0x19, 0x1A, 0x27, 0x22, 0x1D, 0x35, 0x1F,
         /* 2x */ 0x40, 0x5A, 0x7F, 0x7B, 0x5B, 0x6C, 0x50, 0x7D, 0x4D, 0x5D, 0x5C, 0x4E, 0x6B, 0x60, 0x4B, 0x61,
         /* 3x */ 0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0x7A, 0x5E, 0x4C, 0x7E, 0x6E, 0x6F,
         /* 4x */ 0x7C, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xD1, 0xD2, 0xD3, 0xD4, 0xD5, 0xD6,
         /* 5x */ 0xD7, 0xD8, 0xD9, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0xE7, 0xE8, 0xE9, 0xAD, 0xE0, 0xBD, 0x5F, 0x6D,
         /* 6x */ 0x79, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96,
         /* 7x */ 0x97, 0x98, 0x99, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xC0, 0x6A, 0xD0, 0xA1, 0x07,
         /* 8x */ 0x68, 0xDC, 0x51, 0x42, 0x43, 0x44, 0x47, 0x48, 0x52, 0x53, 0x54, 0x57, 0x56, 0x58, 0x63, 0x67,
         /* 9x */ 0x71, 0x9C, 0x9E, 0xCB, 0xCC, 0xCD, 0xDB, 0xDD, 0xDF, 0xEC, 0xFC, 0xB0, 0xB1, 0xB2, 0xB3, 0xB4,
         /* Ax */ 0x45, 0x55, 0xCE, 0xDE, 0x49, 0x69, 0x04, 0x06, 0xAB, 0x08, 0xBA, 0xB8, 0xB7, 0xAA, 0x8A, 0x8B,
         /* Bx */ 0x09, 0x0A, 0x14, 0xBB, 0x15, 0xB5, 0xB6, 0x17, 0x1B, 0xB9, 0x1C, 0x1E, 0xBC, 0x20, 0xBE, 0xBF,
         /* Cx */ 0x21, 0x23, 0x24, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x30, 0x31, 0xCA, 0x33, 0x34, 0x36, 0x38, 0xCF,
         /* Dx */ 0x39, 0x3A, 0x3B, 0x3E, 0x41, 0x46, 0x4A, 0x4F, 0x59, 0x62, 0xDA, 0x64, 0x65, 0x66, 0x70, 0x72,
         /* Ex */ 0x73, 0xE1, 0x74, 0x75, 0x76, 0x77, 0x78, 0x80, 0x8C, 0x8D, 0x8E, 0xEB, 0x8F, 0xED, 0xEE, 0xEF,
         /* Fx */ 0x90, 0x9A, 0x9B, 0x9D, 0x9F, 0xA0, 0xAC, 0xAE, 0xAF, 0xFD, 0xFE, 0xFB, 0x3F, 0xEA, 0xFA, 0xFF
         };     /*  x0    x1    x2    x3    x4    x5    x6    x7    x8    x9    xA    xB    xC    xD    xE    xF */

        public static Byte[]
        ebcdic_to_ascii = {
         /*         x0    x1    x2    x3    x4    x5    x6    x7    x8    x9    xA    xB    xC    xD    xE    xF */
         /* 0x */ 0x00, 0x01, 0x02, 0x03, 0xA6, 0x09, 0xA7, 0x7F, 0xA9, 0xB0, 0xB1, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
         /* 1x */ 0x10, 0x11, 0x12, 0x13, 0xB2, 0x0A, 0x08, 0xB7, 0x18, 0x19, 0x1A, 0xB8, 0xBA, 0x1D, 0xBB, 0x1F,
         /* 2x */ 0xBD, 0xC0, 0x1C, 0xC1, 0xC2, 0x0A, 0x17, 0x1B, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0x05, 0x06, 0x07,
         /* 3x */ 0xC8, 0xC9, 0x16, 0xCB, 0xCC, 0x1E, 0xCD, 0x04, 0xCE, 0xD0, 0xD1, 0xD2, 0x14, 0x15, 0xD3, 0xFC,
         /* 4x */ 0x20, 0xD4, 0x83, 0x84, 0x85, 0xA0, 0xD5, 0x86, 0x87, 0xA4, 0xD6, 0x2E, 0x3C, 0x28, 0x2B, 0xD7,
         /* 5x */ 0x26, 0x82, 0x88, 0x89, 0x8A, 0xA1, 0x8C, 0x8B, 0x8D, 0xD8, 0x21, 0x24, 0x2A, 0x29, 0x3B, 0x5E,
         /* 6x */ 0x2D, 0x2F, 0xD9, 0x8E, 0xDB, 0xDC, 0xDD, 0x8F, 0x80, 0xA5, 0x7C, 0x2C, 0x25, 0x5F, 0x3E, 0x3F,
         /* 7x */ 0xDE, 0x90, 0xDF, 0xE0, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0x60, 0x3A, 0x23, 0x40, 0x27, 0x3D, 0x22,
         /* 8x */ 0xE7, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0xAE, 0xAF, 0xE8, 0xE9, 0xEA, 0xEC,
         /* 9x */ 0xF0, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F, 0x70, 0x71, 0x72, 0xF1, 0xF2, 0x91, 0xF3, 0x92, 0xF4,
         /* Ax */ 0xF5, 0x7E, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0xAD, 0xA8, 0xF6, 0x5B, 0xF7, 0xF8,
         /* Bx */ 0x9B, 0x9C, 0x9D, 0x9E, 0x9F, 0xB5, 0xB6, 0xAC, 0xAB, 0xB9, 0xAA, 0xB3, 0xBC, 0x5D, 0xBE, 0xBF,
         /* Cx */ 0x7B, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0xCA, 0x93, 0x94, 0x95, 0xA2, 0xCF,
         /* Dx */ 0x7D, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50, 0x51, 0x52, 0xDA, 0x96, 0x81, 0x97, 0xA3, 0x98,
         /* Ex */ 0x5C, 0xE1, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0xFD, 0xEB, 0x99, 0xED, 0xEE, 0xEF,
         /* Fx */ 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0xFE, 0xFB, 0x9A, 0xF9, 0xFA, 0xFF
         };     /*  x0    x1    x2    x3    x4    x5    x6    x7    x8    x9    xA    xB    xC    xD    xE    xF */

        /*-------------------------------------------------------------------*/
        /* CKD device definitions                                            */
        /*-------------------------------------------------------------------*/
        public static CKDDEV[] ckdtab = {
        /*           name         type model clas code prime a hd    r0    r1 har0   len sec    rps  f f1  f2   f3   f4 f5 f6  cu */
         new CKDDEV("2305",      0x2305,0x00,0x20,0x00,   48,0, 8,14568,14136, 432,14568, 90,0x0000,-1,202,432,  0,   0,  0,0,"2835"),
         new CKDDEV("2305-1",    0x2305,0x00,0x20,0x00,   48,0, 8,14568,14136, 432,14568, 90,0x0000,-1,202,432,  0,   0,  0,0,"2835"),
         new CKDDEV("2305-2",    0x2305,0x02,0x20,0x00,   96,0, 8,14858,14660, 198,14858, 90,0x0000,-1, 91,198,  0,   0,  0,0,"2835"),
         new CKDDEV("2305-x",    0x2305,0x02,0x20,0x00,65535,0, 8,14858,14660, 198,14858, 90,0x0000,-1, 91,198,  0,   0,  0,0,"2835"),

        /*           name         type model clas code prime a hd    r0    r1 har0   len sec    rps  f f1  f2   f3   f4 f5 f6  cu */
         new CKDDEV("2311",      0x2311,0x00,0x20,0x00,  200,3,10,    0, 3625,   0, 3625,  0,0x0000,-2,20, 61, 537, 512,  0,0,"2841"),
         new CKDDEV("2311-1",    0x2311,0x00,0x20,0x00,  200,3,10,    0, 3625,   0, 3625,  0,0x0000,-2,20, 61, 537, 512,  0,0,"2841"),
         new CKDDEV("2311-x",    0x2311,0x00,0x20,0x00,65535,0,10,    0, 3625,   0, 3625,  0,0x0000,-2,20, 61, 537, 512,  0,0,"2841"),

        /*           name         type model clas code prime a hd    r0    r1 har0   len sec    rps  f f1  f2   f3   f4 f5 f6  cu */
         new CKDDEV("2314",      0x2314,0x00,0x20,0x00,  200,3,20,    0, 7294,   0, 7294,  0,0x0000,-2,45,101,2137,2048,  0,0,"2314"),
         new CKDDEV("2314-1",    0x2314,0x00,0x20,0x00,  200,3,20,    0, 7294,   0, 7294,  0,0x0000,-2,45,101,2137,2048,  0,0,"2314"),
         new CKDDEV("2314-x",    0x2314,0x00,0x20,0x00,65535,0,20,    0, 7294,   0, 7294,  0,0x0000,-2,45,101,2137,2048,  0,0,"2314"),

        /*           name         type model clas code prime a hd    r0    r1 har0   len sec    rps  f f1  f2   f3   f4 f5 f6  cu */
         new CKDDEV("3330",      0x3330,0x01,0x20,0x00,  404,7,19,13165,13030, 135,13165,128,0x0000,-1,56,135,   0,   0,  0,0,"3830"),
         new CKDDEV("3330-1",    0x3330,0x01,0x20,0x00,  404,7,19,13165,13030, 135,13165,128,0x0000,-1,56,135,   0,   0,  0,0,"3830"),
         new CKDDEV("3330-2",    0x3330,0x11,0x20,0x00,  808,7,19,13165,13030, 135,13165,128,0x0000,-1,56,135,   0,   0,  0,0,"3830"),
         new CKDDEV("3330-11",   0x3330,0x11,0x20,0x00,  808,7,19,13165,13030, 135,13165,128,0x0000,-1,56,135,   0,   0,  0,0,"3830"),
         new CKDDEV("3330-x",    0x3330,0x11,0x20,0x00,65535,0,19,13165,13030, 135,13165,128,0x0000,-1,56,135,   0,   0,  0,0,"3830"),

        /*           name         type model clas code prime a hd    r0    r1 har0   len sec    rps  f f1  f2   f3   f4 f5 f6  cu */
         new CKDDEV("3340",      0x3340,0x01,0x20,0x00,  348,1,12, 8535, 8368, 167, 8535, 64,0x0000,-1,75,167,   0,   0,  0,0,"3830"),
         new CKDDEV("3340-1",    0x3340,0x01,0x20,0x00,  348,1,12, 8535, 8368, 167, 8535, 64,0x0000,-1,75,167,   0,   0,  0,0,"3830"),
         new CKDDEV("3340-35",   0x3340,0x01,0x20,0x00,  348,1,12, 8535, 8368, 167, 8535, 64,0x0000,-1,75,167,   0,   0,  0,0,"3830"),
         new CKDDEV("3340-2",    0x3340,0x02,0x20,0x00,  696,2,12, 8535, 8368, 167, 8535, 64,0x0000,-1,75,167,   0,   0,  0,0,"3830"),
         new CKDDEV("3340-70",   0x3340,0x02,0x20,0x00,  696,2,12, 8535, 8368, 167, 8535, 64,0x0000,-1,75,167,   0,   0,  0,0,"3830"),
         new CKDDEV("3340-x",    0x3340,0x02,0x20,0x00,65535,0,12, 8535, 8368, 167, 8535, 64,0x0000,-1,75,167,   0,   0,  0,0,"3830"),

        /*           name         type model clas code prime a hd    r0    r1 har0   len sec    rps  f f1  f2   f3   f4 f5 f6  cu */
         new CKDDEV("3350",      0x3350,0x00,0x20,0x00,  555,5,30,19254,19069, 185,19254,128,0x0000,-1,82,185,   0,   0,  0,0,"3830"),
         new CKDDEV("3350-1",    0x3350,0x00,0x20,0x00,  555,5,30,19254,19069, 185,19254,128,0x0000,-1,82,185,   0,   0,  0,0,"3830"),
         new CKDDEV("3350-x",    0x3350,0x00,0x20,0x00,65535,0,30,19254,19069, 185,19254,128,0x0000,-1,82,185,   0,   0,  0,0,"3830"),

        /*           name         type model clas code prime a hd    r0    r1 har0   len sec    rps  f f1  f2   f3   f4 f5 f6  cu */
         new CKDDEV("3375",      0x3375,0x02,0x20,0x0e,  959,3,12,36000,35616, 832,36000,196,0x5007, 1, 32,384,160,   0,  0,0,"3880"),
         new CKDDEV("3375-1",    0x3375,0x02,0x20,0x0e,  959,3,12,36000,35616, 832,36000,196,0x5007, 1, 32,384,160,   0,  0,0,"3880"),
         new CKDDEV("3375-x",    0x3375,0x02,0x20,0x0e,65535,0,12,36000,35616, 832,36000,196,0x5007, 1, 32,384,160,   0,  0,0,"3880"),

        /*           name         type model clas code prime a hd    r0    r1 har0   len sec    rps  f f1  f2   f3   f4 f5 f6  cu */
         new CKDDEV("3380",      0x3380,0x02,0x20,0x0e,  885,1,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),
         new CKDDEV("3380-1",    0x3380,0x02,0x20,0x0e,  885,1,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),
         new CKDDEV("3380-A",    0x3380,0x02,0x20,0x0e,  885,1,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),
         new CKDDEV("3380-B",    0x3380,0x02,0x20,0x0e,  885,1,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),
         new CKDDEV("3380-D",    0x3380,0x06,0x20,0x0e,  885,1,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),
         new CKDDEV("3380-J",    0x3380,0x16,0x20,0x0e,  885,1,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),
         new CKDDEV("3380-2",    0x3380,0x0a,0x20,0x0e, 1770,2,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),
         new CKDDEV("3380-E",    0x3380,0x0a,0x20,0x0e, 1770,2,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),
         new CKDDEV("3380-3",    0x3380,0x1e,0x20,0x0e, 2655,3,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),
         new CKDDEV("3380-K",    0x3380,0x1e,0x20,0x0e, 2655,3,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),
         new CKDDEV("EMC3380K+", 0x3380,0x1e,0x20,0x0e, 3339,3,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),
         new CKDDEV("EMC3380K++",0x3380,0x1e,0x20,0x0e, 3993,3,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),
         new CKDDEV("3380-x",    0x3380,0x1e,0x20,0x0e,65535,0,15,47988,47476,1088,47968,222,0x5007, 1, 32,492,236,   0,  0,0,"3880"),

        /*           name         type model clas code prime a hd    r0    r1 har0   len sec    rps  f f1  f2   f3   f4 f5 f6  cu */
         new CKDDEV("3390",      0x3390,0x02,0x20,0x26, 1113,1,15,57326,56664,1428,58786,224,0x7708, 2, 34,19,   9,   6,116,6,"3990"),
         new CKDDEV("3390-1",    0x3390,0x02,0x20,0x26, 1113,1,15,57326,56664,1428,58786,224,0x7708, 2, 34,19,   9,   6,116,6,"3990"),
         new CKDDEV("3390-2",    0x3390,0x06,0x20,0x27, 2226,1,15,57326,56664,1428,58786,224,0x7708, 2, 34,19,   9,   6,116,6,"3990"),
         new CKDDEV("3390-3",    0x3390,0x0a,0x20,0x24, 3339,1,15,57326,56664,1428,58786,224,0x7708, 2, 34,19,   9,   6,116,6,"3990"),
         new CKDDEV("3390-9",    0x3390,0x0c,0x20,0x32,10017,3,15,57326,56664,1428,58786,224,0x7708, 2, 34,19,   9,   6,116,6,"3990"),
         new CKDDEV("3390-27",   0x3390,0x0c,0x20,0x32,32760,3,15,57326,56664,1428,58786,224,0x7708, 2, 34,19,   9,   6,116,6,"3990"),
         new CKDDEV("3390-J",    0x3390,0x0c,0x20,0x32,32760,3,15,57326,56664,1428,58786,224,0x7708, 2, 34,19,   9,   6,116,6,"3990"),
         new CKDDEV("3390-54",   0x3390,0x0c,0x20,0x32,65520,3,15,57326,56664,1428,58786,224,0x7708, 2, 34,19,   9,   6,116,6,"3990"),
         new CKDDEV("3390-JJ",   0x3390,0x0c,0x20,0x32,65520,3,15,57326,56664,1428,58786,224,0x7708, 2, 34,19,   9,   6,116,6,"3990"),
         new CKDDEV("3390-x",    0x3390,0x0c,0x20,0x32,65535,0,15,57326,56664,1428,58786,224,0x7708, 2, 34,19,   9,   6,116,6,"3990"),

        /*           name         type model clas code prime a hd    r0    r1 har0   len sec    rps  f f1  f2   f3   f4 f5 f6  cu */
         new CKDDEV("9345",      0x9345,0x04,0x20,0x04, 1440,0,15,48174,46456,1184,48280,213,0x8b07, 2, 34,18,   7,   6,116,6,"9343"),
         new CKDDEV("9345-1",    0x9345,0x04,0x20,0x04, 1440,0,15,48174,46456,1184,48280,213,0x8b07, 2, 34,18,   7,   6,116,6,"9343"),
         new CKDDEV("9345-2",    0x9345,0x04,0x20,0x04, 2156,0,15,48174,46456,1184,48280,213,0x8b07, 2, 34,18,   7,   6,116,6,"9343"),
         new CKDDEV("9345-x",    0x9345,0x04,0x20,0x04,65535,0,15,48174,46456,1184,48280,213,0x8b07, 2, 34,18,   7,   6,116,6,"9343")
        };

        /*-------------------------------------------------------------------*/
        /* FBA device definitions - courtesy of Tomas Masek                  */
        /*-------------------------------------------------------------------*/
        public static FBADEV[] fbatab = {
            /* name                   devt class type mdl  bpg bpp size   blks   cu     */
            new FBADEV("3310",       0x3310,0x21,0x01,0x01, 32,352,512, 125664,"4331"),
            new FBADEV("3310-1",     0x3310,0x21,0x01,0x01, 32,352,512, 125664,"4331"),
            new FBADEV("3310-x",     0x3310,0x21,0x01,0x01, 32,352,512,      0,"4331"),
            /* name                   devt class type mdl  bpg bpp size   blks   cu     */
            new FBADEV("3370",       0x3370,0x21,0x02,0x00, 62,744,512, 558000,"3880"),
            new FBADEV("3370-1",     0x3370,0x21,0x02,0x00, 62,744,512, 558000,"3880"),
            new FBADEV("3370-A1",    0x3370,0x21,0x02,0x00, 62,744,512, 558000,"3880"),
            new FBADEV("3370-B1",    0x3370,0x21,0x02,0x00, 62,744,512, 558000,"3880"),
            new FBADEV("3370-2",     0x3370,0x21,0x05,0x04, 62,744,512, 712752,"3880"),
            new FBADEV("3370-A2",    0x3370,0x21,0x05,0x04, 62,744,512, 712752,"3880"),
            new FBADEV("3370-B2",    0x3370,0x21,0x05,0x04, 62,744,512, 712752,"3880"),
            new FBADEV("3370-x",     0x3370,0x21,0x05,0x04, 62,744,512,      0,"3880"),
            /* name                   devt class type mdl  bpg bpp size   blks   cu     */
            new FBADEV("9332",       0x9332,0x21,0x07,0x00, 73,292,512, 360036,"6310"),
            new FBADEV("9332-400",   0x9332,0x21,0x07,0x00, 73,292,512, 360036,"6310"),
            new FBADEV("9332-600",   0x9332,0x21,0x07,0x01, 73,292,512, 554800,"6310"),
            new FBADEV("9332-x",     0x9332,0x21,0x07,0x01, 73,292,512,      0,"6310"),
            /* name                   devt class type mdl  bpg bpp size   blks   cu     */
            new FBADEV("9335",       0x9335,0x21,0x06,0x01, 71,426,512, 804714,"6310"),
            new FBADEV("9335-x",     0x9335,0x21,0x06,0x01, 71,426,512,      0,"6310"),
            /* name                   devt class type mdl  bpg bpp size   blks   cu     */
            new FBADEV("9313",       0x9313,0x21,0x08,0x00, 96,480,512, 246240,"6310"),
            new FBADEV("9313-x",     0x9313,0x21,0x08,0x00, 96,480,512,      0,"6310"),
            /* name                   devt class type mdl  bpg bpp size   blks   cu     */
            new FBADEV("9336",       0x9336,0x21,0x11,0x00, 63,315,512, 920115,"6310"),
            new FBADEV("9336-10",    0x9336,0x21,0x11,0x00, 63,315,512, 920115,"6310"),
            new FBADEV("9336-20",    0x9336,0x21,0x11,0x10,111,777,512,1672881,"6310"),
            new FBADEV("9336-25",    0x9336,0x21,0x11,0x10,111,777,512,1672881,"6310"),
            new FBADEV("9336-x",     0x9336,0x21,0x11,0x10,111,777,512,      0,"6310"),
            /* name                   devt class type mdl  bpg bpp size   blks   cu     */
            new FBADEV("0671-08",    0x0671,0x21,0x12,0x08, 63,504,512, 513072,"6310"),
            new FBADEV("0671",       0x0671,0x21,0x12,0x00, 63,504,512, 574560,"6310"),
            new FBADEV("0671-04",    0x0671,0x21,0x12,0x04, 63,504,512, 624456,"6310"),
            new FBADEV("0671-x",     0x0671,0x21,0x12,0x04, 63,504,512,      0,"6310")
         };
        #endregion

        public static String[,] extensions = new string[,] { { "masm", "Mainframe Assembler" }, { "jcl", "Mainframe JCL" },
            { "cob", "Cobol" } , { "rexx", "REXX Procedural Language" }, {"cntl", "Control statements"}, {"txt", "Text" } };

        public static String _rtfHdr = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Consolas;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs22 ";
        public static String _rtfEnd = "}\r\n";
        public static String _rtfNL = "\\par\r\n";

        public static String filter = "";
        public static String folder = "";
        private static readonly Int32 _curYY = DateTime.Today.Year % 100;

        public enum SaveResults
        {
            Fail = 0,
            Success = 1,
            Cancel = 2
        }

        public static String JulDate(BINYDD date)
        {
            Int32 curYr = _curYY;
            String ddd = ((UInt16)date.DD.big_endian).ToString("000");
            ddd = ddd.Substring(ddd.Length - 3);
            if (date.Y <= curYr)
            {
                curYr = date.Y + 2000;
            }
            else
            {
                curYr = date.Y + 1900;
            }
            return curYr.ToString() + "." + ddd;
        }

        public static String YYDDD(BINYDD date)
        {
            String ddd = ((UInt16)date.DD.big_endian).ToString("000");
            ddd = ddd.Substring(ddd.Length - 3);
            return date.Y.ToString("00") + "." + ddd;
        }

        #region Assembly Attribute Accessors

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                string[] version = Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                return version[0] + "." + version[1];
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    }
    public class extObject : Object
    {
        public void Debug(String fieldName)
        {
            if (!Global.genDiag) return;
            if (Global.diag == null) return;

            StackTrace st = new StackTrace(new StackFrame(1, true));
            StackTrace st2 = new StackTrace();
            StackFrame sf = st.GetFrame(0);
            MethodBase method = sf.GetMethod();

            String clsName = this.GetType().Name;

            switch (clsName)
            {
                case "DEVBLK":
                    Debug_DEVBLK(fieldName, this, method);
                    break;

                case "HWORD":
                case "FWORD":
                case "DBLWRD":
                case "QUADWRD":
                    Debug_WORDS(fieldName, this, method);
                    break;

                case "BINYDD":
                    Debug_BINYDD(fieldName, this, method);
                    break;

                case "VTOCXTENT":
                    Debug_VTOCXTENT(fieldName, this, method);
                    break;

                case "DSXTENT":
                    Debug_DSXTENT(fieldName, this, method);
                    break;

                case "FORMAT1_DSCB":
                    Debug_FORMAT1_DSCB(fieldName, this, method);
                    break;

                case "FORMAT4_DSCB":
                    Debug_FORMAT1_DSCB(fieldName, this, method);
                    break;

                case "VOL1_LABEL":
                    Debug_VOL1_LABEL(fieldName, this, method);
                    break;

                case "CKD_ImageFileDescriptor":
                    Debug_CKD_ImageFileDescriptor(fieldName, this, method);
                    break;

                case "LVL1TAB":
                    Debug_LVL1TAB(fieldName, this, method);
                    break;

                case "LVL2TAB":
                    Debug_LVL2TAB(fieldName, this, method);
                    break;

                case "LVL2ENTRY":
                    Debug_LVL2ENTRY(fieldName, this, method);
                    break;

                case "CCHHR":
                    Debug_CCHHR(fieldName, this, method);
                    break;

                case "CKDDASD_RECHDR":
                    Debug_CKDDASD_RECHDR(fieldName, this, method);
                    break;

                case "CKD_GROUP":
                    Debug_CKD_GROUP(fieldName, this, method);
                    break;

                case "DASD_DEVHDR":
                    Debug_DASD_DEVHDR(fieldName, this, method);
                    break;

                case "DASD_COMP_DEVHDR":
                    Debug_DASD_COMP_DEVHDR(fieldName, this, method);
                    break;

                case "FBADEV":
                    Debug_FBADEV(fieldName, this, method);
                    break;

                case "CKDDEV":
                    Debug_CKDDEV(fieldName, this, method);
                    break;

                case "DEVHND":
                    Debug_DEVHND(fieldName, this, method);
                    break;

                case "DASDEntry":
                    Debug_DASDEntry(fieldName, this, method);
                    break;

                case "MemberEntry":
                    Debug_MemberEntry(fieldName, this, method);
                    break;

                case "VolumeEntry":
                    Debug_VolumeEntry(fieldName, this, method);
                    break;

                case "DSNEntry":
                    Debug_DSNEntry(fieldName, this, method);
                    break;

                case "TRACKEntry":
                    Debug_TRACKEntry(fieldName, this, method);
                    break;

                case "CKD_HDR":
                    Debug_CKD_HDR(fieldName, this, method);
                    break;

                default:
                    Debug_UnknownEntry(fieldName, this, method);
                    break;
            }
        }

        private void Debug_UnknownEntry(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Type objType = @object.GetType();
            IEnumerable<FieldInfo> fieldInfo = objType.GetRuntimeFields();

            foreach (FieldInfo field in fieldInfo)
            {
                object value = field.GetValue(this);
                if (value != null)
                {
                    string vName = value.GetType().Name.ToLower();

                    switch (vName)
                    {
                        case "string":
                        case "int16":
                        case "int32":
                        case "int64":
                        case "int":
                        case "uint16":
                        case "uint32":
                        case "uint64":
                        case "uint":
                        case "byte":
                            Global.diag.Text += field.Name + ": " + value.ToString() + Environment.NewLine;
                            break;

                        default:
                            Global.diag.Text += field.Name + ": " + value.ToString() + Environment.NewLine;
                            break;
                    }
                }
                else
                {
                    Global.diag.Text += field.Name + ": [null]" + Environment.NewLine;
                }
            }
            Global.diag.Text += Environment.NewLine;
        }

        private void Debug_VolumeEntry(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            VolumeEntry volume = (VolumeEntry)@object;
            Global.diag.Text += "volume: " + volume.volume + Environment.NewLine;
            Global.diag.Text += "device: " + volume.device + Environment.NewLine;
            Global.diag.Text += "filename: " + volume.filename + Environment.NewLine;
            Global.diag.Text += "shadowfile: " + volume.shadowfile + Environment.NewLine;
            Global.diag.Text += "creationDate: " + volume.creationDate + Environment.NewLine;
            Global.diag.Text += Environment.NewLine;
        }

        private void Debug_BINYDD(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine; 
        }
        private void Debug_DSXTENT(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_FORMAT1_DSCB(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_VOL1_LABEL(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_LVL1TAB(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_LVL2TAB(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_LVL2ENTRY(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_CCHHR(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_CKDDASD_RECHDR(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_CKD_GROUP(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_CKD_HDR(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_FBADEV(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_DEVHND(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_TRACKEntry(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_MemberEntry(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_DSNEntry(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_CKD_ImageFileDescriptor(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }

        private void  Debug_DASD_DEVHDR(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            DASD_DEVHDR devHdr = (DASD_DEVHDR)@object;
            Global.diag.Text += "devid: " + devHdr.devid + Environment.NewLine;
            Global.diag.Text += "devtype: " + devHdr.devtype + Environment.NewLine;
            Global.diag.Text += "fileseq: " + devHdr.fileseq + Environment.NewLine;
            Global.diag.Text += "heads: " + devHdr.heads.ToHexString() + Environment.NewLine;
            Global.diag.Text += "highcyl: " + devHdr.highcyl.ToHexString() + Environment.NewLine;
            Global.diag.Text += "trksize: " + devHdr.trksize.ToHexString() + Environment.NewLine;
            Global.diag.Text += Environment.NewLine;
        }

        private void Debug_DASD_COMP_DEVHDR(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            DASD_COMP_DEVHDR compHdr = (DASD_COMP_DEVHDR)@object;
            Global.diag.Text += "vrm: " + HexString(compHdr.vrm) + Environment.NewLine;
            Global.diag.Text += "used: " + compHdr.used + Environment.NewLine;
            Global.diag.Text += "size: " + compHdr.size + Environment.NewLine;
            Global.diag.Text += "options: " + compHdr.options + Environment.NewLine;
            Global.diag.Text += "numl2tab: " + compHdr.numl2tab + Environment.NewLine;
            Global.diag.Text += "numl1tab: " + compHdr.numl1tab + Environment.NewLine;
            Global.diag.Text += "nullfmt: " + compHdr.nullfmt + Environment.NewLine;
            Global.diag.Text += "free_total: " + compHdr.free_total + Environment.NewLine;
            Global.diag.Text += "free_number: " + compHdr.free_number + Environment.NewLine;
            Global.diag.Text += "free_largest: " + compHdr.free_largest + Environment.NewLine;
            Global.diag.Text += "free_imbed: " + compHdr.free_imbed + Environment.NewLine;
            Global.diag.Text += "free: " + compHdr.free + Environment.NewLine;
            Global.diag.Text += "cyls: " + compHdr.cyls.ToHexString() + Environment.NewLine;
            Global.diag.Text += "compress_parm: " + compHdr.compress_parm + Environment.NewLine;
            Global.diag.Text += "compress: " + compHdr.compress + Environment.NewLine;
            Global.diag.Text += Environment.NewLine;
        }

        private void Debug_CKDDEV(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            CKDDEV ckd = (CKDDEV)@object;
            Global.diag.Text += "_altcyls: " + ckd._altcyls + Environment.NewLine;
            Global.diag.Text += "_code: " + ckd._code + Environment.NewLine;
            Global.diag.Text += "_cu: " + ckd._cu + Environment.NewLine;
            Global.diag.Text += "_cyls: " + ckd._cyls + Environment.NewLine;
            Global.diag.Text += "_devclass: " + ckd._devclass + Environment.NewLine;
            Global.diag.Text += "_devt: " + ckd._devt + Environment.NewLine;
            Global.diag.Text += "_f1: " + ckd._f1 + Environment.NewLine;
            Global.diag.Text += "_f2: " + ckd._f2 + Environment.NewLine;
            Global.diag.Text += "_f3: " + ckd._f3 + Environment.NewLine;
            Global.diag.Text += "_f4: " + ckd._f4 + Environment.NewLine;
            Global.diag.Text += "_f5: " + ckd._f5 + Environment.NewLine;
            Global.diag.Text += "_f6: " + ckd._f6 + Environment.NewLine;
            Global.diag.Text += "_formula: " + ckd._formula + Environment.NewLine;
            Global.diag.Text += "_har0: " + ckd._har0 + Environment.NewLine;
            Global.diag.Text += "_heads: " + ckd._heads + Environment.NewLine;
            Global.diag.Text += "_len: " + ckd._len + Environment.NewLine;
            Global.diag.Text += "_model: " + ckd._model + Environment.NewLine;
            Global.diag.Text += "_name: " + ckd._name + Environment.NewLine;
            Global.diag.Text += "_r0: " + ckd._r0 + Environment.NewLine;
            Global.diag.Text += "_r1: " + ckd._r1 + Environment.NewLine;
            Global.diag.Text += "_rpscalc: " + ckd._rpscalc + Environment.NewLine;
            Global.diag.Text += "_sectors: " + ckd._sectors + Environment.NewLine;
            Global.diag.Text += Environment.NewLine;
        }

        private void Debug_WORDS(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            String clsName = @object.GetType().Name;

            switch (clsName)
            {
                case "HWORD":
                    HWORD hWORD = (HWORD)@object;
                    Global.diag.Text += "bytes: " + HexString(hWORD.bytes) + Environment.NewLine;
                    Global.diag.Text += "big_endian: " + hWORD.big_endian + Environment.NewLine;
                    Global.diag.Text += "little_endian: " + hWORD.little_endian + Environment.NewLine;
                    Global.diag.Text += Environment.NewLine;
                    break;

                case "FWORD":
                    FWORD fWORD = (FWORD)@object;
                    Global.diag.Text += "bytes: " + HexString(fWORD.bytes) + Environment.NewLine;
                    Global.diag.Text += "big_endian: " + fWORD.big_endian + Environment.NewLine;
                    Global.diag.Text += "little_endian: " + fWORD.little_endian + Environment.NewLine;
                    Global.diag.Text += Environment.NewLine;
                    break;

                case "DBLWRD":
                    DBLWRD dWORD = (DBLWRD)@object;
                    Global.diag.Text += "bytes: " + HexString(dWORD.bytes) + Environment.NewLine;
                    Global.diag.Text += "big_endian: " + dWORD.big_endian + Environment.NewLine;
                    Global.diag.Text += "little_endian: " + dWORD.little_endian + Environment.NewLine;
                    Global.diag.Text += Environment.NewLine;
                    break;

                case "QUADWRD":
                    QUADWRD qWORD = (QUADWRD)@object;
                    Global.diag.Text += "bytes: " + HexString(qWORD.bytes) + Environment.NewLine;
                    Global.diag.Text += "big_endian: " + qWORD.big_endian + Environment.NewLine;
                    Global.diag.Text += "little_endian: " + qWORD.little_endian + Environment.NewLine;
                    Global.diag.Text += Environment.NewLine;
                    break;

                default:
                    break;
            }
        }

        private void Debug_VTOCXTENT(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            VTOCXTENT vtoc = (VTOCXTENT)@object;
            Global.diag.Text += "xtbcyl: " + vtoc.xtbcyl.ToHexString() + Environment.NewLine;
            Global.diag.Text += "xtbtrk: " + vtoc.xtbtrk.ToHexString() + Environment.NewLine;
            Global.diag.Text += "xtecyl: " + vtoc.xtecyl.ToHexString() + Environment.NewLine;
            Global.diag.Text += "xtetrk: " + vtoc.xtetrk.ToHexString() + Environment.NewLine;
            Global.diag.Text += "xtseqn: " + vtoc.xtseqn + Environment.NewLine;
            Global.diag.Text += "xttype: " + vtoc.xttype + Environment.NewLine;
            Global.diag.Text += Environment.NewLine;
        }

        private void Debug_DEVBLK(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            DEVBLK dev = (DEVBLK)@object;
            Global.diag.Text += "batch: " + dev.batch + Environment.NewLine;
            Global.diag.Text += "bufused: " + dev.bufused + Environment.NewLine;
            Global.diag.Text += "ckddev: " + dev.ckddev + Environment.NewLine;
            Global.diag.Text += "cmpDevHdr: " + dev.cmpDevHdr + Environment.NewLine;
            Global.diag.Text += "curcyl: " + dev.curcyl + Environment.NewLine;
            Global.diag.Text += "curhead: " + dev.curhead + Environment.NewLine;
            Global.diag.Text += "dasdcopy: " + dev.dasdcopy + Environment.NewLine;
            Global.diag.Text += "devHdr: " + dev.devHdr + Environment.NewLine;
            Global.diag.Text += "devimage: " + dev.devimage + Environment.NewLine;
            Global.diag.Text += "devnum: " + dev.devnum + Environment.NewLine;
            Global.diag.Text += "devtype: " + dev.devtype + Environment.NewLine;
            Global.diag.Text += "fbadev: " + dev.fbadev + Environment.NewLine;
            Global.diag.Text += "fileName: " + dev.fileName + Environment.NewLine;
            Global.diag.Text += "fileStream: " + dev.fileStream + Environment.NewLine;
            Global.diag.Text += "heads: " + dev.heads + Environment.NewLine;
            Global.diag.Text += "l1tab: " + dev.l1tab + Environment.NewLine;
            Global.diag.Text += "l2tab: " + dev.l2tab + Environment.NewLine;
            Global.diag.Text += "quiet: " + dev.quiet + Environment.NewLine;
            Global.diag.Text += "rdonly: " + dev.rdonly + Environment.NewLine;
            Global.diag.Text += "showdvol1: " + dev.showdvol1 + Environment.NewLine;
            Global.diag.Text += "trkbuf: " + dev.trkbuf + Environment.NewLine;
            Global.diag.Text += "trkmodif: " + dev.trkmodif + Environment.NewLine;
            Global.diag.Text += "trksz: " + dev.trksz + Environment.NewLine;
            Global.diag.Text += "typname: " + dev.typname + Environment.NewLine;
            Global.diag.Text += Environment.NewLine;
        }

        private void Debug_DASDEntry(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            DASDEntry dEntry = (DASDEntry)@object;
            Global.diag.Text += "filename: " + dEntry.filename + Environment.NewLine;
            Global.diag.Text += "offset: " + dEntry.offset + Environment.NewLine;
            Global.diag.Text += "size: " + dEntry.size + Environment.NewLine;
            Global.diag.Text += "structureName: " + dEntry.structureName + Environment.NewLine;
            Global.diag.Text += Environment.NewLine;
        }

        private String HexString(Byte[] bytes)
        {
            String ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += bytes[i].ToString("X2");
            }
            return ret;
        }
    }
    public class DEVBLK : extObject
    {
        private DEVHND _devHandler;
        private String _devimage;               /* Device image CKD/FBA      */
        private UInt16 _devnum;                 /* Device number             */
        private UInt16 _devtype;                /* Device type               */
        private String _typname;                /* Device type name          */
        private Stream _fileStream;             /* associated File stream    */
        private String _filePath;               /* file path                 */
        private String _fileName;               /* file name                 */
        private Int32 _trksz;                   /* Track size                */
        private Byte[] _trkbuf;                 /* Device data buffer, set
                                           initially to track size   */
        private Int32 _bufUsed;                 /* size of data buffer used  */
        private Boolean _batch;                 /* 1=Called by dasdutil      */
        private Boolean _dasdcopy;              /* 1=Called by dasdcopy      */
        private Boolean _showdvol1;             /* 1=showdvol1 open busy     */
        private Boolean _quiet;                 /* 1=suppress open messages  */
        private Int32 _heads;                   /* # of heads per cylinder   */
        private Int16 _rdonly;                  /* 1=Open read only          */
        private Int32 _curcyl;
        private Int16 _curhead;
        private Boolean _trkmodif;

        private CKDDEV _ckddev;                 /* CKD Device table entry    */
        private FBADEV _fbadev;                 /* FBA Device table entry    */
        private DASD_DEVHDR _devHdr;
        private DASD_COMP_DEVHDR _cmpDevHdr;
        private LVL1TAB _l1tab;
        private LVL2TAB[] _l2tab;

    public DEVBLK()
    {
        _devHandler = new DEVHND();
        _devHdr = new DASD_DEVHDR();
        _cmpDevHdr = new DASD_COMP_DEVHDR();
    }

    public DEVBLK(String fileName)
    {
        _devHandler = new DEVHND();
        _devHdr = new DASD_DEVHDR();
        _cmpDevHdr = new DASD_COMP_DEVHDR();
        _fileName = fileName;
    }

    public DEVBLK(String filePath, String fileName)
    {
        _devHandler = new DEVHND();
        _devHdr = new DASD_DEVHDR();
        _cmpDevHdr = new DASD_COMP_DEVHDR();
        _filePath = filePath;
        _fileName = fileName;
    }

    public Byte[] trkbuf
    {
        get { return _trkbuf; }
        set { _trkbuf = value; }
    }

    public Int32 bufused
    {
        get { return _bufUsed; }
        set { _bufUsed = value; }
    }

    public Int16 rdonly
    {
        get { return _rdonly; }
        set { _rdonly = value; }
    }

    public Boolean batch
    {
        get { return _batch; }
        set { _batch = value; }
    }

    public Boolean dasdcopy
    {
        get { return _dasdcopy; }
        set { _dasdcopy = value; }
    }

    public Boolean showdvol1
    {
        get { return _showdvol1; }
        set { _showdvol1 = value; }
    }

    public Boolean quiet
    {
        get { return _quiet; }
        set { _quiet = value; }
    }

    public UInt16 devtype
    {
        get { return _devtype; }
        set { _devtype = value; }
    }

    public String devimage
    {
        get { return _devimage; }
        set { _devimage = value; }
    }

    public String typname
    {
        get { return _typname; }
        set { _typname = value; }
    }

    public UInt16 devnum
    {
        get { return _devnum; }
        set { _devnum = value; }
    }

    public Int32 heads
    {
        get { return _heads; }
        set { _heads = value; }
    }

    public Stream fileStream
    {
        get { return _fileStream; }
        set { _fileStream = value; }
    }

    public String filePath
    {
        get { return _filePath; }
        set { _filePath = value; }
    }

    public String fileName
    {
        get { return _fileName; }
        set { _fileName = value; }
    }

    public FBADEV fbadev
    {
        get { return _fbadev; }
        set { _fbadev = value; }
    }

    public CKDDEV ckddev
    {
        get { return _ckddev; }
        set { _ckddev = value; }
    }

    public Int32 trksz
    {
        get { return _trksz; }
        set { _trksz = value; }
    }

    public Int32 curcyl
    {
        get { return _curcyl; }
        set { _curcyl = value; }
    }

    public Int16 curhead
    {
        get { return _curhead; }
        set { _curhead = value; }
    }

    public Boolean trkmodif
    {
        get { return _trkmodif; }
        set { _trkmodif = value; }
    }

    public DEVHND devHnd
    {
        get { return _devHandler; }
    }

    public DASD_DEVHDR devHdr
    {
        get { return _devHdr; }
        set { _devHdr = value; }
    }

    public DASD_COMP_DEVHDR cmpDevHdr
    {
        get { return _cmpDevHdr; }
        set { _cmpDevHdr = value; }
    }

    public LVL1TAB l1tab
    {
        get { return _l1tab; }
        set { _l1tab = value; }
    }

    public LVL2TAB[] l2tab
    {
        get { return _l2tab; }
        set { _l2tab = value; }
    }
  }
    public class HWORD : extObject
    {
        public HWORD()
        {
            bytes = new Byte[SIZE];
        }
        public HWORD(params Byte[] b)
        {
            bytes = new Byte[SIZE];
            bytes[0] = b[0];
            bytes[1] = b[1];
        }
        public Byte[] bytes { get; set; }
        public Int16 little_endian
        {
            get
            {
                return (Int16)((bytes[1] << 8) | (bytes[0]));
            }
        }
        public Int16 big_endian
        {
            get
            {
                return (Int16)((bytes[0] << 8) | (bytes[1]));
            }
        }
        public int SIZE { get; } = 2;
        public void setHWORD(params Byte[] b)
        {
            if (bytes == null) { bytes = new Byte[SIZE]; }
            bytes[0] = b[0];
            bytes[1] = b[1];
        }
        public String ToHexString()
        {
            return bytes[0].ToString("X2") + bytes[1].ToString("X2");
        }

    }
    public class FWORD : extObject
    {
        public FWORD()
        {
            bytes = new Byte[SIZE];
        }
        public FWORD(params Byte[] b)
        {
            bytes = new Byte[SIZE];
            bytes[0] = b[0];
            bytes[1] = b[1];
            bytes[2] = b[2];
            bytes[3] = b[3];
        }
        public FWORD(Byte[] b, Int32 start)
        {
            bytes = new Byte[SIZE];
            bytes[0] = b[start];
            bytes[1] = b[start + 1];
            bytes[2] = b[start + 2];
            bytes[3] = b[start + 3];
        }
        public Byte[] bytes { get; set; }
        public Int32 little_endian
        {
            get
            {
                return ((bytes[3] << 24)
                    | (bytes[2] << 16)
                    | (bytes[1] << 8)
                    | (bytes[0]));
            }
        }
        public Int32 big_endian
        {
            get
            {
                return ((bytes[0] << 24)
                    | (bytes[1] << 16)
                    | (bytes[2] << 8)
                    | (bytes[3]));
            }
        }
        public int SIZE { get; } = 4;
        public void setFWORD(params Byte[] b)
        {
            if (bytes == null) { bytes = new Byte[SIZE]; }
            bytes[0] = b[0];
            bytes[1] = b[1];
            bytes[2] = b[2];
            bytes[3] = b[3];
        }
        public void setFWORD(Byte[] b, Int32 start)
        {
            if (bytes == null) { bytes = new Byte[SIZE]; }
            bytes[0] = b[start];
            bytes[1] = b[start + 1];
            bytes[2] = b[start + 2];
            bytes[3] = b[start + 3];
        }

        public String ToHexString()
        {
            String ret = "";
            for (int i = 0; i < SIZE; i++)
            {
                ret += bytes[i].ToString("X2");
            }
            return ret;
        }
    }
    public class DBLWRD : extObject
    {
        public DBLWRD()
        {
            bytes = new Byte[SIZE];
        }
        public DBLWRD(params Byte[] b)
        {
            bytes = new Byte[SIZE];
            Array.Copy(b, 0, bytes, 0, SIZE);
        }
        public DBLWRD(Byte[] b, Int32 start)
        {
            bytes = new Byte[SIZE];
            Array.Copy(b, start, bytes, 0, SIZE);
        }
        public Byte[] bytes { get; set; }
        public Int64 little_endian
        {
            get
            {
                return (Int64)((bytes[7] << 56)
                    | (bytes[6] << 48)
                    | (bytes[5] << 40)
                    | (bytes[4] << 32)
                    | (bytes[3] << 24)
                    | (bytes[2] << 16)
                    | (bytes[1] << 8)
                    | (bytes[0]));
            }
        }
        public Int64 big_endian
        {
            get
            {
                return (Int64)((bytes[0] << 56)
                    | (bytes[1] << 48)
                    | (bytes[2] << 40)
                    | (bytes[3] << 32)
                    | (bytes[4] << 24)
                    | (bytes[5] << 16)
                    | (bytes[6] << 8)
                    | (bytes[7]));
            }
        }
        public int SIZE { get; } = 8;

        public String ToHexString()
        {
            String ret = "";
            for (int i = 0; i < SIZE; i++)
            {
                ret += bytes[i].ToString("X2");
            }
            return ret;
        }
    }
    public class QUADWRD : extObject
    {
        public QUADWRD()
        {
            bytes = new Byte[SIZE];
        }
        public QUADWRD(params Byte[] b)
        {
            bytes = new Byte[SIZE];
            Array.Copy(b, 0, bytes, 0, SIZE);
        }
        public QUADWRD(Byte[] b, Int32 start)
        {
            bytes = new Byte[SIZE];
            Array.Copy(b, start, bytes, 0, SIZE);
        }
        public Byte[] bytes { get; set; }
        public Double little_endian
        {
            get
            {
                return (Double)((bytes[15] << 120)
                    | (bytes[14] << 112)
                    | (bytes[13] << 104)
                    | (bytes[12] << 96)
                    | (bytes[11] << 88)
                    | (bytes[10] << 80)
                    | (bytes[9] << 72)
                    | (bytes[8] << 64)
                    | (bytes[7] << 56)
                    | (bytes[6] << 48)
                    | (bytes[5] << 40)
                    | (bytes[4] << 32)
                    | (bytes[3] << 24)
                    | (bytes[2] << 16)
                    | (bytes[1] << 8)
                    | (bytes[0]));
            }
        }
        public Double big_endian
        {
            get
            {
                return (Double)((bytes[0] << 120)
                    | (bytes[1] << 112)
                    | (bytes[2] << 104)
                    | (bytes[3] << 96)
                    | (bytes[4] << 88)
                    | (bytes[5] << 80)
                    | (bytes[6] << 72)
                    | (bytes[7] << 64)
                    | (bytes[8] << 56)
                    | (bytes[9] << 48)
                    | (bytes[10] << 40)
                    | (bytes[11] << 32)
                    | (bytes[12] << 24)
                    | (bytes[13] << 16)
                    | (bytes[14] << 8)
                    | (bytes[15]));
            }
        }
        public int SIZE { get; } = 16;
        public String ToHexString()
        {
            String ret = "";
            for (int i = 0; i < SIZE; i++)
            {
                ret += bytes[i].ToString("X2");
            }
            return ret;
        }
    }
    public class BINYDD : extObject
    {
        public BINYDD()
        {
            bytes = new Byte[3];
        }
        public BINYDD(Byte[] b, Int32 start)
        {
            bytes = new Byte[3];
            Array.Copy(b, start, bytes, 0, 3);
        }
        public Byte[] bytes { get; set; }
        public Byte Y
        {
            get
            {
                return bytes[0];
            }
            set
            {
                bytes[0] = value;
            }
        }
        public HWORD DD
        {
            get
            {
                return new HWORD(bytes[1], bytes[2]);
            }
            set
            {
                bytes[1] = value.bytes[0];
                bytes[2] = value.bytes[1];
            }
        }
    }
    public class VTOCXTENT : extObject
    {
        public static int SIZE = 10;
        public VTOCXTENT()
        {
            buffer = new Byte[SIZE];
        }
        public VTOCXTENT(Byte[] b, Int32 start)
        {
            buffer = new Byte[SIZE];
            Array.Copy(b, start, buffer, 0, SIZE);
        }
        public Byte[] buffer { get; set; }

        /* Extent type               */
        public Byte xttype
        {
            get
            {
                return buffer[0];
            }
            set
            {
                buffer[0] = value;
            }
        }
        /* Extent sequence number    */
        public Byte xtseqn
        {
            get
            {
                return buffer[1];
            }
            set
            {
                buffer[1] = value;
            }
        }
        /* Extent begin cylinder     */
        public HWORD xtbcyl
        {
            get
            {
                return new HWORD(buffer[2], buffer[3]);
            }
            set
            {
                Array.Copy(value.bytes, 0, buffer, 2, 2);
            }
        }
        /* Extent begin track        */
        public HWORD xtbtrk
        {
            get
            {
                return new HWORD(buffer[4], buffer[5]);
            }
            set
            {
                Array.Copy(value.bytes, 0, buffer, 4, 2);
            }
        }
        /* Extent end cylinder       */
        public HWORD xtecyl
        {
            get
            {
                return new HWORD(buffer[6], buffer[7]);
            }
            set
            {
                Array.Copy(value.bytes, 0, buffer, 6, 2);
            }
        }
        /* Extent end track          */
        public HWORD xtetrk
        {
            get
            {
                return new HWORD(buffer[8], buffer[9]);
            }
            set
            {
                Array.Copy(value.bytes, 0, buffer, 8, 2);
            }
        }
    }
    public class DSXTENT : extObject
    {
        public static int SIZE = 10;
        public DSXTENT()
        {
            buffer = new Byte[SIZE];
        }
        public DSXTENT(Byte[] b, Int32 start)
        {
            buffer = new Byte[SIZE];
            Array.Copy(b, start, buffer, 0, SIZE);
        }
        public Byte[] buffer { get; set; }

        /* Extent type               */
        public Byte xttype
        {
            get
            {
                return buffer[0];
            }
            set
            {
                buffer[0] = value;
            }
        }
        /* Extent sequence number    */
        public Byte xtseqn
        {
            get
            {
                return buffer[1];
            }
            set
            {
                buffer[1] = value;
            }
        }
        /* Extent begin cylinder     */
        public Int16 xtbcyl
        {
            get
            {
                return (Int16)(((buffer[2] & 0x0f) << 8) | (buffer[3]));
            }
            set
            {
                Byte b = (Byte)((value >> 8) & 0x0f);
                buffer[2] = (Byte)((buffer[2] & 0xf0) | b);
                buffer[3] = (Byte)(value & 0xff);
            }
        }
        /* Extent begin track        */
        public Int16 xtbtrk
        {
            get
            {
                return (Int16)(((buffer[4] & 0x0f) << 8) | (buffer[5]));
            }
            set
            {
                Byte b = (Byte)((value >> 8) & 0x0f);
                buffer[4] = (Byte)((buffer[4] & 0xf0) | b);
                buffer[5] = (Byte)(value & 0xff);
            }
        }
        /* Extent end cylinder       */
        public Int16 xtecyl
        {
            get
            {
                return (Int16)(((buffer[6] & 0x0f) << 8) | (buffer[7]));
            }
            set
            {
                Byte b = (Byte)((value >> 8) & 0x0f);
                buffer[6] = (Byte)((buffer[6] & 0xf0) | b);
                buffer[7] = (Byte)(value & 0xff);
            }
        }
        /* Extent end track          */
        public Int16 xtetrk
        {
            get
            {
                return (Int16)(((buffer[8] & 0x0f) << 8) | (buffer[9]));
            }
            set
            {
                Byte b = (Byte)((value >> 8) & 0x0f);
                buffer[8] = (Byte)((buffer[8] & 0xf0) | b);
                buffer[9] = (Byte)(value & 0xff);
            }
        }
    }
    public class FORMAT1_DSCB : extObject
  {   /* DSCB1: DSN descriptor    */
      public static int SIZE = 140;
      public FORMAT1_DSCB()
      {
          buffer = new Byte[SIZE];
      }
      public FORMAT1_DSCB(Byte[] b, Int32 offset)
      {
          buffer = new Byte[SIZE];
          Array.Copy(b, offset, buffer, 0, SIZE);
      }
      public Byte[] buffer { get; set; }

        /*	44	0(X'0')	Character	Data set name.	*/
        public String ds1dsnam
      {
          get
          {
              Byte[] b = new Byte[44];
              DASD_Routines.EBCDIC_to_asciiz(ref b, 44, buffer, 0, 44);
              return new String(' ', 1).ByteArrayToString(44, b);
          }
          set
          {
              Byte[] b = new Byte[44];
              Array.Copy(value.ToByteArray(), 0, b, 0, 44);
              DASD_Routines.make_EBCDIC(ref b, 0, b, 0, 44);
              Array.Copy(b, 0, buffer, 0, 44);
          }
      }
      /*	1	44(X'2C')	Character	Format Identifier.	*/
      public Byte ds1fmtid
      {
          get { return buffer[44]; }
          set { buffer[44] = value; }
      }
      public const Byte DS1IDC = 0xf1;    /*	 	 	 	X'F1'. This is a format-1 DSCB.	*/
      public const Byte DS8IDC = 0xf8;    /*	 	 	 	X'F8'. This is a format-8 DSCB.	*/
      /*	6	45(X'2D')	Character	Data set serial number (identifies the first or only volume containing the data set/space).	*/
      public String ds1dssn
      {
          get
          {
              Byte[] b = new Byte[6];
              DASD_Routines.EBCDIC_to_asciiz(ref b, 6, buffer, 45, 6);
              return new String(' ', 1).ByteArrayToString(6, b);
          }
          set
          {
              Byte[] b = new Byte[6];
              Array.Copy(value.ToByteArray(), 0, b, 0, 6);
              DASD_Routines.make_EBCDIC(ref b, 0, b, 0, 6);
              Array.Copy(b, 0, buffer, 45, 6);
            }
        }
      /*	2	51(X'33')	Unsigned	Volume sequence number.	*/
      public HWORD ds1volsq
      {
          get
          {
              return new HWORD(buffer[51], buffer[52]);
          }
          set
          {
              Array.Copy(value.bytes, 0, buffer, 51, 2);
          }
      }
      /*	3	53(X'35')	Character	Creation date ('YDD'), discontinuous binary. 
          Add 1900 and the value in the Y byte to determine the year. For VSAM data sets that are not SMS-managed, 
          the expiration date is in the catalog.	*/
      public BINYDD ds1credt
      {
          get
          {
              return new BINYDD(buffer, 53);
          }
          set
          {
              Array.Copy(value.bytes, 0, buffer, 53, 3);
          }
      }
      /*	3	56(X'38')	Character	Expiration date ('YDD'), discontinuous binary. Add 1900 and the value in the Y byte to determine the year.	*/
      public BINYDD ds1expdt
      {
          get
          {
              return new BINYDD(buffer, 56);
          }
          set
          {
              Array.Copy(value.bytes, 0, buffer, 56, 3);
          }
      }
      /*	1	59(X'3B')	Unsigned	Number of extents on volume.	*/
      public Byte ds1nmext
      {
          get { return buffer[59]; }
          set { buffer[59] = value; }
      }
      /*	1	60(X'3C')	Unsigned	Number of bytes used in last directory block.	*/
      public Byte ds1nobdb
      {
          get { return buffer[60]; }
          set { buffer[60] = value; }
      }
      /*	1	61(X'3D')	Bitstring	Flags byte	*/
      public Byte ds1flag1
      {
          get { return buffer[61]; }
          set { buffer[61] = value; }
      }
      public const Byte DS1COMPR = 0x80;  /*	 	 	1 . . . . . . .	Compressible format data set (DS1STRP is also 1).	*/
      public const Byte DS1CPOIT = 0x40;  /*	 	 	. 1 . . . . . .	Checkpointed data set.	*/
      public const Byte DS1EXPBY = 0x20;  /*	 	 	. . 1 . . . . .	VSE expiration date specified by retention period (not currently used in z/OS)	*/
      public const Byte DS1RECAL = 0x10;  /*	 	 	. . . 1 . . . .	Data set recalled.	*/
      public const Byte DS1LARGE = 0x08;  /*	 	 	. . . . 1 . . .	Large format data set.	*/
      public const Byte DS1ENCRP = 0x04;  /*	   	   	 . . . . . 1 . . 	 Access method encrypted data set. 	*/
      public const Byte DS1EATTR = 0x03;  /*	 	 	. . . . . .11	"Extended attribute setting as specified on the allocation request.(EATTR=)
                                                      If 0, EATTR has not been specified. For VSAM data sets, the default behavior is equivalent to EATTR=OPT. 
                                                      For non-VSAM data sets, the default behavior is equivalent to EATTR=NO.
                                                      If 1, EATTR=NO has been specified. The data set cannot have extended attributes (format 8 and 9 DSCBs) or optionally reside in EAS.
                                                      If 2, EATTR=OPT has been specified. The data set can have extended attributes and optionally reside in EAS. This is the default behavior for VSAM data sets.
                                                      If 3, Not Used, EATTR treated as not specified."	*/
      /*	13	62(X'3E')	Character	System code.	*/
      public String ds1syscd
      {
          get
          {
              return new String(' ', 1).ByteArrayToString(62, 13, buffer);
          }
          set
          {
              Array.Copy(value.ToByteArray(), 0, buffer, 45, 6);
          }
      }
      /*	3	75(X'4B')	Character	Date last referenced ('YDD' or zero, if not maintained). Add 1900 and the value in the Y byte to determine the year.	*/
      public BINYDD ds1refd
      {
          get
          {
              return new BINYDD(buffer, 75);
          }
          set
          {
              Array.Copy(value.bytes, 0, buffer, 75, 3);
          }
      }
      /*	1	78(X'4E')	Bitstring	System managed storage indicators.	*/
      public Byte ds1smsfg
      {
          get { return buffer[78]; }
          set { buffer[78] = value; }
      }
      public const Byte DS1SMSDS = 0x80;  /*	 	 	1... ....	System managed data set. IEHLIST displays this bit as the letter "S".	*/
      public const Byte DS1SMSUC = 0x40;  /*	 	 	.1.. ....	Uncataloged system managed data set (the VTOC index is an uncataloged system managed data set as are all 
                                                                      temporary data sets on system managed volumes). IEHLIST displays this bit as the letter "U".	*/
      public const Byte DS1REBLK = 0x20;  /*	 	 	..1. ....	System determined the block size and data set can be reblocked (you or the system can reblock the data set). 
                                                                      IEHLIST displays this bit as the letter "R".	*/
      public const Byte DS1CRSDB = 0x10;  /*	 	 	...1 ....	DADSM created original block size and data set has not been opened for output. IEHLIST displays this bit as the letter "B".	*/
      public const Byte DS1PDSE = 0x08;   /*	 	 	.... 1...	Data set is a PDSE or HFS data set (DS1PDSEX is also 1 for HFS). IEHLIST displays this bit as the letter "I" for a PDSE. 
                                                                      IEHLIST displays a "?" when it finds an invalid combination of bits.	*/
      public const Byte DS1STRP = 0x04;   /*	 	 	.... .1..	Sequential extended-format data set. IEHLIST displays this bit as the letter "E". IEHLIST displays a "?" 
                                                                      when it finds an invalid combination of bits.	*/
      public const Byte DS1PDSEX = 0x02;  /*	 	 	.... ..1.	HFS data set (DS1PDSE must also be 1) IEHLIST displays this bit as the letter "H".	*/
      public const Byte DS1DSAE = 0x01;   /*	 	 	.... ...1	Extended attributes exist in the catalog entry.	*/
      /*	3	79(X'4F')	Character	Secondary space extension. Valid only when DS1EXT is on (see offset 94(X'5E')).	*/
      public Byte[] ds1scext
      {
          get
          {
              Byte[] b = new Byte[3];
              Array.Copy(buffer, 79, b, 0, 3);
              return b;
          }
          set
          {
              Array.Copy(value, 0, buffer, 79, 3);
          }
      }

      /*	1	79(X'4F')	Bitstring	Secondary space extension flag byte–only one of the first 4 bits is on.	*/
      public Byte ds1scxtf
      {
          get { return buffer[79]; }
          set { buffer[79] = value; }
      }
      public const Byte DS1SCAVB = 0x80;  /*	 	 	1... ....	If 1, DS1SCXTV is the original block length. If 0, DS1SCXTV is the average record length.	*/
      public const Byte DS1SCMB = 0x40;   /*	 	 	.1.. ....	If 1, DS1SCXTV is in megabytes.	*/
      public const Byte DS1SCKB = 0x20;   /*	 	 	..1. ....	If 1, DS1SCXTV is in kilobytes.	*/
      public const Byte DS1SCUB = 0x10;   /*	 	 	...1 ....	If 1, DS1SCXTV is in bytes.	*/
      public const Byte DS1SCCP1 = 0x08;  /*	 	 	.... 1...	If 1, DS1SCXTV has been compacted by a factor of 256.	*/
      public const Byte DS1SCCP2 = 0x04;  /*	 	 	.... .1..	If 1, DS1SCXTV has been compacted by a factor of 65,536.	*/
      /*	2	80(X'50')	Unsigned	Secondary space extension value for average record length or average block length.	*/
      public HWORD ds1scxtv
      {
          get
          {
              return new HWORD(buffer[80], buffer[81]);
          }
          set
          {
              Array.Copy(value.bytes, 0, buffer, 80, 2);
          }
      }
      /*	2	82(X'52')	Bitstring	Data set organization.	*/
      public Byte ds1dsorg
      {
          get { return buffer[82]; }
          set { buffer[82] = value; }
      }
      /*	 	 	 	First byte of DS1DSORG	*/
      public const Byte DS1DSGIS = 0x80;  /*	 	 	1000 000x	Indexed sequential organization.	*/
      public const Byte DS1DSGPS = 0x40;  /*	 	 	0100 000x	Physical sequential organization.	*/
      public const Byte DS1DSGDA = 0x20;  /*	 	 	0010 000x	Direct organization.	*/
      public const Byte DS1DSGCX = 0x10;  /*	 	 	0001 000x	BTAM or QTAM line group.	*/
                                          /*	 	 	.... xx..	Reserved.	*/
      public const Byte DS1DSGPO = 0x02;  /*	 	 	0000 001x	Partitioned organization.	*/
      public const Byte DS1DSGU = 0x01;   /*	 	 	.... ...1	Unmovable; the data contains location dependent information.	*/
                                          /*	 	 	 	Second byte of DS1DSORG	*/
      public Byte ds1dsorg2
      {
          get { return buffer[83]; }
          set { buffer[83] = value; }
      }
      public const Byte DS1DSGGS = 0x80;  /*	 	 	100x 00xx	Graphics organization.	*/
      public const Byte DS1DSGTX = 0x40;  /*	 	 	010x 00xx	TCAM line group (not supported)	*/
      public const Byte DS1DSGTQ = 0x20;  /*	 	 	001x 00xx	TCAM message queue (not supported).	*/
      public const Byte DS1ACBM = 0x08;   /*	 	 	000x 10xx	VSAM data set/space.	*/
      public const Byte DS1ORGAM = 0x08;  /*	 	 	000x 10xx	VSAM data set/space.	*/
      public const Byte DS1DSGTR = 0x04;  /*	 	 	000x 01xx	TCAM 3705 (not supported).	*/
                                          /*	 	 	...x ..xx	Reserved.	*/
      /*	1	84(X'54')	Character	Record format.	*/
      public Byte ds1recfm
      {
          get { return buffer[84]; }
          set { buffer[84] = value; }
      }
      public const Byte DS1RECFF = 0x80;  /*	 	 	10.. ....	Fixed length.	*/
      public const Byte DS1RECFV = 0x40;  /*	 	 	01.. ....	Variable length.	*/
      public const Byte DS1RECFU = 0xc0;  /*	 	 	11.. ....	Undefined length.	*/
      public const Byte DS1RECFT = 0x20;  /*	 	 	..1. ....	Track overflow. No longer supported by current hardware.	*/
      public const Byte DS1RECFB = 0x10;  /*	 	 	...1 ....	Blocked; cannot occur with undefined.	*/
      public const Byte DS1RECFS = 0x08;  /*	 	 	.... 1...	Fixed length: standard blocks; no truncated blocks or unfilled tracks except possible the last block and track. 
                                                                      Variable length: spanned records.	*/
                                          /*	 	 	.... .00.	No control character.	*/
      public const Byte DS1RECFA = 0x04;  /*	 	 	.... .10.	ISO/ANSI control character.	*/
      public const Byte DS1RECMC = 0x02;  /*	 	 	.... .01.	Machine control character.	*/
                                          /*	 	 	.... .11.	Reserved.	*/
                                          /*	 	 	.... ...x	Reserved.	*/
      /*	1	85(X'55')	Character	Option Code.	*/
      public Byte ds1optcd
      {
          get { return buffer[85]; }
          set { buffer[85] = value; }
      }
      /*		BDAM OPTCD field assignments (applies only if DS1DSGDA is on):			*/
      /*	 	 	1 . . . . . . .	Write validity check.	*/
      /*	 	 	. 1 . . . . . .	Track overflow.	*/
      /*	 	 	. . 1 . . . . .	Extended search.	*/
      /*	 	 	. . . 1 . . . .	Feedback.	*/
      /*	 	 	. . . . 1 . . .	Actual addressing.	*/
      /*	 	 	. . . . . 1 . .	Dynamic buffering.	*/
      /*	 	 	. . . . . . 1 .	Read exclusive.	*/
      /*	 	 	. . . . . . . 1	Relative block addressing.	*/
      /*		ISAM OPTCD field assignments (applies only if DS1DSGIS is on):			*/
      /*	 	 	1 . . . . . . .	Write validity check.	*/
      /*	 	 	. 1 . . . . . .	Accumulate track index entry.	*/
      /*	 	 	. . 1 . . . . .	Master indices.	*/
      /*	 	 	. . . 1 . . . .	Independent overflow area.	*/
      /*	 	 	. . . . 1 . . .	Cylinder overflow area.	*/
      /*	 	 	. . . . . 1 . .	Reserved.	*/
      /*	 	 	. . . . . . 1 .	Delete option.	*/
      /*	 	 	. . . . . . . 1	Reorganization criteria.	*/
      /*		BPAM, BSAM, QSAM OPTCD field assignments (applies only if DS1DSGPO or DS1DSGPS is on):			*/
      /*	 	 	1 . . . . . . .	Write validity check.	*/
      /*	 	 	. 1 . . . . . .	Allow data check (if on printer).	*/
      /*	 	 	. . 1 . . . . .	Chained scheduling.	*/
      /*	 	 	. . . 1 . . . .	VSE/MVS interchange feature on tape.	*/
      /*	 	 	. . . . 1 . . .	Treat EOF as EOV (tape).	*/
      /*	 	 	. . . . . 1 . .	Search direct.	*/
      /*	 	 	. . . . . . 1 .	User label totaling.	*/
      /*	 	 	. . . . . . . 1	Each record contains a table reference character.	*/
      /*	1	85(X'55')	Bitstring	VSAM OPTCD settings.	*/
      public Byte ds1optam
      {
          get { return buffer[85]; }
          set { buffer[85] = value; }
      }
      /*		VSAM OPTCD field assignments (applies only if DS1ORGAM is on):			*/
      /*	 	 	1 . . . . . . .	Reserved.	*/
      public const Byte DS1OPTBC = 0x80;  /*	 	 	.1.. ....	Data set is an integrated catalog facility catalog.	*/
                                          /*	 	 	..xx xxxx	Reserved.	*/
      /*	2	86(X'56')	Binary	Block length (Type F unblocked records), or maximum block size (F blocked, U or V records).	*/
      public HWORD ds1blkl
      {
          get { return new HWORD(buffer[86], buffer[87]); }
          set { Array.Copy(value.bytes, 0, buffer, 86, 2); }
      }
      /*	2	88(X'58')	Binary	Logical record length: Fixed length-record length, Undefined length-zero, 
          Variable unspanned-maximum record length, Variable spanned and < 32757 bytes-maximum record length, 
          Variable spanned and > 32756 bytes-X'8000'.	*/
      public HWORD ds1lrecl
      {
          get { return new HWORD(buffer[88], buffer[89]); }
          set { Array.Copy(value.bytes, 0, buffer, 88, 2); }
      }
      /*	1	90(X'5A')	Binary	Key length ( 0 to 255).	*/
      public Byte ds1keyl
      {
          get { return buffer[90]; }
          set { buffer[90] = value; }
      }
      /*	2	91(X'5B')	Binary	Relative key position.	*/
      public HWORD ds1rkp
      {
          get { return new HWORD(buffer[91], buffer[92]); }
          set { Array.Copy(value.bytes, 0, buffer, 91, 2); }
      }
      /*	1	93(X'5D')	Character	Data set indicators.	*/
      public Byte ds1dsind
      {
          get { return buffer[93]; }
          set { buffer[93] = value; }
      }
      public const Byte DS1IND80 = 0x80;  /*	 	 	1... ....	Last volume containing data in this data set.	*/
      public const Byte DS1IND40 = 0x40;  /*	 	 	.1.. ....	Data set is RACF™, a component of the Security Server for z/OS, defined with a discrete profile.	*/
      public const Byte DS1IND20 = 0x20;  /*	 	 	..1. ....	Block length is a multiple of 8 bytes.	*/
      public const Byte DS1IND10 = 0x10;  /*	 	 	...1 ....	Password is required to read or write, or both; see DS1IND04.	*/
      public const Byte DS1IND08 = 0x08;  /*	 	 	.... 1...	Data set has been modified since last recall.	*/
      public const Byte DS1IND04 = 0x04;  /*	 	 	.... .1..	If DS1IND10 is 1 and DS1IND04 is 1, password required to write, but not to read. 
                                                                      If DS1IND10 is 1 and DS1IND04 is 0, password required both to write and to read.	*/
      public const Byte DS1IND02 = 0x02;  /*	 	 	.... ..1.	Data set opened for other than input since last backup copy made.	*/
      public const Byte DS1DSCHA = 0x02;  /*	 	 	 	Same as DS1IND02.	*/
      public const Byte DS1IND01 = 0x01;  /*	 	 	.... ...1	Secure checkpoint data set.	*/
      public const Byte DS1CHKPT = 0x01;  /*	 	 	 	Same as DS1IND01.	*/
      /*	4	94(X'5E')	Binary	Secondary allocation space parameters.	*/
      public Byte[] ds1scalo
      {
          get
          {
              Byte[] b = new Byte[4];
              Array.Copy(buffer, 94, b, 0, 4);
              return b;
          }
          set
          {
              Array.Copy(value, 0, buffer, 94, 4);
          }
      }
      /*	1	94(X'5E')	Character	Flag byte.	*/
      public Byte ds1scal1
      {
          get { return buffer[94]; }
          set { buffer[94] = value; }
      }
      /* public Byte DS1DSPAC;  	 	 	xxxx xxxx	Space request bits, defined as follows:	*/
      public const Byte DS1DSABS = 0x00;  /*	 	 	00.0 ....	Absolute track request.	*/
      public const Byte DS1EXT = 0x10;    /*	 	 	00.1 ....	Extension to secondary space exists (see DS1SCEXT at offset 79 (X'4F')).	*/
      public const Byte DS1CYL = 0xc0;    /*	 	 	11.. ....	Cylinder request.	*/
      public const Byte DS1TRK = 0x80;    /*	 	 	10.. ....	Track request.	*/
      public const Byte DS1AVR = 0x40;    /*	 	 	01.. ...x	Average block length request.	*/
      public const Byte DS1AVRND = 0x41;  /*	 	 	01.. ...1	Average block and round request.	*/
      public const Byte DS1MSGP = 0x20;   /*	 	 	..1. ....	Mass storage vol group (MSVGP - no longer supported).	*/
      public const Byte DS1CONTG = 0x08;  /*	 	 	.... 1...	Contiguous request.	*/
      public const Byte DS1MXIG = 0x04;   /*	 	 	.... .1..	MXIG request.	*/
      public const Byte DS1ALX = 0x02;    /*	 	 	.... ..1.	ALX request.	*/
                                          /*	 	 	.... ...1	Round request.	*/
      /*	3	95(X'5F')	Binary	Secondary allocation quantity.	*/
      public Int32 ds1scal3
      {
          get
          {
              return ((buffer[95] << 16)
                  | (buffer[96] << 8)
                  | (buffer[97]));
          }
          set
          {
              buffer[95] = (Byte)((value >> 16) & 0xff);
              buffer[96] = (Byte)((value >> 8) & 0xff);
              buffer[97] = (Byte)((value) & 0xff);
          }
      }
      /*	3	98(X'62')	Binary	Last used track and block on track (TTR). Not defined for VSAM, PDSE, HFS and direct (BDAM). See bit DS1LARGE at +61 and byte DS1TTTHI at +104.	*/
      public TTR ds1lstar
      {
          get { return new TTR(buffer, 98); }
          set { Array.Copy(value.buffer, 0, buffer, 98, 3); }
      }
      /*	2	101(X'65')	Binary	If not extended format, this is the value from TRKCALC indicating space remaining on last track used. 
          For extended format data sets this is the high order two bytes (TT) of the four-byte last used track number. See DS1LSTAR. Zero for VSAM, PDSE, and HFS.	*/
      public HWORD ds1trbal
      {
          get { return new HWORD(buffer[101], buffer[102]); }
          set { Array.Copy(value.bytes, 0, buffer, 101, 2); }
      }
      /*	1	103(X'67')	Character	Reserved.	*/
      public Byte ds1reserve
      {
          get { return buffer[103]; }
      }
      /*	1	104(X'68')	Character	High order byte of track number in DS1LSTAR. Valid if DS1Large is on.	*/
      public Byte ds1ttthi
      {
          get { return buffer[104]; }
          set { buffer[104] = value; }
      }
      /*	30	105(X'69')	Character	Three extent fields.	*/
      public DSXTENT[] ds1exnts
      {
          get
          {
              DSXTENT[] extents = new DSXTENT[3];
              extents[0] = new DSXTENT(buffer, 105); extents[0].Debug(nameof(extents));
              extents[1] = new DSXTENT(buffer, 115); extents[1].Debug(nameof(extents));
              extents[2] = new DSXTENT(buffer, 125); extents[2].Debug(nameof(extents));
              return extents;
          }
          set
          {
              Array.Copy(value[0].buffer, 0, buffer, 105, DSXTENT.SIZE);
              Array.Copy(value[1].buffer, 0, buffer, 115, DSXTENT.SIZE);
              Array.Copy(value[2].buffer, 0, buffer, 125, DSXTENT.SIZE);
          }
      }
      /*	10	105(X'69')	Character	First extent description.	*/
      public DSXTENT ds1ext1
      {
          get { return new DSXTENT(buffer, 105); }
          set { Array.Copy(value.buffer, 0, buffer, 105, DSXTENT.SIZE); }
      }
      /*	1	 	Character	Extent type indicator.	*/
      /*	 	 	X'81'	Extent on cylinder boundaries.	*/
      /*	 	 	X'80'	Extent described is sharing cylinder (no longer supported).	*/
      /*	 	 	X'40'	First extent describes the user labels and is not counted in DS1NOEPV.	*/
      /*	 	 	X'04'	Index area extent (ISAM).	*/
      /*	 	 	X'02'	Overflow area extent (ISAM).	*/
      /*	 	 	X'01'	User's data block extent, or a prime area extent (ISAM).	*/
      /*	 	 	X'00'	This is not an extent.	*/
      /*	1	 	 	Extent sequence number.	*/
      /*	4	 	 	"Lower limit (CCHH). These are the bit definitions:
                      0–15
                      Low order 16 bits of the 28-bit cylinder number.
                      16-27
                      High order 12 bits of the 28-bit cylinder number. In a format-1 DSCB, these bits always are zero.
                      28-31
                      Track number from 0 to 14. Use the TRKADDR macro or IECTRKAD routine when performing track address calculations."	*/
      /*	4	 	 	Upper limit (CCHH). Same format as the lower limit.	*/
      /*	10	115(X'73')	Character	Second extent description.	*/
      public DSXTENT ds1ext2
      {
          get { return new DSXTENT(buffer, 115); }
          set { Array.Copy(value.buffer, 0, buffer, 115, DSXTENT.SIZE); }
      }
      /*	10	125(X'7D')	Character	Third extent description.	*/
      public DSXTENT ds1ext3
      {
          get { return new DSXTENT(buffer, 125); }
          set { Array.Copy(value.buffer, 0, buffer, 125, DSXTENT.SIZE); }
      }
      /*	5	135(X'87')	Character	In a format-1 DSCB this can be a pointer (CCHHR) to a format-2 or format-3 DSCB or be zero. 
          In a format-8 DSCB this always is the CCHHR of a format-9 DSCB.	*/
      public CCHHR ds1ptrds
      {
          get { return new CCHHR(buffer, 135); }
          set { Array.Copy(value.buffer, 0, buffer, 135, 5); }
      }

  };
    public class FORMAT4_DSCB : extObject
  {   /* DSCB4: VTOC descriptor    */
      public static int SIZE = 140;
      public FORMAT4_DSCB()
      {
          buffer = new Byte[SIZE];
      }
      public Byte[] buffer { get; set; }

        /* Key (44 Bytes of 0x04)    */
        public Byte[] ds4keyid
      {
          get
          {
              Byte[] b = new Byte[44];
              Array.Copy(buffer, 0, b, 0, 44);
              return b;
          }
      }
      /* Format identifier (0xF4)  */
      public Byte ds4fmtid
      {
          get
          {
              return buffer[44];
          }
      }
      /* CCHHR of highest F1 DSCB  */
      public Byte[] ds4hpchr
      {
          get
          {
              Byte[] b = new Byte[5];
              Array.Copy(buffer, 45, b, 0, 5);
              return b;
          }
          set
          {
              Array.Copy(value, 0, buffer, 45, 5);
          }
      }
      /* Number of format 0 DSCBs  */
      public HWORD ds4dsrec
      {
          get
          {
              return new HWORD(buffer[50], buffer[51]);
          }
          set
          {
              buffer[50] = value.bytes[0];
              buffer[51] = value.bytes[1];
          }
      }
      /* CCHH of next avail alt trk*/
      public Byte[] ds4hcchh
      {
          get
          {
              Byte[] b = new Byte[4];
              Array.Copy(buffer, 52, b, 0, 4);
              return b;
          }
          set
          {
              Array.Copy(value, 0, buffer, 52, 4);
          }
      }
      /* Number of avail alt tracks*/
      public HWORD ds4noatk
      {
          get
          {
              return new HWORD(buffer[56], buffer[57]);
          }
          set
          {
              buffer[56] = value.bytes[0];
              buffer[57] = value.bytes[1];
          }
      }
      /* VTOC indicators           */
      public Byte ds4vtoci
      {
          get
          {
              return buffer[58];
          }
          set
          {
              buffer[58] = value;
          }
      }
      public const Byte DS4DOSBT = 0x80; /* 1... .... VSE bit.Either invalid format 5 DSCBs or indexed VTOC. 
                                            Previously DOS(VSE) bit. See DS4IVTOC. */
      public const Byte DS4DVTOC = 0x40; /* .1.. .... Index was disabled. */
      public const Byte DS4EFVLD = 0x20; /* ..1. .... Extended free-space management flag.When DS4EFVLD is on, 
                                            the volume is in OSVTOC format with valid free space information in the format-7 
                                            DSCBs.See also DS4EFLVL and DS4EFPTR. */
      public const Byte DS4DSTKP = 0x10; /* ...1 .... VSE stacked pack. */
      public const Byte DS4DOCVT = 0x08; /* .... 1... VSE converted VTOC. */
      public const Byte DS4DIRF = 0x04;  /* .... .1.. DIRF bit. A VTOC change is incomplete. */
      public const Byte DS4DICVT = 0x02; /* .... ..1. DIRF reclaimed. */
      public const Byte DS4IVTOC = 0x01; /* .... ...1 Volume uses an indexed VTOC. */
      /* Number of extents in VTOC */
      public Byte ds4noext
      {
          get
          {
              return buffer[59];
          }
          set
          {
              buffer[59] = value;
          }
      }
      /* System managed storage indicators. */
      public Byte DS4SMSFG
      {
          get { return buffer[60]; }
          set { buffer[60] = value; }
      }
      /* Number of alternate cylinders when the volume was formatted. 
          Subtract from first 2 bytes of DS4DEVSZ to get number of useable 
          cylinders (can be 0). Valid only if DS4DEVAV is on. */
      public Byte DS4DEVAC
      {
          get { return buffer[61]; }
          set { buffer[61] = value; }
      }
      /* Device size (CCHH)        */
      public FWORD ds4devsz
      {
          get
          {
              FWORD wd = new FWORD();
              Array.Copy(buffer, 62, wd.bytes, 0, wd.SIZE);
              return wd;
          }
          set
          {
              Array.Copy(value.bytes, 0, buffer, 62, value.SIZE);
          }
      }
      /* Device track length       */
      public HWORD ds4devtk
      {
          get
          {
              return new HWORD(buffer[66], buffer[67]);
          }
          set
          {
              buffer[66] = value.bytes[0];
              buffer[67] = value.bytes[1];
          }
      }
      /* Non-last keyed blk overhd */
      public Byte ds4devi
      {
          get
          {
              return buffer[68];
          }
          set
          {
              buffer[68] = value;
          }
      }
      /* Last keyed block overhead */
      public Byte ds4devl
      {
          get
          {
              return buffer[69];
          }
          set
          {
              buffer[69] = value;
          }
      }
      /* Non-keyed block difference*/
      public Byte ds4devk
      {
          get
          {
              return buffer[70];
          }
          set
          {
              buffer[70] = value;
          }
      }
      /* Device flags              */
      public Byte ds4devfg
      {
          get
          {
              return buffer[71];
          }
          set
          {
              buffer[71] = value;
          }
      }
      /* Device tolerance          */
      public HWORD ds4devtl
      {
          get
          {
              return new HWORD(buffer[72], buffer[73]);
          }
          set
          {
              buffer[72] = value.bytes[0];
              buffer[73] = value.bytes[1];
          }
      }
      /* Number of DSCBs per track */
      public Byte ds4devdt
      {
          get
          {
              return buffer[74];
          }
          set
          {
              buffer[74] = value;
          }
      }
      /* Number of dirblks/track   */
      public Byte ds4devdb
      {
          get
          {
              return buffer[75];
          }
          set
          {
              buffer[75] = value;
          }
      }
      /* VSAM timestamp            */
      public DBLWRD ds4amtim
      {
          get
          {
              DBLWRD wd = new DBLWRD();
              Array.Copy(buffer, 76, wd.bytes, 0, wd.SIZE);
              return wd;
          }
          set
          {
              Array.Copy(value.bytes, 0, buffer, 76, value.SIZE);
          }
      }
      /* VSAM indicators           */
      public Byte ds4vsind
      {
          get
          {
              return buffer[84];
          }
          set
          {
              buffer[84] = value;
          }
      }
      /* CRA track location        */
      public HWORD ds4vscra
      {
          get
          {
              return new HWORD(buffer[85], buffer[86]);
          }
          set
          {
              buffer[85] = value.bytes[0];
              buffer[86] = value.bytes[1];
          }
      }
      /* VSAM vol/cat timestamp    */
      public DBLWRD ds4r2tim
      {
          get
          {
              DBLWRD wd = new DBLWRD();
              Array.Copy(buffer, 87, wd.bytes, 0, wd.SIZE);
              return wd;
          }
          set
          {
              Array.Copy(value.bytes, 0, buffer, 87, value.SIZE);
          }
      }
      /* Reserved                  */
      public Byte[] resv1
      {
          get
          {
              Byte[] b = new Byte[5];
              Array.Copy(b, 0, buffer, 95, 5);
              return b;
          }
      }
      /* CCHHR of first F6 DSCB    */
      public Byte[] ds4f6ptr
      {
          get
          {
              Byte[] b = new Byte[5];
              Array.Copy(buffer, 100, b, 0, 5);
              return b;
          }
          set
          {
              Array.Copy(value, 0, buffer, 100, 5);
          }
      }
      /* VTOC extent descriptor    */
      public VTOCXTENT ds4vtoce
      {
          get
          {
              VTOCXTENT dx = new VTOCXTENT();
              Array.Copy(buffer, 105, dx.buffer, 0, 10); dx.Debug(nameof(dx));
              return dx;
          }
          set
          {
              Array.Copy(value.buffer, 0, buffer, 105, 10);
          }
      }
      /* Reserved                  */
      public Byte[] resv2
      {
          get
          {
              Byte[] b = new Byte[10];
              Array.Copy(buffer, 115, b, 0, 10);
              return b;
          }
      }
      /* Extended free-space management level. X'00' indicates extended free-space management
          is not used for this volume. X'07' indicates extended free-space management is in 
          use for this volume (see also DS4EFVLD). */
      public Byte ds4eflvl
      {
          get
          {
              return buffer[125];
          }
          set
          {
              buffer[125] = value;
          }
      }
      /* Pointer to extended free-space information. If DS4EFLVL=X'00' this is zero. 
          If DS4EFLVL=X'07' this is the CCHHR of the first FMT-7 DSCB and no format-5 
          DSCBs contain free space information. */
      public Byte[] ds4efptr
      {
          get
          {
              Byte[] b = new Byte[5];
              Array.Copy(buffer, 126, b, 0, 5);
              return b;
          }
          set
          {
              Array.Copy(value, 0, buffer, 126, 5);
          }
      }
      /* Minimum allocation size in cylinders for cylinder-managed space. Each extent in 
          this space must be a multiple of this value. */
      public Byte ds4mcu
      {
          get
          {
              return buffer[131];
          }
          set
          {
              buffer[131] = value;
          }
      }
      /* Number of logical cylinders. Valid when DS4DSCYL= X'FFFE'. */
      public Byte[] ds4dcyl
      {
          get
          {
              Byte[] b = new Byte[4];
              Array.Copy(buffer, 132, b, 0, 4);
              return b;
          }
          set
          {
              Array.Copy(value, 0, buffer, 132, 4);
          }
      }
      /* First cylinder address/4095 where space is managed in multicylinder units. 
          Cylinder-managed space begins at this address. Valid when DS4CYLMG is set. */
      public Byte[] ds4lcyl
      {
          get
          {
              Byte[] b = new Byte[2];
              Array.Copy(buffer, 136, b, 0, 2);
              return b;
          }
          set
          {
              Array.Copy(value, 0, buffer, 136, 2);
          }
      }
      /* Device Flags Byte 2 */
      public Byte ds4devf2
      {
          get
          {
              return buffer[138];
          }
          set
          {
              buffer[138] = value;
          }
      }
      public const Byte DS4CYLMG = 0x80; /* 1... .... Cylinder-managed space exists on this volume and begins at 
                                            DS4LCYL in multicylinder units of DS4MCU.DS4EADSCB is also set when this flag is on. */
      public const Byte DS4EADSCB = 0x40; /* .1.. .... Extended attribute DSCBs, Format 8 and 9 DSCBs, are allowed on this volume. */
      /* Reserved                  */
      public Byte[] resv3
      {
          get
          {
              Byte[] b = new Byte[1];
              Array.Copy(buffer, 139, b, 0, 1);
              return b;
          }
      }
  };
    public class VOL1_LABEL : extObject
    {
        public int SIZE { get; } = 80;
        public VOL1_LABEL()
        {
            buffer = new Byte[SIZE];
        }
        public VOL1_LABEL(Byte[] b, Int32 offset)
        {
            buffer = new Byte[SIZE];
            Array.Copy(b, offset, buffer, 0, SIZE);
        }
        public Byte[] buffer { get; set; }

        public String vol1id
        {
            get
            {
                Byte[] b = new Byte[3];
                DASD_Routines.EBCDIC_to_asciiz(ref b, 3, buffer, 0, 3);
                return new String(' ', 1).ByteArrayToString(b);
            }
        }
        public Byte vol1fmtid
        {
            get
            {
                return buffer[3];
            }
        }
        public String vol1ser
        {
            get
            {
                Byte[] b = new Byte[6];
                DASD_Routines.EBCDIC_to_asciiz(ref b, 6, buffer, 4, 6);
                return new String(' ', 1).ByteArrayToString(b);
            }
        }
        public Byte[] vol1resv1
        {
            get
            {
                Byte[] b = new Byte[1];
                Array.Copy(buffer, 10, b, 0, 1);
                return b;
            }
        }
        public CCHHR vol1vtocp
        {
            get
            {
                return new CCHHR(buffer, 11);
            }
        }
        public Byte[] vol1resv2
        {
            get
            {
                Byte[] b = new Byte[21];
                Array.Copy(buffer, 16, b, 0, 21);
                return b;
            }
        }
        public String vol1owner
        {
            get
            {
                Byte[] b = new Byte[14];
                DASD_Routines.EBCDIC_to_asciiz(ref b, 10, buffer, 37, 10);
                return new String(' ', 1).ByteArrayToString(b);
            }
        }
        public Byte[] vol1resv3
        {
            get
            {
                Byte[] b = new Byte[29];
                Array.Copy(buffer, 51, b, 0, 29);
                return b;
            }
        }
    }
    /*-------------------------------------------------------------------*/
    /* Internal structures used by DASD utility functions                */
    /*-------------------------------------------------------------------*/
    public class CKD_ImageFileDescriptor : extObject
    {
        public Byte[] fname;
        public Int32 fd;
        public UInt32 trksz;
        public Byte[] trkbuf;
        public UInt32 curcyl;
        public UInt32 curhead;
        public Int32 trkmodif;
        public UInt32 heads;
        public DEVBLK devblk;
        public CKD_ImageFileDescriptor()
        {
            fname = new Byte[44];
            trkbuf = new Byte[5];
        }
    }
    public class LVL1TAB : extObject
    {
        internal Int32[] _offsets;
        public LVL1TAB(Int32 size)
        {
            buffer = new Byte[size * 4];
            _offsets = new Int32[size];
        }
        public Byte[] buffer { get; set; }

        public Int32[] offsets
        {
            get
            {
                int i = buffer.Length / 4;

                for (int j = 0; j < i; j++)
                {
                    int k = j * 4;
                    _offsets[j] = ((buffer[k + 3] << 24)
                        | (buffer[k + 2] << 16)
                        | (buffer[k + 1] << 8)
                        | (buffer[k]));
                }

                return _offsets;
            }
        }

        public Int32 SIZE
        {
            get { return buffer.Length; }
        }
    }
    public class LVL2ENTRY : extObject
    {
        public Int32 offset;
        public Int16 length;
        public Int16 size;
    }
    public class LVL2TAB : extObject
    {
        internal LVL2ENTRY[] _entries;
        public LVL2TAB(Int32 size)
        {
            buffer = new Byte[size * 8];
            _entries = new LVL2ENTRY[size];
        }
        public Byte[] buffer { get; set; }

        public LVL2ENTRY[] entries
        {
            get
            {
                int i = buffer.Length / 8;

                for (int j = 0; j < i; j++)
                {
                    _entries[j] = new LVL2ENTRY();
                    int k = j * 8;
                    _entries[j].offset = (Int32)((buffer[k + 3] << 24)
                        | (buffer[k + 2] << 16)
                        | (buffer[k + 1] << 8)
                        | (buffer[k]));
                    _entries[j].length = (Int16)((buffer[k + 5] << 8)
                        | (buffer[k + 4]));
                    _entries[j].size = (Int16)((buffer[k + 7] << 8)
                        | (buffer[k + 6]));
                }

                return _entries;
            }
        }

        public Int32 SIZE
        {
            get { return buffer.Length; }
        }
    }
    public class CCHHR : extObject
    {
        public CCHHR()
        {
            buffer = new Byte[5];
        }
        public CCHHR(Byte[] b, Int32 start)
        {
            buffer = new Byte[5];
            Array.Copy(b, start, buffer, 0, 5);
        }
        public Byte[] buffer { get; set; }

        /* Cylinder number           */
        public HWORD CC
        {
            get
            {
                return new HWORD(buffer[0], buffer[1]);
            }
            set
            {
                buffer[0] = value.bytes[0];
                buffer[1] = value.bytes[1];
            }
        }
        /* Head number               */
        public HWORD HH
        {
            get
            {
                return new HWORD(buffer[2], buffer[3]);
            }
            set
            {
                buffer[2] = value.bytes[0];
                buffer[3] = value.bytes[1];
            }
        }
        /* Record number             */
        public Byte R
        {
            get
            {
                return buffer[4];
            }
            set
            {
                buffer[4] = value;
            }
        }
    }
    public class TTR : extObject
    {
        public TTR()
        {
            buffer = new Byte[3];
        }
        public TTR(Byte[] b, Int32 start)
        {
            buffer = new Byte[3];
            Array.Copy(b, start, buffer, 0, 3);
        }
        public Byte[] buffer { get; set; }

        /* Track number           */
        public HWORD TT
        {
            get
            {
                return new HWORD(buffer[0], buffer[1]);
            }
            set
            {
                buffer[0] = value.bytes[0];
                buffer[1] = value.bytes[1];
            }
        }
        /* Record number             */
        public Byte R
        {
            get
            {
                return buffer[2];
            }
            set
            {
                buffer[2] = value;
            }
        }
    }
    public class CKDDASD_RECHDR : extObject
    {   /* Record header             */
        public static int SIZE = 8;
        public CKDDASD_RECHDR()
        {
            buffer = new Byte[SIZE];
        }
        public CKDDASD_RECHDR(Byte[] b, Int32 offset)
        {
            buffer = new Byte[SIZE];
            Array.Copy(b, offset, buffer, 0, 8);
        }
        public Byte[] buffer { get; set; }

        /* Cylinder number           */
        public HWORD CC
        {
            get
            {
                return new HWORD(buffer[0], buffer[1]);
            }
            set
            {
                buffer[0] = value.bytes[0];
                buffer[1] = value.bytes[1];
            }
        }
        /* Head number               */
        public HWORD HH
        {
            get
            {
                return new HWORD(buffer[2], buffer[3]);
            }
            set
            {
                buffer[2] = value.bytes[0];
                buffer[3] = value.bytes[1];
            }
        }
        /* Record number             */
        public Byte R
        {
            get
            {
                return buffer[4];
            }
            set
            {
                buffer[4] = value;
            }
        }
        /* Key length                */
        public Byte K
        {
            get
            {
                return buffer[5];
            }
            set
            {
                buffer[5] = value;
            }
        }
        /* Data length               */
        public HWORD LL
        {
            get
            {
                return new HWORD(buffer[6], buffer[7]);
            }
            set
            {
                buffer[6] = value.bytes[0];
                buffer[7] = value.bytes[1];
            }
        }
    };
    public class CKD_GROUP : extObject
    {
        public CKD_GROUP(Int32 size)
        {
            buffer = new Byte[size];
        }
        public Byte[] buffer { get; set; }

        public Byte comp
        {
            get
            {
                return buffer[0];
            }
            set
            {
                buffer[0] = value;
            }
        }
        public HWORD CC
        {
            get
            {
                return new HWORD(buffer[1], buffer[2]);
            }
            set
            {
                buffer[1] = value.bytes[0];
                buffer[2] = value.bytes[1];
            }
        }
        public HWORD HH
        {
            get
            {
                return new HWORD(buffer[3], buffer[4]);
            }
            set
            {
                buffer[3] = value.bytes[0];
                buffer[4] = value.bytes[1];
            }
        }
        public Byte[] data
        {
            get
            {
                Byte[] _data = new Byte[buffer.Length - 5];
                for (int i = 0; i < _data.Length; i++)
                {
                    _data[i] = buffer[i + 5];
                }
                return _data;
            }
            set
            {
                for (int i = 0; i < (buffer.Length - 5); i++)
                {
                    buffer[i + 5] = value[i];
                }
            }
        }
    }
    /*-------------------------------------------------------------------*/
    /* Device Header                                                     */
    /*-------------------------------------------------------------------*/
    public class DASD_DEVHDR : extObject
    {   /* Device header             */
        int _resvLen = 492;
        public static int SIZE = 512;
        public DASD_DEVHDR()
        {
            buffer = new Byte[SIZE];
        }
        public Byte[] buffer { get; set; }

        /* Device identifier              */
        public String devid
        {
            get
            {
                return new string(' ', 1).ByteArrayToString(0, 8, buffer);
            }
            set
            {
                Array.Copy(value.PadRight(8, ' ').Substring(0, 8).ToByteArray(), 0, buffer, 0, 8);
            }
        }
        /* #of heads per cylinder (Bytes in reverse order)       */
        public FWORD heads
        {
            get
            {
                FWORD fw = new FWORD();
                Array.Copy(buffer, 8, fw.bytes, 0, fw.SIZE);
                return fw;
            }
            set
            {
                Array.Copy(value.bytes, 0, buffer, 8, value.SIZE);
            }
        }
        /* Track size (reverse order)     */
        public FWORD trksize
        {
            get
            {
                FWORD fw = new FWORD();
                Array.Copy(buffer, 12, fw.bytes, 0, fw.SIZE);
               return fw;
            }
            set
            {
                Array.Copy(value.bytes, 0, buffer, 12, value.SIZE);
             }
        }
        /* Last 2 digits of device type (0x80=3380, 0x90=3390)         */
        public Byte devtype
        {
            get { return buffer[16]; }
            set { buffer[16] = value; }
        }
        /* CKD image file sequence no. (0x00=only file, 0x01=first
           file of multiple files)        */
        public Byte fileseq
        {
            get { return buffer[17]; }
            set { buffer[17] = value; }
        }
        /* Highest cylinder number on this file, or zero if this
           is the last or only file (Bytes in reverse order)       */
        public HWORD highcyl
        {
            get
            {
                return new HWORD(buffer[18], buffer[19]);
            }
            set
            {
                Array.Copy(value.bytes, 0, buffer, 18, value.SIZE);
            }
        }
        /* Reserved                       */
        public Byte[] resv
        {
            get
            {
                Byte[] ret = new Byte[_resvLen];

                for (int i = 0; i < _resvLen; i++)
                {
                    ret[i] = buffer[i + 20];
                }
                return ret;
            }
        }
    };
    /*-------------------------------------------------------------------*/
    /* Compressed Device Header                                          */
    /*-------------------------------------------------------------------*/
    public class DASD_COMP_DEVHDR : extObject
    {   /* Device header             */
        int _resvLen = 464;
        public static int SIZE = 512;
        public DASD_COMP_DEVHDR()
        {
            buffer = new Byte[SIZE];
        }
        public Byte[] buffer { get; set; }

        /* Version Release Modifier  */
        public Byte[] vrm
        {
            get
            {
                Byte[] b = new byte[3];

                for (int i = 0; i < 3; i++)
                {
                    b[i] = buffer[i];
                }
                return b;
            }
        }
        /* Options byte              */
        public Byte options
        {
            get
            {
                return buffer[3];
            }
            set
            {
                buffer[3] = value;
            }
        }
        /* Size of lvl 1 table       */
        public Int32 numl1tab
        {
            get
            {
                return ((buffer[7] << 24)
                    | (buffer[6] << 16)
                    | (buffer[5] << 8)
                    | (buffer[4]));
            }
            set
            {
                buffer[7] = (Byte)((value >> 24) & 0xff);
                buffer[6] = (Byte)((value >> 16) & 0xff);
                buffer[5] = (Byte)((value >> 8) & 0xff);
                buffer[4] = (Byte)((value) & 0xff);
            }
        }
        /* Size of lvl 2 tables      */
        public Int32 numl2tab
        {
            get
            {
                return ((buffer[11] << 24)
                    | (buffer[10] << 16)
                    | (buffer[9] << 8)
                    | (buffer[8]));
            }
            set
            {
                buffer[11] = (Byte)((value >> 24) & 0xff);
                buffer[10] = (Byte)((value >> 16) & 0xff);
                buffer[9] = (Byte)((value >> 8) & 0xff);
                buffer[8] = (Byte)((value) & 0xff);
            }
        }
        /* File size                 */
        public Int32 size
        {
            get
            {
                return ((buffer[15] << 24)
                    | (buffer[14] << 16)
                    | (buffer[13] << 8)
                    | (buffer[12]));
            }
            set
            {
                buffer[15] = (Byte)((value >> 24) & 0xff);
                buffer[14] = (Byte)((value >> 16) & 0xff);
                buffer[13] = (Byte)((value >> 8) & 0xff);
                buffer[12] = (Byte)((value) & 0xff);
            }
        }
        /* File used                 */
        public Int32 used
        {
            get
            {
                return ((buffer[19] << 24)
                    | (buffer[18] << 16)
                    | (buffer[17] << 8)
                    | (buffer[16]));
            }
            set
            {
                buffer[19] = (Byte)((value >> 24) & 0xff);
                buffer[18] = (Byte)((value >> 16) & 0xff);
                buffer[17] = (Byte)((value >> 8) & 0xff);
                buffer[16] = (Byte)((value) & 0xff);
            }
        }
        /* Offset to free space      */
        public Int32 free
        {
            get
            {
                return ((buffer[23] << 24)
                    | (buffer[22] << 16)
                    | (buffer[21] << 8)
                    | (buffer[20]));
            }
            set
            {
                buffer[23] = (Byte)((value >> 24) & 0xff);
                buffer[22] = (Byte)((value >> 16) & 0xff);
                buffer[21] = (Byte)((value >> 8) & 0xff);
                buffer[20] = (Byte)((value) & 0xff);
            }
        }
        /* Total free space          */
        public Int32 free_total
        {
            get
            {
                return ((buffer[27] << 24)
                    | (buffer[26] << 16)
                    | (buffer[25] << 8)
                    | (buffer[24]));
            }
            set
            {
                buffer[27] = (Byte)((value >> 24) & 0xff);
                buffer[26] = (Byte)((value >> 16) & 0xff);
                buffer[25] = (Byte)((value >> 8) & 0xff);
                buffer[24] = (Byte)((value) & 0xff);
            }
        }
        /* Largest free space        */
        public Int32 free_largest
        {
            get
            {
                return ((buffer[31] << 24)
                    | (buffer[30] << 16)
                    | (buffer[29] << 8)
                    | (buffer[28]));
            }
            set
            {
                buffer[31] = (Byte)((value >> 24) & 0xff);
                buffer[30] = (Byte)((value >> 16) & 0xff);
                buffer[29] = (Byte)((value >> 8) & 0xff);
                buffer[28] = (Byte)((value) & 0xff);
            }
        }
        /* Number free spaces        */
        public Int32 free_number
        {
            get
            {
                return ((buffer[35] << 24)
                    | (buffer[34] << 16)
                    | (buffer[33] << 8)
                    | (buffer[32]));
            }
            set
            {
                buffer[35] = (Byte)((value >> 24) & 0xff);
                buffer[34] = (Byte)((value >> 16) & 0xff);
                buffer[33] = (Byte)((value >> 8) & 0xff);
                buffer[32] = (Byte)((value) & 0xff);
            }
        }
        /* Imbedded free space       */
        public Int32 free_imbed
        {
            get
            {
                return ((buffer[39] << 24)
                    | (buffer[38] << 16)
                    | (buffer[37] << 8)
                    | (buffer[36]));
            }
            set
            {
                buffer[39] = (Byte)((value >> 24) & 0xff);
                buffer[38] = (Byte)((value >> 16) & 0xff);
                buffer[37] = (Byte)((value >> 8) & 0xff);
                buffer[36] = (Byte)((value) & 0xff);
            }
        }
        /* Cylinders on device       */
        public FWORD cyls
        {
            get
            {
                FWORD wd = new FWORD(buffer, 40);
                return wd;
            }
            set
            {
                Array.Copy(value.bytes, 0, buffer, 40, value.SIZE);
            }
        }
        /* Null track format         */
        public Byte nullfmt
        {
            get
            {
                return buffer[44];
            }
            set
            {
                buffer[44] = value;
            }
        }
        /* Compression algorithm     */
        public Byte compress
        {
            get
            {
                return buffer[45];
            }
            set
            {
                buffer[45] = value;
            }
        }
        /* Compression parameter     */
        public Int16 compress_parm
        {
            get
            {
                return (Int16)((buffer[47] << 8)
                    | (buffer[46]));
            }
            set
            {
                buffer[47] = (Byte)((value >> 8) & 0xff);
                buffer[46] = (Byte)((value) & 0xff);
            }
        }
        /* Reserved                  */
        public Byte[] resv
        {
            get
            {
                Byte[] ret = new Byte[_resvLen];

                for (int i = 0; i < _resvLen; i++)
                {
                    ret[i] = buffer[i + 48];
                }
                return ret;
            }
        }
    };
    /*-------------------------------------------------------------------*/
    /* Definition of a FBA DASD device entry                             */
    /*-------------------------------------------------------------------*/
    public class FBADEV : extObject
    {   /* FBA Device table entry    */
        public String _name;                 /* Device name               */
        public UInt16 _devt;                 /* Device type               */
        public Byte _devclass;               /* Device class              */
        public Byte _type;                   /* Device code               */
        public Byte _model;                  /* Device model              */
        public UInt16 _bpg;                  /* Number primary cylinders  */
        public UInt16 _bpp;                  /* Number alternate cylinders*/
        public UInt16 _size;                 /* Number heads (trks/cyl)   */
        public UInt32 _blks;                 /* R0 max size               */
        public String _cu;                   /* Default control unit name */
        public FBADEV()
        {
        }
        public FBADEV(String name, UInt16 devt, Byte clas, Byte type, Byte model, UInt16 bpg, UInt16 bpp,
            UInt16 size, UInt32 blks, String cu)
        {
            _name = name;
            _devt = devt;
            _model = model;
            _devclass = clas;
            _type = type;
            _model = model;
            _bpg = bpg;
            _bpp = bpp;
            _size = size;
            _blks = blks;
            _cu = cu;
        }
    };
    /*-------------------------------------------------------------------*/
    /* Definition of a CKD DASD device entry                             */
    /*-------------------------------------------------------------------*/
    public class CKDDEV : extObject
    {   /* CKD Device table entry    */
        public String _name;                 /* Device name               */
        public UInt16 _devt;                 /* Device type               */
        public Byte _model;                  /* Device model              */
        public Byte _devclass;               /* Device class              */
        public Byte _code;                   /* Device code               */
        public UInt16 _cyls;                 /* Number primary cylinders  */
        public UInt16 _altcyls;              /* Number alternate cylinders*/
        public UInt16 _heads;                /* Number heads (trks/cyl)   */
        public UInt16 _r0;                   /* R0 max size               */
        public UInt16 _r1;                   /* R1 max size               */
        public UInt16 _har0;                 /* HA/R0 overhead size       */
        public UInt16 _len;                  /* Max length                */
        public UInt16 _sectors;              /* Number sectors            */
        public UInt16 _rpscalc;              /* RPS calculation factor    */
        public Int16 _formula;               /* Space calculation formula */
        public UInt16 _f1;                   /* Space calculation factors */
        public UInt16 _f2;
        public UInt16 _f3;
        public UInt16 _f4;
        public UInt16 _f5;
        public UInt16 _f6;
        public String _cu;                   /* Default control unit name */
        public CKDDEV()
        {
        }
        public CKDDEV(String name, UInt16 type, Byte model, Byte clas, Byte code, UInt16 prime, UInt16 a,
            UInt16 hd, UInt16 r0, UInt16 r1, UInt16 har0, UInt16 len, UInt16 sec, UInt16 rps, Int16 f,
            UInt16 f1, UInt16 f2, UInt16 f3, UInt16 f4, UInt16 f5, UInt16 f6, String cu)
        {
            _name = name;
            _devt = type;
            _model = model;
            _devclass = clas;
            _code = code;
            _cyls = prime;
            _altcyls = a;
            _heads = hd;
            _r0 = r0;
            _r1 = r1;
            _har0 = har0;
            _len = len;
            _sectors = sec;
            _rpscalc = rps;
            _formula = f;
            _f1 = f1;
            _f2 = f2;
            _f3 = f3;
            _f4 = f4;
            _f5 = f5;
            _f6 = f6;
            _cu = cu;
        }
    };
    public class DEVHND : extObject
    {
        private MethodInfo _Init;
        private MethodInfo _Read;
        private MethodInfo _Write;
        private MethodInfo _Start;

        public DEVHND()
        {
            DASD_Routines dasd = new DASD_Routines();
            _Init = dasd.GetType().GetMethod("Init");
            _Read = dasd.GetType().GetMethod("Read");
            _Write = dasd.GetType().GetMethod("Write");
            _Start = dasd.GetType().GetMethod("Start");
        }

        public DEVHND(MethodInfo init, MethodInfo read, MethodInfo write, MethodInfo start)
        {
            _Init = init;
            _Read = read;
            _Write = write;
            _Start = start;
        }

        ///* Device Init                */
        public Int32 Init(ref DEVBLK dev)
        {
            object[] passParms = new object[1];
            passParms[0] = dev;
            return (Int32)_Init.Invoke(this, passParms);
        }
        ///* Device Read                */
        public Int32 Read(ref DEVBLK dev, Int32 track, ref Byte unitstat)
        {
            object[] passParms = new object[3];
            passParms[0] = dev;
            passParms[1] = track;
            passParms[2] = unitstat;
            return (Int32)_Read.Invoke(this, passParms);
        }
        ///* Device Write               */
        public Int32 Write(DEVBLK dev, Int32 rcd, Int32 off, Byte[] buf, Int32 len, ref Byte unitstat)
        {
            object[] passParms = new object[6];
            passParms[0] = dev;
            passParms[1] = rcd;
            passParms[2] = off;
            passParms[3] = buf;
            passParms[4] = len;
            passParms[5] = unitstat;
            return (Int32)_Write.Invoke(this, passParms);
        }
        ///* Device Start channel pgm   */
        public Boolean Start(DEVBLK dev)
        {
            object[] passParms = new object[1];
            passParms[0] = dev;
            return (Boolean)_Start.Invoke(this, passParms);
        }

        public void SetInit(MethodInfo init)
        {
            _Init = init;
        }
        public void SetRead(MethodInfo read)
        {
            _Read = read;
        }
        public void SetWrite(MethodInfo write)
        {
            _Write = write;
        }
        public void SetStart(MethodInfo start)
        {
            _Start = start;
        }
    };
    public class DASDEntry : extObject
    {
        public String pathname;
        public String filename;
        public String structureName;
        public Int32 offset;
        public Int32 size;

        public DASDEntry()
        {
        }

        public DASDEntry(String fname)
        {
            filename = fname;
        }

        public DASDEntry(String fname, String sname, String pname = null)
        {
            pathname = pname;
            filename = fname;
            structureName = sname;
        }

        public DASDEntry(String fname, String sname, Int32 o, Int32 s, String pname = null)
        {
            pathname = pname;
            filename = fname;
            structureName = sname;
            offset = o;
            size = s;
        }
    }
    public class MemberEntry : extObject
    {
        public String member;
        public TTR TTR;
        public Byte C;
        public Byte[] userData;
        public DSNEntry dEntry;
        public MemberEntry()
        {
            member = null;
            TTR = null;
            userData = null;
            dEntry = null;
        }
    }
    public class VolumeEntry : extObject
    {
        public String volume;
        public String device;
        public String pathname;
        public String filename;
        public String shadowfile;
        public String creationDate;
        public VolumeEntry()
        {
            volume = null;
            device = null;
            pathname = null;
            filename = null;
            shadowfile = null;
            creationDate = null;
        }
    }
    public class DSNEntry : extObject
    {
        public FORMAT1_DSCB DSCB;
        public String pathname;
        public String filename;
        public DEVBLK dev;

        public String DSN
        {
            get
            {
                return DSCB.ds1dsnam;
            }
        }

        public CKDDEV ckd
        {
            get
            {
                return dev.ckddev;
            }
        }

        public Int32 allocatedTracks
        {
            get
            {
                Int32 _altrk = 0;
                if (numExtents > 0)
                {
                    int numExt = numExtents;
                    if (numExt > 3) { numExt = 3; }
                    for (int ix = 0; ix < numExt; ix++)
                    {
                        DSXTENT ext = extents[ix];
                        Int32 startTrk = ext.xtbcyl * ckd._heads + ext.xtbtrk;
                        Int32 endTrk = ext.xtecyl * ckd._heads + ext.xtetrk;
                        _altrk += (endTrk - startTrk) + 1;
                    }
                }
                return _altrk;
            }
        }

        public Int32 usedTracks
        {
            get
            {
                Int32 _ustrk = DSCB.ds1lstar.TT.big_endian;
                if (DSCB.ds1lstar.R > 0) { _ustrk++; }
                return _ustrk;
            }
        }

        public Int32 percentUsed
        {
            get
            {
                if (allocatedTracks != 0) return (usedTracks * 100) / allocatedTracks;
                return 0;
            }
        }

        public Int32 numExtents
        {
            get
            {
                return DSCB.ds1nmext;
            }
        }

        public DSXTENT[] extents
        {
            get
            {
                return DSCB.ds1exnts;
            }
        }

        public String creationDate
        {
            get
            {
                return Global.JulDate(DSCB.ds1credt);
            }
        }

        public String creDate
        {
            get
            {
                return Global.YYDDD(DSCB.ds1credt);
            }
        }

        public String expirationDate
        {
            get
            {
                return Global.JulDate(DSCB.ds1expdt);
            }
        }

        public String expDate
        {
            get
            {
                return Global.YYDDD(DSCB.ds1expdt);
            }
        }

        public String referenceDate
        {
            get
            {
                return Global.JulDate(DSCB.ds1refd);
            }
        }

        public String refDate
        {
            get
            {
                return Global.YYDDD(DSCB.ds1refd);
            }
        }

        public String datasetOrganization
        {
            get
            {
                Byte dsOrg = (Byte)(DSCB.ds1dsorg & 0xf2);
                String dsOrgS = null;

                switch (dsOrg)
                {
                    case FORMAT1_DSCB.DS1DSGIS:
                        dsOrgS = "IS";
                        break;

                    case FORMAT1_DSCB.DS1DSGPS:
                        dsOrgS = "PS";
                        break;

                    case FORMAT1_DSCB.DS1DSGDA:
                        dsOrgS = "DA";
                        break;

                    case FORMAT1_DSCB.DS1DSGCX:
                        dsOrgS = "BTAM/QTAM";
                        break;

                    case FORMAT1_DSCB.DS1DSGPO:
                        dsOrgS = "PO";
                        break;

                    default:
                        break;
                }

                dsOrg = (Byte)(DSCB.ds1dsorg2 & 0x08);

                switch (dsOrg)
                {
                    case FORMAT1_DSCB.DS1ACBM:
                        dsOrgS = "VS";
                        break;

                    default:
                        if (dsOrgS == null)
                        {
                            dsOrgS = "UND";
                        }
                        break;
                }

                dsOrg = (Byte)(DSCB.ds1dsorg & 0x01);

                if (dsOrg == 0x01)
                {
                    if (dsOrgS != "UND")
                    {
                        dsOrgS += "U";
                    }
                }
                return dsOrgS;
            }
        }

        public String recordFormat
        {
            get
            {
                Byte recFmt = (Byte)(DSCB.ds1recfm & 0xc0);
                String recFmtS = null;

                switch (recFmt)
                {
                    case FORMAT1_DSCB.DS1RECFF:
                        recFmtS = "F";
                        break;

                    case FORMAT1_DSCB.DS1RECFV:
                        recFmtS = "V";
                        break;

                    case FORMAT1_DSCB.DS1RECFU:
                        recFmtS = "U";
                        break;

                    default:
                        break;
                }

                recFmt = (Byte)(DSCB.ds1recfm & 0x18);

                switch (recFmt)
                {
                    case FORMAT1_DSCB.DS1RECFB:
                        if (recFmtS != "U")
                        {
                            recFmtS += "B";
                        }
                        break;

                    case FORMAT1_DSCB.DS1RECFS:
                        recFmtS += "S";
                        break;

                    default:
                        break;
                }

                recFmt = (Byte)(DSCB.ds1recfm & 0x06);

                switch (recFmt)
                {
                    case FORMAT1_DSCB.DS1RECFA:
                        recFmtS += "A";
                        break;

                    case FORMAT1_DSCB.DS1RECMC:
                        recFmtS += "M";
                        break;

                    default:
                        break;
                }

                return recFmtS;
            }
        }

        public UInt32 lrecl
        {
            get
            {
                return (UInt32)DSCB.ds1lrecl.big_endian;
            }
        }

        public UInt32 blksize
        {
            get
            {
                return (UInt32)DSCB.ds1blkl.big_endian;
            }
        }

        public DSXTENT[] dsextents
        {
            get
            {
                return DSCB.ds1exnts;
            }
        }
    }
    public class TRACKEntry : extObject
    {
        public TRACKEntry()
        {
            pathname = null;
            filename = null;
            offset = 0;
            datasize = 0;
            comp = 0;
            CC = 0;
            HH = 0;
            data = null;
        }

        public String pathname { get; set; }
        public String filename { get; set; }
        public long offset { get; set; }
        public long datasize { get; set; }
        public Byte comp { get; set; }
        public Int32 CC { get; set; }
        public Int16 HH { get; set; }
        public Int32 L1Tab { get; set; }
        public Int32 L2Tab { get; set; }
        public Byte[] data { get; set; }
    }
    public class CKD_HDR : extObject
    {
        public CKD_HDR()
        {
            buffer = new Byte[5];
        }
        public Byte[] buffer { get; set; }

        public Byte comp
        {
            get
            {
                return buffer[0];
            }
            set
            {
                buffer[0] = value;
            }
        }
        public HWORD CC
        {
            get
            {
                return new HWORD(buffer[1], buffer[2]);
            }
            set
            {
                buffer[1] = value.bytes[0];
                buffer[2] = value.bytes[1];
            }
        }
        public HWORD HH
        {
            get
            {
                return new HWORD(buffer[3], buffer[4]);
            }
            set
            {
                buffer[3] = value.bytes[0];
                buffer[4] = value.bytes[1];
            }
        }
    }
}
