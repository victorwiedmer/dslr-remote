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

        public string _stringSdkState;

        public string StringSdkState
        {
            get {return _stringSdkState; }
            private set {_stringSdkState = value;}
        }

        public bool SDKState
        {
            get { return this._sDKState; }
            set { _sDKState = value;
            if (value == true)
            {
                this.StringSdkState = "SDK Initialized";
                update("StringSdkState");
            }
            else
            {
                this.StringSdkState = "SDK not initialized";
                update("StringSdkState");
            }
            }
        }

        private EdsError error;
        public event PropertyChangedEventHandler PropertyChanged;

        public SDK()
        {
            uint tmpError;
            tmpError = EDSDK.EdsInitializeSDK();
            if (tmpError != 0)
            {
                this.SDKState = false;
                System.Windows.MessageBox.Show("SDK can't initilized : " + tmpError);
            }
            else
            {
                this.SDKState = true;
                Console.WriteLine("SDK initialized");
            }

        }

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
                System.Windows.MessageBox.Show("Property has changed from : " + this + " : " + property);
            }
        }

        public void Dispose()
        {
            uint tmpError;
            tmpError=EDSDK.EdsTerminateSDK();
            this.SDKState = false;
        }
    }
}
