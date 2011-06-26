using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

using EDSDKLib;
using System.Collections.Specialized;

namespace Canon_EOS_Remote.classes
{
    class Cameralist : INotifyPropertyChanged
    {
        #region classmembers
        private ObservableCollection<Camera> cameraList;
        private EDSDK.EdsCameraAddedHandler cameraAddedHandler;
        public event PropertyChangedEventHandler PropertyChanged;

        private EDSDK.EdsPropertyEventHandler cameraPropertyEventHandler;
        private EDSDK.EdsStateEventHandler cameraStateEventHandler;
        private EventCodes eventIDs;
        private PropertyCodes propertyCodes;

        public delegate void OnCameraPropertyChangedEventHandler(PropertyEventArgs p);
        public event OnCameraPropertyChangedEventHandler onCameraPropertyChangedEvent;
        #endregion

        #region setter/getter
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
        #endregion

        public uint onCameraAdded(IntPtr inContext) //TODO add Exceptionhandling
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
                //TODO Fehler behandeln
            }
            /*
             * Getting count of cameralist childs to choose the last adding on the list
             * */
            error=EDSDKLib.EDSDK.EdsGetChildCount(tmpPtr, out tmpCount);
            if (error != EDSDK.EDS_ERR_OK)
            {
                //TODO Fehler behandeln
            }
            /*
             * Get the camera pointer of the last object on the cameralist
             * */
            error=EDSDKLib.EDSDK.EdsGetChildAtIndex(tmpPtr, tmpCount - 1, out tmpPtr);
            if (error != EDSDK.EDS_ERR_OK)
            {
                //TODO Fehler behandeln
            }
            /*
             * Getting device info of given camera pointer
             * */
            error=EDSDKLib.EDSDK.EdsGetDeviceInfo(tmpPtr, out deviceInfo);
            if (error != EDSDK.EDS_ERR_OK)
            {
                //TODO Fehler behandeln
            }
            string cameraName;
            EDSDK.EdsOpenSession(tmpPtr);
            EDSDK.EdsGetPropertyData(tmpPtr, EDSDK.PropID_ProductName, 0, out cameraName);
            EDSDK.EdsCloseSession(tmpPtr);
            this.CameraList.Add(new Camera(tmpPtr,cameraName));
            error = EDSDK.EdsSetPropertyEventHandler(tmpPtr, EDSDK.PropertyEvent_All, cameraPropertyEventHandler, tmpPtr);
            error = EDSDK.EdsSetCameraStateEventHandler(tmpPtr, EDSDK.StateEvent_All, this.cameraStateEventHandler, tmpPtr);
            return 0x0;
        }

        private void scanToCameras()
        {
            IntPtr tmpptr;
            IntPtr tmpCameraPtr;
            int tmpCount = 0;
            EDSDK.EdsGetCameraList(out tmpptr);
            EDSDK.EdsGetChildCount(tmpptr, out tmpCount);
            for (int i = 0; i < tmpCount; i++)
            {
                EDSDK.EdsGetChildAtIndex(tmpptr, i, out tmpCameraPtr);
                this.CameraList.Add(new Camera(tmpCameraPtr,""));
                EDSDK.EdsSetPropertyEventHandler(tmpCameraPtr, EDSDK.PropertyEvent_All, cameraPropertyEventHandler, tmpCameraPtr);
                EDSDK.EdsSetCameraStateEventHandler(tmpCameraPtr, EDSDK.StateEvent_All, this.cameraStateEventHandler, tmpCameraPtr);
            }
        }

        public Cameralist()
        {
            uint error = 0;
            //TODO export to init method
            this.cameraList = new ObservableCollection<Camera>();
            this.CameraAddedHandler = new EDSDKLib.EDSDK.EdsCameraAddedHandler(onCameraAdded);
            error = EDSDKLib.EDSDK.EdsSetCameraAddedHandler(cameraAddedHandler, IntPtr.Zero);
            this.cameraPropertyEventHandler = new EDSDK.EdsPropertyEventHandler(onCameraPropertyChanged);
            this.cameraStateEventHandler = new EDSDK.EdsStateEventHandler(onCameraStateChanged);
            if (error != EDSDK.EDS_ERR_OK)
            {
                //TODO Fehler behandeln
            }
            this.eventIDs = new EventCodes();
            this.propertyCodes = new PropertyCodes();
            scanToCameras();
        }

        private uint onCameraPropertyChanged(uint inEvent, uint inPropertyID, uint inParameter, IntPtr inContext)
        {
            if(onCameraPropertyChangedEvent!=null){
                    onCameraPropertyChangedEvent(new PropertyEventArgs(inPropertyID));
                }
            
            return 0x0;
        }

        private uint onCameraStateChanged(uint inEvent, uint inParameter, IntPtr inContext)
        {
            if (inEvent == EDSDK.StateEvent_Shutdown)
            {
                EDSDK.EdsCloseSession(inContext);
                for (int i = 0; i < this.CameraList.Count; i++)
                {
                    if (this.CameraList.ElementAt(i).Ptr == inContext)
                    {
                        this.CameraList.RemoveAt(i);
                    }
                }
                //TODO wenn Kamera von der Liste gelöscht wird müssen alle Felder der GUI über das Databinding gelöscht werden
            }
            return 0x0;
        }


        private int getCameraIndexFromList(IntPtr cameraPtr)
        {
            for (int i = 0; i < this.CameraList.Count; i++)
            {
                if (this.CameraList.ElementAt(i).Ptr == cameraPtr)
                {
                    return i;
                }
            }
            return -1;
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
