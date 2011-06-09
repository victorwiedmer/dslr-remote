using System;
/*
 * Added on 09-05-2011 12:38 to simplify using of the canon sdk.
 * Now, every command , EDSDKLib namespace havent to be written.
 * */
using EDSDKLib;
using System.ComponentModel;
using Canon_EOS_Remote.classes;

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

        private EDSDK.EdsTime _time;
        private UInt32 _batteryLevel;
        private UInt32 _aeMode;
        private UInt32 _driveMode;

        private UInt32 _cameraISOSpeed;
        private UInt32 _cameraMeteringMode;
        private UInt32 _cameraAFMode;
        private UInt32 _cameraAperture;
        private UInt32 _cameraShutterTime;
        private UInt32 _cameraExposureCompensation;
        private UInt32 _cameraAvailableShots;
        private String _currentStorage; //TODO setter and getter korrigieren
        private UInt32 Error;
        private String tmpErrorString; //TODO setter and getter korrigieren
        private String _cameraFirmware; //TODO setter and getter korrigieren
        private bool _lensAttached;

        public event PropertyChangedEventHandler PropertyChanged;
        private EDSDK.EdsPropertyEventHandler cameraPropertyEventHandler;
        private EDSDK.EdsStateEventHandler cameraStateEventHandler;
        private EDSDK.EdsObjectEventHandler cameraObjectEventHandler;

        private DriveModes driveModes;
        private ISOSpeeds isoList;
        private PropertyCodes propertyCodes;
        private EventCodes eventIDs;

        private EDSDK.EdsPropertyDesc availableMeteringModes;
        private EDSDK.EdsPropertyDesc availableApertureValues;

        private EDSDK.EdsPropertyDesc availableShutterspeeds;
        private EDSDK.EdsPropertyDesc availableISOSpeeds;
        private EDSDK.EdsPropertyDesc availableExposureCompensation;
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
            get { return _cameraISOSpeed; }
            set
            {
                _cameraISOSpeed = value;
                update("CameraISOSpeed");
            }
        }

        public UInt32 CameraMeteringMode
        {
            get { return _cameraMeteringMode; }
            set
            {
                _cameraMeteringMode = value;
                update("CameraMeteringMode");
            }
        }

        public UInt32 CameraAFMode
        {
            get { return _cameraAFMode; }
            set
            {
                _cameraAFMode = value;
                update("CameraAFMode");
            }
        }

        public UInt32 CameraAperture
        {
            get { return _cameraAperture; }
            set
            {
                _cameraAperture = value;
                update("CameraAperture");
            }
        }

        public UInt32 CameraShutterTime
        {
            get { return _cameraShutterTime; }
            set
            {
                _cameraShutterTime = value;
                update("CameraShutterTime");
            }
        }

        public UInt32 CameraExposureCompensation
        {
            get { return _cameraExposureCompensation; }
            set
            {
                _cameraExposureCompensation = value;
                update("CameraExposureCompensation");
            }
        }

        public UInt32 CameraAvailableShots
        {
            get { return _cameraAvailableShots; }
            set
            {
                _cameraAvailableShots = value;
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

        #region Constructors

        public Camera(IntPtr cameraPtr)
        {
            throw new NotImplementedException();
        }

        private void initFields()
        {

        }

        public Camera(IntPtr cameraPtr, String cameraName)
        {
            UInt32 error = 0;
            this.Ptr = cameraPtr;
            this.Name = cameraName;
            if (error != 0)
            {
                Console.WriteLine("Cant register property event handler because : " + ErrorCodes.getErrorDataWithCodeNumber(error));
            }

            error = EDSDK.EdsOpenSession(this.Ptr);
            if (error != 0)
            {
                Console.WriteLine("Cant open session with camera because : " + ErrorCodes.getErrorDataWithCodeNumber(error));
            }

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
            getLensStateOfCamera();
            getApertureFromCamera();
            getTimeFromCamera();
            getEbvFromBody();
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

        public void getBatteryLevel()
        {
            UInt32 tmpCameraBatteryLevel = 0;
            Error = 0;
            Error = EDSDK.EdsGetPropertyData(this._ptr, EDSDKLib.EDSDK.PropID_BatteryLevel, 0, out tmpCameraBatteryLevel);
            if (Error != 0)
            {
                //TODO throw Exception more exactly
                throw new Exception("An error has occured");
            }
            else
            {
                this.CameraBatteryLevel = tmpCameraBatteryLevel;
            }
        }

        public void getEbvFromBody()
        {
            UInt32 tmpEbv = 0;
            Error = 0;
            Error = EDSDK.EdsGetPropertyData(this._ptr, EDSDKLib.EDSDK.PropID_ExposureCompensation, 0, out tmpEbv);
            if (Error != 0)
            {
                //TODO throw Exception
            }
            else
            {
                this.CameraExposureCompensation = tmpEbv;
            }
        }

        public void getAeMode()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_AEMode, 0, out tmpProperty);
            if (tmpError == 0 )//TODO change to !=0
            {
                this.CameraAEMode = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get AEMode from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError).ErrorCodeString);
                //TODO throw Exception
            }
        }

        public void getApertureFromCamera()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_Av, 0, out tmpProperty);
            if (tmpError == 0) //TODO chagne to !=0
            {
                this.CameraAperture = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get Aperture from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError).ErrorCodeString);
                //TODO throw Exception
            }
        }

        public void getDriveMode()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_DriveMode, 0, out tmpProperty);
            if (tmpError == 0) //TODO change to !=0
            {
                this.CameraDriveMode = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get DriveMode from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
                //TODO throw Exception
            }
        }

        public void getAfMode()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_AFMode, 0, out tmpProperty);
            if (tmpError == 0) //TODO change to !=0
            {
                this.CameraAFMode = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get AFMode from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
                //TODO throw Exception
            }
        }

        public void getMeteringMode()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_MeteringMode, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraMeteringMode = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get MeteringMode from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getShutterTime()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_Tv, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraShutterTime = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get Shuttertime from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getIsoSpeed()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_ISOSpeed, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraISOSpeed = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get ISOSpeed from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für die verfügbaren freien Fotos aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getAvailableShots()
        {
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_AvailableShots, 0, out this._cameraAvailableShots)) == 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die aktuelle Einstellung für den Kamera Besitzer aus der Kamera und speichert ihn in den Klassenmember
        /// </summary>
        public void getOwner()
        {
            Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_OwnerName, 0, out this._owner);
            if ((Error = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_OwnerName, 0, out this._owner)) == 0)
            {
                publicError(Error);
            }
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
            if ((Error=EDSDK.EdsGetPropertyDesc(this.Ptr, EDSDK.PropID_Tv, out this.availableShutterspeeds)) != 0)
            {
                publicError(Error);
            }
        }

        /// <summary>
        /// Holt die Tabelle der verfügbaren EBV von der Kamera und speichert sie in den Klassemember
        /// </summary>
        private void getpropertyDescExposureCompensation()
        {
            if ((Error=EDSDK.EdsGetPropertyDesc(this.Ptr, EDSDK.PropID_ExposureCompensation, out this.availableExposureCompensation)) != 0)
            {
                publicError(Error);
            }
        }

        public void getLensStateOfCamera()
        {
            UInt32 tmpError = 0;
            UInt32 tmpProperty = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_LensStatus, 0, out tmpProperty);
            if (tmpError == 0)
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
        }

        public void getTimeFromCamera()
        {
            UInt32 tmpError = 0;
            EDSDK.EdsTime tmpProperty;
            tmpError = EDSDK.EdsGetPropertyData(this.Ptr, EDSDK.PropID_DateTime, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraTime = tmpProperty;
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

        private void publicError(uint error)
        {
            Console.WriteLine("An error has oocured : " + ErrorCodes.getErrorDataWithCodeNumber(error));
        }

        #endregion

    }
}
