using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    /// <summary>
    /// Klasse die eine Liste der AE Modi die von Canon EOS Digital Kameras beherrscht werden
    /// enthält und von Hex-String oder String-Hex konvertieren kann
    /// </summary>
    class AEModes
    {
        private List<TAEMode> aeModes;

        /// <summary>
        /// Konstruktor der Liste die alle AE Modi erzeugt
        /// </summary>
        public AEModes()
        {
            this.aeModes = new List<TAEMode>();
            init();
        }

        private void init()
        {
            this.aeModes.Add(new TAEMode("P - Programmautomatik",0));
            this.aeModes.Add(new TAEMode("Tv - Belichtungsautomatik",1));
            this.aeModes.Add(new TAEMode("Av - Blendenautomatik",2));
            this.aeModes.Add(new TAEMode("M - Manueller Modus",3));
            this.aeModes.Add(new TAEMode("Bulb",4));
            this.aeModes.Add(new TAEMode("Auto Depth-of-Field AE",5));
            this.aeModes.Add(new TAEMode("Depth-of Field AE",6));
            this.aeModes.Add(new TAEMode("Camera settings registered",7));
            this.aeModes.Add(new TAEMode("Lock",8));
            this.aeModes.Add(new TAEMode("Vollautomatikmodus",9));
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

        /// <summary>
        /// Gibt den Hex-Code der AE Mode Beschreibung zurück
        /// </summary>
        /// <param name="AEstring">Der String des AE Modes von dem der Hex-Code gesucht werden soll</param>
        /// <example>uint AEMode=getAEHex("Reihenaufnahme")</example>
        /// <returns>uint AE Mode</returns>
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

        /// <summary>
        /// Gibt den String der AE Mode Beschreibung zurück
        /// </summary>
        /// <param name="aeHex">Der Hex-Code des AE Modes von dem der String gesucht werden soll</param>
        /// <example>string AEMode=getAEString(1)</example>
        /// <returns>String AE Mode</returns>
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
