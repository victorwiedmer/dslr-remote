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
    class Camera : INotifyPropertyChanged
    {
        #region Declaration of class members
        /**
         * Added 05-05-2011 11:50
         * basic properties
         * */
        private IntPtr _cameraPtr;
        private string _cameraName;/* The product name of the camera body*/
        private string _cameraOwner; /*The setted name of the camera owner*/
        private string _cameraBodyID;
        private EdsTime _cameraTime;
        private UInt32 _cameraBatteryLevel;
        private UInt32 _cameraAEMode;
        private UInt32 _cameraDriveMode;
        private UInt32 _cameraISOSpeed;
        private UInt32 _cameraMeteringMode;
        private UInt32 _cameraAFMode;
        private UInt32 _cameraAperture;
        private UInt32 _cameraShutterTime;
        private UInt32 _cameraExposureCompensation;
        private UInt32 _cameraAvailableShots;
        private string _currentStorage;
        private UInt32 tmpErrorCodeAfterCommand;
        private string tmpErrorString;
        private string _cameraFirmware;
        private bool _lensAttached;

        public event PropertyChangedEventHandler PropertyChanged;
        private EDSDK.EdsPropertyEventHandler cameraPropertyEventHandler;
        private EDSDK.EdsStateEventHandler cameraStateEventHandler;
        private EDSDK.EdsObjectEventHandler cameraObjectEventHandler;

        private DriveModes driveModes;
        private ISOSpeeds isoList;
        private PropertyCodes propertyCodes;
        private EventCodes eventIDs;

        private EDSDK.EdsPropertyDesc availableISOSpeeds;

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
        private EDSDK.EdsPropertyDesc availableMeteringModes;
        private EDSDK.EdsPropertyDesc availableApertureValues;
        private EDSDK.EdsPropertyDesc availableShutterspeeds;

        public EDSDK.EdsPropertyDesc AvailableShutterspeeds
        {
            get { return availableShutterspeeds; }
            set { availableShutterspeeds = value; }
        }
        private EDSDK.EdsPropertyDesc availableExposureCompensation;

        #endregion

        #region Setter and Getter of class member
        public string CameraName
        {
            get { return _cameraName; }
            set
            {
                update("CameraName");
                _cameraName = value;
            }
        }

        public IntPtr CameraPtr
        {
            get { return _cameraPtr; }
            set
            {
                _cameraPtr = value;
                update("CameraPtr");
            }
        }

        public string CameraOwner
        {
            get { return _cameraOwner; }
            set
            {
                _cameraOwner = value;
                update("CameraOwner");
            }
        }

        public string CameraBodyID
        {
            get { return _cameraBodyID; }
            set
            {
                _cameraBodyID = value;
                update("CameraBodyID");
            }
        }

        public EdsTime CameraTime
        {
            get { return _cameraTime; }
            set
            {
                _cameraTime = value;
                update("CameraTime");
            }
        }

        public UInt32 CameraBatteryLevel
        {
            get { return _cameraBatteryLevel; }
            set
            {
                _cameraBatteryLevel = value;
                update("CameraBatteryLevel");
            }
        }

        public UInt32 CameraAEMode
        {
            get { return _cameraAEMode; }
            set
            {
                _cameraAEMode = value;
                update("CameraAEMode");
            }
        }

        public UInt32 CameraDriveMode
        {
            get { return _cameraDriveMode; }
            set
            {
                _cameraDriveMode = value;
                update("CameraDriveMode");
            }
        }

        public UInt32 CameraISOSpeed
        {
            get { return _cameraISOSpeed; }
            set
            {
                _cameraISOSpeed = value;
                Console.WriteLine("CameraISOSpeed new value " + value);
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
                Console.WriteLine("CameraAperture new value " + value);
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

        public Camera(IntPtr cameraPtr, String cameraName)
        {
            UInt32 error = 0;
            this.CameraPtr = cameraPtr;
            this.CameraName = cameraName;
            this.driveModes = new DriveModes();
            this.isoList = new ISOSpeeds();
            this.propertyCodes = new PropertyCodes();
            this.eventIDs = new EventCodes();
            if (error != 0)
            {
                Console.WriteLine("Cant register property event handler because : " + ErrorCodes.getErrorDataWithCodeNumber(error));
            }

            error = EDSDK.EdsOpenSession(this.CameraPtr);
            if (error != 0)
            {
                Console.WriteLine("Cant open session with camera because : " + ErrorCodes.getErrorDataWithCodeNumber(error));
                EDSDK.EdsCloseSession(this.CameraPtr);
                EDSDK.EdsTerminateSDK();
                Console.WriteLine("Please restart the application.");
            }

            getCameraBatteryLevelFromBody();
            getAEModeFromCamera();
            getDriveModeFromCamera();
            getAFModeFromCamera();
            getMeteringModeFromCamera();
            getTvFromCamera();
            getISOSpeedFromCamera();
            getAvailableShotsFromCamera();
            getCameraOwner();
            getCameraName();
            getFirmwareVersion();
            getBodyID();
            getCurrentStorage();
            getavailableISOSpeedsFromCamera();
            getavailableAEModesFromCamera();
            getavailableApertureValuesFromCamera();
            getavailableExposureCompensationFromCamera();
            getavailableMeteringModesFromCamera();
            getavailableShutterTimesFromCamera();
            getLensStateOfCamera();
            getApertureFromCamera();
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

        public void getCameraBatteryLevelFromBody()
        {
            UInt32 tmpCameraBatteryLevel = 0;
            tmpErrorCodeAfterCommand = 0;
            tmpErrorCodeAfterCommand = EDSDK.EdsGetPropertyData(this._cameraPtr, EDSDKLib.EDSDK.PropID_BatteryLevel, 0, out tmpCameraBatteryLevel);
            if (tmpErrorCodeAfterCommand != 0)
            {

            }
            else
            {
                this.CameraBatteryLevel = tmpCameraBatteryLevel;
            }
        }



        public void getAEModeFromCamera()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_AEMode, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraAEMode = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get AEMode from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError).ErrorCodeString);
            }
        }

        public void getApertureFromCamera()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_Av, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraAperture = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get Aperture from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError).ErrorCodeString);
            }
        }

        public void getDriveModeFromCamera()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_DriveMode, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraDriveMode = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get DriveMode from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getAFModeFromCamera()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_AFMode, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraAFMode = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get AFMode from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getMeteringModeFromCamera()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_MeteringMode, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraMeteringMode = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get MeteringMode from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getTvFromCamera()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_Tv, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraShutterTime = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get Shuttertime from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getISOSpeedFromCamera()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_ISOSpeed, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraISOSpeed = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get ISOSpeed from Camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getAvailableShotsFromCamera()
        {
            UInt32 tmpProperty = 0;
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_AvailableShots, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraAvailableShots = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get available shots from camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getCameraOwner()
        {
            string tmpProperty = "";
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_OwnerName, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraOwner = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get cameraowner from camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getCameraName()
        {
            string tmpProperty = "";
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_ProductName, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraName = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get cameraname from camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getFirmwareVersion()
        {
            string tmpProperty = "";
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_FirmwareVersion, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraFirmware = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get firmware from camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getBodyID()
        {
            string tmpProperty = "";
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_BodyIDEx, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CameraBodyID = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get bodyID from camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getCurrentStorage()
        {
            string tmpProperty = "";
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.kEdsPropID_CurrentStorage, 0, out tmpProperty);
            if (tmpError == 0)
            {
                this.CurrentStorage = tmpProperty;
            }
            else
            {
                Console.WriteLine("Cant get currentstorage from camera because : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getavailableISOSpeedsFromCamera()
        {
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyDesc(this.CameraPtr, EDSDK.PropID_ISOSpeed, out this.availableISOSpeeds);
            if (tmpError != 0)
            {
                Console.WriteLine("An error has oocured : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        public void getavailableAEModesFromCamera()
        {
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyDesc(this.CameraPtr, EDSDK.PropID_AEMode, out this.availableAEModes);
            if (tmpError != 0)
            {
                Console.WriteLine("An error has oocured : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        private void getavailableMeteringModesFromCamera()
        {
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyDesc(this.CameraPtr, EDSDK.PropID_MeteringMode, out this.availableMeteringModes);
            if (tmpError != 0)
            {
                Console.WriteLine("An error has oocured : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        private void getavailableApertureValuesFromCamera()
        {
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyDesc(this.CameraPtr, EDSDK.PropID_Av, out this.availableApertureValues);
            if (tmpError != 0)
            {
                Console.WriteLine("An error has oocured : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        private void getavailableShutterTimesFromCamera()
        {
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyDesc(this.CameraPtr, EDSDK.PropID_Tv, out this.availableShutterspeeds);
            if (tmpError != 0)
            {
                Console.WriteLine("An error has oocured : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        private void getavailableExposureCompensationFromCamera()
        {
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyDesc(this.CameraPtr, EDSDK.PropID_ExposureCompensation, out this.availableExposureCompensation);
            if (tmpError != 0)
            {
                Console.WriteLine("An error has occured : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        private void getLensStateOfCamera()
        {
            UInt32 tmpError = 0;
            UInt32 tmpProperty = 0;
            tmpError = EDSDK.EdsGetPropertyData(this.CameraPtr, EDSDK.PropID_LensStatus, 0, out tmpProperty);
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

        public void setISOSpeedToCamera(int isoSpeed)
        {
            EDSDK.EdsSetPropertyData(this.CameraPtr, EDSDK.PropID_ISOSpeed, 0, sizeof(int), isoSpeed);
        }

        public void setShutterTimeToCamera(int shutterTime)
        {
            EDSDK.EdsSetPropertyData(this.CameraPtr, EDSDK.PropID_Tv, 0, sizeof(int), shutterTime);
        }

        public void setAEModeToCamera(int aeMode)
        {
            EDSDK.EdsSetPropertyData(this.CameraPtr, EDSDK.PropID_AEMode, 0, sizeof(int), aeMode);
        }
        #endregion

    }
}
