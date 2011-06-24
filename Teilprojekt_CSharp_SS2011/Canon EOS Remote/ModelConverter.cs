using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canon_EOS_Remote.classes;

namespace Canon_EOS_Remote
{
    class ModelConverter
    {
        #region Konverter fuer die Umwandlung von String zu Hexadezimal
        private ISOSpeeds isoConverter;

        public ISOSpeeds IsoConverter
        {
            get { return isoConverter; }
            set
            {
                isoConverter = value;
            }
        }
        private ShutterTimes shutterTimeConverter;

        public ShutterTimes ShutterTimeConverter
        {
            get { return shutterTimeConverter; }
            set { shutterTimeConverter = value; }
        }
        private AEModes aeModeConverter;

        public AEModes AeModeConverter
        {
            get { return aeModeConverter; }
            set { aeModeConverter = value; }
        }
        private ExposureCompensation ebvConverter;

        public ExposureCompensation EbvConverter
        {
            get { return ebvConverter; }
            set { ebvConverter = value; }
        }
        private Apertures apertureConverter;

        public Apertures ApertureConverter
        {
            get { return apertureConverter; }
            set { apertureConverter = value; }
        }
        private PropertyCodes propertyCodesConverter;

        public PropertyCodes PropertyCodesConverter
        {
            get { return propertyCodesConverter; }
            set { propertyCodesConverter = value; }
        }

        private ErrorCodes errorCodeConverter;

        public ErrorCodes ErrorCodeConverter
        {
            get { return errorCodeConverter; }
            set { errorCodeConverter = value; }
        }

        #endregion

        public ModelConverter()
        {
            initConverter();
            //TODO checkInit() implementieren
        }

        private void initConverter()
        {
            this.AeModeConverter = new AEModes();
            this.ApertureConverter = new Apertures();
            this.EbvConverter = new ExposureCompensation();
            this.IsoConverter = new ISOSpeeds();
            this.PropertyCodesConverter = new PropertyCodes();
            this.ShutterTimeConverter = new ShutterTimes();
            this.ErrorCodeConverter = new ErrorCodes();
        }

        /// <summary>
        /// Prueft ob der Initialisierungsprozess korrekt abgeschlossen wurde und gibt das Ergebnis als boolean Wert zurueck
        /// </summary>
        /// <returns>Wert der angibt ob der Initialisierungsprozess korrekt ausgefuehrt wurde</returns>
        private bool checkInit()
        {
            bool returnValue = false;
            if (this.AeModeConverter != null)
            {
                returnValue = true;
            }
            if (this.ApertureConverter != null)
            {
                returnValue = returnValue && true;
            }
            if(this.EbvConverter!=null){
                returnValue = returnValue && true;
            }
            if (this.IsoConverter != null)
            {
                returnValue = returnValue && true;
            }
            if (this.PropertyCodesConverter != null)
            {
                returnValue = returnValue && true;
            }
            if (this.ShutterTimeConverter != null)
            {
                returnValue = returnValue && true;
            }
            if (this.ErrorCodeConverter != null)
            {
                returnValue = returnValue && true;
            }
            return returnValue;
        }
    }
}
