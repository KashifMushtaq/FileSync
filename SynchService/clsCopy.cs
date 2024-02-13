using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Reflection;

namespace SynchServiceNS
{
    public static class clsCopy
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetShortPathName(string LongPath, StringBuilder ShortPath, int BufferSize);

        private static object m_Lock = new object();
        private static Logging m_Logger = new Logging();
        private static clsArguments m_arg;
        private static string m_ThreadStatus = string.Empty;

        public static FormMain.SetTextCallback LogMessageToForm;

        private static SynchronizationProvider m_SynchronizationProvider = null;

        public enum SynchType : int
        {
            SourceToDestinationSynch = 1,
            BothWaysSynch = 2,
            DestinationsToSourceSynch = 3
        }

        /// <summary>
        /// Called from service only
        /// </summary>
        /// <param name="arg"></param>
        public static void RunJob(clsArguments arg)
        {
            WriteLine(LOG.INFORMATION, string.Format("Creating Thread for args[{0}]", arg.getValueStringForINI()), true);

            //Thread m_Thread = new Thread(new ParameterizedThreadStart(SynchThreadCopy));
            Thread m_Thread = new Thread(new ParameterizedThreadStart(SyncThreadUsingSyncProvider));
            m_Thread.Name = arg.JobName;

            m_Logger.LogLevel = int.Parse(arg.LogLevel);
            m_Logger.EnableLogBuffer = false;
            m_Thread.Priority = ThreadPriority.Lowest;
            m_Thread.Start(arg);

            WriteLine(LOG.INFORMATION, string.Format("Thread Started [{0}] ", m_Thread.ManagedThreadId), true);
        }
        /// <summary>
        /// Called from Main Form
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="t"></param>
        public static void RunJob(clsArguments arg, SynchServiceNS.FormMain t)
        {
            WriteLine(LOG.INFORMATION, string.Format("Creating Thread for args[{0}]", arg.getValueStringForINI()), true);

            //Thread m_Thread = new Thread(new ParameterizedThreadStart(SynchThreadCopy));
            Thread m_Thread = new Thread(new ParameterizedThreadStart(SyncThreadUsingSyncProvider));

            m_Thread.Name = arg.JobName;

            m_Logger.LogLevel = int.Parse(arg.LogLevel);
            m_Logger.EnableLogBuffer = true;

            m_Thread.Priority = ThreadPriority.Lowest;
            m_Thread.Start(arg);

            WriteLine(LOG.INFORMATION, string.Format("Thread Started [{0}] ", m_Thread.ManagedThreadId), true);
            FormMain.SetTextCallback d = new FormMain.SetTextCallback(t.LogMessage);

            while (m_Thread.IsAlive)
            {
                t.Invoke(d, new object[] { m_Logger.LastLoggedMessage });

                Application.DoEvents();
            }

            //cleanup anything left
            string s = m_Logger.LastLoggedMessage;

        }


        private static void SyncThreadUsingSyncProvider(object arguments)
        {
            if (m_SynchronizationProvider == null) m_SynchronizationProvider = new SynchronizationProvider(m_Logger);

            try
            {
                clsArguments arg = (clsArguments)arguments;
                m_arg = arg;

                string jobSource = arg.JobSource;
                DeleteEmptyDirectories(jobSource);

                if (int.Parse(arg.SynchType) == (int)SynchType.SourceToDestinationSynch) // source to destination
                {
                    foreach (string destination in arg.JobDestinations)
                    {
                        if (!Directory.Exists(jobSource))
                        {
                            WriteLine(LOG.WARNING, string.Format("Source [{0}] could not be found. Can not continue.", jobSource), true);
                            continue;
                        }
                        if (!Directory.Exists(destination))
                        {
                            Directory.CreateDirectory(destination);
                        }

                        m_SynchronizationProvider.StartSynchronization(arg.JobSource, destination, m_arg.FileFiltersEx, m_arg.FileFiltersIn, m_arg.DirFiltersEx, false);

                        WriteLine(LOG.INFORMATION, string.Format("Source [{0}] --> Destination [{1}] Initiated", jobSource, destination), true);

                        DeleteEmptyDirectories(destination);
                    }
                }
                else if (int.Parse(arg.SynchType) == (int)SynchType.BothWaysSynch) // both ways
                {
                    foreach (string destination in arg.JobDestinations)
                    {
                        if (!Directory.Exists(jobSource))
                        {
                            WriteLine(LOG.WARNING, string.Format("Source [{0}] could not be found. Can not continue.", jobSource), true);
                            continue;
                        }

                        if (!Directory.Exists(destination))
                        {
                            Directory.CreateDirectory(destination);
                        }

                        m_SynchronizationProvider.StartSynchronization(arg.JobSource, destination, m_arg.FileFiltersEx, m_arg.FileFiltersIn, m_arg.DirFiltersEx, true);

                        WriteLine(LOG.INFORMATION, string.Format("Both Way [{0}] <--> [{1}] Sync Initiated", jobSource, destination), true);

                        DeleteEmptyDirectories(destination);
                    }
                }
                else if (int.Parse(arg.SynchType) == (int)SynchType.DestinationsToSourceSynch) // Destinations to source
                {
                    foreach (string destination in arg.JobDestinations)
                    {
                        if (!Directory.Exists(destination))
                        {
                            WriteLine(LOG.WARNING, string.Format("Destination [{0}] could not be found. Can not continue.", destination), true);
                            continue;
                        }
                        if (!Directory.Exists(arg.JobSource))
                        {
                            Directory.CreateDirectory(arg.JobSource);
                        }

                        m_SynchronizationProvider.StartSynchronization(destination, arg.JobSource, m_arg.FileFiltersEx, m_arg.FileFiltersIn, m_arg.DirFiltersEx, false);
                        WriteLine(LOG.INFORMATION, string.Format("Destination [{0}] --> Source [{1}] Initiated", destination, jobSource), true);

                        DeleteEmptyDirectories(destination);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine(LOG.ERROR, String.Format("SyncThreadUsingSyncProvider -> {0}", ex.Message));
            }
        }

        /// <summary>
        /// Procedure to delete empty directories
        /// </summary>
        /// <param name="startLocation"></param>
        private static void DeleteEmptyDirectories(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                DeleteEmptyDirectories(directory);
                if (Directory.GetFiles(directory).Length == 0 &&
                    Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                    WriteLine(LOG.WARNING, string.Format("DeleteEmptyDirectories -> [{0}] deleted", directory));
                }
            }
        }

        private static void WriteLine(LOG Level, string Message, bool bIgnoreLevel)
        {
            try
            {
                m_Logger.WriteToLog(Level, Message, bIgnoreLevel);
            }
            catch (Exception) { }
        }
        private static void WriteLine(LOG Level, string Message)
        {
            WriteLine(Level, Message, false);
        }
    }
}
