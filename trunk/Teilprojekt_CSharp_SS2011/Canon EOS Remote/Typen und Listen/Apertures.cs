using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class Apertures
    {
        private List<TAperture> apertureList;

        internal List<TAperture> ApertureList
        {
            get { return apertureList; }
            set { apertureList = value; }
        }

        public Apertures()
        {
            this.ApertureList = new List<TAperture>();
            init();
        }

        private void init()
        {
            this.ApertureList.Add(new TAperture("1", 0x08));
            this.ApertureList.Add(new TAperture("1.1", 0x0B));
            this.ApertureList.Add(new TAperture("1.2", 0x0C));
            this.ApertureList.Add(new TAperture("1.2", 0x0D));
            this.ApertureList.Add(new TAperture("1.4", 0x10));
            this.ApertureList.Add(new TAperture("1.6", 0x13));
            this.ApertureList.Add(new TAperture("1.8", 0x14));
            this.ApertureList.Add(new TAperture("1.8", 0x15));
            this.ApertureList.Add(new TAperture("2", 0x18));
            this.ApertureList.Add(new TAperture("2.2", 0x1B));
            this.ApertureList.Add(new TAperture("2.5", 0x1C));
            this.ApertureList.Add(new TAperture("2.5", 0x1D));
            this.ApertureList.Add(new TAperture("2.8", 0x20));
            this.ApertureList.Add(new TAperture("3.2", 0x23));
            this.ApertureList.Add(new TAperture("3.5", 0x24));
            this.ApertureList.Add(new TAperture("3.5", 0x25));
            this.ApertureList.Add(new TAperture("4", 0x28));
            this.ApertureList.Add(new TAperture("4.5", 0x2B));
            this.ApertureList.Add(new TAperture("4.5", 0x2C));
            this.ApertureList.Add(new TAperture("5.0", 0x2D));
            this.ApertureList.Add(new TAperture("5.6", 0x30));
            this.ApertureList.Add(new TAperture("6.3", 0x33));
            this.ApertureList.Add(new TAperture("6.7", 0x34));
            this.ApertureList.Add(new TAperture("7.1", 0x35));
            this.ApertureList.Add(new TAperture("8", 0x38));
            this.ApertureList.Add(new TAperture("9", 0x3B));
            this.ApertureList.Add(new TAperture("9.5", 0x3C));
            this.ApertureList.Add(new TAperture("10", 0X3D));
            this.ApertureList.Add(new TAperture("11", 0x40));
            this.ApertureList.Add(new TAperture("13", 0x43));
            this.ApertureList.Add(new TAperture("13", 0x44));
            this.ApertureList.Add(new TAperture("14", 0x45));
            this.ApertureList.Add(new TAperture("16", 0x48));
            this.ApertureList.Add(new TAperture("18", 0x4B));
            this.ApertureList.Add(new TAperture("19", 0x4C));
            this.ApertureList.Add(new TAperture("20", 0x4D));
            this.ApertureList.Add(new TAperture("22", 0x50));
            this.ApertureList.Add(new TAperture("25", 0x53));
            this.ApertureList.Add(new TAperture("27", 0x54));
            this.ApertureList.Add(new TAperture("29", 0x55));
            this.ApertureList.Add(new TAperture("32", 0x58));
            this.ApertureList.Add(new TAperture("36", 0x5B));
            this.ApertureList.Add(new TAperture("38", 0x5C));
            this.ApertureList.Add(new TAperture("40", 0x5D));
            this.ApertureList.Add(new TAperture("45", 0x60));
            this.ApertureList.Add(new TAperture("51", 0x63));
            this.ApertureList.Add(new TAperture("54", 0x64));
            this.ApertureList.Add(new TAperture("57", 0x65));
            this.ApertureList.Add(new TAperture("64", 0x68));
            this.ApertureList.Add(new TAperture("72", 0x6B));
            this.ApertureList.Add(new TAperture("76", 0x6C));
            this.ApertureList.Add(new TAperture("80", 0x6D));
            this.ApertureList.Add(new TAperture("91", 0x70));
            this.ApertureList.Add(new TAperture("unbekannt", 0xFFFFFFFF));
            this.ApertureList.Add(new TAperture("nicht verfügbar", 0));
        }

        public uint getApertureHex(string apertureString)
        {
            for (int i = 0; i < this.ApertureList.Count; i++)
            {
                if (this.ApertureList.ElementAt(i).ApertureString == apertureString)
                {
                    return this.ApertureList.ElementAt(i).ApertureHex;
                }
            }
            return 0x0;
        }

        public string getApertureString(UInt32 apertureHex)
        {
            for (int i = 0; i < this.ApertureList.Count; i++)
            {
                if (this.apertureList.ElementAt(i).ApertureHex == apertureHex)
                {
                    return this.ApertureList.ElementAt(i).ApertureString;
                }
            }
            return "unbekannt : " + apertureHex;
        }
    }
}
