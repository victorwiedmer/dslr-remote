using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.Typen_und_Listen
{
    class AFModes
    {
        private List<TAFMode> list;

        public List<TAFMode> List
        {
            get { return list; }
            set { list = value; }
        }

        public AFModes()
        {
            this.List = new List<TAFMode>();
            init();
        }

        private void init()
        {
            this.List.Add(new TAFMode("One Shot AF",0));
            this.List.Add(new TAFMode("AI Server AF",1));
            this.List.Add(new TAFMode("AI Focus AF",2));
            this.List.Add(new TAFMode("Manueller Fokus",3));
            this.List.Add(new TAFMode("Not Available",0xff));
        }

        public string getAfModeString(uint hex)
        {
            for(int i=0;i<this.List.Count;i++){
                if (this.List.ElementAt(i).AfModeHex == hex)
                {
                    return this.List.ElementAt(i).AfModeString;
                }
            }
            return "unknown : " + hex;
        }

        public uint getAFModeHex(string name)
        {
            for (int i = 0; i < this.List.Count;i++ )
            {
                if (this.List.ElementAt(i).AfModeString == name)
                {
                    return this.List.ElementAt(i).AfModeHex;
                }
            }
            return 0x0;
        }
    }
}
