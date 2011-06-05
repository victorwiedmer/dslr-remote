using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canon_EOS_Remote.classes;
using System.ComponentModel;

namespace Canon_EOS_Remote
{
    class Model : INotifyPropertyChanged
    {
        #region Members

        private SDK _sdk;
        private Cameralist _cameraList;
        #endregion

        #region Setter/Getter Methods

        public SDK Sdk
        {
            get { return _sdk; }
            set { _sdk = value;}
        }

        public Cameralist CameraList
        {
            get { return _cameraList; }
            set { _cameraList = value; }
        }

        #endregion

        public Model()
        {
         Sdk = new SDK();
         CameraList = new Cameralist();
        }

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                Console.WriteLine("Property has changed from : " + this + " : " + property);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
