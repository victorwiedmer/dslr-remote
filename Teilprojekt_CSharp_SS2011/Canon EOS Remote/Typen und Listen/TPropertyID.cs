using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.Types
{
    class TPropertyID
    {
        private UInt32 propertyIDNumber;

        public UInt32 PropertyIDNumber
        {
            get { return propertyIDNumber; }
            set { propertyIDNumber = value; }
        }
        private String propertyIDString;

        public String PropertyIDString
        {
            get { return propertyIDString; }
            set { propertyIDString = value; }
        }

        public TPropertyID(string propidstring, UInt32 propidcode)
        {
            this.PropertyIDNumber = propidcode;
            this.PropertyIDString = propidstring;
        }
    }
}
