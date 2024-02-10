using System;
using System.Collections.Generic;
using System.Text;

namespace SynchServiceNS
{
    class MyEventArgs
    {
        private string m_Message;
        
        public MyEventArgs()
        { 
        }
        
        public string Message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }
    }
}
