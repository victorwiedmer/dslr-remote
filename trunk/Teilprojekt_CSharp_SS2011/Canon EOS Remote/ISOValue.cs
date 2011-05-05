using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote
{
    class ISOValue
    {
        private UInt32 decValue;

        public UInt32 DecValue
        {
            get { return decValue; }
            set { decValue = value; }
        }
        private UInt32 hexValue;

        public UInt32 HexValue
        {
            get { return hexValue; }
            set { hexValue = value; }
        }

        public ISOValue(UInt32 decValue, UInt32 hexValue)
        {
            this.decValue = decValue;
            this.hexValue = hexValue;
        }
    }
}
