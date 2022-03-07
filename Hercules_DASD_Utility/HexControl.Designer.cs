namespace Hercules_DASD_Utility
{
    partial class HexControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtHexData = new System.Windows.Forms.RichTextBox();
            this.txtOffsetHeader = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtHexData, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtOffsetHeader, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 146);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtHeader
            // 
            this.txtHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.txtHeader, 2);
            this.txtHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHeader.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeader.Location = new System.Drawing.Point(3, 3);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(478, 18);
            this.txtHeader.TabIndex = 0;
            this.txtHeader.Text = "Line 1";
            // 
            // txtHexData
            // 
            this.txtHexData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.txtHexData, 2);
            this.txtHexData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHexData.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHexData.Location = new System.Drawing.Point(3, 43);
            this.txtHexData.Name = "txtHexData";
            this.txtHexData.Size = new System.Drawing.Size(478, 100);
            this.txtHexData.TabIndex = 4;
            this.txtHexData.Text = "";
            this.txtHexData.WordWrap = false;
            this.txtHexData.VScroll += new System.EventHandler(this.txtHexData_VScroll);
            // 
            // txtOffsetHeader
            // 
            this.txtOffsetHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.txtOffsetHeader, 2);
            this.txtOffsetHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOffsetHeader.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOffsetHeader.Location = new System.Drawing.Point(3, 23);
            this.txtOffsetHeader.Name = "txtOffsetHeader";
            this.txtOffsetHeader.Size = new System.Drawing.Size(478, 18);
            this.txtOffsetHeader.TabIndex = 3;
            this.txtOffsetHeader.Text = "";
            // 
            // HexControl
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "HexControl";
            this.Size = new System.Drawing.Size(484, 146);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HexControl_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HexControl_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HexControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HexControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HexControl_MouseUp);
            this.Resize += new System.EventHandler(this.HexControl_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtOffsetHeader;
        private System.Windows.Forms.RichTextBox txtHexData;
    }
}
