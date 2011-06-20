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
        #region classmembers
        private bool _sDKState;
        public string _stringSdkState;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region setter/getter
        public string StringSdkState
        {
            get { return _stringSdkState; }
            private set
            {
                _stringSdkState = value;
                update("StringSdkState");
            }
        }

        public bool SDKState
        {
            get { return this._sDKState; }
            set
            {
                _sDKState = value;
                if (value == true)
                {
                    this.StringSdkState = "SDK Initialized";
                }
                else
                {
                    this.StringSdkState = "SDK not initialized";
                }
                update("SDKState");
            }
        }
        #endregion

        public SDK()
        {
            uint tmpError;
            tmpError = EDSDK.EdsInitializeSDK();
            if (tmpError != 0)
            {
                this.SDKState = false;
                //TODO Fehler behandeln
            }
            else
            {
                this.SDKState = true;
            }

        }

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void Dispose()
        {
            uint tmpError;
            tmpError = EDSDK.EdsTerminateSDK();
            this.SDKState = false;
        }
    }
}
