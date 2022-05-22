   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Hercules_DASD_Utility
{
	[XmlRoot(ElementName = "Main")]
    public class Main
    {
        private int _Width;
        private int _Height;
        private int _Left;
        private int _Top;
        private string _State;
        private int _VerticalSplit1Location;
        private int _HorizontalSplit1Location;
        private int _HorizontalSplit2Location;

        public int Width
        {
            get { return _Width; }
            set { _Width = value; _isDirty = true; }
        }
        public int Height
        {
            get { return _Height; }
            set { _Height = value; _isDirty = true; }
        }
        public int Left
        {
            get { return _Left; }
            set { _Left = value; _isDirty = true; }
        }
        public int Top
        {
            get { return _Top; }
            set { _Top = value; _isDirty = true; }
        }
        public string State
        {
            get { return _State; }
            set { _State = value; _isDirty = true; }
        }
        public int VerticalSplit1Location
        {
            get { return _VerticalSplit1Location; }
            set { _VerticalSplit1Location = value; _isDirty = true; }
        }
        public int HorizontalSplit1Location
        {
            get { return _HorizontalSplit1Location; }
            set { _HorizontalSplit1Location = value; _isDirty = true; }
        }
        public int HorizontalSplit2Location
        {
            get { return _HorizontalSplit2Location; }
            set { _HorizontalSplit2Location = value; _isDirty = true; }
        }

        private bool _isDirty = false;
        [XmlIgnore]
        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }
    }

    [XmlRoot(ElementName = "Window")]
	public class Window
	{
		[XmlElement(ElementName = "Main")]
		public Main main { get; set; }
        private bool _IsDirty;
        [XmlIgnore]
        public virtual bool IsDirty { get; set; }

        public Window()
        {
            main = new Main();
        }

    }

    [XmlRoot(ElementName = "Configuration")]
	public class XMLConfiguration
	{
		[XmlElement(ElementName = "Window")]
		public Window window { get; set; }

        public XMLConfiguration()
        {
            window = new Window();
        }

        private bool _IsDirty;
        [XmlIgnore]
        public virtual bool IsDirty
        {
            get
            {
                if (window.main.IsDirty)
                {
                    _IsDirty = true;
                }

                return _IsDirty;
            }
            set
            {
                _IsDirty = value;
                window.main.IsDirty = value;
            }
        }

        public static XMLConfiguration Load(string namepath)
        {
            XMLConfiguration cfg;
            var serializer = new XmlSerializer(typeof(XMLConfiguration));
            if (File.Exists(namepath))
            {
                using (var stream = new FileStream(namepath, FileMode.Open))
                {
                    cfg = serializer.Deserialize(stream) as XMLConfiguration;
                    cfg.IsDirty = false;
                    return cfg;
                }
            }
            cfg = new XMLConfiguration();
            cfg.window.main.Width = 1160;
            cfg.window.main.Height = 970;
            cfg.window.main.Left = 560;
            cfg.window.main.Top = 110;
            cfg.window.main.State = "Normal";
            cfg.window.main.VerticalSplit1Location = 460;
            cfg.window.main.HorizontalSplit1Location = 460;
            cfg.window.main.HorizontalSplit2Location = 460;
            cfg.IsDirty = false;
            return cfg;
        }

        public static string GetConfigFileName()
        {
            string sFileName;

            string commonAppDataPath = Application.CommonAppDataPath;
            //RegistryKey commonAppDataRegistry = Application.CommonAppDataRegistry;
            //string executablePath = Application.ExecutablePath;
            //string localUserAppDataPath = Application.LocalUserAppDataPath;
            //string startupPath = Application.StartupPath;
            //string userAppDataPath = Application.UserAppDataPath;
            //RegistryKey userAppDataRegistry = Application.UserAppDataRegistry;

            //string companyName = Application.CompanyName;
            string productName = Application.ProductName;
            string productVersion = Application.ProductVersion;

            sFileName = commonAppDataPath.Replace("dba ", "");
            sFileName = sFileName.Replace(productVersion, "");
            sFileName += productName + ".xml";

            return sFileName;
        }

        public void Save(string namepath)
        {
            char[] pSep = { '/', '\\' };
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(XMLConfiguration));
            if (namepath.IndexOfAny(pSep) >= 0)
            {
                string path = namepath.Substring(0, namepath.IndexOfAny(pSep));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            using (var stream = new FileStream(namepath, FileMode.Create))
            {
                serializer.Serialize(stream, this, ns);
            }
        }
    }

}