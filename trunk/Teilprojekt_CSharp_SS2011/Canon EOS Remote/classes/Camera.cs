using System;
using EDSDKLib;
using System.ComponentModel;
using Canon_EOS_Remote.classes;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System.Threading;

namespace Canon_EOS_Remote
{
    /// <summary>
    /// Klasse des Objektes Kamera
    /// </summary>
    class Camera : INotifyPropertyChanged
    {
        #region Declaration of class members

        private IntPtr _ptr;
        private String _name; //TODO setter and getter korrigieren
        private String _owner; //TODO setter and getter korrigieren
        private String _bodyID; //TODO setter and getter korrigieren

        private EDSDK.EdsTime _time; //TODO setter and getter korrigierenv
        private UInt32 _batteryLevel; //TODO setter and getter korrigieren
        private UInt32 _aeMode; //TODO setter and getter korrigieren
        private UInt32 _driveMode; //TODO setter and getter korrigieren

        private UInt32 _isoSpeed; //TODO setter and getter korrigieren
        private UInt32 _meteringMode; //TODO setter and getter korrigieren
        private UInt32 _afMode; //TODO setter and getter korrigieren
        private UInt32 _aperture; //TODO setter and getter korrigieren
        private UInt32 _shutterTime; //TODO setter and getter korrigieren
        private UInt32 _exposureCompensation; //TODO setter and getter korrigieren
        private UInt32 _availableShots;//TODO setter and getter korrigieren
        private String _currentStorage; //TODO setter and getter korrigieren
        private UInt32 Error;
        private String _cameraFirmware; //TODO setter and getter korrigieren
        private bool _lensAttached;

        public event PropertyChangedEventHandler PropertyChanged;

        private EDSDK.EdsPropertyDesc availableMeteringModes;
        private EDSDK.EdsPropertyDesc availableApertureValues;

        private EDSDK.EdsPropertyDesc availableShutterspeeds;
        private EDSDK.EdsPropertyDesc availableISOSpeeds;
        private EDSDK.EdsPropertyDesc availableExposureCompensation;

        private LiveView liveView;

        internal LiveView LiveView
        {
            get { return liveView; }
            set { liveView = value;
            update("LiveView");
            }
        }

        #endregion

        #region Setter and Getter of class member

        public EDSDK.EdsPropertyDesc AvailableApertureValues
        {
            get { return availableApertureValues; }
            set
            {
                availableApertureValues = value;
                update("AvailableApertureValues");
            }
        }

        public EDSDK.EdsPropertyDesc AvailableExposureCompensation
        {
            get { return availableExposureCompensation; }
            set { availableExposureCompensation = value; }
        }

        public EDSDK.EdsPropertyDesc AvailableISOSpeeds
        {
            get { return availableISOSpeeds; }
            set { availableISOSpeeds = value; }
        }
        private EDSDK.EdsPropertyDesc availableAEModes;

        public EDSDK.EdsPropertyDesc AvailableAEModes
        {
            get { return availableAEModes; }
            set { availableAEModes = value; }
        }


        public EDSDK.EdsPropertyDesc AvailableShutterspeeds
        {
            get { return availableShutterspeeds; }
            set { availableShutterspeeds = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                update("CameraName");
            }
        }

        public IntPtr Ptr
        {
            get { return _ptr; }
            set
            {
                _ptr = value;
                update("CameraPtr");
            }
        }

