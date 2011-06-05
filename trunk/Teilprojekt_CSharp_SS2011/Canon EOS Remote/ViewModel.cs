using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Data;

namespace Canon_EOS_Remote.ViewModel
{
    class ViewModel :INotifyPropertyChanged, IDisposable
    {

        #region Classmembers
        private Model model;
        private CollectionView cameraListView;
        public event PropertyChangedEventHandler PropertyChanged;
        private string currentCameraName;
        private int currentBatteryLevel;
        private string currentBodyID;
        private int currentAvailableShots;
        private Camera currentCamera;
        private delegate void OnCameraPropertyChangedEventHandler();

        public int CurrentAvailableShots
        {
            get { return currentAvailableShots; }
            set { currentAvailableShots = value;
            update("CurrentAvailableShots");
            }
        }

        public string CurrentBodyID
        {
            get { return currentBodyID; }
            set { currentBodyID = value;
            update("CurrentBodyID");
            }
        }

        public int CurrentBatteryLevel
        {
            get { return currentBatteryLevel; }
            set { currentBatteryLevel = value;
            update("CurrentBatteryLevel");
            }
        }

        public string CurrentCameraName
        {
            get { return currentCameraName; }
            set {
                currentCameraName = value;
                update("CurrentCameraName");
            }
        }
        #endregion

        #region Setter/Getter of Classmembers
        public CollectionView CameraListView
        {
            get { return cameraListView; }
            set { cameraListView = value; }
        }

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }
        #endregion

        public ViewModel()
        {
            Model = new Model();
            cameraListView = new CollectionView(Model.CameraList.CameraList);
            CameraListView.CurrentChanged += new EventHandler(setCurrentlyCamera);
            this.CurrentCameraName = "CurrentCameraName";
            this.CurrentBatteryLevel = 50;
            Model.CameraList.onCameraPropertyChangedEvent += updateCurrentlyCamera;
        }

        #region ViewModel Events
        private void setCurrentlyCamera(object sender, EventArgs e)
        {
                Camera tmpCamera=(Camera)CameraListView.CurrentItem;
                if (tmpCamera != null)
                {
                    Console.WriteLine("Got new currently camera : " + tmpCamera.CameraName);
                    Console.WriteLine("He index in the cameralist is : " + Model.CameraList.CameraList.IndexOf(tmpCamera));
                    this.CurrentCameraName = tmpCamera.CameraName;
                    this.CurrentBatteryLevel = (int)tmpCamera.CameraBatteryLevel;
                    this.CurrentBodyID = tmpCamera.CameraBodyID;
                    this.CurrentAvailableShots = (int)tmpCamera.CameraAvailableShots;
                    this.currentCamera = tmpCamera;
                }
        }
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                Console.WriteLine("Property has changed : " + property);
            }
        }

        public void updateCurrentlyCamera()
        {
            Console.WriteLine("Got Event to updateCurrentlyCamera()");
            this.CurrentCameraName = this.currentCamera.CameraName;
            this.CurrentBatteryLevel = (int)this.currentCamera.CameraBatteryLevel;
            this.CurrentBodyID = this.currentCamera.CameraBodyID;
            this.currentCamera.getAvailableShotsFromCamera();
            this.CurrentAvailableShots = (int)this.currentCamera.CameraAvailableShots;
        }
    }
}
