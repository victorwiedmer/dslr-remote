using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    /// <summary>
    /// Klasse des Types Belichtungszeit,
    /// speichert die Belichtungszeit in hexadezimalen Darstellung als Member
    /// und die Stringdarstellung als Member
    /// Version 1.0 ohne Fehlersicherheit in den setter Methoden
    /// </summary>
    class TShutterTime
    {
        private string shutterTimeString;

        public string ShutterTimeString
        {
            get { return shutterTimeString; }
            set { shutterTimeString = value; }
        }
        private uint shutterTimeHex;

        public uint ShutterTimeHex
        {
            get { return shutterTimeHex; }
            set { shutterTimeHex = value; }
        }

        public TShutterTime(string shutterTimeString, uint shutterTimeHex)
        {
            this.ShutterTimeString = shutterTimeString;
            this.ShutterTimeHex = shutterTimeHex;
        }

        public TShutterTime(uint shutterTimeHex, string shutterTimeString)
        {
            this.ShutterTimeString = shutterTimeString;
            this.ShutterTimeHex = shutterTimeHex;
        }
    }
}
