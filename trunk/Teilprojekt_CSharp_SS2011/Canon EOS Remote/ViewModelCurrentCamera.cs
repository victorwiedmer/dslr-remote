using System;
using System.ComponentModel;
using EDSDKLib;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Canon_EOS_Remote.classes;
using System.Collections.Generic;

namespace Canon_EOS_Remote.ViewModel
{
    class ViewModelCurrentCamera : INotifyPropertyChanged
    {
        #region Kamera Parameter der aktuellen Kamera
        private string currentCameraName;
        private int currentBatteryLevel;
        private string currentBodyID;
        private int currentAvailableShots;
        private string currentCameraOwner;
        private string currentCameraFirmware;
        private Camera currentCamera;
        private string currentEBV;
        private EDSDK.EdsPropertyDesc PropertyDescISO;
        private EDSDK.EdsPropertyDesc propertyDescTv;
        private EDSDK.EdsPropertyDesc propertyDescEBV;
        #endregion

        #region Script
        private string script;
        private CommandChangeTv commandChangeTv;
        private Command_RunScript scriptCommand;
        private CommandScriptPhoto scriptTakePhoto;
        private CommandChangeAv commandChangeAv;
        #endregion

        #region CollectionViews für die GUI
        //Collections Views der Kamera Einstellung
        private CollectionView availableISOListView;
        private CollectionView availableShutterTimesView;
        private CollectionView availableEBVView;
        private CollectionView aEView;
        private CollectionView apertureView;
        // Collection Views der Scriptsteuerung
        private CollectionView scriptIso;
        private CollectionView scriptAperture;
        private CollectionView scriptTv;
        private CollectionView scriptEbv;
        #endregion



        private ObservableCollection<string> availableISOListCollection;
        private ObservableCollection<string> availableShutterTimesCollection;
        private ObservableCollection<string> availableEVBCollection;

        #region Konverter fuer die Umwandlung von String zu Hexadezimal
        private ISOSpeeds isoConverter;
        private ShutterTimes shutterTimeConverter;
        private AEModes aeModeConverter;
        private ExposureCompensation ebvConverter;
        #endregion




        

        public delegate void scriptEventHandler(string e);

        private EDSDK.EdsPropertyDesc propertyDescAE;
        
        private ObservableCollection<string> aECollection;
        

        public event PropertyChangedEventHandler PropertyChanged;

        private string currentISO;

        #region Commands

        private Command_TakePhoto commandTakePhoto; //Macht n-Fotos die ueber die Kameraeinstellung festgelegt wurden

        #region Commands fuer die Objektivfokus Steuerung
        private Command_DriveLensNearOne commandDriveLensNearOne;
        private Command_DriveLensNearTwo commandDriveLensNearTwo;
        private Command_DriveLensNearThree commandDriveLensNearThree;

        private Command_DriveLensFarOne commandDriveLensFarOne;
        private Command_DriveLensFarTwo commandDriveLensFarTwo;
        private Command_DriveLensFarThree commandDriveLensFarThree;
        #endregion
        #region Commands der Scriptsteuerung
        private CommandChangeEBV commandChangeEbv;
        #endregion
        #endregion

        private string currentDate;
        private string currentTime;
        private EDSDK.EdsPropertyDesc apertureDesc;

        
        private ObservableCollection<string> apertureCollection;
        private IntPtr streamref;
        private IntPtr imageref;
        private PropertyCodes propertyCodes;
        private string currentProgramm;
        private string currentAperture;
        private Apertures apertureConverter;

        private string currentTv;
        

        private CommandChangeISO commandChangeIso;

        public CommandScriptPhoto Command_ScriptTakePhoto
        {
            get { return scriptTakePhoto; }
            set
            {
                scriptTakePhoto = value;
                update("Command_ScriptTakePhoto");
            }
        }

        public CommandChangeAv CommandChangeAv
        {
            get { return commandChangeAv; }
            set
            {
                commandChangeAv = value;
                update("CommandChangeAv");
            }
        }

        

