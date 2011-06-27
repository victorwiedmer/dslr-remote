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
        private ViewModelCurrentCamera viewModelCurrentCamera;

        public ViewModelCurrentCamera ViewModelCurrentCamera
        {
            get { return viewModelCurrentCamera; }
            set { viewModelCurrentCamera = value; }
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
            viewModelCurrentCamera = new ViewModelCurrentCamera();
            cameraListView = new CollectionView(Model.CameraList.CameraList);
            CameraListView.CurrentChanged += new EventHandler(setCurrentlyCamera);
            if (ViewModelCurrentCamera != null)
            {
                Model.CameraList.onCameraPropertyChangedEvent += ViewModelCurrentCamera.updateCurrentlyCamera;
            }
        }

        #region ViewModel Events
        private void setCurrentlyCamera(object sender, EventArgs e)
        {
                Camera tmpCamera=(Camera)CameraListView.CurrentItem;
                if (tmpCamera != null)
                {
                    this.ViewModelCurrentCamera.CurrentCamera = model.CameraList.CameraList.ElementAt(Model.CameraList.CameraList.IndexOf(tmpCamera));
                    this.ViewModelCurrentCamera.setCurrentlyCamera();
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
            }
        }

    }
}
