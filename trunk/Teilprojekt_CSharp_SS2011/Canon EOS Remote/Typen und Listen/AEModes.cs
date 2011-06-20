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
            this.aeModes.Add(new TAEMode("Tv - Blendenautomatik",1));
            this.aeModes.Add(new TAEMode("Av - Zeitautomatik",2));
            this.aeModes.Add(new TAEMode("M - Manuell",3));
            this.aeModes.Add(new TAEMode("Bulb - Langzeitbelichtung",4));
            this.aeModes.Add(new TAEMode("Auto Depth-of-Field AE",5));
            this.aeModes.Add(new TAEMode("Depth-of Field AE",6));
            this.aeModes.Add(new TAEMode("Camera settings registered",7));
            this.aeModes.Add(new TAEMode("Lock",8));
            this.aeModes.Add(new TAEMode("Vollautomatik",9));
            this.aeModes.Add(new TAEMode("Nachtporträt",10));
            this.aeModes.Add(new TAEMode("Sport",11));
            this.aeModes.Add(new TAEMode("Porträt",12));
            this.aeModes.Add(new TAEMode("Landschaft",13));
            this.aeModes.Add(new TAEMode("Nahaufnahme",14));
            this.aeModes.Add(new TAEMode("Blitz aus",15));
            this.aeModes.Add(new TAEMode("CA - Kreativautomatik",19));
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
            int count = this.aeModes.Count; // 20.6.2011 moved from loop to increase performance
            TAEMode temporaryAeMode; // to advoid two calls to increase performance
            for (int i = 0; i < count; i++)
            {
                temporaryAeMode = this.aeModes.ElementAt(i);
                if (temporaryAeMode.AeModeString == AEstring)
                {
                    return temporaryAeMode.AeModeHex;
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
            int count = this.aeModes.Count; // 20.6.2011 moved from loop to increase performance
            TAEMode temporaryAeMode; // to advoid two calls to increase performance
            for (int i = 0; i < count; i++)
            {
                temporaryAeMode = this.aeModes.ElementAt(i);
                if (temporaryAeMode.AeModeHex == aeHex)
                {
                    return temporaryAeMode.AeModeString;
                }
            }

            return "unkown : " + aeHex;
        }
    }
}