        public CommandChangeEBV CommandChangeEbv
        {
            get { return commandChangeEbv; }
            set
            {
                commandChangeEbv = value;
                update("CommandChangeEbv");
            }
        }

        public Command_RunScript ScriptCommand
        {
            get { return scriptCommand; }
            set
            {
                scriptCommand = value;
                update("ScriptCommand");
            }
        }

        public CommandChangeTv CommandChangeTv
        {
            get { return commandChangeTv; }
            set
            {
                commandChangeTv = value;
                update("CommandChangeTv");
            }
        }

        public CollectionView ScriptIso
        {
            get { return scriptIso; }
            set { scriptIso = value;
            update("ScriptIso");
            }
        }
        public CollectionView ScriptAperture
        {
            get { return scriptAperture; }
            set { scriptAperture = value;
            update("ScriptAperture");
            }
        }

        public CollectionView ScriptTv
        {
            get { return scriptTv; }
            set { scriptTv = value;
            update("ScriptTv");
            }
        }
        public CollectionView ScriptEbv
        {
            get { return scriptEbv; }
            set { scriptEbv = value;
            update("ScriptEbv");
            }
        }

        public CommandChangeISO CommandChangeIso
        {
            get { return commandChangeIso; }
            set { commandChangeIso = value;
            update("CommandChangeIso");
            }
        }

        public CollectionView AvailableEBVView
        {
            get { return availableEBVView; }
            set { availableEBVView = value;
            update("AvailableEBVView");
            }
        }


        public string Script
        {
            get { return script; }
            set
            {
                script = value;
                update("Script");
            }
        }

        public ObservableCollection<string> AvailableEVBCollection
        {
            get { return availableEVBCollection; }
            set { availableEVBCollection = value;
            update("AvailableEVBCollection");
            }
        }

        public EDSDK.EdsPropertyDesc PropertyDescEBV
        {
            get { return propertyDescEBV; }
            set
            {
                propertyDescEBV = value;
                update("propertyDescEBV");
            }
        }

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

        public string CurrentISO
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

        public ObservableCollection<string> AvailableISOListCollection
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

                this.CommandTakePhoto.Camera = currentCamera.Ptr;

                this.CommandDriveLensNearOne.CameraPtr = currentCamera.Ptr;
                this.CommandDriveLensNearTwo.CameraPtr = currentCamera.Ptr;
                this.CommandDriveLensNearThree.CameraPtr = currentCamera.Ptr;

                this.CommandDriveLensFarOne.CameraPtr = currentCamera.Ptr;
                this.CommandDriveLensFarTwo.CameraPtr = currentCamera.Ptr;
                this.CommandDriveLensFarThree.CameraPtr = currentCamera.Ptr;
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
            this.ScriptCommand = new Command_RunScript();
            this.AvailableISOListCollection = new ObservableCollection<string>();
            this.AvailableISOListView = new CollectionView(this.AvailableISOListCollection);
            this.Command_ScriptTakePhoto = new CommandScriptPhoto();
            this.AvailableShutterTimesCollection = new ObservableCollection<string>();
            this.AvailableShutterTimesView = new CollectionView(this.AvailableShutterTimesCollection);

            this.AECollection = new ObservableCollection<string>();
            this.AEView = new CollectionView(this.AECollection);

            this.AvailableEVBCollection = new ObservableCollection<string>();
            this.AvailableEBVView = new CollectionView(this.AvailableEVBCollection);

            this.IsoConverter = new ISOSpeeds();
            this.ShutterTimeConverter = new ShutterTimes();
            this.AeModes = new classes.AEModes();
            this.CurrentISO = "CurrentISO";
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
            this.Script = "";
            this.commandChangeIso = new CommandChangeISO();
            this.CommandChangeTv = new CommandChangeTv();
            this.CommandChangeAv = new CommandChangeAv();
            this.CommandChangeEbv = new CommandChangeEBV();
            this.commandChangeIso.changeIsoCommand += addCommandToScript;
            this.CommandChangeTv.changeTvCommand += addCommandToScript;
            this.Command_ScriptTakePhoto.takePhotoCommand += addCommandToScript;
            this.CommandChangeAv.changeAvCommand += addCommandToScript;
            this.CommandChangeEbv.changeEbvCommand += addCommandToScript;
            //Initialise CollectionViews for Scriptpanel
            this.ScriptAperture = new CollectionView(this.apertureCollection);
            this.ScriptTv = new CollectionView(this.AvailableShutterTimesCollection);
            this.ScriptIso = new CollectionView(this.AvailableISOListCollection);
            this.ScriptEbv = new CollectionView(this.AvailableEVBCollection);

        }

