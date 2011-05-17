using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDSDKLib;

namespace Canon_EOS_Remote.classes
{
    class Lens
    {
        private string _lensName;

        private void driveLensNear1(IntPtr cameraPtr)
        {
            EDSDK.EdsSendCommand(cameraPtr, EDSDK.EvfDriveLens_Near1, 0);
        }
        private void driveLensNear2(IntPtr cameraPtr)
        {
            EDSDK.EdsSendCommand(cameraPtr, EDSDK.EvfDriveLens_Near2, 0);
        }
        private void driveLensNear3(IntPtr cameraPtr)
        {
            EDSDK.EdsSendCommand(cameraPtr, EDSDK.EvfDriveLens_Near3, 0);
        }

        private void driveLensFar1(IntPtr cameraPtr)
        {
            EDSDK.EdsSendCommand(cameraPtr, EDSDK.EvfDriveLens_Far1, 0);
        }
        private void driveLensFar2(IntPtr cameraPtr)
        {
            EDSDK.EdsSendCommand(cameraPtr, EDSDK.EvfDriveLens_Far2, 0);
        }
        private void driveLensFar3(IntPtr cameraPtr)
        {
            EDSDK.EdsSendCommand(cameraPtr, EDSDK.EvfDriveLens_Far3, 0);
        }
    }
}
