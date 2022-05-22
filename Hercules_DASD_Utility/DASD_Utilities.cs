/*
 * DASD_Utilities: Main form for displaying contents of DASD files used by Hercules.
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
using ExpTreeLib;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using CustomExtensions;
using Hercules_DASD_Utility.Properties;

namespace Hercules_DASD_Utility
{
    public partial class DASD_Utilities : Form
    {
        private ListViewColumnSorter lvColumnSorter;
        private XMLConfiguration XMLConfig;
        private static bool formLoading = true;
        private CShItem LastSelectedCSI;
        private readonly ManualResetEvent Event1 = new ManualResetEvent(true);
        private readonly DateTime testTime = new DateTime(1, 1, 1, 0, 0, 0);
        private Int32 tpIndex = -1;
        List<DASDEntry> dasdGroup;
        List<TRACKEntry> trackGroup;
        List<DSNEntry> dsnGroup;

        public DASD_Utilities()
        {
            InitializeComponent();
            MyInitialize();
            formLoading = false;
        }

        private void MyInitialize()
        {
            this.Text = Global.AssemblyTitle;
            SystemImageListManager.SetListViewImageList(lvDirectory, false, false);
            SystemImageListManager.SetListViewImageList(lvDirectory, true, false);
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            lvColumnSorter = new ListViewColumnSorter();
            Global.diag = txtDiagnosticData;

            iDVX = scDirectoryVolume.SplitterDistance;
            iDDVX = scDesktopDirectoryVolume.SplitterDistance;
            iDSNLMDSND = scDSNListMemberDSNData.SplitterDistance;

            Global.filter = "";

            for (int i = 0; i <= Global.extensions.GetUpperBound(0); i++)
            {
                Global.filter += Global.extensions[i, 1] + " file (*." + Global.extensions[i, 0] + ")|*." + Global.extensions[i, 0] + "|";
            }

            Global.filter += "All files (*.*)|*.*";

            if (!String.IsNullOrEmpty(Settings.Default.UnloadPath))
            {
                Global.folder = Settings.Default.UnloadPath;
            }

            LoadConfiguration();
        }

        private void DASD_Utilities_Load(object sender, EventArgs e)
        {
            HideTabPage(tpDiagnostics);

            SetDiagnosticsGeneration();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        private void LoadConfiguration()
        {
            XMLConfig = XMLConfiguration.Load(XMLConfiguration.GetConfigFileName());

            Left = XMLConfig.window.main.Left;
            Top = XMLConfig.window.main.Top;
            Width = XMLConfig.window.main.Width;
            Height = XMLConfig.window.main.Height;

            switch (XMLConfig.window.main.State)
            {
                case "Minimized":
                    WindowState = FormWindowState.Minimized;
                    break;
                case "Maximized":
                    WindowState = FormWindowState.Maximized;
                    break;
                default:
                    WindowState = FormWindowState.Normal;
                    break;
            }
        }

        private void DASD_Utilities_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XMLConfig.IsDirty)
            {
                XMLConfig.Save(XMLConfiguration.GetConfigFileName());
            }
        }

        private void DASD_Utilities_Resize(object sender, EventArgs e)
        {
            if (formLoading) return;

            if (this.WindowState == FormWindowState.Normal)
            {
                XMLConfig.window.main.Left = this.Left;
                XMLConfig.window.main.Top = this.Top;
                XMLConfig.window.main.Height = this.Height;
                XMLConfig.window.main.Width = this.Width;
            }
            XMLConfig.window.main.State = WindowState.ToString();
        }

        private void etvDesktop_ExpTreeNodeSelected(string SelPath, CShItem CSI)
        {
            ArrayList dirList;
            ArrayList fileList = new ArrayList();
            int TotalItems;

            LastSelectedCSI = CSI;

            if (CSI.DisplayName.Equals(CShItem.strMyComputer))
            {
                dirList = CSI.GetDirectories();
            }
            else
            {
                dirList = CSI.GetDirectories();
                fileList = CSI.GetFiles();
            }

            Event1.WaitOne();
            TotalItems = dirList.Count + fileList.Count;
            
            if (TotalItems > 0)
            {
                dirList.Sort();
                fileList.Sort();

                statusStrip1.Items[0].Text = SelPath + "  " + dirList.Count.ToString() + " Directories " + fileList.Count.ToString() + " Files";

                ArrayList combList = new ArrayList(TotalItems);
                combList.AddRange(dirList);
                combList.AddRange(fileList);

                //  Build the ListViewItems & add to lv1
                lvDirectory.BeginUpdate();
                this.Cursor = Cursors.WaitCursor;
                lvDirectory.Items.Clear();
                lvDirectory.Refresh();
                
                foreach(CShItem item in combList)
                {
                    ListViewItem lvi = new ListViewItem(item.DisplayName);
                    
                    if (!item.IsDisk && item.IsFileSystem)
                    {
                        FileAttributes attr = item.Attributes;
                        StringBuilder SB = new StringBuilder();

                        if (attr.HasFlag(FileAttributes.System)) { SB.Append("S"); }
                        if (attr.HasFlag(FileAttributes.Hidden)) { SB.Append("H"); }
                        if (attr.HasFlag(FileAttributes.ReadOnly)) { SB.Append("R"); }
                        if (attr.HasFlag(FileAttributes.Archive)) { SB.Append("A"); }

                        lvi.SubItems.Add(SB.ToString());
                    }
                    else
                    {
                        lvi.SubItems.Add("");
                    }

                    if (!item.IsDisk && item.IsFileSystem && !item.IsFolder)
                        if (item.Length > 1024)
                        {
                            if (item.Length > 1048576)
                            {
                                lvi.SubItems.Add(String.Format("{0} MB", item.Length / 1048576));
                            }
                            else
                            {
                                lvi.SubItems.Add(String.Format("{0} KB", item.Length / 1024));
                            }
                        }
                        else
                        {
                            lvi.SubItems.Add(String.Format("{0} bytes", item.Length));
                        }
                    else
                    {
                        lvi.SubItems.Add("");
                    }

                    lvi.SubItems.Add(item.TypeName);

                    if (!item.IsDisk)
                    {
                        lvi.SubItems.Add("");
                    }
                    else
                    {
                        if (item.LastWriteTime == testTime)
                        {
                            lvi.SubItems.Add(" ");
                        }
                        else
                        {
                            lvi.SubItems.Add(item.LastWriteTime.ToString());
                        }
                    }

                    CShItem refItem = item;
                    lvi.ImageIndex = SystemImageListManager.GetIconIndex(refItem, false, false);
                    lvi.Tag = item;
                    lvDirectory.Items.Add(lvi);
                }

                this.Cursor = Cursors.Default;
                lvDirectory.EndUpdate();
                // LoadLV1Images();
            }
            else
            {
                lvDirectory.Items.Clear();
                statusStrip1.Text = SelPath + " Has No Items";
            }
        }

        private void lvDirectory_DoubleClick(object sender, EventArgs e)
        {
            CShItem csi = (CShItem)lvDirectory.SelectedItems[0].Tag;

            if (csi.IsFolder)
            {
                etvDesktop.ExpandANode(csi);
            }
            else
            {
                msDirOpen_Click(sender, e);
            }
        }

        //private void LoadLV1Images()
        //{
        //    foreach (ListViewItem lvi in lvDirectory.Items)
        //    {
        //        CShItem CSI = (CShItem)lvi.Tag;
        //        lvi.ImageIndex = SystemImageListManager.GetIconIndex(ref CSI, false, false);
        //    }
        //    Event1.Set();
        //}

        private void mnuFILEOpenHerculesConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Config files (*.conf,*.cnf)|*.conf;*.cnf|All files (*.*)|*.*"
            };
            DialogResult dr = dlg.ShowDialog();
            
            lblConfigName.Text = "";

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                lblConfigName.Text = dlg.FileName;
                Clear_Tabs();
                DASD_Routines.Open_Config(dlg.FileName, lvVolume);
            }
        }

        private void lvVolume_DoubleClick(object sender, EventArgs e)
        {
            tpDSNMember.Text = "...";
            tpDSNList.Text = "...";
            textDASDInfo.Text = "";
            textVolumeData.Text = "";
            textMemberHexData.Text = "";
            lblDSN.Text = "";
            lblMemberHexList.Text = "";
            lvMemberList.Items.Clear();

            tpVolume.Text = "Volume";
            tpVTOC.Text = "VTOC";
            tpCKDFBA.Text = "CKD/FBA";

            lvDASDInfo.BeginUpdate();
            lvDASDInfo.Items.Clear();
            lvTrackList.BeginUpdate();
            lvTrackList.Items.Clear();
            lvVTOC_DSNList.BeginUpdate();
            lvVTOC_DSNList.Items.Clear();
            this.Cursor = Cursors.WaitCursor;

            if (lvVolume.SelectedItems.Count > 0)
            {
                String fileName = lvVolume.SelectedItems[0].SubItems[2].Text;
                String shadowName = lvVolume.SelectedItems[0].SubItems[3].Text;
                String deviceName = lvVolume.SelectedItems[0].SubItems[1].Text;
                VolumeEntry dasd = (VolumeEntry)lvVolume.SelectedItems[0].Tag;
                CKDDEV ckd = (CKDDEV)DASD_Routines.dasd_lookup(-1, deviceName, 0, 0);
                String pathName = null;
                if (dasd == null) { }
                else { pathName = dasd.pathname; }

                dasdGroup = DASD_Volume.get_dasd_list(fileName, shadowName, pathName);

                if (dasdGroup != null)
                {
                    tpVolume.Text = "Volume - " + lvVolume.SelectedItems[0].SubItems[0].Text;
                    tpDSNList.Text = lvVolume.SelectedItems[0].SubItems[0].Text;
                    tpVTOC.Text = "VTOC - " + tpDSNList.Text;
                    tpCKDFBA.Text = "CKD/FBA - " + lvVolume.SelectedItems[0].SubItems[0].Text;

                    for (int i = 0; i < dasdGroup.Count; i++)
                    {
                        DASDEntry dEntry = dasdGroup[i]; dEntry.Debug(nameof(dEntry));

                        ListViewItem lvi = new ListViewItem(dEntry.structureName);
                        lvi.SubItems.Add(dEntry.offset.ToString("X8"));
                        lvi.SubItems.Add(dEntry.size.ToString("X4"));
                        lvi.SubItems.Add(dEntry.filename);
                        lvi.Tag = dEntry;
                        lvDASDInfo.Items.Add(lvi);
                    }

                    trackGroup = DASD_Volume.get_track_list(fileName, shadowName, pathName);

                    trackGroup.Sort(delegate (TRACKEntry x, TRACKEntry y)
                    {
                        Int32 ix = (((UInt16)x.CC << 16)
                                  | ((UInt16)x.HH));
                        Int32 iy = (((UInt16)y.CC << 16)
                                  | ((UInt16)y.HH));
                        int a = ix.CompareTo(iy);
                        return a;
                    });

                    for (int i = 0; i < trackGroup.Count; i++)
                    {
                        TRACKEntry dGroup = trackGroup[i];

                        ListViewItem lvi = new ListViewItem(i.ToString());
                        lvi.SubItems.Add(dGroup.CC.ToString("X4"));
                        lvi.SubItems.Add(dGroup.HH.ToString("X4"));
                        if (dGroup.comp == 0)
                        {
                            lvi.SubItems.Add(dGroup.datasize.ToString("X4"));
                        }
                        else
                        {
                            lvi.SubItems.Add("Pack<" + dGroup.datasize.ToString("X4") + ">");
                        }
                        lvi.SubItems.Add(dGroup.L1Tab.ToString());
                        lvi.SubItems.Add(dGroup.L2Tab.ToString());
                        lvi.SubItems.Add(dGroup.filename);
                        lvi.Tag = dGroup;
                        lvTrackList.Items.Add(lvi);
                    }

                    dsnGroup = DASD_Volume.get_DSN_list(fileName, shadowName, pathName);

                    dsnGroup.Sort(delegate (DSNEntry x, DSNEntry y)
                    {
                        String ix = x.DSN;
                        String iy = y.DSN;
                        int a = ix.CompareTo(iy);
                        return a;
                    });

                    for (int i = 0; i < dsnGroup.Count; i++)
                    {
                        DSNEntry dGroup = dsnGroup[i]; dGroup.Debug(nameof(dGroup));

                        Int32 altrk = dGroup.allocatedTracks;
                        Int32 ustrk = dGroup.usedTracks;
                        Int32 pct = dGroup.percentUsed;
                        String date;

                        ListViewItem lvi = new ListViewItem(dGroup.DSN.TrimEnd());
                        lvi.SubItems.Add(altrk.ToString());
                        lvi.SubItems.Add(ustrk.ToString());
                        lvi.SubItems.Add(dGroup.datasetOrganization);
                        lvi.SubItems.Add(dGroup.recordFormat);
                        lvi.SubItems.Add(pct.ToString());
                        lvi.SubItems.Add(dGroup.numExtents.ToString());
                        String lrecl = dGroup.lrecl.ToString();
                        if (lrecl == "0") { lrecl = ""; }
                        lvi.SubItems.Add(lrecl);
                        lvi.SubItems.Add(dGroup.blksize.ToString());
                        date = dGroup.refDate;
                        if (date == "00000") { date = ""; }
                        lvi.SubItems.Add(date);
                        date = dGroup.creDate;
                        if (date == "00000") { date = ""; }
                        lvi.SubItems.Add(date);
                        date = dGroup.expDate;
                        if (date == "00000") { date = ""; }
                        lvi.SubItems.Add(date);
                        lvi.Tag = dGroup;
                        lvVTOC_DSNList.Items.Add(lvi);
                    }
                }
            }

            this.Cursor = Cursors.Default;
            lvTrackList.EndUpdate();
            lvVTOC_DSNList.EndUpdate();
            lvDASDInfo.EndUpdate();
        }

        private void lvDASDInfo_DoubleClick(object sender, EventArgs e)
        {
            textDASDInfo.Text = "";
            if (lvDASDInfo.SelectedItems.Count > 0)
            {
                //String structName = lvDASDInfo.SelectedItems[0].Text;
                DASDEntry dEntry = (DASDEntry)lvDASDInfo.SelectedItems[0].Tag; dEntry.Debug(nameof(dEntry));
                String filename = dEntry.filename;
                if (filename.Contains(":")) { }
                else { filename = dEntry.pathname + "\\" + filename; }
                FileStream sReader = new FileStream(filename, FileMode.Open, FileAccess.Read);
                long rc = sReader.Seek(dEntry.offset, 0);
                if (rc >= 0)
                {
                    Byte[] data = new Byte[dEntry.size];
                    rc = sReader.Read(data, 0, dEntry.size);

                    if (rc > 0)
                    {
                        String hdrStr = "";

                        hdrStr = hdrStr + "Structure=" + dEntry.structureName;
                        hdrStr = hdrStr + "  Offset=" + dEntry.offset.ToString("X4");
                        hdrStr = hdrStr + "  Size=" + dEntry.size.ToString("X4");
                        textDASDInfo.HeaderText = hdrStr;
                        textDASDInfo.DumpWidth = -1;
                        textDASDInfo.EBCDIC = false;
                        textDASDInfo.Bytes = data;
                    }
                }
            }
        }

        private void lvTrackList_DoubleClick(object sender, EventArgs e)
        {
            textVolumeData.Text = "";
            if (lvTrackList.SelectedItems.Count > 0)
            {
                String grpName = lvTrackList.SelectedItems[0].SubItems[0].Text;
                String idxS = grpName; //.Substring(6);
                Int32 idx = Convert.ToInt32(idxS);
                TRACKEntry dGroup = trackGroup[idx];
                String hdrStr = "";

                if (dGroup.data == null)
                {
                    String filename = dGroup.filename;
                    if (filename.Contains(":")) { }
                    else { filename = dGroup.pathname + "\\" + filename; }
                    FileStream sReader = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    long rc = sReader.Seek(dGroup.offset, 0);

                    if (rc > 0)
                    {
                        Byte[] newData = new Byte[dGroup.datasize];
                        rc = sReader.Read(newData, 0, (Int32)dGroup.datasize);
                        if (rc == dGroup.datasize)
                        {
                            if (dGroup.comp == 0)
                            {
                                dGroup.data = newData;
                            }
                            else
                            {
                                Int32 udLen = (Int32)dGroup.datasize * 8;
                                Byte[] uncompData = new Byte[udLen];
                                Int32 dLen = (Int32)dGroup.datasize;
                                Int32 dOffset = 5;

                                rc = DASD_Routines.uncompress(ref uncompData, ref udLen, ref newData, ref dOffset, ref dLen);
                                if (rc < 0) { return; }

                                dGroup.comp = 0;
                                dGroup.datasize = udLen;

                                trackGroup[idx].comp = 0;
                                trackGroup[idx].datasize = udLen;
                                Byte[] data = new byte[udLen];
                                Array.Copy(uncompData, 0, data, 0, udLen);

                                dGroup.data = data;
                                trackGroup[idx].data = data;

                                lvTrackList.SelectedItems[0].SubItems[3].Text = udLen.ToString("X4");
                            }
                        }
                    }
                    sReader.Close();
                }

                if (dGroup.data != null)
                {
                    if (dGroup.data.Length > 5)
                    {

                        hdrStr = hdrStr + "Item=" + grpName;
                        hdrStr = hdrStr + "  CC=" + lvTrackList.SelectedItems[0].SubItems[1].Text;
                        hdrStr = hdrStr + "  HH=" + lvTrackList.SelectedItems[0].SubItems[2].Text;
                        hdrStr = hdrStr + "  Length=" + lvTrackList.SelectedItems[0].SubItems[3].Text;
                        textVolumeData.HeaderText = hdrStr;
                        textVolumeData.DumpWidth = -1;
                        textVolumeData.EBCDIC = true;
                        textVolumeData.Bytes = dGroup.data;
                    }
                }
            }
        }

        private void lvDSNList_DoubleClick(object sender, EventArgs e)
        {
            tpDSNMember.Text = "...";
            textMemberHexData.Text = "";
            lblDSN.Text = "";
            lblMemberHexList.Text = lvVTOC_DSNList.SelectedItems[0].SubItems[0].Text;
            lvMemberList.Items.Clear();

            if (tcDSNMemberList.TabCount > 1)
            {
                for (int i = tcDSNMemberList.TabCount; i > 1; i--)
                {
                    TabPage tp = tcDSNMemberList.TabPages[1];
                    tcDSNMemberList.TabPages.Remove(tcDSNMemberList.TabPages[1]);
                    tp.Dispose();
                }
            }
            
            if (lvVTOC_DSNList.SelectedItems.Count > 0)
            {
                DSNEntry dEntry = (DSNEntry)lvVTOC_DSNList.SelectedItems[0].Tag; dEntry.Debug(nameof(dEntry));
                DEVBLK dev = null;
                String rtfStr = Global._rtfHdr;

                Int32 ccyl = dEntry.dsextents[0].xtbcyl;
                Int16 chead = dEntry.dsextents[0].xtbtrk;
                //Int32 ecyl = dEntry.dsextents[0].xtecyl;
                //Int16 ehead = dEntry.dsextents[0].xtetrk;

                Int32 rc;
                Int32 fnd = 0;

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

                    switch (dEntry.datasetOrganization)
                    {
                        case "PS":
                        case "PSU":
                            rtfStr += DASD_Routines.Process_Text_DSN(dEntry);
                            break;

                        case "PO":
                        case "POU":
                            lblDSN.Text = lblMemberHexList.Text;
                            tpDSNMember.Text = lblMemberHexList.Text;
                            rtfStr += DASD_Routines.Fill_Member_ListView(dEntry, lvMemberList);
                            break;

                        default:
                            rtfStr += DASD_Routines.Process_Block_DSN(dEntry);
                            break;
                    }
                }

                rtfStr = rtfStr + Global._rtfNL + Global._rtfEnd;
                textMemberHexData.Rtf = rtfStr;
            }
        }

        private void lvMemberList_DoubleClick(object sender, EventArgs e)
        {
            if (lvMemberList.SelectedItems.Count > 0)
            {
                MemberEntry memEntry = (MemberEntry)lvMemberList.SelectedItems[0].Tag; memEntry.Debug(nameof(memEntry));

                if (memEntry != null)
                {
                    this.Cursor = Cursors.WaitCursor;

                    RichTextBox textBox = NewTabPageTextBox(memEntry);

                    String retStr = DASD_Routines.Get_Member_Contents(memEntry);

                    if (retStr != null)
                    {
                        if (retStr.StartsWith("{\\rtf1"))
                        {
                            textBox.Rtf = retStr;
                        }
                        else
                        {
                            textBox.Text = retStr;
                        }
                    }
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private RichTextBox NewTabPageTextBox(MemberEntry memEntry)
        {
            TabPage tabPage = new TabPage();
            RichTextBox textBox = new RichTextBox();
            TableLayoutPanel layoutPanel = new TableLayoutPanel();
            Label label = new Label();
            Button button = new Button();

            tabPage.Name = "tp_" + memEntry.member + tcDSNMemberList.TabPages.Count.ToString();
            tabPage.Padding = new Padding(3);
            tabPage.UseVisualStyleBackColor = true;
            tcDSNMemberList.TabPages.Add(tabPage);
            tabPage.Controls.Add(layoutPanel);

            layoutPanel.ColumnCount = 2;
            layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 24f));
            layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            layoutPanel.Dock = DockStyle.Fill;
            layoutPanel.Location = new Point(3, 3);
            layoutPanel.Name = "lp_" + memEntry.member + tcDSNMemberList.TabPages.Count.ToString();
            layoutPanel.RowCount = 2;
            layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            layoutPanel.Size = new Size(1172, 459);
            layoutPanel.TabIndex = 1;
            layoutPanel.Controls.Add(textBox, 0, 1);
            layoutPanel.Controls.Add(button, 0, 0);
            layoutPanel.Controls.Add(label, 1, 0);
            layoutPanel.SetColumnSpan(textBox, 2);
            layoutPanel.SetColumnSpan(button, 1);
            layoutPanel.SetColumnSpan(label, 1);

            button.Name = "btn_" + memEntry.member + tcDSNMemberList.TabPages.Count.ToString();
            button.Image = imageList1.Images[0];
            button.Text = "";
            button.Width = 16;
            button.Height = 16;
            button.Click += new EventHandler(button_Click);
            button.Dock = DockStyle.Fill;
            button.TextAlign = ContentAlignment.TopCenter;
            button.Tag = tabPage;

            textBox.ContextMenuStrip = this.cmsDSNData;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox.Location = new System.Drawing.Point(3, 23);
            textBox.Name = "txt_" + memEntry.member + tcDSNMemberList.TabPages.Count.ToString();
            textBox.Size = new System.Drawing.Size(1166, 433);
            textBox.TabIndex = 1;
            textBox.Text = "";
            textBox.WordWrap = false;

            label.AutoSize = true;
            label.Dock = DockStyle.Fill;
            label.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label.Location = new System.Drawing.Point(3, 0);
            label.Name = "label" + tcDSNMemberList.TabPages.Count.ToString();
            label.Size = new System.Drawing.Size(1166, 20);
            label.TabIndex = 2;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            tabPage.Text = memEntry.member;
            label.Text = memEntry.member;

            tabPage.Tag = memEntry.member;
            label.Tag = memEntry.member;
            textBox.Tag = memEntry.member;

            return textBox;
        }

        private void Clear_Tabs()
        {
            textDASDInfo.Text = "";
            textVolumeData.Text = "";
            textMemberHexData.Text = "";
            lblDSN.Text = "";
            lblMemberHexList.Text = "";
            lvDASDInfo.Items.Clear();
            lvTrackList.Items.Clear();
            lvVTOC_DSNList.Items.Clear();
            lvMemberList.Items.Clear();

            tpVolume.Text = "Volume";
            tpVTOC.Text = "VTOC";
            tpCKDFBA.Text = "CKD/FBA";

            tpDSNMember.Text = "...";
            tpDSNList.Text = "...";

            if (tcDSNMemberList.TabCount > 1)
            {
                for (int i = tcDSNMemberList.TabCount; i > 1; i--)
                {
                    TabPage tp = tcDSNMemberList.TabPages[1];
                    tcDSNMemberList.TabPages.Remove(tcDSNMemberList.TabPages[1]);
                    tp.Dispose();
                }
            }
        }

        private void mnuViewLargeIcons_Click(object sender, EventArgs e)
        {
            lvDirectory.View = View.LargeIcon;
        }

        private void mnuViewSmallIcons_Click(object sender, EventArgs e)
        {
            lvDirectory.View = View.SmallIcon;
        }

        private void mnuViewList_Click(object sender, EventArgs e)
        {
            lvDirectory.View = View.List;
        }

        private void mnuViewDetails_Click(object sender, EventArgs e)
        {
            lvDirectory.View = View.Details;
        }

        private void mnuViewHexDumpOnly_Click(object sender, EventArgs e)
        {
            mnuViewHexDumpOnly.Checked = !mnuViewHexDumpOnly.Checked;
            Global.HexDumpOnly = mnuViewHexDumpOnly.Checked;
        }

        private void msDirOpen_Click(object sender, EventArgs e)
        {
            CShItem csi = (CShItem)lvDirectory.SelectedItems[0].Tag;
            string csiFileName = csi.GetFileName();

            if (csi.IsFolder)
            {
                etvDesktop.ExpandANode(csi);
            }
            else
            {
                Clear_Tabs();
                this.Cursor = Cursors.WaitCursor;
                if (csiFileName.EndsWith(".conf") || csiFileName.EndsWith(".cnf"))
                {
                    DASD_Routines.Open_Config(csi.Path, lvVolume);
                }
                else
                {
                    DASD_Routines.Open_DASD(csi.Path, lvVolume);
                    lvVolume_DoubleClick(this, new EventArgs());
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void msDirEdit_Click(object sender, EventArgs e)
        {
            CShItem csi = (CShItem)lvDirectory.SelectedItems[0].Tag;

            if (csi.IsFolder)
            {
                etvDesktop.ExpandANode(csi);
            }
            else
            {
                try
                {
                    Process.Start(csi.Path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error in starting application", MessageBoxButtons.OK);
                    throw;
                }
            }
        }

        private void msDirStart_Click(object sender, EventArgs e)
        {
            CShItem csi = (CShItem)lvDirectory.SelectedItems[0].Tag;

            if (csi.IsFolder)
            {
                etvDesktop.ExpandANode(csi);
            }
            else
            {
                try
                {
                    Process.Start(csi.Path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error in starting application", MessageBoxButtons.OK);
                    throw;
                }
            }
        }

        private void msDataUndo_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)menuItem.Owner;
            RichTextBox richText = (menu.SourceControl == null) ? textMemberHexData : (RichTextBox)menu.SourceControl;

            if (richText != null)
            {
                richText.Undo();
            }
        }

        private void msDataRedo_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)menuItem.Owner;
            RichTextBox richText = (menu.SourceControl == null) ? textMemberHexData : (RichTextBox)menu.SourceControl;

            if (richText != null)
            {
                richText.Redo();
            }
        }

        private void msDataCut_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)menuItem.Owner;
            RichTextBox richText = (menu.SourceControl == null) ? textMemberHexData : (RichTextBox)menu.SourceControl;

            if (richText != null)
            {
                richText.Cut();
            }
        }

        private void msDataCopy_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)menuItem.Owner;
            RichTextBox richText = (menu.SourceControl == null) ? textMemberHexData : (RichTextBox)menu.SourceControl;

            if (richText != null)
            {
                richText.Copy();
            }
        }

        private void msDataPaste_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)menuItem.Owner;
            RichTextBox richText = (menu.SourceControl == null) ? textMemberHexData : (RichTextBox)menu.SourceControl;

            if (richText != null)
            {
                richText.Paste();
            }
        }

        private void msDataDelete_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)menuItem.Owner;
            RichTextBox richText = (menu.SourceControl == null) ? textMemberHexData : (RichTextBox)menu.SourceControl;

            if (richText != null)
            {
                richText.SelectedText = "";
            }
        }

        private void msDataSave_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)menuItem.Owner;
            RichTextBox richText = (menu.SourceControl == null) ? textMemberHexData : (RichTextBox)menu.SourceControl;
            string fileName = (menu.SourceControl == null) ? "" : menu.SourceControl.Tag.ToString();

            if (richText != null)
            {
                if (!(richText.Text.Equals(null) || (richText.Text == "")))
                {
                    SaveFileDialog dlg = new SaveFileDialog
                    {
                        FileName = fileName,
                        Filter = Global.filter
                    };
                    DialogResult dr = dlg.ShowDialog(this);

                    if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        DASD_Routines.Save_Text(dlg.FileName, richText.Text);
                    }
                }
            }
        }

        private void msMemListSave_Click(object sender, EventArgs e)
        {
            //String senderType = sender.GetType().ToString();
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            //String ownerType = menuItem.Owner.GetType().ToString();
            ContextMenuStrip menu = (ContextMenuStrip)menuItem.Owner;
            //String sourceType = menu.SourceControl.GetType().ToString();
            ListView listView = (ListView)menu.SourceControl;
            if (!listView.Equals(null))
            {
                if (listView.SelectedItems.Count > 0)
                {
                    String tagItem = listView.SelectedItems[0].Tag.ToString();
                    tagItem = tagItem.Substring(tagItem.LastIndexOf('.') + 1).ToLower();
                    switch (tagItem)
                    {
                        case "memberentry":
                            MemberEntry memEntry = (MemberEntry)listView.SelectedItems[0].Tag; memEntry.Debug(nameof(memEntry));

                            if (memEntry != null)
                            {
                                DASD_Routines.Save_MemberEntry(memEntry);
                            }
                            break;
                        case "dsnentry":
                            DSNEntry dsnEntry = (DSNEntry)listView.SelectedItems[0].Tag; dsnEntry.Debug(nameof(dsnEntry));
                            String rtfStr = "";

                            if (dsnEntry != null)
                            {
                                switch (dsnEntry.datasetOrganization)
                                {
                                    case "BTAM/QTAM":     // BTAM/QTAM
                                        break;

                                    case "DA":            // direct access
                                    case "DAU":
                                        break;

                                    case "IS":            // ISAM
                                    case "ISU":
                                        break;

                                    case "PS":            // sequential
                                    case "PSU":
                                        rtfStr += DASD_Routines.Process_Text_DSN(dsnEntry);
                                        break;

                                    case "PO":            // partitioned
                                    case "POU":
                                        DASD_MemberList_Unload unldForm = new DASD_MemberList_Unload
                                        {
                                            dsnEntry = dsnEntry,
                                            memList = DASD_Routines.Build_MemberEntry_List(dsnEntry)
                                        };
                                        unldForm.ShowDialog(this);
                                        break;

                                    case "UND":           // undefined
                                        break;

                                    case "VS":            // VSAM
                                    case "VSU":
                                        break;

                                    default:
                                        break;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void msMemListBinarySave_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)menuItem.Owner;
            ListView listView = (ListView)menu.SourceControl;

            if (!listView.Equals(null))
            {
                if (listView.SelectedItems.Count > 0)
                {
                    String tagItem = listView.SelectedItems[0].Tag.ToString();
                    tagItem = tagItem.Substring(tagItem.LastIndexOf('.') + 1).ToLower();
                    switch (tagItem)
                    {
                        case "memberentry":
                            MemberEntry memEntry = (MemberEntry)listView.SelectedItems[0].Tag; memEntry.Debug(nameof(memEntry));

                            if (memEntry != null)
                            {
                                DASD_Routines.Binary_Save_MemberEntry(memEntry);
                            }
                            break;
                        case "dsnentry":
                            //DSNEntry dsnEntry = (DSNEntry)listView.SelectedItems[0].Tag; dsnEntry.Debug(nameof(dsnEntry));
                            //String rtfStr = "";

                            //if (dsnEntry != null)
                            //{
                            //    switch (dsnEntry.datasetOrganization)
                            //    {
                            //        case "BTAM/QTAM":     // BTAM/QTAM
                            //            break;

                            //        case "DA":            // direct access
                            //        case "DAU":
                            //            break;

                            //        case "IS":            // ISAM
                            //        case "ISU":
                            //            break;

                            //        case "PS":            // sequential
                            //        case "PSU":
                            //            rtfStr += DASD_Routines.Process_Text_DSN(dsnEntry);
                            //            break;

                            //        case "PO":            // partitioned
                            //        case "POU":
                            //            DASD_MemberList_Unload unldForm = new DASD_MemberList_Unload
                            //            {
                            //                dsnEntry = dsnEntry,
                            //                memList = DASD_Routines.Build_MemberEntry_List(dsnEntry)
                            //            };
                            //            unldForm.ShowDialog(this);
                            //            break;

                            //        case "UND":           // undefined
                            //            break;

                            //        case "VS":            // VSAM
                            //        case "VSU":
                            //            break;

                            //        default:
                            //            break;
                            //    }
                            //}
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ((ListView)sender).ListViewItemSorter = lvColumnSorter;
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvColumnSorter.Order == SortOrder.Ascending)
                {
                    lvColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvColumnSorter.SortColumn = e.Column;
                lvColumnSorter.Order = SortOrder.Ascending;
            }

            if (((ListView)sender).Columns[e.Column].TextAlign == HorizontalAlignment.Right) { lvColumnSorter.type = ListViewColumnSorter.CompareType.ofNumber; }
            else { lvColumnSorter.type = ListViewColumnSorter.CompareType.ofString; }

            // Perform the sort with these new sort options.
            ((ListView)sender).Sort();
            ((ListView)sender).ListViewItemSorter = null;
        }

        private void mnuViewDiagnostics_Click(object sender, EventArgs e)
        {
            mnuViewDiagnostics.Checked = !mnuViewDiagnostics.Checked;

            if (!mnuViewDiagnostics.Checked)
            {
                HideTabPage(tpDiagnostics);
            }
            else
            {
                ShowTabPage(tpDiagnostics);
            }

            SetDiagnosticsGeneration();
        }

        private void HideTabPage(TabPage tp)
        {
            if (tabControl1.TabPages.Contains(tp))
            {
                tpIndex = tabControl1.TabPages.IndexOf(tp);
                tabControl1.TabPages.Remove(tp);
            }
        }

        private void ShowTabPage(TabPage tp)
        {
            if (tpIndex >= 0)
            {
                tabControl1.TabPages.Insert(tpIndex, tp);
            }
            else
            {
                ShowTabPage(tp, tabControl1.TabPages.Count);
            }
        }

        private void ShowTabPage(TabPage tp, int index)
        {
            if (tabControl1.TabPages.Contains(tp)) return;
            InsertTabPage(tp, index);
        }

        //private Boolean IsTabPageVisible(TabPage tp)
        //{
        //    return tabControl1.TabPages.Contains(tp);
        //}

        private void InsertTabPage(TabPage tabpage, int index)
        {
            if (index < 0 || index > tabControl1.TabCount)
                throw new ArgumentException("Index out of Range.");
            tabControl1.TabPages.Add(tabpage);
            if (index < tabControl1.TabCount - 1)
                do
                {
                    SwapTabPages(tabpage, (tabControl1.TabPages[tabControl1.TabPages.IndexOf(tabpage) - 1]));
                }
                while (tabControl1.TabPages.IndexOf(tabpage) != index);
            tabControl1.SelectedTab = tabpage;
        }

        private void SwapTabPages(TabPage tp1, TabPage tp2)
        {
            if (tabControl1.TabPages.Contains(tp1) == false || tabControl1.TabPages.Contains(tp2) == false)
                throw new ArgumentException("TabPages must be in the TabControls TabPageCollection.");

            int Index1 = tabControl1.TabPages.IndexOf(tp1);
            int Index2 = tabControl1.TabPages.IndexOf(tp2);
            tabControl1.TabPages[Index1] = tp2;
            tabControl1.TabPages[Index2] = tp1;

            //Uncomment the following section to overcome bugs in the Compact Framework
            //tabControl1.SelectedIndex = tabControl1.SelectedIndex; 
            //string tp1Text, tp2Text;
            //tp1Text = tp1.Text;
            //tp2Text = tp2.Text;
            //tp1.Text=tp2Text;
            //tp2.Text=tp1Text;
        }

        private void SetDiagnosticsGeneration()
        {
            if ((mnuViewDiagnostics.Checked) && (cbGenerateDiagnostics.Checked)) { Global.genDiag = true; }
            else { Global.genDiag = false; }
        }

        private void cbGenerateDiagnostics_CheckedChanged(object sender, EventArgs e)
        {
            SetDiagnosticsGeneration();
        }

        private void cbHeaderOnOff_CheckedChanged(object sender, EventArgs e)
        {
            hexData.IncludeHeader = cbHeaderOnOff.Checked;
        }

        private void cbOffsetOnOff_CheckedChanged(object sender, EventArgs e)
        {
            hexData.IncludeOffset = cbOffsetOnOff.Checked;
        }

        private void btnGenerateChars_Click(object sender, EventArgs e)
        {
            int extant;
            Byte[] data;
            int i;
            int len;
            if (txtExtant.Text.IsNumeric(0, txtExtant.Text.Length))
            {
                extant = Convert.ToInt16(txtExtant.Text);
            }
            else
            {
                extant = 32;
            }
            if (txtCharCount.Text.IsNumeric(0, txtCharCount.Text.Length))
            {
                len = Convert.ToInt16(txtCharCount.Text);
            }
            else
            {
                len = 64;
            }
            data = new Byte[len];

            for (i = 0; i < len; i++)
            {
                int k = i & 255;
                data[i] = (Byte)k;
            }

            hexData.EBCDIC = cbEBCDIC.Checked;
            hexData.DumpWidth = extant;
            hexData.Bytes = data;
        }

        private void cbEBCDIC_CheckedChanged(object sender, EventArgs e)
        {
            hexData.EBCDIC = cbEBCDIC.Checked;
        }

        private void cbIncludeAscii_CheckedChanged(object sender, EventArgs e)
        {
            hexData.IncludeAscii = cbIncludeAscii.Checked;
        }

        int iDVX;
        private void scDirectoryVolume_SplitterMoved(object sender, SplitterEventArgs e)
        {
            iDVX = e.SplitX;
        }

        int iDDVX;
        private void scDesktopDirectoryVolume_SplitterMoved(object sender, SplitterEventArgs e)
        {
            //if (scDirectoryVolume.SplitterDistance == iDVX) { }
            //else
            //{
            //    scDesktopDirectoryVolume.SplitterDistance = iDDVX;
            //    e.SplitX = iDDVX;
            //}
            //iDDVX = e.SplitX;
        }

        int iMDSND;
        private void scMemberDSNData_SplitterMoved(object sender, SplitterEventArgs e)
        {
            iMDSND = e.SplitX;
        }

        int iDSNLMDSND;
        private void scDSNListMemberDSNData_SplitterMoved(object sender, SplitterEventArgs e)
        {
            //if (scMemberDSNData.SplitterDistance == iMDSND) { }
            //else
            //{
            //    scDSNListMemberDSNData.SplitterDistance = iDSNLMDSND;
            //    e.SplitX = iDSNLMDSND;
            //}
            //iDSNLMDSND = e.SplitX;
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            DASD_About dlg = new DASD_About();
            dlg.ShowDialog();
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TabPage page = (TabPage)button.Tag;

            int iPage = tcDSNMemberList.TabPages.IndexOf(page);
            iPage--;

            tcDSNMemberList.TabPages.Remove(page);
            page.Dispose();

            if (iPage > 0)
            {
                tcDSNMemberList.SelectTab(iPage);
            }
        }
    }
}
