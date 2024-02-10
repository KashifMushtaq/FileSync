using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Threading;

namespace SynchServiceNS
{

    public partial class SynchService : ServiceBase
    {
        static private INI m_INI = new INI();
        static private Logging m_Logger = new Logging(); 
        static private object threadLock=new object();

        private const string SPLIT_CHAR_MAIN = "|";
        private const string SPLIT_CHAR = ",";
        private const string SPLIT_CHAR_EQUAL = "=";


        AutoResetEvent autoEventMonitor = new AutoResetEvent(false);
        AutoResetEvent autoEventMonitorLog = new AutoResetEvent(false);
        private static Timer m_monitorThread;

        public SynchService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            
            try
            {
                //create a timer thread which ticks every minutes
                m_monitorThread = new Timer(new TimerCallback(timer_Tick), autoEventMonitor, 10000, 60000);
                m_Logger.EnableLogBuffer = false;
                WriteLine(LOG.DEBUG, "Synch Service Started");
            }
            catch (Exception)
            {
            }
        }

        protected override void OnStop()
        {
            m_monitorThread.Dispose(autoEventMonitor);
            WriteLine(LOG.DEBUG, "Synch Service Stopped");
        }

        public static void timer_Tick(Object stateInfo)
        {
            string[] iniSECTION;
            WriteLine(LOG.DEBUG, "Reading INI file");

            lock (threadLock)
            {
                //INI File
                //Job_Name=Job Description|Source|Destination 1...n|FileFilterIn 1...n|FileFilterEx 1...n|DirFilterEx 1...n|SynchType|JobStatus|LastRun|RunAfter

                
                
                //read all jobs from ini file
                iniSECTION = m_INI.ReadSection();
            }



            if (iniSECTION == null)
            {
                WriteLine(LOG.INFORMATION, "No Jobs in INI File.");
                return;
            }
            
            List<string>Jobs = new List<string>();

            WriteLine(LOG.DEBUG, string.Format("Total Jobs [{0}]", iniSECTION.Length));
            //make job list
            foreach (string iniValue in iniSECTION)
            {
                string JobName = iniValue.Split(SPLIT_CHAR_EQUAL[0])[0];
                if (!string.IsNullOrEmpty(JobName))
                {
                    Jobs.Add(JobName.Trim());
                }
            }

            //get job data
            foreach (string Job in Jobs)
            {
                string JobValue = m_INI.IniReadValue(Job);

                if (!string.IsNullOrEmpty(JobValue))
                {
                    //INI File
                    //Job_Name=Job Description|Source|Destination 1...n|FileFilterIn 1...n|FileFilterEx 1...n|DirFilterEx 1...n|SynchType|JobStatus|LastRun|RunAfter
                    string[] JobData = JobValue.Split(SPLIT_CHAR_MAIN[0]);

                    WriteLine(LOG.DEBUG, string.Format("Parsing Job [{0}]", Job));

                    try
                    {
                        clsArguments arg=new clsArguments();
                        arg.LoadData(Job, JobData);
                        
                        m_Logger.LogLevel = int.Parse(arg.LogLevel);

                        if (arg.JobStatus == "1")
                        {
                            WriteLine(LOG.DEBUG, string.Format("Job [{0}] is Active", Job));
                            if (arg.UseRunAt == "1")
                            {
                                TimeSpan lastRun = TimeSpan.FromTicks(long.Parse(arg.JobLastRun));
                                TimeSpan timeSpanNow = TimeSpan.FromTicks(DateTime.Now.Ticks);
                                double intervalMinutes = double.Parse("60");
                                int hr = int.Parse(arg.RunAt.Split(':')[0]);
                                int min = int.Parse(arg.RunAt.Split(':')[1]);
                                
                                WriteLine(LOG.DEBUG, string.Format("TimeSpan Total Minutes Now[{0}])", timeSpanNow.TotalMinutes));
                                WriteLine(LOG.DEBUG, string.Format("TimeSpan Total Minutes Last Run[{0}]", lastRun.TotalMinutes));
                                WriteLine(LOG.DEBUG, string.Format("Now - Last Run[{0}] Minutes", (timeSpanNow.TotalMinutes - lastRun.TotalMinutes)));
                                WriteLine(LOG.DEBUG, string.Format("Job [{0}] Interval[{1}] Minutes)", Job, intervalMinutes));
                                WriteLine(LOG.DEBUG, string.Format("Job [{0}] Hr[{1}])", Job, hr));
                                WriteLine(LOG.DEBUG, string.Format("Job [{0}] Min[{1}])", Job, min));

                                if ((timeSpanNow.TotalMinutes - lastRun.TotalMinutes) > intervalMinutes && DateTime.Now.Hour == hr && DateTime.Now.Minute==min)
                                {
                                    WriteLine(LOG.DEBUG, string.Format("Thread Created for Job[{0}], Arguments [{1}]", Job, arg.getValueStringForINI()));
                                    clsCopy.RunJob(arg);
                                    arg.JobLastRun = timeSpanNow.Ticks.ToString();
                                    m_INI.IniWriteValue(Job, arg.getValueStringForINI());
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(arg.JobLastRun) || arg.JobLastRun == "0")
                                    {
                                        arg.JobLastRun = timeSpanNow.Ticks.ToString();
                                        m_INI.IniWriteValue(Job, arg.getValueStringForINI());
                                    }
                                    WriteLine(LOG.DEBUG, string.Format("Job[{0}] run is not due now", Job));
                                }
                            }
                            else
                            {
                                TimeSpan lastRun = TimeSpan.FromTicks(long.Parse(arg.JobLastRun));
                                double intervalMinutes = double.Parse(arg.RunAfter);
                                TimeSpan timeSpaninterval = TimeSpan.FromTicks(DateTime.Now.AddMinutes(intervalMinutes).Ticks);
                                TimeSpan timeSpanNow = TimeSpan.FromTicks(DateTime.Now.Ticks);

                                WriteLine(LOG.DEBUG, string.Format("TimeSpan Total Minutes Now[{0}])", timeSpanNow.TotalMinutes));
                                WriteLine(LOG.DEBUG, string.Format("TimeSpan Total Minutes Last Run[{0}]", lastRun.TotalMinutes));
                                WriteLine(LOG.DEBUG, string.Format("Now - Last Run[{0}] Minutes", (timeSpanNow.TotalMinutes - lastRun.TotalMinutes)));
                                WriteLine(LOG.DEBUG, string.Format("Job [{0}] Interval[{1}] Minutes)", Job, intervalMinutes));

                                if ((timeSpanNow.TotalMinutes - lastRun.TotalMinutes) > intervalMinutes)
                                {
                                    WriteLine(LOG.DEBUG, string.Format("Thread Created for Job[{0}], Arguments [{1}]", Job, arg.getValueStringForINI()));

                                    clsCopy.RunJob(arg);

                                    arg.JobLastRun = timeSpanNow.Ticks.ToString();
                                    m_INI.IniWriteValue(Job, arg.getValueStringForINI());
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(arg.JobLastRun) || arg.JobLastRun == "0")
                                    {
                                        arg.JobLastRun = timeSpanNow.Ticks.ToString();
                                        m_INI.IniWriteValue(Job, arg.getValueStringForINI());
                                    }
                                    WriteLine(LOG.DEBUG, string.Format("Job[{0}] run is not due now", Job));
                                }
                            }
                        }
                        else
                        {
                            WriteLine(LOG.INFORMATION, string.Format("Job [{0}] is Disabled", Job));
                        }
                    }
                    catch (Exception Ex)
                    {
                        //error in this job parameters, move to next job
                        WriteLine(LOG.ERROR, string.Format("Error in Running Job [{0}]", Job));
                        WriteLine(LOG.ERROR, string.Format("[{0}]", Ex.Message));
                        continue;
                    }
 
                }
            }

        }

        private static void WriteLine(LOG Level, string Message)
        {
            m_Logger.WriteToLog(Level, Message);
        }
    }
}
