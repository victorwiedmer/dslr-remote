using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.Typen_und_Listen
{
    class MeteringModes
    {
        private List<TMeteringMode> _meteringModesList;

        public List<TMeteringMode> MeteringModesList
        {
            get { return _meteringModesList; }
            set { _meteringModesList = value; }
        }

        public MeteringModes()
        {
            this.MeteringModesList = new List<TMeteringMode>();
            init();
        }

        private void init()
        {
            this.MeteringModesList.Add(new TMeteringMode("Spotmessung", 1));
            this.MeteringModesList.Add(new TMeteringMode("Mehrfeldmessung", 3));
            this.MeteringModesList.Add(new TMeteringMode("Selektivmessung", 4));
            this.MeteringModesList.Add(new TMeteringMode("Mittenbetonte Messung", 5));
            this.MeteringModesList.Add(new TMeteringMode("Not available", 0));
        }

        public string getMeteringModeString(uint hex)
        {
            for (int i = 0; i < this.MeteringModesList.Count; i++)
            {
                if (this.MeteringModesList.ElementAt(i).MeteringModeHex == hex)
                {
                    return this.MeteringModesList.ElementAt(i).MeteringModeString;
                }
            }
            return "unkown : " + hex;
        }

        public uint getMeteringModeHex(string name)
        {
            for (int i = 0; i < this.MeteringModesList.Count; i++)
            {
                if (this.MeteringModesList.ElementAt(i).MeteringModeString == name)
                {
                    return this.MeteringModesList.ElementAt(i).MeteringModeHex;
                }
            }
            return 0x0;
        }
    }
}
