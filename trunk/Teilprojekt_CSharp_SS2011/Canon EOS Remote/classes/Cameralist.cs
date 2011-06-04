using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

using EDSDKLib;

namespace Canon_EOS_Remote.classes
{
    class Cameralist : INotifyPropertyChanged
    {

        private ObservableCollection<Camera> cameraList;
        private Camera currentlyCamera;

        internal Camera CurrentlyCamera
        {
            get { return currentlyCamera; }
            set {
                update("currentlyCamera");
                currentlyCamera = value; }
        }
        private EDSDK.EdsCameraAddedHandler cameraAddedHandler;
        private EDSDK.EdsStateEventHandler cameraStateEventHandler;

        public EDSDK.EdsStateEventHandler CameraStateEventHandler
        {
            get { return cameraStateEventHandler; }
            set { cameraStateEventHandler = value; }
        }

        public EDSDK.EdsCameraAddedHandler CameraAddedHandler
        {
            get { return cameraAddedHandler; }
            set { cameraAddedHandler = value; }
        }

        public ObservableCollection<Camera> CameraList
        {
            get { return cameraList; }
            set { cameraList = value; }
        }

        public uint onCameraAdded(IntPtr inContext)
        {
            IntPtr tmpPtr = IntPtr.Zero;
            int tmpCount = 0;
            EDSDKLib.EDSDK.EdsDeviceInfo deviceInfo;
            char[] tmpName = new char[32];
            uint error = 0;
            /**
             * First getting cameralist pointer
             * */
            error=EDSDKLib.EDSDK.EdsGetCameraList(out tmpPtr);
            if (error != EDSDK.EDS_ERR_OK)
            {
                System.Windows.MessageBox.Show("Error while getting cameralist : " + error);
            }
            /*
             * Getting count of cameralist childs to choose the last adding on the list
             * */
            error=EDSDKLib.EDSDK.EdsGetChildCount(tmpPtr, out tmpCount);
            if (error != EDSDK.EDS_ERR_OK)
            {
                System.Windows.MessageBox.Show("Error while getting count of cameralist childs : " + error);
            }
            /*
             * Get the camera pointer of the last object on the cameralist
             * */
            error=EDSDKLib.EDSDK.EdsGetChildAtIndex(tmpPtr, tmpCount - 1, out tmpPtr);
            if (error != EDSDK.EDS_ERR_OK)
            {
                System.Windows.MessageBox.Show("Error while getting camerapointer : " + error);
            }
            /*
             * Getting device info of given camera pointer
             * */
            error=EDSDKLib.EDSDK.EdsGetDeviceInfo(tmpPtr, out deviceInfo);
            if (error != EDSDK.EDS_ERR_OK)
            {
                System.Windows.MessageBox.Show("Error while getting deviceinfo : " + error);
            }
            this.CameraList.Add(new Camera(tmpPtr,deviceInfo.szDeviceDescription));
            return 0x0;
        }

        public Cameralist()
        {
            uint error = 0;
            this.cameraList = new ObservableCollection<Camera>();
            this.CameraAddedHandler = new EDSDKLib.EDSDK.EdsCameraAddedHandler(onCameraAdded);
            error = EDSDKLib.EDSDK.EdsSetCameraAddedHandler(cameraAddedHandler, IntPtr.Zero);
            if (error != EDSDK.EDS_ERR_OK)
            {
                System.Windows.MessageBox.Show("Error while adding cameraAddedEvent : " + error);
            }
            //this.CameraStateEventHandler = new EDSDK.EdsStateEventHandler();
        }

        //private uint cameraStateEvent(

        public event PropertyChangedEventHandler PropertyChanged;

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
