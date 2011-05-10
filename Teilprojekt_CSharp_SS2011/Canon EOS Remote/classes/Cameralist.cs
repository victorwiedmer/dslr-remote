using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class Cameralist
    {
        private List<Camera> _cameraList {set;get;}

        public Cameralist()
        {
            _cameraList = new List<Camera>();
        }

        public void addCameraToList(Camera camera){
            this._cameraList.Add(camera);
        }

        public void deleteCameraFromList(Camera camera)
        {
            this._cameraList.Remove(camera);
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
    }
}
