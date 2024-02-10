using System;

namespace SynchServiceNS
{
    public class clsArguments
    {
        private string m_JobName;
        private string m_JobDescription;
        private string m_JobSource;
        private string[] m_JobDestinations;
        private string[] m_FileFiltersIn;
        private string[] m_FileFiltersEx;
        private string[] m_DirFiltersEx;
        private string m_SynchType;
        private string m_JobStatus;
        private string m_JobLastRun;
        private string m_RunAfter;
        private string m_LogLevel;
        private string m_UseSynchFramework;
        private string m_UseRunAt;
        private string m_RunAt;
        
        private const string SPLIT_CHAR_MAIN = "|";
        private const string SPLIT_CHAR = ",";

        public clsArguments()
        { }

        public string JobName
        {
            get { return m_JobName; }
            set { m_JobName = value; }
        }
        public string JobDescription
        {
            get { return m_JobDescription; }
            set { m_JobDescription = value; }
        }
        public string JobSource
        {
            get { return m_JobSource; }
            set { m_JobSource = value; }
        }
        public string[] JobDestinations
        {
            get { return m_JobDestinations; }
            set { m_JobDestinations = value; }
        }
        public string[] FileFiltersIn
        {
            get { return m_FileFiltersIn; }
            set { m_FileFiltersIn = value; }
        }
        public string[] FileFiltersEx
        {
            get { return m_FileFiltersEx; }
            set { m_FileFiltersEx = value; }
        }
        public string[] DirFiltersEx
        {
            get { return m_DirFiltersEx; }
            set { m_DirFiltersEx = value; }
        }
        public string SynchType
        {
            get { return m_SynchType; }
            set { m_SynchType = value; }
        }
        public string JobStatus
        {
            get { return m_JobStatus; }
            set { m_JobStatus = value; }
        }
        public string JobLastRun
        {
            get { return m_JobLastRun; }
            set { m_JobLastRun = value; }
        }
        public string RunAfter
        {
            get { return m_RunAfter; }
            set { m_RunAfter = value; }
        }
        public string LogLevel
        {
            get { return m_LogLevel; }
            set { m_LogLevel = value; }
        }
        public string UseSynchFramework
        {
            get { return m_UseSynchFramework; }
            set { m_UseSynchFramework = value; }
        }

        public string UseRunAt
        {
            get { return m_UseRunAt; }
            set { m_UseRunAt = value; }
        }
        public string RunAt
        {
            get { return m_RunAt; }
            set { m_RunAt = value; }
        }
        public string getValueStringForINI()
        {
            //Job_Name=Job Description|Source|Destination 1...n|FileFilterIn 1...n|FileFilterEx 1...n|DirFilterEx 1...n|SynchType|JobStatus|LastRun|RunAfter
            return m_JobDescription + SPLIT_CHAR_MAIN +
               m_JobSource + SPLIT_CHAR_MAIN +
               getConcatinated(m_JobDestinations) + SPLIT_CHAR_MAIN +
               getConcatinated(m_FileFiltersIn) + SPLIT_CHAR_MAIN +
               getConcatinated(m_FileFiltersEx) + SPLIT_CHAR_MAIN +
               getConcatinated(m_DirFiltersEx) + SPLIT_CHAR_MAIN +
               m_SynchType + SPLIT_CHAR_MAIN +
               m_JobStatus + SPLIT_CHAR_MAIN +
               m_JobLastRun + SPLIT_CHAR_MAIN +
               m_RunAfter + SPLIT_CHAR_MAIN +
               m_LogLevel + SPLIT_CHAR_MAIN +
               m_UseSynchFramework + SPLIT_CHAR_MAIN +
               m_UseRunAt + SPLIT_CHAR_MAIN +
               m_RunAt;
        }
        private string getConcatinated(string[] CommaSeparatedValues)
        {
            string sR = string.Empty;

          
            if (CommaSeparatedValues == null)
            {
                return sR;
            }

            foreach (string sVal in CommaSeparatedValues)
            {
                if (!string.IsNullOrEmpty(sR))
                {
                    sR += SPLIT_CHAR;
                }

                sR += sVal;
            }
            return sR;
        }
        public bool LoadData(string Job, string[] JobData)
        {
            bool bR=false;

            try
            {
                this.m_JobName = Job;
                this.m_JobDescription = JobData[0];
                this.m_JobSource = JobData[1];

                this.m_JobDestinations = JobData[2].Split(SPLIT_CHAR[0]);

                if (JobData[3].IndexOf(SPLIT_CHAR) != -1 || !string.IsNullOrEmpty(JobData[3]))
                {
                    this.m_FileFiltersIn = JobData[3].Split(SPLIT_CHAR[0]);
                }
                else
                {
                    this.m_FileFiltersIn = null;
                }


                if (JobData[4].IndexOf(SPLIT_CHAR) != -1 || !string.IsNullOrEmpty(JobData[4]))
                {
                    this.m_FileFiltersEx = JobData[4].Split(SPLIT_CHAR[0]);
                }
                else
                {
                    this.m_FileFiltersEx = null;
                }

                if (JobData[5].IndexOf(SPLIT_CHAR) != -1 || !string.IsNullOrEmpty(JobData[5]))
                {
                    this.m_DirFiltersEx = JobData[5].Split(SPLIT_CHAR[0]);
                }
                else
                {
                    this.m_DirFiltersEx = null;
                }


                this.m_SynchType = JobData[6];
                this.m_JobStatus = JobData[7];
                this.m_JobLastRun = JobData[8];
                this.m_RunAfter = JobData[9];
                this.m_LogLevel = JobData[10];
                this.m_UseSynchFramework = JobData[11];
                this.m_UseRunAt = JobData[12];
                this.m_RunAt = JobData[13];
                bR = true;
            }
            catch (Exception)
            {
            }
            return bR;
        }
    }
}
