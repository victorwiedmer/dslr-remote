using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDSDKLib;

namespace Canon_EOS_Remote.classes
{
    class SDK : IDisposable
    {
        private bool _sDKState;
        private EdsError error;

        public SDK()
        {
            uint tmpError;
            tmpError=EDSDK.EdsInitializeSDK();
            if (tmpError != 0)
            {
                error = new EdsError(tmpError);
                throw new Exception(error.ToString());
            }
            this._sDKState = true;
        }

        public void Dispose()
        {
            uint tmpError;
            tmpError=EDSDK.EdsTerminateSDK();
            if (tmpError != 0)
            {
                error = new EdsError(tmpError);
                throw new Exception(error.ToString());
            }
            this._sDKState = false;
        }
    }
}
