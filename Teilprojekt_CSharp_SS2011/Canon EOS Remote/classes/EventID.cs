using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class EventID
    {
        private string eventIDString;

        public string EventIDString
        {
            get { return eventIDString; }
            set { eventIDString = value; }
        }
        private UInt32 eventIDCode;

        public UInt32 EventIDCode
        {
            get { return eventIDCode; }
            set { eventIDCode = value; }
        }

        public EventID(string eventIDString, UInt32 eventIDCode)
        {
            this.EventIDCode = eventIDCode;
            this.EventIDString = eventIDString;
        }

    }
}
