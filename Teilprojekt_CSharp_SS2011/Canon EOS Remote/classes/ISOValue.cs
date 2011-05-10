using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote
{
    class ISOValue
    {
        #region Declaration of class members

        private UInt32 decValue;
        private UInt32 hexValue;

        #endregion

        #region Getter and Setter of class members

        public UInt32 HexValue
        {
            get { return hexValue; }
            set { hexValue = value; }
        }

        public UInt32 DecValue
        {
            get { return decValue; }
            set { decValue = value; }
        }

        #endregion

        #region Construtors

        public ISOValue(UInt32 decValue, UInt32 hexValue)
        {
            this.decValue = decValue;
            this.hexValue = hexValue;
        }

        #endregion
    }
}
