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

        private List<Camera> _cameraList {set;get;}
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public Cameralist()
        {
            _cameraList = new List<Camera>();
        }

        public void addCameraToList(Camera camera){
            this._cameraList.Add(camera);
            PropertyChanged(this,new PropertyChangedEventArgs("CameraAddedToList"));
        }

        public void deleteCameraFromList(Camera camera)
        {
            this._cameraList.Remove(camera);
            PropertyChanged(this, new PropertyChangedEventArgs("CameraDeletedFromList"));
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
        }
    }
}
