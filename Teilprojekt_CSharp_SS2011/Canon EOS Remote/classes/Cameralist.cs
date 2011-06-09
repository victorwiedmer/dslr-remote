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
                Console.WriteLine("Error while getting cameralist : " + error);
            }
            /*
             * Getting count of cameralist childs to choose the last adding on the list
             * */
            error=EDSDKLib.EDSDK.EdsGetChildCount(tmpPtr, out tmpCount);
            if (error != EDSDK.EDS_ERR_OK)
            {
                Console.WriteLine("Error while getting count of cameralist childs : " + error);
            }
            /*
             * Get the camera pointer of the last object on the cameralist
             * */
            error=EDSDKLib.EDSDK.EdsGetChildAtIndex(tmpPtr, tmpCount - 1, out tmpPtr);
            if (error != EDSDK.EDS_ERR_OK)
            {
                Console.WriteLine("Error while getting camerapointer : " + error);
            }
            /*
             * Getting device info of given camera pointer
             * */
            error=EDSDKLib.EDSDK.EdsGetDeviceInfo(tmpPtr, out deviceInfo);
            if (error != EDSDK.EDS_ERR_OK)
            {
                Console.WriteLine("Error while getting deviceinfo : " + error);
            }
            this.CameraList.Add(new Camera(tmpPtr,deviceInfo.szDeviceDescription));
            error = EDSDK.EdsSetPropertyEventHandler(tmpPtr, EDSDK.PropertyEvent_All, cameraPropertyEventHandler, tmpPtr);
            error = EDSDK.EdsSetCameraStateEventHandler(tmpPtr, EDSDK.StateEvent_All, this.cameraStateEventHandler, tmpPtr);
            return 0x0;
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
                Console.WriteLine("Error while adding cameraAddedEvent : " + ErrorCodes.getErrorDataWithCodeNumber(error));
            }
            this.eventIDs = new EventCodes();
            this.propertyCodes = new PropertyCodes();
        }

        private uint onCameraPropertyChanged(uint inEvent, uint inPropertyID, uint inParameter, IntPtr inContext)
        {
            Console.WriteLine("Cameralist meldet, in einer Kamera hat sich was geaendert : \n" +
                this.CameraList.ElementAt(getCameraIndexFromList(inContext)).Name + "\nEventID:" +
                this.eventIDs.getEventIDString(inEvent) +"\nPropertyID : " + this.propertyCodes.getPropertyString(inPropertyID));
                
            if(onCameraPropertyChangedEvent!=null){
                    onCameraPropertyChangedEvent(new PropertyEventArgs(inPropertyID));
                }
            
            return 0x0;
        }

        private uint onCameraStateChanged(uint inEvent, uint inParameter, IntPtr inContext)
        {
            Console.WriteLine("State changed : " + this.eventIDs.getEventIDString(inEvent));
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
                Console.WriteLine("Cant delete camera from list");
                //TODO find out why he dont cant delete camera from list , try method getCameraIndexFromList
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
                Console.WriteLine("Cameralist say : PropertyChanged : " + property);
            }
        }

    }
}
