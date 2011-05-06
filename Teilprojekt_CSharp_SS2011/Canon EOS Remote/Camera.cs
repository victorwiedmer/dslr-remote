using System;


namespace Canon_EOS_Remote
{
    unsafe class Camera
    {
        /**
         * Added 05-05-2011 11:50
         * basic properties
         * */
        private IntPtr _cameraPtr;
        private string _cameraName; /* The product name of the camera body*/
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

        public Camera(IntPtr cameraPtr)
        {
            if (cameraPtr == IntPtr.Zero) this._cameraPtr = cameraPtr;
            else throw new Exception("Cant get cameraPointer");
        }

        private void getCameraNameFromBody()
        {
            string tmpName;
            EDSDKLib.EDSDK.EdsGetPropertyData(this._cameraPtr, EDSDKLib.EDSDK.PropID_ProductName, 0, out tmpName);
        }
    }
}
