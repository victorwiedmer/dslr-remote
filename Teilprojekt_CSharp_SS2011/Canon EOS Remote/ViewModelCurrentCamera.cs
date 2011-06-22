using System;
using System.ComponentModel;
using EDSDKLib;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Canon_EOS_Remote.classes;
using System.Collections.Generic;
using Canon_EOS_Remote.Commands;

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
        private EDSDK.EdsPropertyDesc propertyDescAE;
        private EDSDK.EdsPropertyDesc apertureDesc;
        private string currentDate;
        private string currentTime;
        private string currentISO;
        private string currentProgramm;
        private string currentAperture;
        private string currentTv;
        #endregion

        #region Script Commands und Felder
        private string script;
        private CommandChangeTv commandChangeTv;
        private Command_RunScript scriptCommand;
        private CommandScriptPhoto scriptTakePhoto;
        private CommandChangeAv commandChangeAv;
        private Command_DelScript commandDelScript;
        private Command_DelScript_LastCommand commandDelLastCommandScript;
        private CommandHDR commandHDR;
        private CommandChangeISO commandChangeIso;
        private CommandChangeEBV commandChangeEbv;
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

        #region ObservableCollections fuer die CollectionViews der GUI
        private ObservableCollection<string> availableISOListCollection;
        private ObservableCollection<string> availableShutterTimesCollection;
        private ObservableCollection<string> availableEVBCollection;
        private ObservableCollection<string> apertureCollection;
        private ObservableCollection<string> aECollection;
        #endregion

        #region Konverter fuer die Umwandlung von String zu Hexadezimal
        private ISOSpeeds isoConverter;
        private ShutterTimes shutterTimeConverter;
        private AEModes aeModeConverter;
        private ExposureCompensation ebvConverter;
        private Apertures apertureConverter;
        private PropertyCodes propertyCodesConverter;
        #endregion

        #region Events
        public delegate void scriptEventHandler(string e);
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

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
        #endregion

        private IntPtr streamref;
        private IntPtr imageref;
        
        #region setter und getter methoden der klassenfelder

        public CommandHDR CommandHDR
        {
            get { return commandHDR; }
            set { commandHDR = value;
            update("CommandHDR");
            }
        }

        public EDSDK.EdsPropertyDesc PropertyDescTv
        {
            get { return propertyDescTv; }
            set
            {
                propertyDescTv = value;
                update("PropertyDescTv");
            }
        }

        public Command_DelScript_LastCommand CommandDelLastCommandScript
        {
            get { return commandDelLastCommandScript; }
            set { commandDelLastCommandScript = value;
            update("CommandDelLastCommandScript");
            }
        }

        public Command_DelScript CommandDelScript
        {
            get { return commandDelScript; }
            set { commandDelScript = value;
            update("CommandDelScript");
            }
        }

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
       
        public ObservableCollection<string> ApertureCollection
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
            get { return propertyCodesConverter; }
            set { propertyCodesConverter = value;
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

#endregion

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                Console.WriteLine("\n\nViewModelCurrentCamera Property has changed : " + property + "\n\n");
            }
        }

        public ViewModelCurrentCamera()
        {
            init();
        }

        private void instance()
        {
            // Instance Commands
            this.ScriptCommand = new Command_RunScript();
            this.Command_ScriptTakePhoto = new CommandScriptPhoto();
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
            this.commandChangeIso = new CommandChangeISO();
            this.CommandChangeTv = new CommandChangeTv();
            this.CommandChangeAv = new CommandChangeAv();
            this.CommandChangeEbv = new CommandChangeEBV();
            this.CommandDelScript = new Command_DelScript();
            this.CommandDelLastCommandScript = new Command_DelScript_LastCommand();
            this.CommandHDR = new CommandHDR();
            // Instance ObservableCollections
            this.AvailableISOListCollection = new ObservableCollection<string>();
            this.AvailableShutterTimesCollection = new ObservableCollection<string>();
            this.AECollection = new ObservableCollection<string>();
            this.AvailableEVBCollection = new ObservableCollection<string>();
            this.ApertureCollection = new ObservableCollection<string>();
            //Instance CollectionViews
            this.AvailableISOListView = new CollectionView(this.AvailableISOListCollection);
            this.AvailableShutterTimesView = new CollectionView(this.AvailableShutterTimesCollection);
            this.AEView = new CollectionView(this.AECollection);
            this.AvailableEBVView = new CollectionView(this.AvailableEVBCollection);
            this.ApertureView = new CollectionView(this.ApertureCollection);
            //Initialise CollectionViews for Scriptpanel
            this.ScriptAperture = new CollectionView(this.apertureCollection);
            this.ScriptTv = new CollectionView(this.AvailableShutterTimesCollection);
            this.ScriptIso = new CollectionView(this.AvailableISOListCollection);
            this.ScriptEbv = new CollectionView(this.AvailableEVBCollection);
            //Instance Konverter
            this.IsoConverter = new ISOSpeeds();
            this.ShutterTimeConverter = new ShutterTimes();
            this.AeModeConverter = new classes.AEModes();
            this.PropertyCodes = new PropertyCodes();
            this.ApertureConverter = new Apertures();
            this.EbvConverter = new ExposureCompensation();
            this.AeModeConverter = new AEModes();
        }

        /// <summary>
        /// Setzt die Methoden an den Events, die fuer die Scriptsteuerung benoetigt werden
        /// </summary>
        private void setEvents()
        {
            this.commandChangeIso.changeIsoCommand += addCommandToScript;           //Button-Script-ISO Aendern
            this.CommandChangeTv.changeTvCommand += addCommandToScript;             //Button-Script-Belichtungszeit Aendern
            this.Command_ScriptTakePhoto.takePhotoCommand += addCommandToScript;    //Button-Script-Foto aufnehmen
            this.CommandChangeAv.changeAvCommand += addCommandToScript;             //Button-Script-Blende Aendern
            this.CommandChangeEbv.changeEbvCommand += addCommandToScript;           //Button-Script-EBV Aendern
            this.CommandDelScript.delscriptHandler += delScript;                    //Button-Script-Script loeschen
            this.CommandDelLastCommandScript.delLastCommand += delScript;           //Button-Script-letzten Befehl loeschen
            this.CommandHDR.HDRCommand += addCommandToScript;                       //Button-Script-HDR
        }

        private void init()
        {
            this.CurrentCameraName = "";
            this.CurrentCameraOwner = "";
            this.CurrentCameraFirmware = "";
            this.currentProgramm = "";
            this.CurrentAperture = "";
            this.CurrentDate = " ";
            this.CurrentTime = "";
            this.CurrentBatteryLevel = 0;
            this.CurrentEBV = "";
            this.CurrentISO = "";
            this.CurrentTv = "";
            this.Script = "";
            instance();
            setEvents();
        }

        private void delScript(string e)
        {
            if (e == "DelScript")
            {
                this.Script = "";
                this.ScriptCommand.ScriptCommands.Clear();
            }
            if (e == "DelLastCommand")
            {
                //TODO funktioniert nicht
                this.Script.Remove(
                this.Script.LastIndexOf(";")-1);
                this.Script.Remove(this.Script.LastIndexOf(";"));
            }
        }

        private void addCommandToScript(string e)
        {
            if (e == "ChangeISO")
            {
                this.Script += "Ändere ISO nach : " + this.ScriptIso.CurrentItem + ";\n";
                this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr, EDSDK.PropID_ISOSpeed,sizeof(int),this.IsoConverter.getISOSpeedFromDec((string)this.ScriptIso.CurrentItem)));
            }
            if (e == "TakePhoto")
            {
                this.Script += "Foto Aufnahme;\n";
                this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr,EDSDK.CameraCommand_TakePicture,0,0));
            }
            if (e == "ChangeTv")
            {
                this.Script += "Ändere Tv nach : " + this.ScriptTv.CurrentItem + ";\n";
                this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr, EDSDK.PropID_Tv, sizeof(int), this.ShutterTimeConverter.getShutterTimeStringFromDec((string)this.ScriptTv.CurrentItem)));
            }
            if (e == "ChangeEBV")
            {
                this.Script += "Ändere EBV nach : " + this.ScriptEbv.CurrentItem + ";\n";
                this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr, EDSDK.PropID_ExposureCompensation, sizeof(int), this.EbvConverter.getebvHex((string)this.ScriptEbv.CurrentItem)));
            }
            if (e == "ChangeAv")
            {
                this.Script += "Ändere Av nach : " + this.ScriptAperture.CurrentItem + ";\n";
                this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr, EDSDK.PropID_Av, sizeof(int), this.ApertureConverter.getApertureHex((string)this.ScriptAperture.CurrentItem)));
            }
            if (e == "HDR")
            {
                this.Script += "HDR;\n";
                for (int i = 0; i < this.PropertyDescEBV.NumElements; i++)
                {
                    this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr, EDSDK.PropID_ExposureCompensation, sizeof(int), (uint)this.PropertyDescEBV.PropDesc[i]));
                    this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr, EDSDK.CameraCommand_TakePicture, 0, 0));
                }
            }

        }

        /// <summary>
        /// Ruft die Methoden auf um die Anzeigefelder der GUI, die Parametertabellen der Kamera und EventHandler zu setzen
        /// </summary>
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

        /// <summary>
        /// Updated die Property Desc fuer die Belichtungszeiten
        /// </summary>
        private void updatePropertyDescTv()
        {
            this.CurrentCamera.getpropertyDescShutterTimes();
            this.PropertyDescTv = this.CurrentCamera.AvailableShutterspeeds;
            copyPropertyDescShutterTimesToCollection();
        }

        private void updatePropertyDescAv()
        {
            this.CurrentCamera.getpropertyDescApertureValues();
            this.ApertureDesc = this.CurrentCamera.AvailableApertureValues;
            copyPropertyDescAperturesToCollection();
        }

        private void updatePropertyDescEBV()
        {
            this.CurrentCamera.getpropertyDescExposureCompensation();
            this.PropertyDescEBV = this.CurrentCamera.AvailableExposureCompensation;
            copyPropertyDescEbvToCollection();
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
            this.CurrentCameraFirmware = this.CurrentCamera.CameraFirmware;
            this.CurrentTv = this.shutterTimeConverter.getShutterTimeStringFromHex(this.CurrentCamera.CameraShutterTime);
            this.CurrentProgramm = this.AeModeConverter.getAEString(this.CurrentCamera.CameraAEMode);
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
                case EDSDK.PropID_Tv: // Wenn sich die Eigenschaft Belichtungszeit aendert
                    {
                        this.CurrentCamera.getShutterTime();
                        this.CurrentTv = this.shutterTimeConverter.getShutterTimeStringFromHex(this.currentCamera.CameraShutterTime);
                        break;
                    }
                case EDSDK.PropID_ISOSpeed: // Wenn sich die Eigenschaft ISO aendert
                    {
                        this.CurrentCamera.getIsoSpeed();
                        this.CurrentISO = this.isoConverter.getISOSpeedFromHex(this.currentCamera.CameraISOSpeed);
                        break;
                    }
                case EDSDK.PropID_AvailableShots: //Wenn sich die Eigenschaft Anzahl der freien Fotos aendert
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
                        this.CurrentProgramm = this.AeModeConverter.getAEString(this.CurrentCamera.CameraAEMode);
                        if (this.CurrentCamera.CameraAEMode==1)
                        {
                            updatePropertyDescTv();
                            updatePropertyDescEBV();
                            delPropertyDescAperturesFromCollection();
                        }
                        if (this.CurrentCamera.CameraAEMode == 2)
                        {
                            updatePropertyDescAv();
                            updatePropertyDescEBV();
                            delPropertyDescShutterTimesFromCollection();
                        }
                        if (this.CurrentCamera.CameraAEMode == 3)
                        {
                            updatePropertyDescTv();
                            updatePropertyDescAv();
                            delPropertyDescEbvFromCollection();
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

        /// <summary>
        /// Kopiert die hexadezimalen Werte der Tabelle fuer die verfuegbaren ISO Werte als String in die CollectionList
        /// </summary>
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
            if (this.AvailableISOListView.CurrentItem != null)
            {
                this.CurrentCamera.setISOSpeedToCamera((int)this.isoConverter.getISOSpeedFromDec((string)this.AvailableISOListView.CurrentItem));
            }
        }

        private void copyPropertyDescShutterTimesToCollection()
        {
            this.AvailableShutterTimesCollection.Clear();
            for (int i = 0; i < this.PropertyDescTv.NumElements; i++)
            {
                this.AvailableShutterTimesCollection.Add(this.ShutterTimeConverter.getShutterTimeStringFromHex((uint)this.PropertyDescTv.PropDesc[i]));
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
            this.ApertureCollection.Clear();
            for (int i = 0; i < this.ApertureDesc.NumElements; i++)
            {
                this.ApertureCollection.Add(this.apertureConverter.getApertureString((uint)this.ApertureDesc.PropDesc[i]));
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

        private void delPropertyDescAperturesFromCollection()
        {
            this.ApertureCollection.Clear();
        }

        private void delPropertyDescEbvFromCollection()
        {
            this.AvailableEVBCollection.Clear();
        }

        private void delPropertyDescShutterTimesFromCollection()
        {
            this.AvailableShutterTimesCollection.Clear();
        }

        private void delPropertyDescISOFromCollection()
        {
            this.AvailableISOListCollection.Clear();
        }

        private void sendAEModeToCamera(object sender, EventArgs e)
        {
            string tmpProperty = "";
            if (this.AEView.CurrentItem != null)
            {
                tmpProperty = (string)this.AEView.CurrentItem;
                this.CurrentCamera.setAEModeToCamera((int)this.AeModeConverter.getAEHex(tmpProperty));
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
