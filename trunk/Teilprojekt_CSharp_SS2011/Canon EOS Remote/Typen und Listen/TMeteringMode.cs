using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.Typen_und_Listen
{
    class TMeteringMode
    {
        private string meteringModeString;

        public string MeteringModeString
        {
            get { return meteringModeString; }
            set { meteringModeString = value; }
        }
        private uint meteringModeHex;

        public uint MeteringModeHex
        {
            get { return meteringModeHex; }
            set { meteringModeHex = value; }
        }

        public TMeteringMode(uint hex, string name)
        {
            this.MeteringModeHex = hex;
            this.MeteringModeString = name;
        }

        public TMeteringMode(string name, uint hex)
        {
            this.MeteringModeHex = hex;
            this.MeteringModeString = name;
        }
    }
}
