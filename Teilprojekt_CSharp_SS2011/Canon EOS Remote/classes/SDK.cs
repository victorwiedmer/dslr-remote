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

        public bool SDKState
        {
            get { return _sDKState; }
            set { _sDKState = value; }
        }
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
            System.Windows.MessageBox.Show("SDK created");
        }

        public void Dispose()
        {
            //uint tmpError;
            //tmpError=EDSDK.EdsTerminateSDK();
            //if (tmpError != 0)
            //{
            //    error = new EdsError(tmpError);
            //    throw new Exception(error.ToString());
            //}
            //this._sDKState = false;
        }
    }
}
