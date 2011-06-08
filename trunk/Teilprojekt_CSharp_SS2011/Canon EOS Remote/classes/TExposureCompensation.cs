using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class TExposureCompensation
    {
        private string eBVString;

        public string EBVString
        {
            get { return eBVString; }
            set { eBVString = value; }
        }
        private uint eBVHex;

        public uint EBVHex
        {
            get { return eBVHex; }
            set { eBVHex = value; }
        }

        public TExposureCompensation(string eBVString , uint eBVHex){
            this.EBVHex = eBVHex;
            this.EBVString = eBVString;
        }
    }
}
