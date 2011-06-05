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
            this.aeModes.Add(new TAEMode("Programm AE",0x0));
            this.aeModes.Add(new TAEMode("Shutter-Speed Priority AE",0x1));
            this.aeModes.Add(new TAEMode("Aperture-Priority AE",0x2));
            this.aeModes.Add(new TAEMode("Manual Exposure",0x3));
            this.aeModes.Add(new TAEMode("Bulb",0x4));
            this.aeModes.Add(new TAEMode("Auto Depth-of-Field AE",0x5));
            this.aeModes.Add(new TAEMode("Depth-of Field AE",0x6));
            this.aeModes.Add(new TAEMode("Camera settings registered",0x7));
            this.aeModes.Add(new TAEMode("Lock",0x8));
            this.aeModes.Add(new TAEMode("Auto",0x9));
            this.aeModes.Add(new TAEMode("Night Scene Portrait",0x10));
            this.aeModes.Add(new TAEMode("Sports",0x11));
            this.aeModes.Add(new TAEMode("Portrait",0x12));
            this.aeModes.Add(new TAEMode("Landscape",0x13));
            this.aeModes.Add(new TAEMode("Close-Up",0x14));
            this.aeModes.Add(new TAEMode("Flash Off",0x15));
            this.aeModes.Add(new TAEMode("Creative Auto",0x19));
            this.aeModes.Add(new TAEMode("Photo In Movie",0x21));
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

            return "unkown";
        }
    }
}
