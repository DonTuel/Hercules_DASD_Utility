namespace Hercules_DASD_Utility
{
    partial class DASD_Utilities
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DASD_Utilities));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.scDirectoryVolume = new System.Windows.Forms.SplitContainer();
            this.scDesktopDirectoryVolume = new System.Windows.Forms.SplitContainer();
            this.etvDesktop = new ExpTreeLib.ExpTree();
            this.lvDirectory = new System.Windows.Forms.ListView();
            this.chDirName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDirAttributes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDirSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDirType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDirModifyDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsDirectory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.msDirOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.msDirEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.msDirStart = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.lvVolume = new System.Windows.Forms.ListView();
            this.chVolName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVolDevice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVolBaseFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVolShadowFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblConfigName = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFILEOpenHerculesConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewLargeIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSmallIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewHexDumpOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewRecordLength = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRecordLengthValue = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewDiagnostics = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpDirectory = new System.Windows.Forms.TabPage();
            this.tpCKDFBA = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lvDASDInfo = new System.Windows.Forms.ListView();
            this.chDASDStucture = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDASDOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDASDSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDASDFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textDASDInfo = new Hercules_DASD_Utility.HexControl();
            this.tpVolume = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lvTrackList = new System.Windows.Forms.ListView();
            this.chTrkIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTrkCC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTrkHH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTrkDataSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTrkL1Tab = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTrkL2Tab = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTrkVFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textVolumeData = new Hercules_DASD_Utility.HexControl();
            this.tpVTOC = new System.Windows.Forms.TabPage();
            this.tlpVTOC = new System.Windows.Forms.TableLayoutPanel();
            this.tcVTOCEntries = new System.Windows.Forms.TabControl();
            this.tpDSNList = new System.Windows.Forms.TabPage();
            this.scDSNListMemberDSNData = new System.Windows.Forms.SplitContainer();
            this.lvVTOC_DSNList = new System.Windows.Forms.ListView();
            this.chVTOCDSN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVTOCAllocTrk = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVTOCUsedTrk = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVTOCOrg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVTOCRecFmt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVTOCPct = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVTOCXT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVTOCLRECL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVTOCBLKSZ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVTOCRefDt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVTOCCreDt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVTOCExpDt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsMemList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiUnloadSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnloadBinarySave = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.textMemberHexData = new System.Windows.Forms.RichTextBox();
            this.cmsDSNData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.msDataUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.msDataRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.msDataCut = new System.Windows.Forms.ToolStripMenuItem();
            this.msDataCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.msDataPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.msDataDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.msDataSave = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMemberHexList = new System.Windows.Forms.Label();
            this.tpDSNMember = new System.Windows.Forms.TabPage();
            this.tlpDSNMembers = new System.Windows.Forms.TableLayoutPanel();
            this.tcDSNMemberList = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cmsDeleteTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpDSNMemberList = new System.Windows.Forms.TableLayoutPanel();
            this.lvMemberList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblDSN = new System.Windows.Forms.Label();
            this.tpDiagnostics = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDiagnosticInitiator = new System.Windows.Forms.Label();
            this.txtDiagnosticData = new System.Windows.Forms.RichTextBox();
            this.cbGenerateDiagnostics = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tpHexCtrl = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.hexData = new Hercules_DASD_Utility.HexControl();
            this.cbHeaderOnOff = new System.Windows.Forms.CheckBox();
            this.cbOffsetOnOff = new System.Windows.Forms.CheckBox();
            this.txtCharCount = new System.Windows.Forms.TextBox();
            this.btnGenerateChars = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExtant = new System.Windows.Forms.TextBox();
            this.cbEBCDIC = new System.Windows.Forms.CheckBox();
            this.cbIncludeAscii = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDirectoryVolume)).BeginInit();
            this.scDirectoryVolume.Panel1.SuspendLayout();
            this.scDirectoryVolume.Panel2.SuspendLayout();
            this.scDirectoryVolume.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDesktopDirectoryVolume)).BeginInit();
            this.scDesktopDirectoryVolume.Panel1.SuspendLayout();
            this.scDesktopDirectoryVolume.Panel2.SuspendLayout();
            this.scDesktopDirectoryVolume.SuspendLayout();
            this.cmsDirectory.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpDirectory.SuspendLayout();
            this.tpCKDFBA.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tpVolume.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tpVTOC.SuspendLayout();
            this.tlpVTOC.SuspendLayout();
            this.tcVTOCEntries.SuspendLayout();
            this.tpDSNList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDSNListMemberDSNData)).BeginInit();
            this.scDSNListMemberDSNData.Panel1.SuspendLayout();
            this.scDSNListMemberDSNData.Panel2.SuspendLayout();
            this.scDSNListMemberDSNData.SuspendLayout();
            this.cmsMemList.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.cmsDSNData.SuspendLayout();
            this.tpDSNMember.SuspendLayout();
            this.tlpDSNMembers.SuspendLayout();
            this.tcDSNMemberList.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.cmsDeleteTab.SuspendLayout();
            this.tlpDSNMemberList.SuspendLayout();
            this.tpDiagnostics.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tpHexCtrl.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Controls.Add(this.scDirectoryVolume, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1200, 535);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // scDirectoryVolume
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.scDirectoryVolume, 7);
            this.scDirectoryVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDirectoryVolume.Location = new System.Drawing.Point(3, 3);
            this.scDirectoryVolume.Name = "scDirectoryVolume";
            // 
            // scDirectoryVolume.Panel1
            // 
            this.scDirectoryVolume.Panel1.Controls.Add(this.scDesktopDirectoryVolume);
            // 
            // scDirectoryVolume.Panel2
            // 
            this.scDirectoryVolume.Panel2.Controls.Add(this.tableLayoutPanel8);
            this.scDirectoryVolume.Size = new System.Drawing.Size(1194, 519);
            this.scDirectoryVolume.SplitterDistance = 640;
            this.scDirectoryVolume.TabIndex = 1;
            this.scDirectoryVolume.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scDirectoryVolume_SplitterMoved);
            // 
            // scDesktopDirectoryVolume
            // 
            this.scDesktopDirectoryVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDesktopDirectoryVolume.Location = new System.Drawing.Point(0, 0);
            this.scDesktopDirectoryVolume.Name = "scDesktopDirectoryVolume";
            // 
            // scDesktopDirectoryVolume.Panel1
            // 
            this.scDesktopDirectoryVolume.Panel1.Controls.Add(this.etvDesktop);
            // 
            // scDesktopDirectoryVolume.Panel2
            // 
            this.scDesktopDirectoryVolume.Panel2.Controls.Add(this.lvDirectory);
            this.scDesktopDirectoryVolume.Size = new System.Drawing.Size(640, 519);
            this.scDesktopDirectoryVolume.SplitterDistance = 234;
            this.scDesktopDirectoryVolume.TabIndex = 0;
            this.scDesktopDirectoryVolume.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scDesktopDirectoryVolume_SplitterMoved);
            // 
            // etvDesktop
            // 
            this.etvDesktop.AllowFolderRename = false;
            this.etvDesktop.Cursor = System.Windows.Forms.Cursors.Default;
            this.etvDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.etvDesktop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.etvDesktop.Location = new System.Drawing.Point(0, 0);
            this.etvDesktop.Name = "etvDesktop";
            this.etvDesktop.ShowRootLines = false;
            this.etvDesktop.Size = new System.Drawing.Size(234, 519);
            this.etvDesktop.StartUpDirectory = ExpTreeLib.ExpTree.StartDir.MyComputer;
            this.etvDesktop.TabIndex = 0;
            this.etvDesktop.ExpTreeNodeSelected += new ExpTreeLib.ExpTree.ExpTreeNodeSelectedEventHandler(this.etvDesktop_ExpTreeNodeSelected);
            // 
            // lvDirectory
            // 
            this.lvDirectory.AllowDrop = true;
            this.lvDirectory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDirName,
            this.chDirAttributes,
            this.chDirSize,
            this.chDirType,
            this.chDirModifyDate});
            this.lvDirectory.ContextMenuStrip = this.cmsDirectory;
            this.lvDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvDirectory.HideSelection = false;
            this.lvDirectory.Location = new System.Drawing.Point(0, 0);
            this.lvDirectory.MultiSelect = false;
            this.lvDirectory.Name = "lvDirectory";
            this.lvDirectory.Size = new System.Drawing.Size(402, 519);
            this.lvDirectory.TabIndex = 0;
            this.lvDirectory.UseCompatibleStateImageBehavior = false;
            this.lvDirectory.View = System.Windows.Forms.View.Details;
            this.lvDirectory.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.lvDirectory.DoubleClick += new System.EventHandler(this.lvDirectory_DoubleClick);
            // 
            // chDirName
            // 
            this.chDirName.Text = "Name";
            this.chDirName.Width = 180;
            // 
            // chDirAttributes
            // 
            this.chDirAttributes.Text = "Attributes";
            this.chDirAttributes.Width = 72;
            // 
            // chDirSize
            // 
            this.chDirSize.Text = "Size";
            this.chDirSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chDirSize.Width = 80;
            // 
            // chDirType
            // 
            this.chDirType.Text = "Type";
            this.chDirType.Width = 100;
            // 
            // chDirModifyDate
            // 
            this.chDirModifyDate.Text = "Modified";
            this.chDirModifyDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chDirModifyDate.Width = 80;
            // 
            // cmsDirectory
            // 
            this.cmsDirectory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msDirOpen,
            this.msDirEdit,
            this.msDirStart});
            this.cmsDirectory.Name = "contextMenuStrip1";
            this.cmsDirectory.Size = new System.Drawing.Size(105, 70);
            // 
            // msDirOpen
            // 
            this.msDirOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msDirOpen.Name = "msDirOpen";
            this.msDirOpen.Size = new System.Drawing.Size(104, 22);
            this.msDirOpen.Text = "Open";
            this.msDirOpen.Click += new System.EventHandler(this.msDirOpen_Click);
            // 
            // msDirEdit
            // 
            this.msDirEdit.Name = "msDirEdit";
            this.msDirEdit.Size = new System.Drawing.Size(104, 22);
            this.msDirEdit.Text = "Edit";
            this.msDirEdit.Click += new System.EventHandler(this.msDirEdit_Click);
            // 
            // msDirStart
            // 
            this.msDirStart.Name = "msDirStart";
            this.msDirStart.Size = new System.Drawing.Size(104, 22);
            this.msDirStart.Text = "Start";
            this.msDirStart.Click += new System.EventHandler(this.msDirStart_Click);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.lvVolume, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.lblConfigName, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(550, 519);
            this.tableLayoutPanel8.TabIndex = 2;
            // 
            // lvVolume
            // 
            this.lvVolume.AllowDrop = true;
            this.lvVolume.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chVolName,
            this.chVolDevice,
            this.chVolBaseFile,
            this.chVolShadowFile});
            this.lvVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvVolume.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvVolume.FullRowSelect = true;
            this.lvVolume.HideSelection = false;
            this.lvVolume.Location = new System.Drawing.Point(3, 23);
            this.lvVolume.MultiSelect = false;
            this.lvVolume.Name = "lvVolume";
            this.lvVolume.Size = new System.Drawing.Size(544, 493);
            this.lvVolume.TabIndex = 1;
            this.lvVolume.UseCompatibleStateImageBehavior = false;
            this.lvVolume.View = System.Windows.Forms.View.Details;
            this.lvVolume.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.lvVolume.DoubleClick += new System.EventHandler(this.lvVolume_DoubleClick);
            // 
            // chVolName
            // 
            this.chVolName.Text = "Volume";
            this.chVolName.Width = 80;
            // 
            // chVolDevice
            // 
            this.chVolDevice.Text = "Device";
            // 
            // chVolBaseFile
            // 
            this.chVolBaseFile.Text = "File Name";
            this.chVolBaseFile.Width = 240;
            // 
            // chVolShadowFile
            // 
            this.chVolShadowFile.Text = "Shadow File";
            this.chVolShadowFile.Width = 240;
            // 
            // lblConfigName
            // 
            this.lblConfigName.AutoSize = true;
            this.lblConfigName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConfigName.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfigName.Location = new System.Drawing.Point(3, 0);
            this.lblConfigName.Name = "lblConfigName";
            this.lblConfigName.Size = new System.Drawing.Size(544, 20);
            this.lblConfigName.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1214, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileOpen,
            this.toolStripMenuItem6,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFILEOpenHerculesConfig});
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(103, 22);
            this.mnuFileOpen.Text = "Open";
            // 
            // mnuFILEOpenHerculesConfig
            // 
            this.mnuFILEOpenHerculesConfig.Name = "mnuFILEOpenHerculesConfig";
            this.mnuFILEOpenHerculesConfig.Size = new System.Drawing.Size(171, 22);
            this.mnuFILEOpenHerculesConfig.Text = "Hercules Config ...";
            this.mnuFILEOpenHerculesConfig.Click += new System.EventHandler(this.mnuFILEOpenHerculesConfig_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(100, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(103, 22);
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditClear,
            this.toolStripSeparator1,
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste,
            this.mnuEditDelete,
            this.toolStripSeparator2,
            this.mnuEditSelectAll});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "Edit";
            this.mnuEdit.Visible = false;
            // 
            // mnuEditClear
            // 
            this.mnuEditClear.Name = "mnuEditClear";
            this.mnuEditClear.Size = new System.Drawing.Size(122, 22);
            this.mnuEditClear.Text = "Clear";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(122, 22);
            this.mnuEditCut.Text = "Cut";
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(122, 22);
            this.mnuEditCopy.Text = "Copy";
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.Size = new System.Drawing.Size(122, 22);
            this.mnuEditPaste.Text = "Paste";
            // 
            // mnuEditDelete
            // 
            this.mnuEditDelete.Name = "mnuEditDelete";
            this.mnuEditDelete.Size = new System.Drawing.Size(122, 22);
            this.mnuEditDelete.Text = "Delete";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(119, 6);
            // 
            // mnuEditSelectAll
            // 
            this.mnuEditSelectAll.Name = "mnuEditSelectAll";
            this.mnuEditSelectAll.Size = new System.Drawing.Size(122, 22);
            this.mnuEditSelectAll.Text = "Select All";
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewLargeIcons,
            this.mnuViewSmallIcons,
            this.mnuViewList,
            this.mnuViewDetails,
            this.toolStripMenuItem1,
            this.mnuViewHexDumpOnly,
            this.toolStripMenuItem4,
            this.mnuViewRecordLength,
            this.toolStripMenuItem5,
            this.mnuViewDiagnostics});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "&View";
            // 
            // mnuViewLargeIcons
            // 
            this.mnuViewLargeIcons.Name = "mnuViewLargeIcons";
            this.mnuViewLargeIcons.Size = new System.Drawing.Size(160, 22);
            this.mnuViewLargeIcons.Text = "Lar&ge Icons";
            this.mnuViewLargeIcons.Click += new System.EventHandler(this.mnuViewLargeIcons_Click);
            // 
            // mnuViewSmallIcons
            // 
            this.mnuViewSmallIcons.Name = "mnuViewSmallIcons";
            this.mnuViewSmallIcons.Size = new System.Drawing.Size(160, 22);
            this.mnuViewSmallIcons.Text = "S&mall Icons";
            this.mnuViewSmallIcons.Click += new System.EventHandler(this.mnuViewSmallIcons_Click);
            // 
            // mnuViewList
            // 
            this.mnuViewList.Name = "mnuViewList";
            this.mnuViewList.Size = new System.Drawing.Size(160, 22);
            this.mnuViewList.Text = "&List";
            this.mnuViewList.Click += new System.EventHandler(this.mnuViewList_Click);
            // 
            // mnuViewDetails
            // 
            this.mnuViewDetails.Name = "mnuViewDetails";
            this.mnuViewDetails.Size = new System.Drawing.Size(160, 22);
            this.mnuViewDetails.Text = "&Details";
            this.mnuViewDetails.Click += new System.EventHandler(this.mnuViewDetails_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
            // 
            // mnuViewHexDumpOnly
            // 
            this.mnuViewHexDumpOnly.Name = "mnuViewHexDumpOnly";
            this.mnuViewHexDumpOnly.Size = new System.Drawing.Size(160, 22);
            this.mnuViewHexDumpOnly.Text = "Hex Dump Only";
            this.mnuViewHexDumpOnly.Click += new System.EventHandler(this.mnuViewHexDumpOnly_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(157, 6);
            // 
            // mnuViewRecordLength
            // 
            this.mnuViewRecordLength.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRecordLengthValue});
            this.mnuViewRecordLength.Name = "mnuViewRecordLength";
            this.mnuViewRecordLength.Size = new System.Drawing.Size(160, 22);
            this.mnuViewRecordLength.Text = "Record Length...";
            // 
            // mnuViewRecordLengthValue
            // 
            this.mnuViewRecordLengthValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mnuViewRecordLengthValue.MaxLength = 6;
            this.mnuViewRecordLengthValue.Name = "mnuViewRecordLengthValue";
            this.mnuViewRecordLengthValue.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(157, 6);
            // 
            // mnuViewDiagnostics
            // 
            this.mnuViewDiagnostics.Name = "mnuViewDiagnostics";
            this.mnuViewDiagnostics.Size = new System.Drawing.Size(160, 22);
            this.mnuViewDiagnostics.Text = "Diagnostics";
            this.mnuViewDiagnostics.Click += new System.EventHandler(this.mnuViewDiagnostics_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.mnuHelpAbout.Text = "About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpDirectory);
            this.tabControl1.Controls.Add(this.tpCKDFBA);
            this.tabControl1.Controls.Add(this.tpVolume);
            this.tabControl1.Controls.Add(this.tpVTOC);
            this.tabControl1.Controls.Add(this.tpDiagnostics);
            this.tabControl1.Controls.Add(this.tpHexCtrl);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1214, 567);
            this.tabControl1.TabIndex = 2;
            // 
            // tpDirectory
            // 
            this.tpDirectory.Controls.Add(this.tableLayoutPanel1);
            this.tpDirectory.Location = new System.Drawing.Point(4, 22);
            this.tpDirectory.Name = "tpDirectory";
            this.tpDirectory.Padding = new System.Windows.Forms.Padding(3);
            this.tpDirectory.Size = new System.Drawing.Size(1206, 541);
            this.tpDirectory.TabIndex = 0;
            this.tpDirectory.Text = "Directory";
            this.tpDirectory.UseVisualStyleBackColor = true;
            // 
            // tpCKDFBA
            // 
            this.tpCKDFBA.Controls.Add(this.tableLayoutPanel2);
            this.tpCKDFBA.Location = new System.Drawing.Point(4, 22);
            this.tpCKDFBA.Name = "tpCKDFBA";
            this.tpCKDFBA.Size = new System.Drawing.Size(1206, 541);
            this.tpCKDFBA.TabIndex = 3;
            this.tpCKDFBA.Text = "CKD/FBA";
            this.tpCKDFBA.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.Controls.Add(this.splitContainer3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1206, 541);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.splitContainer3, 7);
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lvDASDInfo);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.textDASDInfo);
            this.splitContainer3.Size = new System.Drawing.Size(1200, 515);
            this.splitContainer3.SplitterDistance = 492;
            this.splitContainer3.TabIndex = 1;
            // 
            // lvDASDInfo
            // 
            this.lvDASDInfo.AllowDrop = true;
            this.lvDASDInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDASDStucture,
            this.chDASDOffset,
            this.chDASDSize,
            this.chDASDFilename});
            this.lvDASDInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDASDInfo.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvDASDInfo.FullRowSelect = true;
            this.lvDASDInfo.HideSelection = false;
            this.lvDASDInfo.Location = new System.Drawing.Point(0, 0);
            this.lvDASDInfo.MultiSelect = false;
            this.lvDASDInfo.Name = "lvDASDInfo";
            this.lvDASDInfo.Size = new System.Drawing.Size(492, 515);
            this.lvDASDInfo.TabIndex = 3;
            this.lvDASDInfo.UseCompatibleStateImageBehavior = false;
            this.lvDASDInfo.View = System.Windows.Forms.View.Details;
            this.lvDASDInfo.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.lvDASDInfo.DoubleClick += new System.EventHandler(this.lvDASDInfo_DoubleClick);
            // 
            // chDASDStucture
            // 
            this.chDASDStucture.Text = "Structure";
            this.chDASDStucture.Width = 160;
            // 
            // chDASDOffset
            // 
            this.chDASDOffset.Text = "Offset";
            this.chDASDOffset.Width = 80;
            // 
            // chDASDSize
            // 
            this.chDASDSize.Text = "Size";
            // 
            // chDASDFilename
            // 
            this.chDASDFilename.Text = "Filename";
            this.chDASDFilename.Width = 280;
            // 
            // textDASDInfo
            // 
            this.textDASDInfo.BackColor = System.Drawing.SystemColors.Window;
            this.textDASDInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textDASDInfo.ByteArray = null;
            this.textDASDInfo.Bytes = null;
            this.textDASDInfo.CharArray = null;
            this.textDASDInfo.Chars = null;
            this.textDASDInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textDASDInfo.DumpWidth = 16;
            this.textDASDInfo.EBCDIC = false;
            this.textDASDInfo.HeaderText = "";
            this.textDASDInfo.IncludeAscii = true;
            this.textDASDInfo.IncludeHeader = true;
            this.textDASDInfo.IncludeOffset = true;
            this.textDASDInfo.Location = new System.Drawing.Point(0, 0);
            this.textDASDInfo.Name = "textDASDInfo";
            this.textDASDInfo.Reflow = false;
            this.textDASDInfo.Size = new System.Drawing.Size(704, 515);
            this.textDASDInfo.TabIndex = 0;
            this.textDASDInfo.TextArray = null;
            // 
            // tpVolume
            // 
            this.tpVolume.Controls.Add(this.tableLayoutPanel4);
            this.tpVolume.Location = new System.Drawing.Point(4, 22);
            this.tpVolume.Name = "tpVolume";
            this.tpVolume.Padding = new System.Windows.Forms.Padding(3);
            this.tpVolume.Size = new System.Drawing.Size(1206, 541);
            this.tpVolume.TabIndex = 1;
            this.tpVolume.Text = "Volume";
            this.tpVolume.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel4.Controls.Add(this.splitContainer4, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1200, 535);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // splitContainer4
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.splitContainer4, 7);
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.lvTrackList);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.textVolumeData);
            this.splitContainer4.Size = new System.Drawing.Size(1194, 509);
            this.splitContainer4.SplitterDistance = 490;
            this.splitContainer4.TabIndex = 1;
            // 
            // lvTrackList
            // 
            this.lvTrackList.AllowDrop = true;
            this.lvTrackList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTrkIndex,
            this.chTrkCC,
            this.chTrkHH,
            this.chTrkDataSize,
            this.chTrkL1Tab,
            this.chTrkL2Tab,
            this.chTrkVFileName});
            this.lvTrackList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTrackList.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvTrackList.FullRowSelect = true;
            this.lvTrackList.HideSelection = false;
            this.lvTrackList.Location = new System.Drawing.Point(0, 0);
            this.lvTrackList.MultiSelect = false;
            this.lvTrackList.Name = "lvTrackList";
            this.lvTrackList.Size = new System.Drawing.Size(490, 509);
            this.lvTrackList.TabIndex = 3;
            this.lvTrackList.UseCompatibleStateImageBehavior = false;
            this.lvTrackList.View = System.Windows.Forms.View.Details;
            this.lvTrackList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.lvTrackList.DoubleClick += new System.EventHandler(this.lvTrackList_DoubleClick);
            // 
            // chTrkIndex
            // 
            this.chTrkIndex.Text = "Index";
            this.chTrkIndex.Width = 120;
            // 
            // chTrkCC
            // 
            this.chTrkCC.Text = "CC";
            // 
            // chTrkHH
            // 
            this.chTrkHH.Text = "HH";
            // 
            // chTrkDataSize
            // 
            this.chTrkDataSize.Text = "Size";
            this.chTrkDataSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chTrkDataSize.Width = 120;
            // 
            // chTrkL1Tab
            // 
            this.chTrkL1Tab.Text = "L1Tab";
            // 
            // chTrkL2Tab
            // 
            this.chTrkL2Tab.Text = "L2Tab";
            // 
            // chTrkVFileName
            // 
            this.chTrkVFileName.Text = "File Name";
            // 
            // textVolumeData
            // 
            this.textVolumeData.BackColor = System.Drawing.SystemColors.Window;
            this.textVolumeData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textVolumeData.ByteArray = null;
            this.textVolumeData.Bytes = null;
            this.textVolumeData.CharArray = null;
            this.textVolumeData.Chars = null;
            this.textVolumeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textVolumeData.DumpWidth = 16;
            this.textVolumeData.EBCDIC = true;
            this.textVolumeData.HeaderText = "";
            this.textVolumeData.IncludeAscii = true;
            this.textVolumeData.IncludeHeader = true;
            this.textVolumeData.IncludeOffset = true;
            this.textVolumeData.Location = new System.Drawing.Point(0, 0);
            this.textVolumeData.Name = "textVolumeData";
            this.textVolumeData.Reflow = false;
            this.textVolumeData.Size = new System.Drawing.Size(700, 509);
            this.textVolumeData.TabIndex = 0;
            this.textVolumeData.TextArray = null;
            // 
            // tpVTOC
            // 
            this.tpVTOC.Controls.Add(this.tlpVTOC);
            this.tpVTOC.Location = new System.Drawing.Point(4, 22);
            this.tpVTOC.Name = "tpVTOC";
            this.tpVTOC.Padding = new System.Windows.Forms.Padding(3);
            this.tpVTOC.Size = new System.Drawing.Size(1206, 541);
            this.tpVTOC.TabIndex = 2;
            this.tpVTOC.Text = "VTOC";
            this.tpVTOC.UseVisualStyleBackColor = true;
            // 
            // tlpVTOC
            // 
            this.tlpVTOC.ColumnCount = 1;
            this.tlpVTOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVTOC.Controls.Add(this.tcVTOCEntries, 0, 0);
            this.tlpVTOC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVTOC.Location = new System.Drawing.Point(3, 3);
            this.tlpVTOC.Name = "tlpVTOC";
            this.tlpVTOC.RowCount = 2;
            this.tlpVTOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVTOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tlpVTOC.Size = new System.Drawing.Size(1200, 535);
            this.tlpVTOC.TabIndex = 2;
            // 
            // tcVTOCEntries
            // 
            this.tcVTOCEntries.Controls.Add(this.tpDSNList);
            this.tcVTOCEntries.Controls.Add(this.tpDSNMember);
            this.tcVTOCEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcVTOCEntries.Location = new System.Drawing.Point(0, 0);
            this.tcVTOCEntries.Margin = new System.Windows.Forms.Padding(0);
            this.tcVTOCEntries.Name = "tcVTOCEntries";
            this.tcVTOCEntries.Padding = new System.Drawing.Point(0, 0);
            this.tcVTOCEntries.SelectedIndex = 0;
            this.tcVTOCEntries.Size = new System.Drawing.Size(1200, 523);
            this.tcVTOCEntries.TabIndex = 2;
            // 
            // tpDSNList
            // 
            this.tpDSNList.Controls.Add(this.scDSNListMemberDSNData);
            this.tpDSNList.Location = new System.Drawing.Point(4, 22);
            this.tpDSNList.Name = "tpDSNList";
            this.tpDSNList.Padding = new System.Windows.Forms.Padding(3);
            this.tpDSNList.Size = new System.Drawing.Size(1192, 497);
            this.tpDSNList.TabIndex = 0;
            this.tpDSNList.Text = "...";
            this.tpDSNList.UseVisualStyleBackColor = true;
            // 
            // scDSNListMemberDSNData
            // 
            this.scDSNListMemberDSNData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDSNListMemberDSNData.Location = new System.Drawing.Point(3, 3);
            this.scDSNListMemberDSNData.Name = "scDSNListMemberDSNData";
            // 
            // scDSNListMemberDSNData.Panel1
            // 
            this.scDSNListMemberDSNData.Panel1.Controls.Add(this.lvVTOC_DSNList);
            // 
            // scDSNListMemberDSNData.Panel2
            // 
            this.scDSNListMemberDSNData.Panel2.Controls.Add(this.tableLayoutPanel7);
            this.scDSNListMemberDSNData.Size = new System.Drawing.Size(1186, 491);
            this.scDSNListMemberDSNData.SplitterDistance = 779;
            this.scDSNListMemberDSNData.TabIndex = 0;
            this.scDSNListMemberDSNData.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scDSNListMemberDSNData_SplitterMoved);
            // 
            // lvVTOC_DSNList
            // 
            this.lvVTOC_DSNList.AllowDrop = true;
            this.lvVTOC_DSNList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chVTOCDSN,
            this.chVTOCAllocTrk,
            this.chVTOCUsedTrk,
            this.chVTOCOrg,
            this.chVTOCRecFmt,
            this.chVTOCPct,
            this.chVTOCXT,
            this.chVTOCLRECL,
            this.chVTOCBLKSZ,
            this.chVTOCRefDt,
            this.chVTOCCreDt,
            this.chVTOCExpDt});
            this.lvVTOC_DSNList.ContextMenuStrip = this.cmsMemList;
            this.lvVTOC_DSNList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvVTOC_DSNList.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvVTOC_DSNList.FullRowSelect = true;
            this.lvVTOC_DSNList.HideSelection = false;
            this.lvVTOC_DSNList.Location = new System.Drawing.Point(0, 0);
            this.lvVTOC_DSNList.MultiSelect = false;
            this.lvVTOC_DSNList.Name = "lvVTOC_DSNList";
            this.lvVTOC_DSNList.Size = new System.Drawing.Size(779, 491);
            this.lvVTOC_DSNList.TabIndex = 2;
            this.lvVTOC_DSNList.UseCompatibleStateImageBehavior = false;
            this.lvVTOC_DSNList.View = System.Windows.Forms.View.Details;
            this.lvVTOC_DSNList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.lvVTOC_DSNList.DoubleClick += new System.EventHandler(this.lvDSNList_DoubleClick);
            // 
            // chVTOCDSN
            // 
            this.chVTOCDSN.Text = "DSN";
            this.chVTOCDSN.Width = 160;
            // 
            // chVTOCAllocTrk
            // 
            this.chVTOCAllocTrk.Text = "AlTrk";
            this.chVTOCAllocTrk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chVTOCUsedTrk
            // 
            this.chVTOCUsedTrk.Text = "UsTrk";
            this.chVTOCUsedTrk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chVTOCOrg
            // 
            this.chVTOCOrg.Text = "Org";
            this.chVTOCOrg.Width = 40;
            // 
            // chVTOCRecFmt
            // 
            this.chVTOCRecFmt.Text = "Fmt";
            this.chVTOCRecFmt.Width = 40;
            // 
            // chVTOCPct
            // 
            this.chVTOCPct.Text = "%";
            this.chVTOCPct.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chVTOCPct.Width = 40;
            // 
            // chVTOCXT
            // 
            this.chVTOCXT.Text = "XT";
            this.chVTOCXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chVTOCXT.Width = 30;
            // 
            // chVTOCLRECL
            // 
            this.chVTOCLRECL.Text = "LRECL";
            this.chVTOCLRECL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chVTOCBLKSZ
            // 
            this.chVTOCBLKSZ.Text = "BLKSZ";
            this.chVTOCBLKSZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chVTOCRefDt
            // 
            this.chVTOCRefDt.Text = "RefDt";
            this.chVTOCRefDt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chVTOCRefDt.Width = 64;
            // 
            // chVTOCCreDt
            // 
            this.chVTOCCreDt.Text = "CreDt";
            this.chVTOCCreDt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chVTOCCreDt.Width = 70;
            // 
            // chVTOCExpDt
            // 
            this.chVTOCExpDt.Text = "ExpDt";
            this.chVTOCExpDt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chVTOCExpDt.Width = 64;
            // 
            // cmsMemList
            // 
            this.cmsMemList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUnloadSave,
            this.tsmiUnloadBinarySave});
            this.cmsMemList.Name = "contextMenuStrip3";
            this.cmsMemList.Size = new System.Drawing.Size(196, 48);
            // 
            // tsmiUnloadSave
            // 
            this.tsmiUnloadSave.Name = "tsmiUnloadSave";
            this.tsmiUnloadSave.Size = new System.Drawing.Size(195, 22);
            this.tsmiUnloadSave.Text = "Unload (Save) ...";
            this.tsmiUnloadSave.Click += new System.EventHandler(this.msMemListSave_Click);
            // 
            // tsmiUnloadBinarySave
            // 
            this.tsmiUnloadBinarySave.Name = "tsmiUnloadBinarySave";
            this.tsmiUnloadBinarySave.Size = new System.Drawing.Size(195, 22);
            this.tsmiUnloadBinarySave.Text = "Unload (Binary Save) ...";
            this.tsmiUnloadBinarySave.Click += new System.EventHandler(this.msMemListBinarySave_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 7;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Controls.Add(this.textMemberHexData, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.lblMemberHexList, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(403, 491);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // textMemberHexData
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.textMemberHexData, 8);
            this.textMemberHexData.ContextMenuStrip = this.cmsDSNData;
            this.textMemberHexData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textMemberHexData.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMemberHexData.Location = new System.Drawing.Point(3, 23);
            this.textMemberHexData.Name = "textMemberHexData";
            this.textMemberHexData.Size = new System.Drawing.Size(397, 465);
            this.textMemberHexData.TabIndex = 1;
            this.textMemberHexData.Text = "";
            this.textMemberHexData.WordWrap = false;
            // 
            // cmsDSNData
            // 
            this.cmsDSNData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msDataUndo,
            this.msDataRedo,
            this.toolStripMenuItem3,
            this.msDataCut,
            this.msDataCopy,
            this.msDataPaste,
            this.msDataDelete,
            this.toolStripMenuItem2,
            this.msDataSave});
            this.cmsDSNData.Name = "contextMenuStrip2";
            this.cmsDSNData.Size = new System.Drawing.Size(160, 170);
            // 
            // msDataUndo
            // 
            this.msDataUndo.Name = "msDataUndo";
            this.msDataUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.msDataUndo.Size = new System.Drawing.Size(159, 22);
            this.msDataUndo.Text = "Undo";
            this.msDataUndo.Click += new System.EventHandler(this.msDataUndo_Click);
            // 
            // msDataRedo
            // 
            this.msDataRedo.Name = "msDataRedo";
            this.msDataRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.msDataRedo.Size = new System.Drawing.Size(159, 22);
            this.msDataRedo.Text = "Redo";
            this.msDataRedo.Click += new System.EventHandler(this.msDataRedo_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(156, 6);
            // 
            // msDataCut
            // 
            this.msDataCut.Name = "msDataCut";
            this.msDataCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.msDataCut.Size = new System.Drawing.Size(159, 22);
            this.msDataCut.Text = "Cut";
            this.msDataCut.Click += new System.EventHandler(this.msDataCut_Click);
            // 
            // msDataCopy
            // 
            this.msDataCopy.Name = "msDataCopy";
            this.msDataCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.msDataCopy.Size = new System.Drawing.Size(159, 22);
            this.msDataCopy.Text = "Copy";
            this.msDataCopy.Click += new System.EventHandler(this.msDataCopy_Click);
            // 
            // msDataPaste
            // 
            this.msDataPaste.Name = "msDataPaste";
            this.msDataPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.msDataPaste.Size = new System.Drawing.Size(159, 22);
            this.msDataPaste.Text = "Paste";
            this.msDataPaste.Click += new System.EventHandler(this.msDataPaste_Click);
            // 
            // msDataDelete
            // 
            this.msDataDelete.Name = "msDataDelete";
            this.msDataDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.msDataDelete.Size = new System.Drawing.Size(159, 22);
            this.msDataDelete.Text = "Delete";
            this.msDataDelete.Click += new System.EventHandler(this.msDataDelete_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(156, 6);
            // 
            // msDataSave
            // 
            this.msDataSave.Name = "msDataSave";
            this.msDataSave.Size = new System.Drawing.Size(159, 22);
            this.msDataSave.Text = "Unload (Save) ...";
            this.msDataSave.Click += new System.EventHandler(this.msDataSave_Click);
            // 
            // lblMemberHexList
            // 
            this.lblMemberHexList.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this.lblMemberHexList, 7);
            this.lblMemberHexList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMemberHexList.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberHexList.Location = new System.Drawing.Point(3, 0);
            this.lblMemberHexList.Name = "lblMemberHexList";
            this.lblMemberHexList.Size = new System.Drawing.Size(397, 20);
            this.lblMemberHexList.TabIndex = 2;
            this.lblMemberHexList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tpDSNMember
            // 
            this.tpDSNMember.Controls.Add(this.tlpDSNMembers);
            this.tpDSNMember.Location = new System.Drawing.Point(4, 22);
            this.tpDSNMember.Name = "tpDSNMember";
            this.tpDSNMember.Padding = new System.Windows.Forms.Padding(3);
            this.tpDSNMember.Size = new System.Drawing.Size(1192, 497);
            this.tpDSNMember.TabIndex = 1;
            this.tpDSNMember.Text = "...";
            this.tpDSNMember.UseVisualStyleBackColor = true;
            // 
            // tlpDSNMembers
            // 
            this.tlpDSNMembers.ColumnCount = 1;
            this.tlpDSNMembers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDSNMembers.Controls.Add(this.tcDSNMemberList, 0, 0);
            this.tlpDSNMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDSNMembers.Location = new System.Drawing.Point(3, 3);
            this.tlpDSNMembers.Name = "tlpDSNMembers";
            this.tlpDSNMembers.RowCount = 1;
            this.tlpDSNMembers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDSNMembers.Size = new System.Drawing.Size(1186, 491);
            this.tlpDSNMembers.TabIndex = 4;
            // 
            // tcDSNMemberList
            // 
            this.tcDSNMemberList.Controls.Add(this.tabPage3);
            this.tcDSNMemberList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDSNMemberList.Location = new System.Drawing.Point(0, 0);
            this.tcDSNMemberList.Margin = new System.Windows.Forms.Padding(0);
            this.tcDSNMemberList.Name = "tcDSNMemberList";
            this.tcDSNMemberList.Padding = new System.Drawing.Point(0, 0);
            this.tcDSNMemberList.SelectedIndex = 0;
            this.tcDSNMemberList.Size = new System.Drawing.Size(1186, 491);
            this.tcDSNMemberList.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage3.ContextMenuStrip = this.cmsDeleteTab;
            this.tabPage3.Controls.Add(this.tlpDSNMemberList);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1178, 465);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Member List";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cmsDeleteTab
            // 
            this.cmsDeleteTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.cmsDeleteTab.Name = "cmsDeleteTab";
            this.cmsDeleteTab.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // tlpDSNMemberList
            // 
            this.tlpDSNMemberList.ColumnCount = 1;
            this.tlpDSNMemberList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDSNMemberList.Controls.Add(this.lvMemberList, 0, 1);
            this.tlpDSNMemberList.Controls.Add(this.lblDSN, 0, 0);
            this.tlpDSNMemberList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDSNMemberList.Location = new System.Drawing.Point(3, 3);
            this.tlpDSNMemberList.Name = "tlpDSNMemberList";
            this.tlpDSNMemberList.RowCount = 2;
            this.tlpDSNMemberList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDSNMemberList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDSNMemberList.Size = new System.Drawing.Size(1170, 457);
            this.tlpDSNMemberList.TabIndex = 2;
            // 
            // lvMemberList
            // 
            this.lvMemberList.AllowDrop = true;
            this.lvMemberList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvMemberList.ContextMenuStrip = this.cmsMemList;
            this.lvMemberList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMemberList.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMemberList.FullRowSelect = true;
            this.lvMemberList.HideSelection = false;
            this.lvMemberList.Location = new System.Drawing.Point(3, 23);
            this.lvMemberList.MultiSelect = false;
            this.lvMemberList.Name = "lvMemberList";
            this.lvMemberList.Size = new System.Drawing.Size(1164, 431);
            this.lvMemberList.TabIndex = 3;
            this.lvMemberList.UseCompatibleStateImageBehavior = false;
            this.lvMemberList.View = System.Windows.Forms.View.Details;
            this.lvMemberList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.lvMemberList.DoubleClick += new System.EventHandler(this.lvMemberList_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Member";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "TTR";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "User Data";
            this.columnHeader3.Width = 180;
            // 
            // lblDSN
            // 
            this.lblDSN.AutoSize = true;
            this.lblDSN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDSN.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDSN.Location = new System.Drawing.Point(3, 0);
            this.lblDSN.Name = "lblDSN";
            this.lblDSN.Size = new System.Drawing.Size(1164, 20);
            this.lblDSN.TabIndex = 0;
            this.lblDSN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tpDiagnostics
            // 
            this.tpDiagnostics.Controls.Add(this.tableLayoutPanel3);
            this.tpDiagnostics.Location = new System.Drawing.Point(4, 22);
            this.tpDiagnostics.Name = "tpDiagnostics";
            this.tpDiagnostics.Size = new System.Drawing.Size(1206, 541);
            this.tpDiagnostics.TabIndex = 4;
            this.tpDiagnostics.Text = "Diagnostics";
            this.tpDiagnostics.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblDiagnosticInitiator, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtDiagnosticData, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.cbGenerateDiagnostics, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.checkBox1, 1, 3);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 13;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1206, 520);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // lblDiagnosticInitiator
            // 
            this.lblDiagnosticInitiator.AutoSize = true;
            this.lblDiagnosticInitiator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiagnosticInitiator.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosticInitiator.Location = new System.Drawing.Point(163, 22);
            this.lblDiagnosticInitiator.Name = "lblDiagnosticInitiator";
            this.lblDiagnosticInitiator.Size = new System.Drawing.Size(1040, 22);
            this.lblDiagnosticInitiator.TabIndex = 3;
            this.lblDiagnosticInitiator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDiagnosticData
            // 
            this.txtDiagnosticData.ContextMenuStrip = this.cmsDSNData;
            this.txtDiagnosticData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiagnosticData.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosticData.Location = new System.Drawing.Point(163, 47);
            this.txtDiagnosticData.Name = "txtDiagnosticData";
            this.tableLayoutPanel3.SetRowSpan(this.txtDiagnosticData, 11);
            this.txtDiagnosticData.Size = new System.Drawing.Size(1040, 470);
            this.txtDiagnosticData.TabIndex = 4;
            this.txtDiagnosticData.Text = "";
            this.txtDiagnosticData.WordWrap = false;
            // 
            // cbGenerateDiagnostics
            // 
            this.cbGenerateDiagnostics.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.cbGenerateDiagnostics, 3);
            this.cbGenerateDiagnostics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbGenerateDiagnostics.Location = new System.Drawing.Point(3, 47);
            this.cbGenerateDiagnostics.Name = "cbGenerateDiagnostics";
            this.cbGenerateDiagnostics.Size = new System.Drawing.Size(154, 16);
            this.cbGenerateDiagnostics.TabIndex = 5;
            this.cbGenerateDiagnostics.Text = "Generate diagnostics";
            this.cbGenerateDiagnostics.UseVisualStyleBackColor = true;
            this.cbGenerateDiagnostics.CheckedChanged += new System.EventHandler(this.cbGenerateDiagnostics_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.checkBox1, 2);
            this.checkBox1.Location = new System.Drawing.Point(23, 69);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 16);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tpHexCtrl
            // 
            this.tpHexCtrl.Controls.Add(this.tableLayoutPanel5);
            this.tpHexCtrl.Location = new System.Drawing.Point(4, 22);
            this.tpHexCtrl.Name = "tpHexCtrl";
            this.tpHexCtrl.Size = new System.Drawing.Size(1206, 541);
            this.tpHexCtrl.TabIndex = 5;
            this.tpHexCtrl.Text = "HexCtrl";
            this.tpHexCtrl.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.hexData, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbHeaderOnOff, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbOffsetOnOff, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.txtCharCount, 2, 4);
            this.tableLayoutPanel5.Controls.Add(this.btnGenerateChars, 2, 6);
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.txtExtant, 2, 5);
            this.tableLayoutPanel5.Controls.Add(this.cbEBCDIC, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.cbIncludeAscii, 1, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 9;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1206, 541);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // hexData
            // 
            this.hexData.BackColor = System.Drawing.SystemColors.Window;
            this.hexData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.hexData.ByteArray = null;
            this.hexData.Bytes = null;
            this.hexData.CharArray = null;
            this.hexData.Chars = null;
            this.hexData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexData.DumpWidth = 16;
            this.hexData.EBCDIC = false;
            this.hexData.HeaderText = "";
            this.hexData.IncludeAscii = true;
            this.hexData.IncludeHeader = true;
            this.hexData.IncludeOffset = true;
            this.hexData.Location = new System.Drawing.Point(163, 3);
            this.hexData.Name = "hexData";
            this.hexData.Reflow = true;
            this.tableLayoutPanel5.SetRowSpan(this.hexData, 8);
            this.hexData.Size = new System.Drawing.Size(1040, 515);
            this.hexData.TabIndex = 6;
            this.hexData.TextArray = null;
            // 
            // cbHeaderOnOff
            // 
            this.cbHeaderOnOff.AutoSize = true;
            this.cbHeaderOnOff.Checked = true;
            this.cbHeaderOnOff.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel5.SetColumnSpan(this.cbHeaderOnOff, 2);
            this.cbHeaderOnOff.Location = new System.Drawing.Point(23, 3);
            this.cbHeaderOnOff.Name = "cbHeaderOnOff";
            this.cbHeaderOnOff.Size = new System.Drawing.Size(61, 16);
            this.cbHeaderOnOff.TabIndex = 5;
            this.cbHeaderOnOff.Text = "Header";
            this.cbHeaderOnOff.UseVisualStyleBackColor = true;
            this.cbHeaderOnOff.CheckedChanged += new System.EventHandler(this.cbHeaderOnOff_CheckedChanged);
            // 
            // cbOffsetOnOff
            // 
            this.cbOffsetOnOff.AutoSize = true;
            this.cbOffsetOnOff.Checked = true;
            this.cbOffsetOnOff.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel5.SetColumnSpan(this.cbOffsetOnOff, 2);
            this.cbOffsetOnOff.Location = new System.Drawing.Point(23, 25);
            this.cbOffsetOnOff.Name = "cbOffsetOnOff";
            this.cbOffsetOnOff.Size = new System.Drawing.Size(54, 16);
            this.cbOffsetOnOff.TabIndex = 7;
            this.cbOffsetOnOff.Text = "Offset";
            this.cbOffsetOnOff.UseVisualStyleBackColor = true;
            this.cbOffsetOnOff.CheckedChanged += new System.EventHandler(this.cbOffsetOnOff_CheckedChanged);
            // 
            // txtCharCount
            // 
            this.txtCharCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCharCount.Location = new System.Drawing.Point(43, 91);
            this.txtCharCount.MaxLength = 5;
            this.txtCharCount.Name = "txtCharCount";
            this.txtCharCount.Size = new System.Drawing.Size(114, 20);
            this.txtCharCount.TabIndex = 8;
            this.txtCharCount.Text = "64";
            // 
            // btnGenerateChars
            // 
            this.btnGenerateChars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGenerateChars.Location = new System.Drawing.Point(43, 139);
            this.btnGenerateChars.Name = "btnGenerateChars";
            this.btnGenerateChars.Size = new System.Drawing.Size(114, 22);
            this.btnGenerateChars.TabIndex = 9;
            this.btnGenerateChars.Text = "Generate";
            this.btnGenerateChars.UseVisualStyleBackColor = true;
            this.btnGenerateChars.Click += new System.EventHandler(this.btnGenerateChars_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 24);
            this.label1.TabIndex = 11;
            this.label1.Text = "Ch:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 24);
            this.label2.TabIndex = 12;
            this.label2.Text = "E:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExtant
            // 
            this.txtExtant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExtant.Location = new System.Drawing.Point(43, 115);
            this.txtExtant.Name = "txtExtant";
            this.txtExtant.Size = new System.Drawing.Size(114, 20);
            this.txtExtant.TabIndex = 13;
            this.txtExtant.Text = "32";
            // 
            // cbEBCDIC
            // 
            this.cbEBCDIC.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.cbEBCDIC, 2);
            this.cbEBCDIC.Location = new System.Drawing.Point(23, 69);
            this.cbEBCDIC.Name = "cbEBCDIC";
            this.cbEBCDIC.Size = new System.Drawing.Size(65, 16);
            this.cbEBCDIC.TabIndex = 10;
            this.cbEBCDIC.Text = "EBCDIC";
            this.cbEBCDIC.UseVisualStyleBackColor = true;
            this.cbEBCDIC.CheckedChanged += new System.EventHandler(this.cbEBCDIC_CheckedChanged);
            // 
            // cbIncludeAscii
            // 
            this.cbIncludeAscii.AutoSize = true;
            this.cbIncludeAscii.Checked = true;
            this.cbIncludeAscii.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel5.SetColumnSpan(this.cbIncludeAscii, 2);
            this.cbIncludeAscii.Location = new System.Drawing.Point(23, 47);
            this.cbIncludeAscii.Name = "cbIncludeAscii";
            this.cbIncludeAscii.Size = new System.Drawing.Size(86, 16);
            this.cbIncludeAscii.TabIndex = 14;
            this.cbIncludeAscii.Text = "Include Ascii";
            this.cbIncludeAscii.UseVisualStyleBackColor = true;
            this.cbIncludeAscii.CheckedChanged += new System.EventHandler(this.cbIncludeAscii_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 569);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1214, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Delete.png");
            // 
            // DASD_Utilities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 591);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DASD_Utilities";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DASD_Utilities_FormClosing);
            this.Load += new System.EventHandler(this.DASD_Utilities_Load);
            this.Move += new System.EventHandler(this.DASD_Utilities_Resize);
            this.Resize += new System.EventHandler(this.DASD_Utilities_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.scDirectoryVolume.Panel1.ResumeLayout(false);
            this.scDirectoryVolume.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDirectoryVolume)).EndInit();
            this.scDirectoryVolume.ResumeLayout(false);
            this.scDesktopDirectoryVolume.Panel1.ResumeLayout(false);
            this.scDesktopDirectoryVolume.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDesktopDirectoryVolume)).EndInit();
            this.scDesktopDirectoryVolume.ResumeLayout(false);
            this.cmsDirectory.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpDirectory.ResumeLayout(false);
            this.tpCKDFBA.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tpVolume.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tpVTOC.ResumeLayout(false);
            this.tlpVTOC.ResumeLayout(false);
            this.tcVTOCEntries.ResumeLayout(false);
            this.tpDSNList.ResumeLayout(false);
            this.scDSNListMemberDSNData.Panel1.ResumeLayout(false);
            this.scDSNListMemberDSNData.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDSNListMemberDSNData)).EndInit();
            this.scDSNListMemberDSNData.ResumeLayout(false);
            this.cmsMemList.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.cmsDSNData.ResumeLayout(false);
            this.tpDSNMember.ResumeLayout(false);
            this.tlpDSNMembers.ResumeLayout(false);
            this.tcDSNMemberList.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.cmsDeleteTab.ResumeLayout(false);
            this.tlpDSNMemberList.ResumeLayout(false);
            this.tlpDSNMemberList.PerformLayout();
            this.tpDiagnostics.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tpHexCtrl.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer scDirectoryVolume;
        private System.Windows.Forms.SplitContainer scDesktopDirectoryVolume;
        private ExpTreeLib.ExpTree etvDesktop;
        private System.Windows.Forms.ColumnHeader chDirName;
        private System.Windows.Forms.ColumnHeader chDirAttributes;
        private System.Windows.Forms.ColumnHeader chDirSize;
        private System.Windows.Forms.ColumnHeader chDirType;
        private System.Windows.Forms.ColumnHeader chDirModifyDate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpDirectory;
        private System.Windows.Forms.TabPage tpVolume;
        private System.Windows.Forms.StatusStrip statusStrip1;
        internal System.Windows.Forms.ListView lvDirectory;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuFILEOpenHerculesConfig;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TabPage tpVTOC;
        private System.Windows.Forms.SplitContainer scDSNListMemberDSNData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.RichTextBox textMemberHexData;
        private System.Windows.Forms.TableLayoutPanel tlpDSNMemberList;
        internal System.Windows.Forms.ListView lvVolume;
        private System.Windows.Forms.ColumnHeader chVolName;
        private System.Windows.Forms.ColumnHeader chVolDevice;
        private System.Windows.Forms.ColumnHeader chVolBaseFile;
        private System.Windows.Forms.ColumnHeader chVolShadowFile;
        internal System.Windows.Forms.ListView lvVTOC_DSNList;
        private System.Windows.Forms.ColumnHeader chVTOCDSN;
        private System.Windows.Forms.ColumnHeader chVTOCOrg;
        private System.Windows.Forms.ColumnHeader chVTOCRefDt;
        private System.Windows.Forms.Label lblDSN;
        internal System.Windows.Forms.ListView lvMemberList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label lblMemberHexList;
        private System.Windows.Forms.ColumnHeader chVTOCRecFmt;
        internal System.Windows.Forms.ListView lvTrackList;
        private System.Windows.Forms.ColumnHeader chTrkIndex;
        private System.Windows.Forms.ColumnHeader chTrkCC;
        private System.Windows.Forms.ColumnHeader chTrkHH;
        private System.Windows.Forms.ColumnHeader chTrkDataSize;
        private System.Windows.Forms.ColumnHeader chTrkL1Tab;
        private System.Windows.Forms.ColumnHeader chTrkL2Tab;
        private System.Windows.Forms.ContextMenuStrip cmsDirectory;
        private System.Windows.Forms.ToolStripMenuItem msDirOpen;
        private System.Windows.Forms.ToolStripMenuItem msDirEdit;
        private System.Windows.Forms.ToolStripMenuItem msDirStart;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuViewLargeIcons;
        private System.Windows.Forms.ToolStripMenuItem mnuViewSmallIcons;
        private System.Windows.Forms.ToolStripMenuItem mnuViewList;
        private System.Windows.Forms.ToolStripMenuItem mnuViewDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuViewHexDumpOnly;
        private System.Windows.Forms.TabPage tpCKDFBA;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        internal System.Windows.Forms.ListView lvDASDInfo;
        private System.Windows.Forms.ColumnHeader chDASDStucture;
        private System.Windows.Forms.ColumnHeader chDASDOffset;
        private System.Windows.Forms.ColumnHeader chDASDSize;
        private System.Windows.Forms.ColumnHeader chDASDFilename;
        private System.Windows.Forms.ColumnHeader chTrkVFileName;
        private System.Windows.Forms.ContextMenuStrip cmsDSNData;
        private System.Windows.Forms.ToolStripMenuItem msDataCut;
        private System.Windows.Forms.ToolStripMenuItem msDataCopy;
        private System.Windows.Forms.ToolStripMenuItem msDataPaste;
        private System.Windows.Forms.ToolStripMenuItem msDataDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem msDataSave;
        private System.Windows.Forms.ContextMenuStrip cmsMemList;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnloadSave;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem msDataUndo;
        private System.Windows.Forms.ToolStripMenuItem msDataRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuViewRecordLength;
        private System.Windows.Forms.ToolStripTextBox mnuViewRecordLengthValue;
        private System.Windows.Forms.TabPage tpDiagnostics;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RichTextBox txtDiagnosticData;
        private System.Windows.Forms.Label lblDiagnosticInitiator;
        private System.Windows.Forms.CheckBox cbGenerateDiagnostics;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuViewDiagnostics;
        private System.Windows.Forms.TabPage tpHexCtrl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private HexControl hexData;
        private System.Windows.Forms.CheckBox cbHeaderOnOff;
        private System.Windows.Forms.CheckBox cbOffsetOnOff;
        private System.Windows.Forms.TextBox txtCharCount;
        private System.Windows.Forms.Button btnGenerateChars;
        private System.Windows.Forms.CheckBox cbEBCDIC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExtant;
        private HexControl textVolumeData;
        private HexControl textDASDInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label lblConfigName;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuEditClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCut;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuEditPaste;
        private System.Windows.Forms.ToolStripMenuItem mnuEditDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuEditSelectAll;
        private System.Windows.Forms.TabControl tcVTOCEntries;
        private System.Windows.Forms.TabPage tpDSNList;
        private System.Windows.Forms.TabPage tpDSNMember;
        private System.Windows.Forms.TableLayoutPanel tlpVTOC;
        private System.Windows.Forms.TabControl tcDSNMemberList;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tlpDSNMembers;
        private System.Windows.Forms.ColumnHeader chVTOCAllocTrk;
        private System.Windows.Forms.ColumnHeader chVTOCUsedTrk;
        private System.Windows.Forms.ColumnHeader chVTOCPct;
        private System.Windows.Forms.ColumnHeader chVTOCXT;
        private System.Windows.Forms.ColumnHeader chVTOCLRECL;
        private System.Windows.Forms.ColumnHeader chVTOCBLKSZ;
        private System.Windows.Forms.ColumnHeader chVTOCCreDt;
        private System.Windows.Forms.ColumnHeader chVTOCExpDt;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.CheckBox cbIncludeAscii;
        private System.Windows.Forms.ContextMenuStrip cmsDeleteTab;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnloadBinarySave;
    }
}

