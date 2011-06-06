using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class TAperture
    {
        private string apertureString;

        public string ApertureString
        {
            get { return apertureString; }
            set { apertureString = value; }
        }
        private UInt32 apertureHex;

        public UInt32 ApertureHex
        {
            get { return apertureHex; }
            set { apertureHex = value; }
        }

        public TAperture(string apertureString, uint apertureHex){
            this.ApertureString = apertureString;
            this.ApertureHex = apertureHex;
        }
    }
}