        private void addCommandToScript(string e)
        {
            if (e == "ChangeISO")
            {
                this.Script += "Ändere ISO nach : " + this.ScriptIso.CurrentItem + "\n";
                this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr, EDSDK.PropID_ISOSpeed,sizeof(int),this.IsoConverter.getISOSpeedFromDec((string)this.ScriptIso.CurrentItem)));
            }
            if (e == "TakePhoto")
            {
                this.Script += "Foto Aufnahme\n";
                this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr,EDSDK.CameraCommand_TakePicture,0,0));
            }
            if (e == "ChangeTv")
            {
                this.Script += "Ändere Tv nach : " + this.ScriptTv.CurrentItem + "\n";
                this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr, EDSDK.PropID_Tv, sizeof(int), this.ShutterTimeConverter.getShutterTimeStringFromDec((string)this.ScriptTv.CurrentItem)));
            }
            if (e == "ChangeEBV")
            {
                this.Script += "Ändere EBV nach : " + this.ScriptEbv.CurrentItem + "\n";
                this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr, EDSDK.PropID_ExposureCompensation, sizeof(int), this.EbvConverter.getebvHex((string)this.ScriptEbv.CurrentItem)));
            }
            if (e == "ChangeAv")
            {
                this.Script += "Ändere Av nach : " + this.ScriptAperture.CurrentItem + "\n";
                this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr, EDSDK.PropID_Av, sizeof(int), this.ApertureConverter.getApertureHex((string)this.ScriptAperture.CurrentItem)));
            }
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
            updatePropertyDescTv();
            this.propertyDescAE = this.CurrentCamera.AvailableAEModes;
            copyPropertyDescAEModesToCollection();
            this.ApertureDesc = this.CurrentCamera.AvailableApertureValues;
            copyPropertyDescAperturesToCollection();
            this.PropertyDescEBV = this.CurrentCamera.AvailableExposureCompensation;
            copyPropertyDescEbvToCollection();
        }

        private void updatePropertyDescTv()
        {
            this.propertyDescTv = this.CurrentCamera.AvailableShutterspeeds;
            copyPropertyDescShutterTimesToCollection();
        }

        /// <summary>
        /// Holt die einzelnen Felder der aktuellen Kamera
        /// </summary>
        private void getCurrentCameraFields()
        {
            this.CurrentCameraName = this.CurrentCamera.Name;
            this.CurrentBatteryLevel = (int)this.CurrentCamera.CameraBatteryLevel;
            this.CurrentBodyID = this.CurrentCamera.BodyID;
            this.CurrentAvailableShots = (int)this.CurrentCamera.CameraAvailableShots;
            this.CurrentCameraOwner = this.CurrentCamera.Owner;
            Console.WriteLine("Current CameraOwner is : " + this.CurrentCameraOwner);
            this.CurrentCameraFirmware = this.CurrentCamera.CameraFirmware;
            this.CurrentTv = this.shutterTimeConverter.getShutterTimeStringFromHex(this.CurrentCamera.CameraShutterTime);
            this.CurrentProgramm = this.AeModes.getAEString(this.CurrentCamera.CameraAEMode);
            this.CurrentAperture = this.apertureConverter.getApertureString(this.CurrentCamera.CameraAperture);
            this.CurrentDate = convertEdsTimeToDateString(this.CurrentCamera.CameraTime);
            this.CurrentTime = convertEdsTimeToTimeString(this.CurrentCamera.CameraTime);
            this.CurrentISO = this.isoConverter.getISOSpeedFromHex(this.CurrentCamera.CameraISOSpeed);
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
            this.AvailableEBVView.CurrentChanged += new EventHandler(sendEbvToCamera);
        }

        public void updateCurrentlyCamera(classes.PropertyEventArgs p)
        {
            switch (p.PropertyName)
            {
                case EDSDK.PropID_Tv:
                    {
                        this.CurrentCamera.getShutterTime();
                        this.CurrentTv = this.shutterTimeConverter.getShutterTimeStringFromHex(this.currentCamera.CameraShutterTime);
                        break;
                    }
                case EDSDK.PropID_ISOSpeed:
                    {
                        this.CurrentCamera.getIsoSpeed();
                        this.CurrentISO = this.isoConverter.getISOSpeedFromHex(this.currentCamera.CameraISOSpeed);
                        break;
                    }
                case EDSDK.PropID_AvailableShots:
                    {
                        this.currentCamera.getAvailableShots();
                        this.CurrentAvailableShots = (int)this.currentCamera.CameraAvailableShots;
                        break;
                    }
                case EDSDK.PropID_BatteryLevel:
                    {
                        this.currentCamera.getBatteryLevel();
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
                        this.currentCamera.getName();
                        this.CurrentCameraName = this.currentCamera.Name;
                        break;
                    }
                case EDSDK.PropID_OwnerName:
                    {
                        this.currentCamera.getOwner();
                        this.CurrentCameraOwner = this.currentCamera.Owner;
                        break;
                    }
                case EDSDK.PropID_BodyIDEx:
                    {
                        this.currentCamera.getBodyID();
                        this.CurrentBodyID = this.currentCamera.BodyID;
                        break;
                    }
                case EDSDK.PropID_AEMode:
                    {
                        this.currentCamera.getAeMode();
                        this.CurrentProgramm = this.AeModes.getAEString(this.CurrentCamera.CameraAEMode);
                        if (this.CurrentProgramm == "Tv - Blendenautomatik")
                        {
                            updatePropertyDescTv();
                        }
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
                        this.CurrentCamera.getTime();
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
                this.AvailableISOListCollection.Add(this.isoConverter.getISOSpeedFromHex((uint)this.AvailableISOList.PropDesc[i]));
            }
        }

        private void sendISOSpeedToCamera(object sender, EventArgs e)
        {
            string tmpProperty = "";
            if (this.AvailableISOListView.CurrentItem != null)
            {
                tmpProperty = (string)this.AvailableISOListView.CurrentItem;
                this.CurrentCamera.setISOSpeedToCamera((int)this.isoConverter.getISOSpeedFromDec(tmpProperty));
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

        private void copyPropertyDescEbvToCollection()
        {
            this.AvailableEVBCollection.Clear();
            for (int i = 0; i < this.PropertyDescEBV.NumElements; i++)
            {
                this.AvailableEVBCollection.Add(this.EbvConverter.getEbvString((uint)this.PropertyDescEBV.PropDesc[i]));
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

        private void sendEbvToCamera(object sender, EventArgs e)
        {
            if (this.AvailableEBVView.CurrentItem != null)
            {
                this.CurrentCamera.setEbvToCamera((int)this.EbvConverter.getebvHex((string)this.AvailableEBVView.CurrentItem));
            }
        }

        /// <summary>
        /// Konvertiert das Kameradatum vom Typ EdsTime zum String
        /// </summary>
        /// <param name="time">Datum das aus der Kamera gelesen wurde vom Typ EdsTime</param>
        /// <returns>String mit dem Kameradatum aus dem Parameter</returns>
        public string convertEdsTimeToDateString(EDSDK.EdsTime time)
        {
            return time.Year + "-" + time.Month + "-" + time.Day;
        }

        /// <summary>
        /// Konvertiert die Kamerazeit vom Typ EdsTime zum String
        /// </summary>
        /// <param name="time">Zeit die aus der Kamera gelesen wurde vom Typ EdsTime</param>
        /// <returns>String mit der Kamerazeit aus dem Parameter</returns>
        public string convertEdsTimeToTimeString(EDSDK.EdsTime time)
        {
            return time.Hour + "-" + time.Minute + "-" + time.Second;
        }
    }
}
