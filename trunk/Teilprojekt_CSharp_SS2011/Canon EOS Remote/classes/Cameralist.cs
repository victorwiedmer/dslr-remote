using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Canon_EOS_Remote.classes
{
    class Cameralist : INotifyPropertyChanged, IDisposable
    {
        #region Classmember

        /*
         * Added on 11-05-2011 22:30
         * This list hold the connected cameras from the type Camera
         * */
        private List<Camera> _cameraList;

        internal List<Camera> CameraList
        {
            get { return _cameraList; }
            set { _cameraList = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /*
         * Standard constructor of Cameralist
         * its initilize the list _cameraList
         * */
        public Cameralist()
        {
            _cameraList = new List<Camera>();
        }

        public void addCameraToList(Camera camera){
            this._cameraList.Add(camera);
            PropertyChanged(this,new PropertyChangedEventArgs("_cameraList"));
        }

        public void deleteCameraFromList(Camera camera)
        {
            this._cameraList.Remove(camera);
            PropertyChanged(this, new PropertyChangedEventArgs("_cameraList"));
        }

        public Camera getCameraFromList(string cameraName)
        {
            Camera tmpCamera=null;
            for (int i = 0; i < this._cameraList.Count; i++)
            {
                if ((tmpCamera = this._cameraList.ElementAt(i)).CameraName == cameraName)
                {
                    return tmpCamera;
                }
            }
            return tmpCamera;
        }

        public void Dispose()
        {
            this._cameraList = null;
            PropertyChanged(this, new PropertyChangedEventArgs("_cameraList"));
        }
    }
}
