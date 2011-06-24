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
        private ModelConverter _modelConverter;

        public ModelConverter ModelConverter
        {
            get { return _modelConverter; }
            set { _modelConverter = value; }
        }
        private ModelCurrentCamera _modelCurrentCamera;

        internal ModelCurrentCamera ModelCurrentCamera
        {
            get { return _modelCurrentCamera; }
            set { _modelCurrentCamera = value;
            update("ModelCurrentCamera");
            }
        }
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
         this.ModelConverter = new ModelConverter();
         this.ModelCurrentCamera = new ModelCurrentCamera();
        }

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
