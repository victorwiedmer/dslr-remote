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

        public string CameraName
        {
            get { return _cameraName; }
            set {
                update("CameraName");
                _cameraName = value; }
        }
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
        private EDSDK.EdsPropertyDesc availableAEModes;
        private EDSDK.EdsPropertyDesc availableMeteringModes;
        private EDSDK.EdsPropertyDesc availableApertureValues;
        private EDSDK.EdsPropertyDesc availableShutterspeeds;
        private EDSDK.EdsPropertyDesc availableExposureCompensation;

        #endregion

        #region Setter and Getter of class member
        public IntPtr CameraPtr
        {
            get { return _cameraPtr; }
            set { _cameraPtr = value;
            update("_cameraPtr");
            }
        }    

        public string CameraOwner
        {
            get { return _cameraOwner; }
            set { _cameraOwner = value;
            update("_cameraOwner");
            }
        }        

        public string CameraBodyID
        {
            get { return _cameraBodyID; }
            set { _cameraBodyID = value;
            update("_cameraBodyID");
            }
        }

        public EdsTime CameraTime
        {
            get { return _cameraTime; }
            set { _cameraTime = value;
            update("_cameraTime");
            }
        }

        public UInt32 CameraBatteryLevel
        {
            get { return _cameraBatteryLevel; }
            set { _cameraBatteryLevel = value;
            update("_cameraBatteryLeveL");
            }
        }

        public UInt32 CameraAEMode
        {
            get { return _cameraAEMode; }
            set { _cameraAEMode = value;
            update("_cameraAEMode");
            }
        }

        public UInt32 CameraDriveMode
        {
            get { return _cameraDriveMode; }
            set { _cameraDriveMode = value;
            update("_cameraDriveMode");
            }
        }

        public UInt32 CameraISOSpeed
        {
            get { return _cameraISOSpeed; }
            set { _cameraISOSpeed = value;
            update("_cameraISOSpeed");
            }
        }

        public UInt32 CameraMeteringMode
        {
            get { return _cameraMeteringMode; }
            set { _cameraMeteringMode = value;
            update("_cameraMeteringMode");
            }
        }

        public UInt32 CameraAFMode
        {
            get { return _cameraAFMode; }
            set { _cameraAFMode = value;
            update("_cameraAFMode");
            }
        }

        public UInt32 CameraAperture
        {
            get { return _cameraAperture; }
            set { _cameraAperture = value;
            update("_cameraAperture");
            }
        }

        public UInt32 CameraShutterTime
        {
            get { return _cameraShutterTime; }
            set { _cameraShutterTime = value;
            update("_cameraShutterTime");
            }
        }

        public UInt32 CameraExposureCompensation
        {
            get { return _cameraExposureCompensation; }
            set { _cameraExposureCompensation = value;
            update("_cameraExposureCompensation");
            }
        }

        public UInt32 CameraAvailableShots
        {
            get { return _cameraAvailableShots; }
            set { _cameraAvailableShots = value;
            update("CameraAvailableShots");
            }
        }

        public string CurrentStorage
        {
            get { return _currentStorage; }
            set { _currentStorage = value;
            update("_currentStorage");
            }
        }    

        public string CameraFirmware
        {
            get { return _cameraFirmware; }
            set { _cameraFirmware = value;
            update("_cameraFirmware");
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
            this.cameraPropertyEventHandler = new EDSDK.EdsPropertyEventHandler(onCameraPropertyChanged);
            if (error != 0)
            {
                Console.WriteLine("Cant register property event handler because : " + ErrorCodes.getErrorDataWithCodeNumber(error));
            }
            this.cameraStateEventHandler = new EDSDK.EdsStateEventHandler(onCameraStateChanged);
            error = EDSDK.EdsOpenSession(this.CameraPtr);
            if (error != 0)
            {
                Console.WriteLine("Cant open session with camera because : " + ErrorCodes.getErrorDataWithCodeNumber(error));
                EDSDK.EdsCloseSession(this.CameraPtr);
                EDSDK.EdsTerminateSDK();
                Console.WriteLine("Please restart the application.");
            }
            error = EDSDK.EdsSetPropertyEventHandler(this.CameraPtr, EDSDK.PropertyEvent_All, this.cameraPropertyEventHandler, _cameraPtr);
            error = EDSDK.EdsSetCameraStateEventHandler(this.CameraPtr, EDSDK.StateEvent_All, this.cameraStateEventHandler, _cameraPtr);
            error = EDSDK.EdsSetObjectEventHandler(this.CameraPtr, EDSDK.ObjectEvent_All, this.cameraObjectEventHandler, _cameraPtr);
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
            Console.WriteLine("Got following properties : \n" +
            "Batterielevel : " + this.CameraBatteryLevel + "%" + "\n" +
            "AEMode : " + this.CameraAEMode + "\n" +
            "DriveMode : " + this.CameraDriveMode + " = " + this.driveModes.getDriveModeString(this.CameraDriveMode) + "\n" +
            "AFMode : " + this.CameraAFMode + "\n" +
            "MeteringMode : " + this.CameraMeteringMode + "\n" +
            "Tv : " + this.CameraShutterTime + "\n" +
            "ISO : " + this.CameraISOSpeed + " = " + this.isoList.getISOSpeedFromHex(this.CameraISOSpeed) + "\n" +
            "Available Shots : " + this.CameraAvailableShots + "\n" +
            "Firmware Version : " + this.CameraFirmware + "\n" +
            "CameraOwner : " + this.CameraOwner + "\n" +
            "Body ID : " + this.CameraBodyID + "\n" +
            "Current Storage : " + this.CurrentStorage + "\n" 
            + "Available ISO Speeds : " + this.isoList.getISOSpeedFromHex(this.availableISOSpeeds.PropDesc[0]) + " - " + this.isoList.getISOSpeedFromHex(this.availableISOSpeeds.PropDesc[this.availableISOSpeeds.NumElements-1])
            //+ "\n Available AEModes : " + this.availableAEModes.PropDesc[0]  + " - " + this.availableAEModes.PropDesc[this.availableAEModes.NumElements-1]
            + "\n Lens attached : " + this._lensAttached
    );

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

        private void getCameraBatteryLevelFromBody()
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

        private uint onCameraStateChanged(uint inEvent, uint inParameter, IntPtr inContext)
        {
            Console.WriteLine("State changed : " + this.eventIDs.getEventIDString(inEvent));
            if (inEvent == EDSDK.StateEvent_Shutdown)
            {
                EDSDK.EdsCloseSession(this.CameraPtr);
                Console.WriteLine("Close camera session because : " + this.eventIDs.getEventIDString(inEvent));
                EDSDK.EdsTerminateSDK();
                Console.WriteLine("SDK terminated ....");
            }
            return 0x0;
        }

        private uint onCameraObjectChanged(uint inEvent, uint inParameter, IntPtr inContext)
        {
            Console.WriteLine("Object changed : " + this.eventIDs.getEventIDString(inEvent));
            return 0x0;
        }

        private uint onCameraPropertyChanged(uint inEvent, uint inPropertyID, uint inParameter, IntPtr inContext)
        {
            Console.WriteLine("CameraPropertey changed : " + 
                "EventID : " + this.eventIDs.getEventIDString(inEvent) + "\nPropID : " + this.propertyCodes.getPropertyString(inPropertyID) + "\nParameter : " + inParameter + "\nContext : " + inContext);
            if (inEvent == EDSDK.PropertyEvent_PropertyChanged)
            {
                switch (inPropertyID)
                {
                    case EDSDK.PropID_ISOSpeed: { getISOSpeedFromCamera();
                    Console.WriteLine("New ISO Speed is : " + this.isoList.getISOSpeedFromHex(this.CameraISOSpeed));
                        break; }
                    case EDSDK.PropID_Tv:
                        {
                            getTvFromCamera();
                            Console.WriteLine("New Tv is : " + this.CameraShutterTime);
                            break;
                        }
                    case EDSDK.PropID_AvailableShots: {
                        Console.WriteLine("Refresh available Shots from Camera");
                        getAvailableShotsFromCamera();break ; }
                }
            }
            return 0x0;
        }

        private void getAEModeFromCamera()
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

        private void getDriveModeFromCamera()
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

        private void getAFModeFromCamera()
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

        private void getMeteringModeFromCamera()
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

        private void getTvFromCamera()
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

        private void getISOSpeedFromCamera()
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

        private void getCameraOwner()
        {
            string tmpProperty="";
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

        private void getCameraName()
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

        private void getFirmwareVersion()
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

        private void getBodyID()
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

        private void getCurrentStorage()
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

        private void getavailableISOSpeedsFromCamera()
        {
            UInt32 tmpError = 0;
            tmpError = EDSDK.EdsGetPropertyDesc(this.CameraPtr, EDSDK.PropID_ISOSpeed, out this.availableISOSpeeds);
            if (tmpError != 0)
            {
                Console.WriteLine("An error has oocured : " + ErrorCodes.getErrorDataWithCodeNumber(tmpError));
            }
        }

        private void getavailableAEModesFromCamera()
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
            UInt32 tmpProperty=0;
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

        #endregion

    }
}
