using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote
{
    class Eventhandling
    {
        private EDSDKLib.EDSDK.EdsCameraAddedHandler cameraAddedHandler;

        public Eventhandling()
        {
            uint error = 0;
            cameraAddedHandler = new EDSDKLib.EDSDK.EdsCameraAddedHandler(onCameraAdded);
            error=EDSDKLib.EDSDK.EdsSetCameraAddedHandler(cameraAddedHandler, IntPtr.Zero);
            System.Windows.MessageBox.Show("Errorcode after adding cameraAddedHandler : " + error);
        }

        public uint onCameraAdded(IntPtr inContext)
        {
            IntPtr tmpPtr = IntPtr.Zero;
            int tmpCount = 0;
            EDSDKLib.EDSDK.EdsDeviceInfo deviceInfo;
            char[] tmpName = new char[32];
            EDSDKLib.EDSDK.EdsGetCameraList(out tmpPtr);
            EDSDKLib.EDSDK.EdsGetChildCount(tmpPtr, out tmpCount);
            EDSDKLib.EDSDK.EdsGetChildAtIndex(tmpPtr, tmpCount - 1, out tmpPtr);
            EDSDKLib.EDSDK.EdsGetDeviceInfo(tmpPtr, out deviceInfo);
            System.Windows.MessageBox.Show("New Camera added : " + deviceInfo.szDeviceDescription);
            return 0x0;
        }

    }
}