        public string Owner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                update("CameraOwner");
            }
        }

        /// <summary>
        /// Setter and Getter of classmember BodyID
        /// </summary>
        public string BodyID
        {
            get { return _bodyID; }
            set
            {
                _bodyID = value;
                update("CameraBodyID");
            }
        }

        public EDSDK.EdsTime CameraTime
        {
            get { return _time; }
            set
            {
                _time = value;
                update("CameraTime");
            }
        }

        public UInt32 CameraBatteryLevel
        {
            get { return _batteryLevel; }
            set
            {
                _batteryLevel = value;
                update("CameraBatteryLevel");
            }
        }

        public UInt32 CameraAEMode
        {
            get { return _aeMode; }
            set
            {
                _aeMode = value;
                update("CameraAEMode");
            }
        }

        public UInt32 CameraDriveMode
        {
            get { return _driveMode; }
            set
            {
                _driveMode = value;
                update("CameraDriveMode");
            }
        }

        public UInt32 CameraISOSpeed
        {
            get { return _isoSpeed; }
            set
            {
                _isoSpeed = value;
                update("CameraISOSpeed");
            }
        }

        public UInt32 CameraMeteringMode
        {
            get { return _meteringMode; }
            set
            {
                _meteringMode = value;
                update("CameraMeteringMode");
            }
        }

        public UInt32 CameraAFMode
        {
            get { return _afMode; }
            set
            {
                _afMode = value;
                update("CameraAFMode");
            }
        }

        public UInt32 CameraAperture
        {
            get { return _aperture; }
            set
            {
                _aperture = value;
                update("CameraAperture");
            }
        }

        public UInt32 CameraShutterTime
        {
            get { return _shutterTime; }
            set
            {
                _shutterTime = value;
                update("CameraShutterTime");
            }
        }

        public UInt32 CameraExposureCompensation
        {
            get { return _exposureCompensation; }
            set
            {
                _exposureCompensation = value;
                update("CameraExposureCompensation");
            }
        }

        public UInt32 CameraAvailableShots
        {
            get { return _availableShots; }
            set
            {
                _availableShots = value;
                update("CameraAvailableShots");
            }
        }

        public string CurrentStorage
        {
            get { return _currentStorage; }
            set
            {
                _currentStorage = value;
                update("CurrentStorage");
            }
        }

        public string CameraFirmware
        {
            get { return _cameraFirmware; }
            set
            {
                _cameraFirmware = value;
                update("CameraFirmware");
            }
        }
        #endregion

        private void startLiveView()
        {
            EDSDK.EdsSetPropertyData(this.Ptr, EDSDK.PropID_Evf_OutputDevice, 0, Marshal.SizeOf(EDSDK.EvfOutputDevice_PC), EDSDK.EvfOutputDevice_PC);
        }

        private void startLVThread()
        {
            Thread LV = new Thread(new ThreadStart(startLiveView));
            //LV.Start();
        }

        #region Constructors

        /// <summary>
        /// Konstruktor für das Objekt Kamera
        /// </summary>
        /// <param name="cameraPtr">Zeiger auf die Kamera der von der SDK zurückgegeben wurde</param>
        /// <remarks>Werte die nicht vom Konstruktor festgelegt wurden, werden automatisch während der Instanzierung ausgelesen</remarks>
        public Camera(IntPtr cameraPtr)
        {
            throw new NotImplementedException();
            //TODO implementieren
        }

        private void initFields()
        {
            startLVThread();
            getBatteryLevel();
            getAeMode();
            getDriveMode();
            getAfMode();
            getMeteringMode();
            getShutterTime();
            getIsoSpeed();
            getAvailableShots();
            getOwner();
            getName();
            getFirmwareVersion();
            getBodyID();
            getCurrentStorage();
            getavailableISOSpeedsFromCamera();
            getpropertyDescAeModes();
            getpropertyDescApertureValues();
            getpropertyDescExposureCompensation();
            getpropertyDescMeteringModes();
            getpropertyDescShutterTimes();
            getLensState();
            getApertureFromCamera();
            getTime();
            getEbvFromBody();
            LiveView = new LiveView(this.Ptr);
            //StartLV();
        }

        /// <summary>
        /// Konstruktor für das Objekt Kamera
        /// </summary>
        /// <param name="cameraPtr">Zeiger auf die Kamera der von der SDK zurückgegeben wurde</param>
        /// <param name="cameraName">Produkt Name der Kamera der aus der SDK ausgelesen wurde</param>
        /// <remarks>Werte die nicht vom Konstruktor festgelegt wurden, werden automatisch während der Instanzierung ausgelesen</remarks>
        public Camera(IntPtr cameraPtr, String cameraName)
        {
            this.Ptr = cameraPtr;
            this.Name = cameraName;
            if ((Error = EDSDK.EdsOpenSession(this.Ptr)) != 0)
            {
                publicError(Error);
            }
            initFields();
        }

        #endregion

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                Console.WriteLine("Property has changed from : " + this + " : " + property);
            }
        }

        #region camera methods

        /// <summary>
        /// Holt die aktuelle Einstellung für den Akkuladezustand aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getBatteryLevel()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this._ptr, EDSDKLib.EDSDK.PropID_BatteryLevel, 0, out this._batteryLevel)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für EBV aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getEbvFromBody()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this._ptr, EDSDKLib.EDSDK.PropID_ExposureCompensation, 0, out this._exposureCompensation)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für den AE Mode aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getAeMode()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_AEMode, 0, out this._aeMode)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für die Blende aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getApertureFromCamera()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_Av, 0, out this._aperture)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für Drive Mode aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getDriveMode()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_DriveMode, 0, out this._driveMode)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für AF Modus aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getAfMode()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_AFMode, 0, out this._afMode)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für Belichtungsmessung aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getMeteringMode()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_MeteringMode, 0, out this._meteringMode)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für Belichtungszeit aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getShutterTime()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_Tv, 0, out this._shutterTime)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für ISO aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getIsoSpeed()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_ISOSpeed, 0, out this._isoSpeed)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für die verfügbaren freien Fotos aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getAvailableShots()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_AvailableShots, 0, out this._availableShots)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für den Kamera Besitzer aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getOwner()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_OwnerName, 0, out this._owner)) != 0)
            {
                publicError(Error);
            }
            update("Owner");
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für den Produkt Namen aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getName()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_ProductName, 0, out this._name)) != 0)
            {
                publicError(Error);
            }
            update("Name");
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für die Firmware Version aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getFirmwareVersion()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_FirmwareVersion, 0, out this._cameraFirmware)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für die Gehäuse ID aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getBodyID()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_BodyIDEx, 0, out this._bodyID)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für den Speicherort aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getCurrentStorage()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.kEdsPropID_CurrentStorage, 0, out this._currentStorage)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die Tabelle der verfübaren ISO Werte aus der Kamera und speichert sie in den Klassemember 
        /// </summary>
        public void getavailableISOSpeedsFromCamera()
        {
            if ((Error = EDSDK.EdsGetPropertyDesc(this.Ptr, EDSDK.PropID_ISOSpeed, out this.availableISOSpeeds)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die Tabelle der verfübaren Programme aus der Kamera und speichert sie in den Klassemember 
        /// </summary>
        public void getpropertyDescAeModes()
        {
            if ((Error = EDSDK.EdsGetPropertyDesc(this.Ptr, EDSDK.PropID_AEMode, out this.availableAEModes)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die Tabelle der verfübaren Belichtungsmessungen aus der Kamera und speichert sie in den Klassemember 
        /// </summary>
        private void getpropertyDescMeteringModes()
        {
            if ((Error = EDSDK.EdsGetPropertyDesc(this.Ptr, EDSDK.PropID_MeteringMode, out this.availableMeteringModes)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die Tabelle der verfügbaren Blendenwerte von der Kamera und speichert sie in den Klassemember
        /// </summary>
        private void getpropertyDescApertureValues()
        {
            if ((Error = EDSDK.EdsGetPropertyDesc(this.Ptr, EDSDK.PropID_Av, out this.availableApertureValues)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die Tabelle der verfügbaren Belichtungszeiten von der Kamera und speichert sie in den Klassemember
        /// </summary>
        private void getpropertyDescShutterTimes()
        {
            if ((Error = EDSDK.EdsGetPropertyDesc(this.Ptr, EDSDK.PropID_Tv, out this.availableShutterspeeds)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die Tabelle der verfügbaren EBV von der Kamera und speichert sie in den Klassemember
        /// </summary>
        private void getpropertyDescExposureCompensation()
        {
            if ((Error = EDSDK.EdsGetPropertyDesc(this.Ptr, EDSDK.PropID_ExposureCompensation, out this.availableExposureCompensation)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die Einstellung für den Objektivstatus(angeschlossen) von der Kamera und speichert sie in den Klassemember
        /// </summary>
        public void getLensState()
        {
            UInt32 tmpProperty = 0;
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_LensStatus, 0, out tmpProperty)) == 0)
            {
                if (tmpProperty == 0x1)
                {
                    this._lensAttached = true;
                }
                else
                {
                    this._lensAttached = false;
                }
            }
            else
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die Einstellung für die Zeit von der Kamera und speichert sie in den Klassemember
        /// </summary>
        public void getTime()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_DateTime, 0, out this._time))!= 0)
            {
                publicError(Error);
            }
        }

        public void setISOSpeedToCamera(int isoSpeed)
        {
            EDSDK.EdsSetPropertyData(this.Ptr, EDSDK.PropID_ISOSpeed, 0, sizeof(int), isoSpeed);
        }

        public void setShutterTimeToCamera(int shutterTime)
        {
            EDSDK.EdsSetPropertyData(this.Ptr, EDSDK.PropID_Tv, 0, sizeof(int), shutterTime);
        }

        public void setAEModeToCamera(int aeMode)
        {
            EDSDK.EdsSetPropertyData(this.Ptr, EDSDK.PropID_AEMode, 0, sizeof(int), aeMode);
        }

        public void setApertureToCamera(int aperture)
        {
            EDSDK.EdsSetPropertyData(this.Ptr, EDSDK.PropID_Av, 0, sizeof(int), aperture);
        }

        public void setEbvToCamera(int ebv)
        {
            EDSDK.EdsSetPropertyData(this.Ptr, EDSDK.PropID_ExposureCompensation,0, sizeof(int), ebv);
        }

        /// <summary>
        /// Wenn innerhalb der Methoden des Objektes Fehler auftreten, werden die von dieser Methode veröffentlicht
        /// </summary>
        /// <param name="error">ErrorCode der SDK</param>
        private void publicError(uint error)
        {
            Console.WriteLine("An error has oocured : " + ErrorCodes.getErrorDataWithCodeNumber(error));
        }

        #endregion

        private Thread LVThread;
        private bool LVrunning = false;
        private bool LVinit = false;


        public void StartLV()
        {
            if (!LVinit)
            {
                LiveView.startLV();

                LVinit = true;

                ContinueLV();
            }
        }

        private void ContinueLV()
        {
            if (!LVrunning & LVinit)
            {
                LVThread = new Thread(new ThreadStart(LiveView.Run));
                LVThread.Start();

                LVrunning = true;
            }
        }

        public void StopLV()
        {
            if (LVrunning)
            {
                LVrunning = false;
                LVinit = false;
                LiveView.pauseLV();
                LiveView.stopLV();
                LVThread.Join();
                EDSDK.EdsSetPropertyData(this.Ptr, (uint)EDSDK.PropID_Evf_OutputDevice, 0, Marshal.SizeOf((uint)EDSDK.EvfOutputDevice_PC), (uint)EDSDK.EvfOutputDevice_TFT);
            }
        }


    }
}
