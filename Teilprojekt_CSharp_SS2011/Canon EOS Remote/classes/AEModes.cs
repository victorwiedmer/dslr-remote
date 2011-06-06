using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class AEModes
    {
        private List<TAEMode> aeModes;

        public AEModes()
        {
            this.aeModes = new List<TAEMode>();
            init();
        }

        private void init()
        {
            this.aeModes.Add(new TAEMode("Programm AE",0));
            this.aeModes.Add(new TAEMode("Shutter-Speed Priority AE",1));
            this.aeModes.Add(new TAEMode("Aperture-Priority AE",2));
            this.aeModes.Add(new TAEMode("Manual Exposure",3));
            this.aeModes.Add(new TAEMode("Bulb",4));
            this.aeModes.Add(new TAEMode("Auto Depth-of-Field AE",5));
            this.aeModes.Add(new TAEMode("Depth-of Field AE",6));
            this.aeModes.Add(new TAEMode("Camera settings registered",7));
            this.aeModes.Add(new TAEMode("Lock",8));
            this.aeModes.Add(new TAEMode("Auto",9));
            this.aeModes.Add(new TAEMode("Night Scene Portrait",10));
            this.aeModes.Add(new TAEMode("Sports",11));
            this.aeModes.Add(new TAEMode("Portrait",12));
            this.aeModes.Add(new TAEMode("Landscape",13));
            this.aeModes.Add(new TAEMode("Close-Up",14));
            this.aeModes.Add(new TAEMode("Flash Off",15));
            this.aeModes.Add(new TAEMode("Creative Auto",19));
            this.aeModes.Add(new TAEMode("Photo In Movie",21));
            this.aeModes.Add(new TAEMode("Not Valid or No settings changes",0xff));
        }

        public uint getAEHex(string AEstring) {
            for (int i = 0; i < this.aeModes.Count; i++)
            {
                if (this.aeModes.ElementAt(i).AeModeString == AEstring)
                {
                    return this.aeModes.ElementAt(i).AeModeHex;
                }
            }

            return 0x0;
        }

        public string getAEString(uint aeHex)
        {
            for (int i = 0; i < this.aeModes.Count; i++)
            {
                if (this.aeModes.ElementAt(i).AeModeHex == aeHex)
                {
                    return this.aeModes.ElementAt(i).AeModeString;
                }
            }

            return "unkown : " + aeHex;
        }
    }
}
