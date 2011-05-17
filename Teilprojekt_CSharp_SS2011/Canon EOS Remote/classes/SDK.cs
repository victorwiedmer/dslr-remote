using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDSDKLib;
using System.ComponentModel;

namespace Canon_EOS_Remote.classes
{
    class SDK : IDisposable, INotifyPropertyChanged
    {
        private bool _sDKState;
        private EdsError error;
        public event PropertyChangedEventHandler PropertyChanged;

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
            PropertyChanged(this, new PropertyChangedEventArgs("_sDKState"));
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
            PropertyChanged(this, new PropertyChangedEventArgs("_sDKState"));
        }
    }
}
