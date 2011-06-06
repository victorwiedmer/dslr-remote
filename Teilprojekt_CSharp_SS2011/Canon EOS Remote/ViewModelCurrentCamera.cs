using System;
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
        private EDSDK.EdsPropertyDesc PropertyDescISO;
        private CollectionView availableISOListView;
        private ObservableCollection<int> availableISOListCollection;
        private ISOSpeeds isoSpeeds;
        private classes.ShutterTimes shutterTimes;
        private EDSDK.EdsPropertyDesc propertyDescTv;
        private CollectionView availableShutterTimesView;
        private ObservableCollection<string> availableShutterTimesCollection;
        private EDSDK.EdsPropertyDesc propertyDescAE;
        private CollectionView aEView;
        private ObservableCollection<string> aECollection;
        private classes.AEModes aeModes;
        private bool isolistemtpy;
        private bool shuttertimeslistempty;
        private bool aelistempty;
        private int currentISO;
        private Command_TakePhoto commandTakePhoto;
        private IntPtr streamref;
        private IntPtr imageref;
        private classes.PropertyCodes propertyCodes;
        private string currentProgramm;

        public string CurrentProgramm
        {
            get { return currentProgramm; }
            set { currentProgramm = value;
            update("CurrentProgramm");
            }
        }

        public classes.PropertyCodes PropertyCodes
        {
            get { return propertyCodes; }
            set { propertyCodes = value; }
        }

        public IntPtr Imageref
        {
            get { return imageref; }
            set { imageref = value; }
        }

        public Command_TakePhoto CommandTakePhoto
        {
            get { return commandTakePhoto; }
            set { commandTakePhoto = value; }
        }

        public int CurrentISO
        {
            get { return currentISO; }
            set
            {
                currentISO = value;
                update("CurrentISO");
            }
        }
        private string currentTv;

        public string CurrentTv
        {
            get { return currentTv; }
            set
            {
                currentTv = value;
                update("CurrentTv");
            }
        }

        public EDSDK.EdsPropertyDesc PropertyDescAE
        {
            get { return propertyDescAE; }
            set
            {
                propertyDescAE = value;
                update("PropertyDescAE");
            }
        }

        public CollectionView AEView
        {
            get { return aEView; }
            set
            {
                aEView = value;
                update("AEView");
            }
        }

        public ObservableCollection<string> AECollection
        {
            get { return aECollection; }
            set
            {
                aECollection = value;
                update("AECollection");
            }
        }

        public classes.AEModes AeModes
        {
            get { return aeModes; }
            set
            {
                aeModes = value;
                update("AeModes");
            }
        }

        public CollectionView AvailableShutterTimesView
        {
            get { return availableShutterTimesView; }
            set
            {
                availableShutterTimesView = value;
                update("AvailableShutterTimesView");
            }
        }

        public ObservableCollection<string> AvailableShutterTimesCollection
        {
            get { return availableShutterTimesCollection; }
            set
            {
                availableShutterTimesCollection = value;
                update("AvailableShutterTimesCollection");
            }
        }

        public ObservableCollection<int> AvailableISOListCollection
        {
            get { return availableISOListCollection; }
            set
            {
                availableISOListCollection = value;
                update("AvailableISOListCollection");
            }
        }

        public CollectionView AvailableISOListView
        {
            get { return availableISOListView; }
            set
            {
                availableISOListView = value;
                update("AvailableISOListView");
            }
        }

        public EDSDK.EdsPropertyDesc AvailableISOList
        {
            get { return PropertyDescISO; }
            set
            {
                PropertyDescISO = value;
                update("AvailableISOList");
            }
        }

        public Camera CurrentCamera
        {
            get { return currentCamera; }
            set
            {
                currentCamera = value;
                update("CurrentCamera");
                this.CommandTakePhoto.Camera = currentCamera.CameraPtr;
            }
        }

        public ViewModelCurrentCamera()
        {
            Console.WriteLine("Intance of ViewModelCurrentCamera created.");
            this.CurrentCameraName = "CurrentCameraName";
            this.CurrentCameraOwner = "CurrentCameraOwner";
            this.CurrentCameraFirmware = "CurrentCameraFirmware";
            this.currentProgramm = "CurrentProgramm";
            this.CurrentBatteryLevel = 50;
            this.AvailableISOListCollection = new ObservableCollection<int>();
            this.AvailableISOListView = new CollectionView(this.AvailableISOListCollection);
            this.AvailableShutterTimesCollection = new ObservableCollection<string>();
            this.availableShutterTimesView = new CollectionView(this.AvailableShutterTimesCollection);
            this.AECollection = new ObservableCollection<string>();
            this.AEView = new CollectionView(this.AECollection);
            this.isoSpeeds = new ISOSpeeds();
            this.shutterTimes = new classes.ShutterTimes();
            this.AeModes = new classes.AEModes();
            this.isolistemtpy = true;
            this.shuttertimeslistempty = true;
            this.aelistempty = true;
            this.CurrentISO = 100;
            this.CurrentTv = "Lange";
            this.CommandTakePhoto = new Command_TakePhoto();
            this.CommandTakePhoto.Camera = IntPtr.Zero;
            this.propertyCodes = new classes.PropertyCodes();
        }

        public string CurrentCameraFirmware
        {
            get { return currentCameraFirmware; }
            set
            {
                currentCameraFirmware = value;
                update("CurrentCameraFirmware");
            }
        }

        public string CurrentCameraOwner
        {
            get { return currentCameraOwner; }
            set
            {
                currentCameraOwner = value;
                update("CurrentCameraOwner");
            }
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

        public void setCurrentlyCamera()
        {
            Console.WriteLine("Got Event to setCurrentlyCamera()");
            this.CurrentCameraName = this.currentCamera.CameraName;
            this.CurrentBatteryLevel = (int)this.currentCamera.CameraBatteryLevel;
            this.CurrentBodyID = this.currentCamera.CameraBodyID;
            this.currentCamera.getAvailableShotsFromCamera();
            this.CurrentAvailableShots = (int)this.currentCamera.CameraAvailableShots;
            this.CurrentCameraOwner = this.currentCamera.CameraOwner;
            this.CurrentCameraFirmware = this.currentCamera.CameraFirmware;
            this.AvailableISOList = this.CurrentCamera.AvailableISOSpeeds;
            if (this.isolistemtpy) copyPropertyDescISOToCollection();
            this.AvailableISOListView.CurrentChanged += new EventHandler(sendISOSpeedToCamera);
            this.AvailableShutterTimesView.CurrentChanged += new EventHandler(sendShutterTimeToCamera);
            this.AEView.CurrentChanged += new EventHandler(sendAEModeToCamera);
            this.propertyDescTv = this.CurrentCamera.AvailableShutterspeeds;
            if (this.shuttertimeslistempty) copyPropertyDescShutterTimesToCollection();
            this.propertyDescAE = this.CurrentCamera.AvailableAEModes;
            if (this.aelistempty) copyPropertyDescAEModesToCollection();
            this.CurrentCamera.getISOSpeedFromCamera();
            this.CurrentISO = (int)this.isoSpeeds.getISOSpeedFromHex(this.currentCamera.CameraISOSpeed);
            this.CurrentCamera.getTvFromCamera();
            this.CurrentTv = this.shutterTimes.getShutterTimeStringFromHex(this.currentCamera.CameraShutterTime);
            this.CurrentProgramm = this.AeModes.getAEString(this.CurrentCamera.CameraAEMode);
        }

        public void updateCurrentlyCamera(classes.PropertyEventArgs p)
        {
            Console.WriteLine("Got Event to updateCurrentlyCamera() to update following property from camera " + this.PropertyCodes.getPropertyString(p.PropertyName));
            switch (p.PropertyName)
            {
                case EDSDK.PropID_Tv:
                    {
                        this.CurrentCamera.getTvFromCamera();
                        this.CurrentTv = this.shutterTimes.getShutterTimeStringFromHex(this.currentCamera.CameraShutterTime);
                        break;
                    }
                case EDSDK.PropID_ISOSpeed:
                    {
                        this.CurrentCamera.getISOSpeedFromCamera();
                        this.CurrentISO = (int)this.isoSpeeds.getISOSpeedFromHex(this.currentCamera.CameraISOSpeed);
                        break;
                    }
                case EDSDK.PropID_AvailableShots:
                    {
                        this.currentCamera.getAvailableShotsFromCamera();
                        this.CurrentAvailableShots = (int)this.currentCamera.CameraAvailableShots;
                        break;
                    }
                case EDSDK.PropID_BatteryLevel:
                    {
                        this.currentCamera.getCameraBatteryLevelFromBody();
                        this.CurrentBatteryLevel = (int)this.currentCamera.CameraBatteryLevel;
                        break;
                    }
                case EDSDK.PropID_FirmwareVersion:
                    {
                        this.currentCamera.getFirmwareVersion();
                        this.CurrentCameraFirmware = this.currentCamera.CameraFirmware;
                        break;
                    }

                case EDSDK.PropID_ProductName:
                    {
                        this.currentCamera.getCameraName();
                        this.CurrentCameraName = this.currentCamera.CameraName;
                        break;
                    }
                case EDSDK.PropID_OwnerName:
                    {
                        this.currentCamera.getCameraOwner();
                        this.CurrentCameraOwner = this.currentCamera.CameraOwner;
                        break;
                    }
                case EDSDK.PropID_BodyIDEx:
                    {
                        this.currentCamera.getBodyID();
                        this.CurrentBodyID = this.currentCamera.CameraBodyID;
                        break;
                    }
                case EDSDK.PropID_AEMode:
                    {
                        this.currentCamera.getAEModeFromCamera();
                        this.CurrentProgramm = this.AeModes.getAEString(this.CurrentCamera.CameraAEMode);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Cant identify PropertyID");
                        break;
                    }
            }
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

        private void sendISOSpeedToCamera(object sender, EventArgs e)
        {
            int tmpProperty = 0;
            Console.WriteLine("Got currentItem from ISO Combobox : " + this.AvailableISOListView.CurrentItem);
            if (this.AvailableISOListView.CurrentItem != null)
            {
                tmpProperty = (int)this.AvailableISOListView.CurrentItem;
                this.CurrentCamera.setISOSpeedToCamera((int)this.isoSpeeds.getISOSpeedFromDec((uint)tmpProperty));
            }
        }

        private void copyPropertyDescShutterTimesToCollection()
        {
            this.shuttertimeslistempty = false;
            this.AvailableShutterTimesCollection.Clear();
            Console.WriteLine("Copy all PropertyDesc values from ShutterTimes to CollectionView");
            for (int i = 0; i < this.propertyDescTv.NumElements; i++)
            {
                this.AvailableShutterTimesCollection.Add(this.shutterTimes.getShutterTimeStringFromHex((uint)this.propertyDescTv.PropDesc[i]));
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

        private void copyPropertyDescAEModesToCollection()
        {
            this.aelistempty = false;
            this.AECollection.Clear();
            Console.WriteLine("Copy all PropertyDesc values from AEModes to CollectionView");
            for (int i = 0; i < this.propertyDescAE.NumElements; i++)
            {
                this.AECollection.Add("value" + i);
            }
            Console.WriteLine("Copy Finished (" + this.AECollection.Count + ") items");
        }

        private void sendAEModeToCamera(object sender, EventArgs e)
        {
            string tmpProperty = "";
            Console.WriteLine("Got currentItem from ShutterTime Combobox : " + this.AEView.CurrentItem);
            if (this.AEView.CurrentItem != null)
            {
                tmpProperty = (string)this.AEView.CurrentItem;
                this.CurrentCamera.setAEModeToCamera((int)this.AeModes.getAEHex(tmpProperty));
            }
        }
    }
}
