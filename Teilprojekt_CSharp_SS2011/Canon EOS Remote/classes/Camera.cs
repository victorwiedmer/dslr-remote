using System;
/*
 * Added on 09-05-2011 12:38 to simplify using of the canon sdk.
 * Now, every command , EDSDKLib namespace havent to be written.
 * */
using EDSDKLib;
using System.ComponentModel; 

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
        private string _cameraName; /* The product name of the camera body*/

        public string CameraName
        {
            get { return _cameraName; }
            set { _cameraName = value; }
        }
        private string _cameraOwner; /*The setted name of the camera owner*/
        private string _cameraBodyID;
        private EdsTime _cameraTime;
        private UInt32 _cameraBatteryLevel;
        private AEMode _cameraAEMode;
        private UInt32 _cameraDriveMode;
        private ISOSpeed _cameraISOSpeed;
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
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors

        public Camera(IntPtr cameraPtr)
        {
            if (cameraPtr == IntPtr.Zero) this._cameraPtr = cameraPtr;
            else throw new Exception("Cant get cameraPointer");
        }

        #endregion

        /*
         * Added on 09-05-2011 12:40 to generalize the exception handling,
         * e.g. to get all eds error code informations
         * */
        private string getErrorString(uint errorCodeNumber)
        {
            EdsError tmpError = new EdsError(tmpErrorCodeAfterCommand);
            tmpErrorString = "ErrorNumber : " + tmpError.ErrorCodeNumber + "\n CodeString : " +
            tmpError.ErrorCodeString + "\n Description : " + tmpError.ErrorDescription;
            tmpError = null; /* To dispose the garbage collector to delete this allocation */
            return tmpErrorString;
        }

        #region camera methods
        private void getCameraNameFromBody()
        {
            tmpErrorCodeAfterCommand = 0;
            tmpErrorCodeAfterCommand = EDSDK.EdsGetPropertyData(this._cameraPtr, EDSDKLib.EDSDK.PropID_ProductName, 0, out this._cameraName);
            if (tmpErrorCodeAfterCommand != 0)
            {
                throw new Exception("Command execution not succesfull because :" + getErrorString(tmpErrorCodeAfterCommand));
            }
        }

        private void getCameraOwnerFromBody()
        {
            tmpErrorCodeAfterCommand = 0;
            tmpErrorCodeAfterCommand = EDSDK.EdsGetPropertyData(this._cameraPtr, EDSDKLib.EDSDK.PropID_OwnerName, 0, out this._cameraName);
            if (tmpErrorCodeAfterCommand != 0)
            {
                throw new Exception("Command execution not succesfull because :" + getErrorString(tmpErrorCodeAfterCommand));
            }
        }

        private void getCameraBodyIDFromBody()
        {
            tmpErrorCodeAfterCommand = 0;
            tmpErrorCodeAfterCommand = EDSDK.EdsGetPropertyData(this._cameraPtr, EDSDKLib.EDSDK.PropID_BodyIDEx, 0, out this._cameraBodyID);
            if (tmpErrorCodeAfterCommand != 0)
            {
                throw new Exception("Command execution not succesfull because :" + getErrorString(tmpErrorCodeAfterCommand));
            }
        }

        private void getCameraBatteryLevelFromBody()
        {
            tmpErrorCodeAfterCommand = 0;
            tmpErrorCodeAfterCommand = EDSDK.EdsGetPropertyData(this._cameraPtr, EDSDKLib.EDSDK.PropID_BatteryLevel, 0, out this._cameraBatteryLevel);
            if (tmpErrorCodeAfterCommand != 0)
            {
                throw new Exception("Command execution not succesfull because :" + getErrorString(tmpErrorCodeAfterCommand));
            }
        }

        private void getCameraTime()
        {
            /*
             * @TODO 
             */
        }

        private void getCameraFirmwareFromBody()
        {
            tmpErrorCodeAfterCommand = 0;
            tmpErrorCodeAfterCommand = EDSDK.EdsGetPropertyData(this._cameraPtr, EDSDKLib.EDSDK.PropID_FirmwareVersion, 0, out this._cameraFirmware);
            if (tmpErrorCodeAfterCommand != 0)
            {
                throw new Exception("Command execution not succesfull because :" + getErrorString(tmpErrorCodeAfterCommand));
            }
        }
        #endregion

    }
}
