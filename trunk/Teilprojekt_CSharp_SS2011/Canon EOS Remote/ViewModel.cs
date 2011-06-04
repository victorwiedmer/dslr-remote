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
        }

        #region ViewModel Events
        private void setCurrentlyCamera(object sender, EventArgs e)
        {
            model.CameraList.CurrentlyCamera = (Camera)CameraListView.CurrentItem;
            System.Windows.MessageBox.Show("Got new currently camera");
            update("model.CameraList.CurrentlyCamera");
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
                System.Windows.MessageBox.Show("Property has changed : " + property);
            }
        }
    }
}
