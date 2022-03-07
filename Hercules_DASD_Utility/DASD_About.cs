/*
 * DASD_About: Form to display the About information for Hercules_DASD_Utilities.
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
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hercules_DASD_Utility
{
    partial class DASD_About : Form
    {
        public DASD_About()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", Global.AssemblyTitle);
            this.labelProductName.Text = Global.AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", Global.AssemblyVersion);
            this.labelCopyright.Text = Global.AssemblyCopyright;
            this.labelCompanyName.Text = Global.AssemblyCompany;
            this.textBoxDescription.Text = Global.AssemblyDescription;
        }
    }
}
