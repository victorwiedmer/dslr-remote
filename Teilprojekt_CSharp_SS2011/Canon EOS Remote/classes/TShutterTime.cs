using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class TShutterTime
    {
        private string shutterTimeDec;

        public string ShutterTimeDec
        {
            get { return shutterTimeDec; }
            set { shutterTimeDec = value; }
        }
        private uint shutterTimeHex;

        public uint ShutterTimeHex
        {
            get { return shutterTimeHex; }
            set { shutterTimeHex = value; }
        }

        public TShutterTime(string shutterTimeString, uint shutterTimeHex)
        {
            this.ShutterTimeDec = shutterTimeString;
            this.ShutterTimeHex = shutterTimeHex;
        }
    }
}
