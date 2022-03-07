/*
 * HexControl: RichTextBox user control to format dump data from input text, bytes, or characters.
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CustomExtensions;

namespace Hercules_DASD_Utility
{
    public partial class HexControl : UserControl
    {
        //private bool _Created = false;    			//Is control created or not yet.
        //private bool _Float = false;                  //Is control window float or not.
        //private bool _Virtual = false;                //Is control works in "Virtual" mode.
        //private bool _Mutable = false;                //Is control works in Edit mode.
        private bool _LMousePressed = false;            //Is left mouse button pressed.
        private bool _EBCDIC = false;
        private bool _includeHeader = true;
        private bool _includeOffset = true;
        private bool _includeAscii = true;
        private static String _rtfHdr = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Consolas;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs22 ";
        private static String _rtfNL = "\\par\r\n";
        private static String _rtfEnd = "}\r\n";
        private String _HeaderText = "";
        private String _Text;
        private String[] _TextArray;
        private Char[] _Chars;
        private Char[][] _CharArray;
        private Byte[] _Bytes;
        private Byte[][] _ByteArray;
        private int _dumpWidth = 16;
        private int[] _Widths = { 140, 172, 236, 364, 620, 1132 };
        private int[] _DumpWidth = { 1, 2, 4, 8, 16, 32 };

        #region Properties
        #region Reflow
        [Browsable(true)]
        [Category("HexDump")]
        [Description("Control whether the hex dump is reflowed when the text box width changes.")]
        /// <summary>
        /// Control whether the hex dump is reflowed when the text box width changes.
        /// </summary>
        public Boolean Reflow { get; set; }
        #endregion

        #region IncludeAscii
        [Browsable(true)]
        [Category("HexDump")]
        [Description("Control whether the ascii text is displayed after the hex data.")]
        /// <summary>
        /// Control whether the ascii text is displayed after the hex data.
        /// </summary>
        public Boolean IncludeAscii
        {
            get { return _includeAscii; }
            set
            {
                _includeAscii = value;
                RedisplayDump();
                RebuildOffsetHeader();
            }
        }
        #endregion

        #region IncludeHeader
        [Browsable(true)]
        [Category("HexDump")]
        [Description("Control whether a header is displayed before the hex dump.")]
        /// <summary>
        /// Control whether a header is displayed before the hex dump.
        /// </summary>
        public Boolean IncludeHeader
        {
            get { return _includeHeader; }
            set
            {
                _includeHeader = value;
                HeaderDisplay();
            }
        }
        #endregion

        #region IncludeOffset
        [Browsable(true)]
        [Category("HexDump")]
        [Description("Control whether an offset is displayed for each line of the hex dump.")]
        /// <summary>
        /// Control whether an offset is displayed for each line of the hex dump.
        /// </summary>
        public Boolean IncludeOffset
        {
            get { return _includeOffset; }
            set
            {
                _includeOffset = value;
                OffsetDisplay();
            }
        }
        #endregion

        #region EBCDIC
        [Browsable(true)]
        [Category("HexDump")]
        [Description("Indicates whether the data is EBCDIC or Ascii.")]
        /// <summary>
        /// Indicates whether the data is EBCDIC or Ascii.
        /// </summary>
        public Boolean EBCDIC
        {
            get { return _EBCDIC; }
            set
            {
                if (_EBCDIC != value)
                {
                    _EBCDIC = value;
                    if (_Bytes != null)
                    {
                        if (_Bytes.Length > 0)
                        {
                            txtHexData.Rtf = Hex_Dump(_Bytes, _Bytes.Length);
                        }
                    }
                }
                _EBCDIC = value;
            }
        }
        #endregion

        #region Text
        [Browsable(true)]
        [Category("HexDump")]
        [Description("The text string to be displayed in the hex dump.")]
        /// <summary>
        /// The text string to be displayed in the hex dump.
        /// </summary>
        public override String Text
        {
            get { return _Text; }
            set
            {
                if (value != null)
                {
                    _Text = value;
                    _Bytes = _Text.ToByteArray();
                    _Chars = _Text.ToCharArray();
                    RebuildOffsetHeader();
                    if (_Text != null && _Text.Length > 0)
                    {
                        txtHexData.Rtf = Hex_Dump(_Bytes, _Chars.Length);
                    }
                }
            }
        }
        #endregion

        #region Chars
        [Browsable(true)]
        [Category("HexDump")]
        [Description("A character array to be displayed in the hex dump.")]
        /// <summary>
        /// A character array to be displayed in the hex dump.
        /// </summary>
        public Char[] Chars
        {
            get { return _Chars; }
            set
            {
                if (value != null)
                {
                    _Chars = value;
                    _Text = _Chars.CharsToString(0, value.Length);
                    _Bytes = _Chars.CharsToBytes(0, value.Length);
                    RebuildOffsetHeader();
                    if (_Bytes.Length > 0)
                    {
                        txtHexData.Rtf = Hex_Dump(_Bytes, _Chars.Length);
                    }
                }
            }
        }
        #endregion

        #region Bytes
        [Browsable(true)]
        [Category("HexDump")]
        [Description("A byte array to be displayed in the hex dump.")]
        /// <summary>
        /// A byte array to be displayed in the hex dump.
        /// </summary>
        public Byte[] Bytes
        {
            get { return _Bytes; }
            set
            {
                if (value != null)
                {
                    _Bytes = value;
                    _Chars = new Char[_Bytes.Length];
                    RebuildOffsetHeader();
                    int i;
                    for (i = 0; i < _Bytes.Length; i++)
                    {
                        _Chars[i] = (Char)_Bytes[i];
                    }
                    _Text = _Bytes.ToString();
                    if (_Bytes.Length > 0)
                    {
                        txtHexData.Rtf = Hex_Dump(_Bytes, _Bytes.Length);
                    }
                }
            }
        }
        #endregion

        #region TextArray
        [Browsable(true)]
        [Category("HexDump")]
        [Description("A text array to be displayed in the hex dump.")]
        /// <summary>
        /// A text array to be displayed in the hex dump.
        /// </summary>
        public String[] TextArray
        {
            get { return _TextArray; }
            set
            {
                if (value != null)
                {
                    String RtfDump = "";
                    _TextArray = value;
                    _ByteArray = new Byte[_TextArray.Length][];
                    _CharArray = new Char[_TextArray.Length][];
                    for (int i = 0; i < _TextArray.Length; i++)
                    {
                        _ByteArray[i] = _TextArray[i].ToByteArray();
                        _CharArray[i] = _TextArray[i].ToCharArray();
                        if (_TextArray[i].Length > 0)
                        {
                            RtfDump += Hex_Dump(_ByteArray[i], _ByteArray[i].Length);
                        }
                    }
                    txtHexData.Rtf = RtfDump;
                }
            }
        }
        #endregion

        #region CharArray
        [Browsable(true)]
        [Category("HexDump")]
        [Description("Multiple character arrays to be displayed in the hex dump.")]
        /// <summary>
        /// Multiple character arrays to be displayed in the hex dump.
        /// </summary>
        public Char[][] CharArray
        {
            get { return _CharArray; }
            set
            {
                if (value != null)
                {
                    String RtfDump = "";
                    _CharArray = value;
                    _ByteArray = new Byte[_CharArray.GetLength(0)][];
                    _TextArray = new String[_CharArray.GetLength(0)];
                    for (int i = 0; i < _CharArray.GetLength(0); i++)
                    {
                        _ByteArray[i] = _CharArray[i].CharsToBytes(0, _CharArray[i].Length);
                        _TextArray[i] = _CharArray[i].CharsToString(0, _CharArray[i].Length);
                        if (_ByteArray[i].Length > 0)
                        {
                            RtfDump += Hex_Dump(_ByteArray[i], _ByteArray[i].Length);
                        }
                    }
                    txtHexData.Rtf = RtfDump;
                }
            }
        }
        #endregion

        #region ByteArray
        [Browsable(true)]
        [Category("HexDump")]
        [Description("Multiple byte arrays to be displayed in the hex dump.")]
        /// <summary>
        /// Multiple byte arrays to be displayed in the hex dump.
        /// </summary>
        public Byte[][] ByteArray
        {
            get { return _ByteArray; }
            set
            {
                if (value != null)
                {
                    String RtfDump = "";
                    _ByteArray = value;
                    _CharArray = new Char[_ByteArray.GetLength(0)][];
                    _TextArray = new String[_ByteArray.GetLength(0)];
                    for (int i = 0; i < _ByteArray.GetLength(0); i++)
                    {
                        _CharArray[i] = new Char[_ByteArray[i].Length];
                        for (int k = 0; k < _Bytes.Length; k++)
                        {
                            _CharArray[i][k] = (Char)_ByteArray[i][k];
                        }
                        _TextArray[i] = _ByteArray[i].ToString();
                        if (_ByteArray[i].Length > 0)
                        {
                            RtfDump += Hex_Dump(_ByteArray[i], _ByteArray[i].Length);
                        }
                    }
                    txtHexData.Rtf = RtfDump;
                }
            }
        }
        #endregion

        #region DumpWidth
        [Browsable(true)]
        [Category("HexDump")]
        [Description("Number of colums of data to display in the dump.")]
        /// <summary>
        /// Number of colums of data to display in the dump.
        /// </summary>
        public int DumpWidth
        {
            get { return _dumpWidth; }
            set
            {
                if (value < 1)
                {
                    _dumpWidth = CalcDumpWidth();
                    Reflow = true;
                }
                else
                {
                    _dumpWidth = value;
                    Reflow = false;
                }
                RebuildOffsetHeader();
            }
        }
        #endregion

        #region HeaderText
        [Browsable(true)]
        [Category("HexDump")]
        [Description("The header text to be displaye if IncludeHeader is set to true.")]
        /// <summary>
        /// The header text to be displaye if IncludeHeader is set to true.
        /// </summary>
        public String HeaderText
        {
            get { return _HeaderText; }
            set
            {
                _HeaderText = value;
                txtHeader.Text = _HeaderText;
            }
        }
        #endregion
        #endregion

        public HexControl()
        {
            InitializeComponent();
        }

        private int CalcDumpWidth()
        {
            for (int i = _Widths.Length - 1; i >= 0; i--)
            {
                if (this.Width > _Widths[i])
                {
                    return _DumpWidth[i];
                }
            }
            return 1;
        }

        private void HeaderDisplay()
        {
            if (_includeHeader)
            {
                txtHeader.Visible = true;
                tableLayoutPanel1.RowStyles[0].Height = 20F;
            }
            else
            {
                txtHeader.Visible = false;
                tableLayoutPanel1.RowStyles[0].Height = 0F;
            }
        }

        private void OffsetDisplay ()
        {
            if (_includeOffset)
            {
                txtOffsetHeader.Visible = true;
                tableLayoutPanel1.RowStyles[1].Height = 20F;
            }
            else
            {
                txtOffsetHeader.Visible = false;
                tableLayoutPanel1.RowStyles[1].Height = 0F;
            }
            RedisplayDump();
        }

        private String Hex_Dump(Byte[] buf, Int32 buflen)
        {
            if (buflen <= 0) return string.Empty;

            StringBuilder str = new StringBuilder(_rtfHdr);
            int i = 0;

            while (i < buflen)
            {
                int curIdx = i;

                if (i != 0) str.AppendLine(_rtfNL);

                if (IncludeOffset)
                {
                    str.Append("{\\b " + $"{curIdx:X6}" + "} ");
                }

                for (int cDW = 0; cDW < DumpWidth; cDW++)
                {
                    if (cDW != 0) str.Append(" ");

                    str.Append(i < buflen
                        ? buf[i].ToString("X2")
                        : "  ");
                    i++;
                }

                if (IncludeAscii)
                {
                    str.Append("   ");

                    i = curIdx;

                    for (int cDW = 0; cDW < DumpWidth; cDW++)
                    {
                        if (i < buflen)
                        {
                            if (EBCDIC)
                            {
                                str.Append(ByteToChar(Global.ebcdic_to_ascii[buf[i]]));
                            }
                            else
                            {
                                str.Append(ByteToChar(buf[i]));
                            }
                        }
                        else
                        {
                            str.Append(" ");
                        }

                       i++;
                    }
                }
            }

            str.Append(_rtfNL);
            str.Append(_rtfEnd);

            return str.ToString();
        }

        private static Char ByteToChar(Byte byt)
        {
            //var c = (char)byt;
            //return char.IsControl(c) ? '.' : c;
            if ((byt > 0x1f) && (byt < 0xff)) return (Char)byt;
            return '.';
        }

        private void RedisplayDump()
        {
            if (_Bytes != null)
            {
                if (_Bytes.Length > 0)
                {
                    txtHexData.Rtf = Hex_Dump(_Bytes, _Bytes.Length);
                }
            }
        }

        private void RebuildOffsetHeader()
        {
            if ((Text == null || Text.Length <= 0) &&
                (Bytes == null || Bytes.Length <= 0) &&
                (Chars == null || Chars.Length <= 0) &&
                (TextArray == null || TextArray.Length == 0) &&
                (ByteArray == null || ByteArray.Length == 0) &&
                (CharArray == null || CharArray.Length == 0))
            {
                return;
            }
            String txt = "Offset ";
            String txt2 = "";
            for (int i = 0; i < _dumpWidth; i++)
            {
                txt = txt + i.ToString("X2") + " ";
            }
            if (IncludeAscii)
            {
                for (int i = 0; i < _dumpWidth; i += 8)
                {
                    txt2 += ".......+";
                }
                txt2 = " |" + txt2.Substring(0, _dumpWidth) + "|";
            }
            txtOffsetHeader.Text = txt + txt2;
        }


        #region Event handlers
        private void HexControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_LMousePressed)
            {
                Rectangle rcClient = ClientRectangle;
                
            }
        }

        private void HexControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _LMousePressed = true;
            }
            else
            {
                _LMousePressed = false;
            }
        }

        private void HexControl_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void HexControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void HexControl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtHexData_VScroll(object sender, EventArgs e)
        {
        }

        private void txtOffsetList_VScroll(object sender, EventArgs e)
        {
        }

        private void HexControl_Resize(object sender, EventArgs e)
        {
            if (Reflow)
            {
                int ext = CalcDumpWidth();
                int w = this.Width;

                if (ext != _dumpWidth)
                {
                    DumpWidth = ext;
                    Reflow = true;
                }

                RedisplayDump();
            }
        }
        #endregion
    }
}
