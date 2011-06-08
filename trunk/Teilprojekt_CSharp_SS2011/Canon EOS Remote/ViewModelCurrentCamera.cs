using System;
using System.ComponentModel;
using EDSDKLib;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Canon_EOS_Remote.classes;

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
        private string currentEBV;

        private EDSDK.EdsPropertyDesc PropertyDescISO;

        private CollectionView availableISOListView;

        private ObservableCollection<int> availableISOListCollection;

        private ISOSpeeds isoConverter;

        private ShutterTimes shutterTimeConverter;

        private EDSDK.EdsPropertyDesc propertyDescTv;
        private CollectionView availableShutterTimesView;
        private ObservableCollection<string> availableShutterTimesCollection;
        private EDSDK.EdsPropertyDesc propertyDescAE;
        private CollectionView aEView;
        private ObservableCollection<string> aECollection;
        private AEModes aeModeConverter;

        public event PropertyChangedEventHandler PropertyChanged;

        private int currentISO;

        #region Commands

        private Command_TakePhoto commandTakePhoto;

        private Command_DriveLensNearOne commandDriveLensNearOne;
        private Command_DriveLensNearTwo commandDriveLensNearTwo;
        private Command_DriveLensNearThree commandDriveLensNearThree;

        private Command_DriveLensFarOne commandDriveLensFarOne;
        private Command_DriveLensFarTwo commandDriveLensFarTwo;
        private Command_DriveLensFarThree commandDriveLensFarThree;
        #endregion

        private string currentDate;
        private string currentTime;
        private EDSDK.EdsPropertyDesc apertureDesc;

        private CollectionView apertureView;
        private ObservableCollection<string> apertureCollection;
        private IntPtr streamref;
        private IntPtr imageref;
        private PropertyCodes propertyCodes;
        private string currentProgramm;
        private string currentAperture;
        private Apertures apertureConverter;

        private string currentTv;
        private ExposureCompensation ebvConverter;

        public ExposureCompensation EbvConverter
        {
            get { return ebvConverter; }
            set { ebvConverter = value;
            update("EbvConverter");
            }
        }

        public string CurrentEBV
        {
            get { return currentEBV; }
            set { currentEBV = value;
            update("CurrentEBV");
            }
        }

        public AEModes AeModeConverter
        {
            get { return aeModeConverter; }
            set { aeModeConverter = value;
            update("AeModeConverter");
            }
        }

        public EDSDK.EdsPropertyDesc ApertureDesc
        {
            get { return apertureDesc; }
            set { apertureDesc = value;
            update("ApertureDesc");
            }
        }
        
        public CollectionView ApertureView
        {
            get { return apertureView; }
            set { apertureView = value;
            update("ApertureView");
            }
        }
       
        internal ObservableCollection<string> AptureCollection
        {
            get { return apertureCollection; }
            set { apertureCollection = value;
            update("AptureCollection");
            }
        }

        public string CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value;
            update("CurrentTime");
            }
        }

        public string CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value;
            update("CurrentDate");
            }
        }

        public Command_DriveLensNearOne CommandDriveLensNearOne
        {
            get { return commandDriveLensNearOne; }
            set { commandDriveLensNearOne = value;
            update("CommandDriveLensNearOne");
            }
        }

        public Command_DriveLensNearTwo CommandDriveLensNearTwo
        {
            get { return commandDriveLensNearTwo; }
            set { commandDriveLensNearTwo = value;
            update("CommandDriveLensNearTwo");
            }
        }

        public Command_DriveLensFarThree CommandDriveLensFarThree
        {
            get { return commandDriveLensFarThree; }
            set { commandDriveLensFarThree = value;
            update("CommandDriveLensFarThree");
            }
        }

        public Command_DriveLensFarTwo CommandDriveLensFarTwo
        {
            get { return commandDriveLensFarTwo; }
            set { commandDriveLensFarTwo = value;
            update("CommandDriveLensFarTwo");
            }
        }

        public Command_DriveLensFarOne CommandDriveLensFarOne
        {
            get { return commandDriveLensFarOne; }
            set { commandDriveLensFarOne = value;
            update("CommandDriveLensFarOne");
            }
        }

        public Command_DriveLensNearThree CommandDriveLensNearThree
        {
            get { return commandDriveLensNearThree; }
            set { commandDriveLensNearThree = value;
            update("CommandDriveLensNearThree");
            }
        }

        public string CurrentAperture
        {
            get { return currentAperture; }
            set  { currentAperture = value;
                update("CurrentAperture");
            }
        }

        public string CurrentProgramm
        {
            get { return currentProgramm; }
            set
            {
                currentProgramm = value;
                update("CurrentProgramm");
            }
        }

        public PropertyCodes PropertyCodes
        {
            get { return propertyCodes; }
            set { propertyCodes = value;
            update("PropertyCodes");
            }
        }

        public IntPtr Imageref
        {
            get { return imageref; }
            set { imageref = value;
            update("Imageref");
            }
        }

        public Command_TakePhoto CommandTakePhoto
        {
            get { return commandTakePhoto; }
            set { commandTakePhoto = value;
            update("CommandTakePhoto");
            }
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
            set { propertyDescAE = value;
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

        public AEModes AeModes
        {
            get { return aeModeConverter; }
            set
            {
                aeModeConverter = value;
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

                #region Setze neuen Zeiger bei den Commands

                this.CommandTakePhoto.Camera = currentCamera.CameraPtr;

                this.CommandDriveLensNearOne.CameraPtr = currentCamera.CameraPtr;
                this.CommandDriveLensNearTwo.CameraPtr = currentCamera.CameraPtr;
                this.CommandDriveLensNearThree.CameraPtr = currentCamera.CameraPtr;

                this.CommandDriveLensFarOne.CameraPtr = currentCamera.CameraPtr;
                this.CommandDriveLensFarTwo.CameraPtr = currentCamera.CameraPtr;
                this.CommandDriveLensFarThree.CameraPtr = currentCamera.CameraPtr;
                #endregion

                update("CurrentCamera");
            }
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
                update("CurrentCameraName");
            }
        }

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                Console.WriteLine("\n\nViewModelCurrentCamera Property has changed : " + property+"\n\n");
            }
        }

        public ISOSpeeds IsoConverter
        {
            get { return isoConverter; }
            set
            {
                isoConverter = value;
                update("IsoConverter");
            }
        }

        public ShutterTimes ShutterTimeConverter
        {
            get { return shutterTimeConverter; }
            set
            {
                shutterTimeConverter = value;
                update("ShutterTimeConverter");
            }
        }

        public Apertures ApertureConverter
        {
            get { return apertureConverter; }
            set
            {
                apertureConverter = value;
                update("ApertureConverter");
            }
        }

        public ViewModelCurrentCamera()
        {
            init();
        }

        public void init()
        {
            this.CurrentCameraName = "CurrentCameraName";
            this.CurrentCameraOwner = "CurrentCameraOwner";
            this.CurrentCameraFirmware = "CurrentCameraFirmware";
            this.currentProgramm = "CurrentProgramm";
            this.CurrentAperture = "CurrentAperture";
            this.CurrentDate = " CurrentDate";
            this.CurrentTime = "CurrentTime";
            this.CurrentBatteryLevel = 50;
            this.CurrentEBV = "EBV";

            this.AvailableISOListCollection = new ObservableCollection<int>();
            this.AvailableISOListView = new CollectionView(this.AvailableISOListCollection);

            this.AvailableShutterTimesCollection = new ObservableCollection<string>();
            this.AvailableShutterTimesView = new CollectionView(this.AvailableShutterTimesCollection);
            this.AECollection = new ObservableCollection<string>();
            this.AEView = new CollectionView(this.AECollection);
            this.IsoConverter = new ISOSpeeds();
            this.ShutterTimeConverter = new ShutterTimes();
            this.AeModes = new classes.AEModes();
            this.CurrentISO = 100;
            this.CurrentTv = "Lange";
            this.CommandTakePhoto = new Command_TakePhoto();
            this.CommandTakePhoto.Camera = IntPtr.Zero;
            this.CommandDriveLensNearOne = new Command_DriveLensNearOne();
            this.CommandDriveLensNearOne.CameraPtr = IntPtr.Zero;
            this.CommandDriveLensNearTwo = new Command_DriveLensNearTwo();
            this.CommandDriveLensNearTwo.CameraPtr = IntPtr.Zero;
            this.CommandDriveLensNearThree = new Command_DriveLensNearThree();
            this.CommandDriveLensNearThree.CameraPtr = IntPtr.Zero;
            this.CommandDriveLensFarOne = new Command_DriveLensFarOne();
            this.CommandDriveLensFarOne.CameraPtr = IntPtr.Zero;
            this.CommandDriveLensFarTwo = new Command_DriveLensFarTwo();
            this.CommandDriveLensFarTwo.CameraPtr = IntPtr.Zero;
            this.CommandDriveLensFarThree = new Command_DriveLensFarThree();
            this.CommandDriveLensFarThree.CameraPtr = IntPtr.Zero;
            this.PropertyCodes = new PropertyCodes();
            this.ApertureConverter = new Apertures();
            this.AptureCollection = new ObservableCollection<string>();
            this.ApertureView = new CollectionView(this.AptureCollection);
            this.AeModeConverter = new AEModes();
            this.EbvConverter = new ExposureCompensation();
        }

        public void setCurrentlyCamera()
        {
            getCurrentCameraFields();
            CurrentCameraPropertyDescs();
            setEventHandlerToViews();
        }

        /// <summary>
        /// Holt die PropertyDescs von der aktuellen Kamera und kopiert sie in die Collections
        /// </summary>
        private void CurrentCameraPropertyDescs()
        {
            this.AvailableISOList = this.CurrentCamera.AvailableISOSpeeds;
            copyPropertyDescISOToCollection();
            this.propertyDescTv = this.CurrentCamera.AvailableShutterspeeds;
            copyPropertyDescShutterTimesToCollection();
            this.propertyDescAE = this.CurrentCamera.AvailableAEModes;
            copyPropertyDescAEModesToCollection();
            this.ApertureDesc = this.CurrentCamera.AvailableApertureValues;
            copyPropertyDescAperturesToCollection();
        }

        /// <summary>
        /// Holt die einzelnen Felder der aktuellen Kamera
        /// </summary>
        private void getCurrentCameraFields()
        {
            this.CurrentCameraName = this.CurrentCamera.CameraName;
            this.CurrentBatteryLevel = (int)this.CurrentCamera.CameraBatteryLevel;
            this.CurrentBodyID = this.CurrentCamera.CameraBodyID;
            this.CurrentAvailableShots = (int)this.CurrentCamera.CameraAvailableShots;
            this.CurrentCameraOwner = this.CurrentCamera.CameraOwner;
            this.CurrentCameraFirmware = this.CurrentCamera.CameraFirmware;
            this.CurrentTv = this.shutterTimeConverter.getShutterTimeStringFromHex(this.CurrentCamera.CameraShutterTime);
            this.CurrentProgramm = this.AeModes.getAEString(this.CurrentCamera.CameraAEMode);
            this.CurrentAperture = this.apertureConverter.getApertureString(this.CurrentCamera.CameraAperture);
            this.CurrentDate = convertEdsTimeToDateString(this.CurrentCamera.CameraTime);
            this.CurrentTime = convertEdsTimeToTimeString(this.CurrentCamera.CameraTime);
            this.CurrentISO = (int)this.isoConverter.getISOSpeedFromHex(this.CurrentCamera.CameraISOSpeed);
            this.CurrentEBV = this.EbvConverter.getEbvString(this.CurrentCamera.CameraExposureCompensation);
        }

       /// <summary>
        /// Wenn in den ComboBoxen ein neuer Wert gewählt wird, werden diese durch die EventHandler bei CurrentChanged an die Kamera gesendet 
       /// </summary>
        private void setEventHandlerToViews()
        {
            this.ApertureView.CurrentChanged += new EventHandler(sendApertureToCamera);
            this.AvailableISOListView.CurrentChanged += new EventHandler(sendISOSpeedToCamera);
            this.AvailableShutterTimesView.CurrentChanged += new EventHandler(sendShutterTimeToCamera);
            this.AEView.CurrentChanged += new EventHandler(sendAEModeToCamera);
        }

        public void updateCurrentlyCamera(classes.PropertyEventArgs p)
        {
            switch (p.PropertyName)
            {
                case EDSDK.PropID_Tv:
                    {
                        this.CurrentCamera.getTvFromCamera();
                        this.CurrentTv = this.shutterTimeConverter.getShutterTimeStringFromHex(this.currentCamera.CameraShutterTime);
                        break;
                    }
                case EDSDK.PropID_ISOSpeed:
                    {
                        this.CurrentCamera.getISOSpeedFromCamera();
                        this.CurrentISO = (int)this.isoConverter.getISOSpeedFromHex(this.currentCamera.CameraISOSpeed);
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
                case EDSDK.PropID_Av:
                    {
                        this.currentCamera.getApertureFromCamera();
                        this.CurrentAperture = this.apertureConverter.getApertureString(this.CurrentCamera.CameraAperture);
                        break;
                    }
                case EDSDK.PropID_DateTime:
                    {
                        this.CurrentCamera.getTimeFromCamera();
                        this.CurrentDate = convertEdsTimeToDateString(this.CurrentCamera.CameraTime);
                        this.CurrentTime = convertEdsTimeToTimeString(this.CurrentCamera.CameraTime);
                        break;
                    }
                case EDSDK.PropID_ExposureCompensation:
                    {
                        this.CurrentCamera.getEbvFromBody();
                        this.CurrentEBV = this.EbvConverter.getEbvString(this.CurrentCamera.CameraExposureCompensation);
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
            this.availableISOListCollection.Clear();
            for (int i = 0; i < this.AvailableISOList.NumElements; i++)
            {
                this.AvailableISOListCollection.Add((int)this.isoConverter.getISOSpeedFromHex(this.AvailableISOList.PropDesc[i]));
            }
        }

        private void sendISOSpeedToCamera(object sender, EventArgs e)
        {
            int tmpProperty = 0;
            if (this.AvailableISOListView.CurrentItem != null)
            {
                tmpProperty = (int)this.AvailableISOListView.CurrentItem;
                this.CurrentCamera.setISOSpeedToCamera((int)this.isoConverter.getISOSpeedFromDec((uint)tmpProperty));
            }
        }

        private void copyPropertyDescShutterTimesToCollection()
        {
            this.AvailableShutterTimesCollection.Clear();
            for (int i = 0; i < this.propertyDescTv.NumElements; i++)
            {
                this.AvailableShutterTimesCollection.Add(this.shutterTimeConverter.getShutterTimeStringFromHex((uint)this.propertyDescTv.PropDesc[i]));
            }
        }

        private void sendShutterTimeToCamera(object sender, EventArgs e)
        {
            string tmpProperty = "";
            if (this.AvailableShutterTimesView.CurrentItem != null)
            {
                tmpProperty = (string)this.AvailableShutterTimesView.CurrentItem;
                this.CurrentCamera.setShutterTimeToCamera((int)this.shutterTimeConverter.getShutterTimeStringFromDec(tmpProperty));
            }
        }

        private void copyPropertyDescAEModesToCollection()
        {
            this.AECollection.Clear();
            for (int i = 0; i < this.propertyDescAE.NumElements; i++)
            {
                this.AECollection.Add(this.AeModeConverter.getAEString((uint)this.propertyDescAE.PropDesc[i]));
            }
        }

        private void copyPropertyDescAperturesToCollection()
        {
            this.AptureCollection.Clear();
            for (int i = 0; i < this.ApertureDesc.NumElements; i++)
            {
                this.AptureCollection.Add(this.apertureConverter.getApertureString((uint)this.ApertureDesc.PropDesc[i]));
            }
        }

        private void sendAEModeToCamera(object sender, EventArgs e)
        {
            string tmpProperty = "";
            if (this.AEView.CurrentItem != null)
            {
                tmpProperty = (string)this.AEView.CurrentItem;
                this.CurrentCamera.setAEModeToCamera((int)this.AeModes.getAEHex(tmpProperty));
            }
        }

        private void sendApertureToCamera(object sender, EventArgs e)
        {
            string tmpProperty = "";
            if (this.ApertureView.CurrentItem != null)
            {
                tmpProperty = (string)this.ApertureView.CurrentItem;
                this.CurrentCamera.setApertureToCamera((int)this.apertureConverter.getApertureHex(tmpProperty));
            }
        }

        public string convertEdsTimeToDateString(EDSDK.EdsTime time)
        {
            return time.Year + "-" + time.Month + "-" + time.Day;
        }

        public string convertEdsTimeToTimeString(EDSDK.EdsTime time)
        {
            return time.Hour + "-" + time.Minute + "-" + time.Second;
        }
    }
}
