using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using EDSDKLib;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace Canon_EOS_Remote.ViewModel
{
   class ViewModelCurrentCamera : INotifyPropertyChanged
    {
        private string currentCameraName;
        private int currentBatteryLevel;
        private string currentBodyID;
        private int currentAvailableShots;
        private string currentCameraOwner;
        private string currentCameraFirmware;
        private Camera currentCamera;

        private EDSDK.EdsPropertyDesc availableISOList;
        private CollectionView availableISOListView;
        private ObservableCollection<int> availableISOListCollection;
        private ISOSpeeds isoSpeeds;

        private classes.ShutterTimes shutterTimes;
        private EDSDK.EdsPropertyDesc availableShutterTimes;
        private CollectionView availableShutterTimesView;
        private ObservableCollection<string> availableShutterTimesCollection;

        public CollectionView AvailableShutterTimesView
        {
            get { return availableShutterTimesView; }
            set { availableShutterTimesView = value;
            update("AvailableShutterTimesView");
            }
        }

        public ObservableCollection<string> AvailableShutterTimesCollection
        {
            get { return availableShutterTimesCollection; }
            set { availableShutterTimesCollection = value;
            update("AvailableShutterTimesCollection");
            }
        }
       

        private bool isolistemtpy;
        private bool shuttertimeslistempty;

        public ObservableCollection<int> AvailableISOListCollection
        {
            get { return availableISOListCollection; }
            set { availableISOListCollection = value;
            update("AvailableISOListCollection");
            }
        }

        public CollectionView AvailableISOListView
        {
            get { return availableISOListView; }
            set { availableISOListView = value;
            update("AvailableISOListView");
            }
        } 

        public EDSDK.EdsPropertyDesc AvailableISOList
        {
            get { return availableISOList; }
            set { availableISOList = value;
            update("AvailableISOList");
            }
        }

        public Camera CurrentCamera
        {
            get { return currentCamera; }
            set { currentCamera = value;
            update("CurrentCamera");
            }
        }

        public ViewModelCurrentCamera()
        {
            Console.WriteLine("Intance of ViewModelCurrentCamera created.");
            this.CurrentCameraName = "CurrentCameraName";
            this.CurrentCameraOwner = "CurrentCameraOwner";
            this.CurrentCameraFirmware = "CurrentCameraFirmware";
            this.CurrentBatteryLevel = 50;
            this.AvailableISOListCollection = new ObservableCollection<int>();
            this.AvailableISOListView = new CollectionView(this.AvailableISOListCollection);
            this.AvailableShutterTimesCollection = new ObservableCollection<string>();
            this.availableShutterTimesView = new CollectionView(this.AvailableShutterTimesCollection);
            this.isoSpeeds = new ISOSpeeds();
            this.shutterTimes = new classes.ShutterTimes();
            this.isolistemtpy = true;
            this.shuttertimeslistempty = true;
        }

        public string CurrentCameraFirmware
        {
            get { return currentCameraFirmware; }
            set { currentCameraFirmware = value; 
                update("CurrentCameraFirmware"); }
        }

        public string CurrentCameraOwner
        {
            get { return currentCameraOwner; }
            set { currentCameraOwner = value; 
                update("CurrentCameraOwner"); }
        }

        public int CurrentAvailableShots
        {
            get { return currentAvailableShots; }
            set
            {
                currentAvailableShots = value;
                update("CurrentAvailableShots");
            }
        }

        public string CurrentBodyID
        {
            get { return currentBodyID; }
            set
            {
                currentBodyID = value;
                update("CurrentBodyID");
            }
        }

        public int CurrentBatteryLevel
        {
            get { return currentBatteryLevel; }
            set
            {
                currentBatteryLevel = value;
                update("CurrentBatteryLevel");
            }
        }

        public string CurrentCameraName
        {
            get { return currentCameraName; }
            set
            {
                currentCameraName = value;
                Console.WriteLine("Setter CurrentCameraName called ...");
                update("CurrentCameraName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                Console.WriteLine("ViewModelCurrentCamera Property has changed : " + property);
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
            this.CurrentCameraOwner = this.currentCamera.CameraOwner;
            this.CurrentCameraFirmware = this.currentCamera.CameraFirmware;
            this.AvailableISOList = this.CurrentCamera.AvailableISOSpeeds;
            if(this.isolistemtpy)copyPropertyDescISOToCollection();
            this.AvailableISOListView.CurrentChanged += new EventHandler(sendISOSpeedToCamera);
            this.AvailableShutterTimesView.CurrentChanged += new EventHandler(sendShutterTimeToCamera);
            this.availableShutterTimes = this.CurrentCamera.AvailableShutterspeeds;
            if (this.shuttertimeslistempty) copyPropertyDescShutterTimesToCollection();
        }

        private void copyPropertyDescISOToCollection()
        {
            this.isolistemtpy = false;
            this.availableISOListCollection.Clear();
            for (int i = 0; i < this.AvailableISOList.NumElements; i++)
            {
                this.AvailableISOListCollection.Add((int)this.isoSpeeds.getISOSpeedFromHex(this.AvailableISOList.PropDesc[i]));
            }
        }

       private void sendISOSpeedToCamera(object sender , EventArgs e){
           int tmpProperty = 0;
           Console.WriteLine("Got currentItem from ISO Combobox : " + this.AvailableISOListView.CurrentItem);
           if (this.AvailableISOListView.CurrentItem != null)
           {
           tmpProperty = (int) this.AvailableISOListView.CurrentItem;
               this.CurrentCamera.setISOSpeedToCamera((int)this.isoSpeeds.getISOSpeedFromDec((uint)tmpProperty));
           }
       }

       private void copyPropertyDescShutterTimesToCollection()
       {
           this.shuttertimeslistempty = false;
           this.AvailableShutterTimesCollection.Clear();
           Console.WriteLine("Copy all PropertyDesc values from ShutterTimes to CollectionView");
           for (int i = 0; i < this.availableShutterTimes.NumElements; i++)
           {
               this.AvailableShutterTimesCollection.Add(this.shutterTimes.getShutterTimeStringFromHex((uint)this.availableShutterTimes.PropDesc[i]));
           }
           Console.WriteLine("Copy Finished (" + this.AvailableShutterTimesCollection.Count + ") items");
       }

       private void sendShutterTimeToCamera(object sender, EventArgs e)
       {
           string tmpProperty = "";
           Console.WriteLine("Got currentItem from ShutterTime Combobox : " + this.AvailableShutterTimesView.CurrentItem);
           if (this.AvailableShutterTimesView.CurrentItem != null)
           {
               tmpProperty = (string)this.AvailableShutterTimesView.CurrentItem;
               this.CurrentCamera.setShutterTimeToCamera((int)this.shutterTimes.getShutterTimeStringFromDec(tmpProperty));
           }
       }
    }
}
