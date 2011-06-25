using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.Typen_und_Listen
{
    class TAFMode
    {
        private string afModeString;

        public string AfModeString
        {
            get { return afModeString; }
            set { afModeString = value; }
        }
        private uint afModeHex;

        public uint AfModeHex
        {
            get { return afModeHex; }
            set { afModeHex = value; }
        }

        public TAFMode(uint hex, string name)
        {
            this.AfModeHex = hex;
            this.AfModeString = name;
        }

        public TAFMode(string name, uint hex)
        {
            this.AfModeHex = hex;
            this.AfModeString = name;
        }
    }
}
