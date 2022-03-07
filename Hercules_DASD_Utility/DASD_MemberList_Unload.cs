/*
 * DASD_MemberList_Unload: Form to unload members from DASD partitioned datasets.
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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CustomExtensions;
using Hercules_DASD_Utility.Properties;

namespace Hercules_DASD_Utility
{
    public partial class DASD_MemberList_Unload : Form
    {
        private DSNEntry _dsnEntry;
        private List<MemberEntry> _memList;

        public DASD_MemberList_Unload()
        {
            InitializeComponent();

            MyInitializeComponent();
        }

        private void MyInitializeComponent()
        {
            cboxExtension.Items.Clear();

            for (int i = 0; i <= Global.extensions.GetUpperBound(0); i++)
            {
                cboxExtension.Items.Add(Global.extensions[i, 0]);
            }

            txtUnloadIntoFolder.Text = Global.folder;
        }

        public DSNEntry dsnEntry
        {
            get { return _dsnEntry; }
            set
            {
                _dsnEntry = value;

                this.Text = _dsnEntry.DSN;
            }
        }

        public List<MemberEntry> memList
        {
            get { return _memList; }
            set { _memList = value; }
        }

        private void cbConfirmUnload_CheckedChanged(object sender, EventArgs e)
        {
            cboxExtension.Enabled = !cbConfirmUnload.Checked;
            lblExtension.Enabled = !cbConfirmUnload.Checked;
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.OK;

            if ((cboxExtension.Text.Trim() == "") && (!cbConfirmUnload.Checked))
            {
                result = MessageBox.Show("No extension selected. Do you wish to continue?", "No extension", MessageBoxButtons.YesNo);
            }

            if ((result == DialogResult.OK) || (result == DialogResult.Yes))
            {
                if (Global.folder == "")
                {
                    DASD_Routines.SelectUnloadFolder();
                    txtUnloadIntoFolder.Text = Global.folder;
                }

                foreach (ListViewItem item in lvMemberUnloadList.CheckedItems)
                {
                    Global.SaveResults sr;
                    if (cboxExtension.Text.Trim() == "")
                    {
                        sr = DASD_Routines.Save_MemberEntry((MemberEntry)item.Tag);
                    }
                    else
                    {
                        sr = DASD_Routines.Save_MemberEntry((MemberEntry)item.Tag, cboxExtension.Text);
                    }
                    item.Checked = false;

                    switch (sr)
                    {
                        case Global.SaveResults.Fail:
                            item.SubItems[2].Text = "Failed";
                            break;
                        case Global.SaveResults.Success:
                            item.SubItems[2].Text = "Unloaded";
                            break;
                        case Global.SaveResults.Cancel:
                            item.SubItems[2].Text = "Canceled";
                            break;
                        default:
                            break;
                    }
                }

                lvMemberUnloadList.Columns[0].ImageIndex = 0;
            }
        }

        private void lvMemberUnloadList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lvMemberUnloadList.CheckedItems.Count > 0)
            {
                btnUnload.Enabled = true;
            }
            else
            {
                btnUnload.Enabled = false;
            }

            if (lvMemberUnloadList.CheckedItems.Count == lvMemberUnloadList.Items.Count)
            {
                lvMemberUnloadList.Columns[0].ImageIndex = 2;
            }
            else
            {
                lvMemberUnloadList.Columns[0].ImageIndex = 1;
            }
        }

        private void lvMemberUnloadList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                int imageIndex = lvMemberUnloadList.Columns[0].ImageIndex;
                foreach (ListViewItem item in lvMemberUnloadList.Items)
                {
                    if (imageIndex == 0)
                    {
                        item.Checked = true;
                    }
                    else
                    {
                        item.Checked = false;
                    }

                }

                if (imageIndex == 0)
                {
                    lvMemberUnloadList.Columns[0].ImageIndex = 2;
                }
                else
                {
                    lvMemberUnloadList.Columns[0].ImageIndex = 0;
                }
            }
        }

        private void DASD_MemberList_Unload_Shown(object sender, EventArgs e)
        {
            lvMemberUnloadList.Items.Clear();

            foreach (MemberEntry mem in _memList)
            {
                ListViewItem lvi = new ListViewItem("")
                {
                    Tag = mem
                };
                lvi.SubItems.Add(mem.member);
                lvi.SubItems.Add("...");
                lvMemberUnloadList.Items.Add(lvi);
            }

            lvMemberUnloadList.Columns[0].ImageIndex = 0;
        }

        private void btnUnloadInto_Click(object sender, EventArgs e)
        {
            DASD_Routines.SelectUnloadFolder();
            txtUnloadIntoFolder.Text = Global.folder;
        }
    }
}
