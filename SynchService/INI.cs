using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SynchServiceNS
{
    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>
    public class INI
    {
        public string m_PATH;
        public string SECTION = "JOBS";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileSection(string section, IntPtr lpReturnedString, int nSize, string lpFileName);


        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public INI(string INIPath)
        {
            m_PATH = INIPath;
        }
        public INI()
        {
            String sPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.ToLower();
            sPath = sPath.Replace(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName.ToLower(), "");
            m_PATH = sPath.Replace("file:///", "");
            //m_PATH = m_PATH.Replace("/bin/debug", "");
            //m_PATH = m_PATH.Replace("/bin/release", "") + "SynchService.ini";
            m_PATH += "SynchService.ini";
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.m_PATH);
        }
        public void IniWriteValue(string Key, string Value)
        {
            WritePrivateProfileString(SECTION, Key, Value, this.m_PATH);
        }
        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(4096);
            int i = GetPrivateProfileString(Section, Key, "", temp, 4096, this.m_PATH);
            return temp.ToString();
        }
        public string IniReadValue(string Key)
        {
            StringBuilder temp = new StringBuilder(4096);
            int i = GetPrivateProfileString(SECTION, Key, "", temp, 4096, this.m_PATH);
            return temp.ToString();
        }

        public int GetNextJobId()
        {
            string[] keys = ReadSection(SECTION);
            return keys.Length + 1;
        }

        public string IniReadValue(string Key, int DefaultValue)
        {
            StringBuilder temp = new StringBuilder(4096);
            int i = GetPrivateProfileString(SECTION, Key, DefaultValue.ToString(), temp, 4096, this.m_PATH);
            return temp.ToString();
        }

        /// <summary>
        /// Reads a whole section of the INI file.
        /// </summary>
        /// <param name="section">Section to read.</param>
        public string[] ReadSection(string section)
        {
            const int bufferSize = 8192;

            StringBuilder returnedString = new StringBuilder();

            IntPtr pReturnedString = Marshal.AllocCoTaskMem(bufferSize);
            try
            {
                int bytesReturned = GetPrivateProfileSection(section, pReturnedString, bufferSize, m_PATH);

                //bytesReturned -1 to remove trailing \0
                for (int i = 0; i < bytesReturned - 1; i++)
                    returnedString.Append((char)Marshal.ReadByte(new IntPtr((uint)pReturnedString + (uint)i)));
            }
            finally
            {
                Marshal.FreeCoTaskMem(pReturnedString);
            }

            string sectionData = returnedString.ToString();
            if (sectionData.Length > 0)
            {
                return sectionData.Split('\0');
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Reads a whole section of the INI file.
        /// </summary>
        public string[] ReadSection()
        {
            return ReadSection(SECTION);
        }
    }
}
