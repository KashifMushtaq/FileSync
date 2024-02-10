using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SynchServiceNS
{
    public enum LOG
    {
        DEBUG = 5,
        INFORMATION = 4,
        WARNING = 3,
        ERROR = 2,
        CATASTROPHIE = 1
    }
    public class Logging
    {
        private static int m_LogLevel = 4;
        private static string m_LogFile = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + "SynchService" + "-" + DateTime.Now.ToString("yyyy-MM-dd", null) + ".log";
        private static object lockObject = new object();
        private static List<string> m_LoggedMessages = new List<string>();
        private static string m_LastLoggedMessage = string.Empty;
        private  static bool m_StoreLogMessages = false;

        public Logging()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log\\"))
            {
                try
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log\\");
                }
                catch (Exception) { }
            }
        }
        public bool EnableLogBuffer
        {
            set
            {
                m_StoreLogMessages = value;
            }
        }
        public int LogLevel
        {
            set
            {
                m_LogLevel = value;
            }
            get
            {
                return m_LogLevel;
            }
        }
        public string LogFilePath
        {
            set
            {
                m_LogFile = value;
            }
            get
            {
                return m_LogFile;
            }
        }
        private string getCurrentLogFileName()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "Log\\" + "SynchService" + "-" + DateTime.Now.ToString("yyyy-MM-dd", null) + ".log";
        }
        /* 
         * Returns last 50 lines from current log file
         */
        public string getLogText()
        {
            m_LogFile = getCurrentLogFileName();

            if (!File.Exists(m_LogFile))
            {
                return string.Format("Sync Service is not Running or No Log File at [{0}]", m_LogFile);
            }

            string sR = string.Empty;
            try
            {
                lock (lockObject)
                {
                    string sLog = File.ReadAllText(m_LogFile, Encoding.ASCII);
                    string SPLIT = "\n";
                    string[] lines = sLog.Split(SPLIT[0]);
                    Array.Reverse(lines);

                    if (lines.Length > 50)
                    {
                        for (int i = 0; i < lines.Length; i++)
                        {
                            sR += lines[i] + "\n";
                            if (i >= 50)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < lines.Length; i++)
                        {
                            sR += lines[i] + "\n";
                        }
                    }
                }
            }
            catch(Exception)
            {
                //WriteToEventLog(EventLogEntryType.Error, ex.Message);
            }
            return sR;
        }
        public void WriteToLog(LOG LogLevel, string Message, bool bIgnoreLevel)
        {
            m_LogFile = getCurrentLogFileName();

            Message = Message.Replace("\r\n", " ");
            Message = Message.Replace("\n", " ");
            Message = Message.Replace("\r", " ");
            try
            {

                lock (lockObject)
                {
                    using (StreamWriter s = File.AppendText(m_LogFile))
                    {

                        string Level = "";
                        if (LogLevel == LOG.CATASTROPHIE)
                        {
                            Level = "CATASTROPHIE";
                            m_LastLoggedMessage = DateTime.Now.ToString("yyyy-MMM-dd:HH:mm:ss [zz]", null) + "\t" + Level + "\t" + Message;
                            if((int)m_LogLevel >= (int)LogLevel || bIgnoreLevel)s.WriteLine(m_LastLoggedMessage);
                            if (m_StoreLogMessages) m_LoggedMessages.Add(m_LastLoggedMessage);
                        }
                        else if (LogLevel == LOG.ERROR)
                        {
                            Level = "ERROR";
                            m_LastLoggedMessage = DateTime.Now.ToString("yyyy-MMM-dd:HH:mm:ss [zz]", null) + "\t" + Level + "\t\t" + Message;
                            if ((int)m_LogLevel >= (int)LogLevel || bIgnoreLevel) s.WriteLine(m_LastLoggedMessage);
                            if (m_StoreLogMessages) m_LoggedMessages.Add(m_LastLoggedMessage);
                        }
                        else if (LogLevel == LOG.WARNING)
                        {
                            Level = "WARNING";
                            m_LastLoggedMessage = DateTime.Now.ToString("yyyy-MMM-dd:HH:mm:ss [zz]", null) + "\t" + Level + "\t\t" + Message;
                            if ((int)m_LogLevel >= (int)LogLevel || bIgnoreLevel) s.WriteLine(m_LastLoggedMessage);
                            if (m_StoreLogMessages) m_LoggedMessages.Add(m_LastLoggedMessage);
                        }
                        else if (LogLevel == LOG.INFORMATION)
                        {
                            Level = "INFORMATION";
                            m_LastLoggedMessage = DateTime.Now.ToString("yyyy-MMM-dd:HH:mm:ss [zz]", null) + "\t" + Level + "\t" + Message;
                            if ((int)m_LogLevel >= (int)LogLevel || bIgnoreLevel) s.WriteLine(m_LastLoggedMessage);
                            if (m_StoreLogMessages) m_LoggedMessages.Add(m_LastLoggedMessage);
                        }
                        else if (LogLevel == LOG.DEBUG)
                        {
                            Level = "DEBUG";
                            m_LastLoggedMessage = DateTime.Now.ToString("yyyy-MMM-dd:HH:mm:ss [zz]", null) + "\t" + Level + "\t\t" + Message;
                            if ((int)m_LogLevel >= (int)LogLevel || bIgnoreLevel) s.WriteLine(m_LastLoggedMessage);
                            if (m_StoreLogMessages) m_LoggedMessages.Add(m_LastLoggedMessage);
                        }

                        s.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                WriteToEventLog(EventLogEntryType.Error, ex.Message);
            }
        }
        
        //returns what is current thread doing
        public string LastLoggedMessage
        {
            get
            {
                string sR = string.Empty;

                lock (lockObject)
                {
                    foreach(string s in m_LoggedMessages)
                    {
                        sR += s + "\n";
                    }
                    m_LoggedMessages = new List<string>();
                }
                return sR;
            }
        }

        public void WriteToLog(LOG LogLevel, string Message)
        {
            WriteToLog(LogLevel, Message, false);
        }

        public void ClearLogFile()
        {
            try
            {
                m_LogFile = getCurrentLogFileName();

                lock (lockObject)
                {
                    if (File.Exists(m_LogFile))
                    {
                        File.Delete(m_LogFile);
                    }

                    using (StreamWriter s = File.CreateText(m_LogFile))
                    {

                        string Level = "INFORMATION";
                        s.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd:HH:mm:ss [zz]", null) + "\t" + Level + "\t" + "Log File Cleaned.");
                        s.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(EventLogEntryType.Error, ex.Message);
                throw new Exception("To Clear Log File Run Synch Manager as Administrator");
            }
        }
        public void WriteToEventLog(System.Diagnostics.EventLogEntryType EntryType, string Message)
        {
            string source = "SynchService";
            try
            {
                if (!EventLog.SourceExists(source))
                {
                    EventLog.CreateEventSource(source, "Application");
                }
                EventLog.WriteEntry(source, Message, EntryType);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}