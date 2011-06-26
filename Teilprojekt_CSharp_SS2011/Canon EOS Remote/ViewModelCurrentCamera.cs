using System;
using System.ComponentModel;
using EDSDKLib;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Canon_EOS_Remote.classes;
using System.Collections.Generic;
using Canon_EOS_Remote.Commands;
using Canon_EOS_Remote.Typen_und_Listen;

namespace Canon_EOS_Remote.ViewModel
{
    class ViewModelCurrentCamera : INotifyPropertyChanged
    {
        #region Kamera Parameter der aktuellen Kamera
        private string currentCameraName;//direkte Anzeige auf der GUI
        private int currentBatteryLevel;//direkte Anzeige auf der GUI
        private string currentBodyID;//direkte Anzeige auf der GUI
        private int currentAvailableShots;//direkte Anzeige auf der GUI
        private string currentCameraOwner;//direkte Anzeige auf der GUI
        private string currentCameraFirmware;//direkte Anzeige auf der GUI

        private Camera currentCamera;

        private string currentEBV;//direkte Anzeige auf der GUI

        //Exported to ModelCurrentCamera
        private EDSDK.EdsPropertyDesc PropertyDescISO; //TODO nach Model
        private EDSDK.EdsPropertyDesc propertyDescTv;//TODO nach Model
        private EDSDK.EdsPropertyDesc propertyDescEBV;//TOD//TODO nach Model
        private EDSDK.EdsPropertyDesc propertyDescAE;//TODO nach Model
        private EDSDK.EdsPropertyDesc propertyDescAperture;//TODO nach Model
        private EDSDK.EdsPropertyDesc propertyDescDriveMode;
        private EDSDK.EdsPropertyDesc propertyDescMeteringMode;
        private EDSDK.EdsPropertyDesc propertyDescAfMode;



        //
        private string currentDate; //direkte Anzeige auf der GUI
        private string currentTime;//direkte Anzeige auf der GUI
        private string currentISO;//direkte Anzeige auf der GUI
        private string currentProgramm;//direkte Anzeige auf der GUI
        private string currentAperture;//direkte Anzeige auf der GUI
        private string currentTv;//direkte Anzeige auf der GUI
        private string driveMode;
        private string meteringMode;
        private string afMode;


        #endregion

        #region Script Commands und Felder
        private string script;
        private CommandChangeTv commandChangeTv; //TODO nach ViewModelScript
        private Command_RunScript scriptCommand; //TODO nach ViewModelScript
        private CommandScriptPhoto scriptTakePhoto;//TODO nach ViewModelScript
        private CommandChangeAv commandChangeAv;//TODO nach ViewModelScript
        private Command_DelScript commandDelScript;//TODO nach ViewModelScript
        private Command_DelScript_LastCommand commandDelLastCommandScript;//TODO nach ViewModelScript
        private CommandHDR commandHDR;//TODO nach ViewModelScript
        private CommandChangeISO commandChangeIso;//TODO nach ViewModelScript
        private CommandChangeEBV commandChangeEbv;//TODO nach ViewModelScript
        #endregion

        #region CollectionViews für die GUI
        //Collections Views der Kamera Einstellung
        private CollectionView availableISOListView;//direkte Anzeige auf der GUI
        private CollectionView availableShutterTimesView;//direkte Anzeige auf der GUI
        private CollectionView availableEBVView;//direkte Anzeige auf der GUI
        private CollectionView aEView;//direkte Anzeige auf der GUI
        private CollectionView apertureView;//direkte Anzeige auf der GUI
        private CollectionView driveModeView;
        private CollectionView meteringModeView;
        private CollectionView afModeView;
        // Collection Views der Scriptsteuerung
        private CollectionView scriptIso;//direkte Anzeige auf der GUI
        private CollectionView scriptAperture;//direkte Anzeige auf der GUI
        private CollectionView scriptTv;//direkte Anzeige auf der GUI
        private CollectionView scriptEbv;//direkte Anzeige auf der GUI
        #endregion

        #region ObservableCollections fuer die CollectionViews der GUI
        private ObservableCollection<string> availableISOListCollection;
        private ObservableCollection<string> availableShutterTimesCollection;
        private ObservableCollection<string> availableEVBCollection;
        private ObservableCollection<string> apertureCollection;
        private ObservableCollection<string> aECollection;
        private ObservableCollection<string> driveModeCollection;
        private ObservableCollection<string> meteringModeCollection;
        private ObservableCollection<string> afModeCollection;
        #endregion

        #region Konverter fuer die Umwandlung von String zu Hexadezimal
        private ISOSpeeds isoConverter;
        private ShutterTimes shutterTimeConverter;
        private AEModes aeModeConverter;
        private ExposureCompensation ebvConverter;
        private Apertures apertureConverter;
        private PropertyCodes propertyCodesConverter;
        private DriveModes driveModeConverter;
        private MeteringModes meteringModeConverter;
        private AFModes afModeConverter;
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

        #region setter und getter methoden der klassenfelder

        #region PropertyDescs
        public EDSDK.EdsPropertyDesc PropertyDescAfMode
        {
            get { return propertyDescAfMode; }
            set { propertyDescAfMode = value;
            update("PropertyDescAfMode");
            }
        }
        public EDSDK.EdsPropertyDesc PropertyDescMeteringMode
        {
            get { return propertyDescMeteringMode; }
            set
            {
                propertyDescMeteringMode = value;
                update("PropertyDescMeteringMode");
            }
        }
        public EDSDK.EdsPropertyDesc PropertyDescDriveMode
        {
            get { return propertyDescDriveMode; }
            set
            {
                propertyDescDriveMode = value;
                update("PropertyDescDriveMode");
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
        public EDSDK.EdsPropertyDesc PropertyDescEBV
        {
            get { return propertyDescEBV; }
            set
            {
                propertyDescEBV = value;
                update("PropertyDescEBV");
            }
        }
        public EDSDK.EdsPropertyDesc ApertureDesc
        {
            get { return propertyDescAperture; }
            set
            {
                propertyDescAperture = value;
                update("ApertureDesc");
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
        public EDSDK.EdsPropertyDesc AvailableISOList
        {
            get { return PropertyDescISO; }
            set
            {
                PropertyDescISO = value;
                update("AvailableISOList");
            }
        }
        #endregion

        #region Einzelfelder
        public string AfMode
        {
            get { return afMode; }
            set
            {
                afMode = value;
                update("AfMode");
            }
        }
        public string MeteringMode
        {
            get { return meteringMode; }
            set
            {
                meteringMode = value;
                update("MeteringMode");
            }
        }
        public string DriveMode
        {
            get { return driveMode; }
            set
            {
                driveMode = value;
                update("DriveMode");
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
        public string CurrentEBV
        {
            get { return currentEBV; }
            set
            {
                currentEBV = value;
                update("CurrentEBV");
            }
        }
        public string CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                update("CurrentTime");
            }
        }
        public string CurrentDate
        {
            get { return currentDate; }
            set
            {
                currentDate = value;
                update("CurrentDate");
            }
        }
        public string CurrentAperture
        {
            get { return currentAperture; }
            set
            {
                currentAperture = value;
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
        public string Script
        {
            get { return script; }
            set
            {
                script = value;
                update("Script");
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
        #endregion

        #region CollectionViews
        public CollectionView AfModeView
        {
            get { return afModeView; }
            set
            {
                afModeView = value;
                update("AfModeView");
            }
        }

        public CollectionView MeteringModeView
        {
            get { return meteringModeView; }
            set
            {
                meteringModeView = value;
                update("MeteringModeView");
            }
        }
        public CollectionView ScriptIso
        {
            get { return scriptIso; }
            set
            {
                scriptIso = value;
                update("ScriptIso");
            }
        }
        public CollectionView ScriptAperture
        {
            get { return scriptAperture; }
            set
            {
                scriptAperture = value;
                update("ScriptAperture");
            }
        }

        public CollectionView ScriptTv
        {
            get { return scriptTv; }
            set
            {
                scriptTv = value;
                update("ScriptTv");
            }
        }
        public CollectionView ScriptEbv
        {
            get { return scriptEbv; }
            set
            {
                scriptEbv = value;
                update("ScriptEbv");
            }
        }
        public CollectionView DriveModeView
        {
            get { return driveModeView; }
            set
            {
                driveModeView = value;
                update("DriveModeView");
            }
        }
        public CollectionView AvailableEBVView
        {
            get { return availableEBVView; }
            set
            {
                availableEBVView = value;
                update("AvailableEBVView");
            }
        }
        public CollectionView ApertureView
        {
            get { return apertureView; }
            set
            {
                apertureView = value;
                update("ApertureView");
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
        public CollectionView AvailableShutterTimesView
        {
            get { return availableShutterTimesView; }
            set
            {
                availableShutterTimesView = value;
                update("AvailableShutterTimesView");
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
        #endregion

        #region ObservableCollections
        public ObservableCollection<string> AfModeCollection
        {
            get { return afModeCollection; }
            set
            {
                afModeCollection = value;
                update("AfModeCollection");
            }
        }

        public ObservableCollection<string> MeteringModeCollection
        {
            get { return meteringModeCollection; }
            set
            {
                meteringModeCollection = value;
                update("MeteringModeCollection");
            }
        }
        public ObservableCollection<string> DriveModeCollection
        {
            get { return driveModeCollection; }
            set
            {
                driveModeCollection = value;
                update("DriveModeCollection");
            }
        }
        public ObservableCollection<string> AvailableEVBCollection
        {
            get { return availableEVBCollection; }
            set
            {
                availableEVBCollection = value;
                update("AvailableEVBCollection");
            }
        }
        public ObservableCollection<string> ApertureCollection
        {
            get { return apertureCollection; }
            set
            {
                apertureCollection = value;
                update("AptureCollection");
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
        public ObservableCollection<string> AECollection
        {
            get { return aECollection; }
            set
            {
                aECollection = value;
                update("AECollection");
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
        #endregion

        #region Konverter
        public AFModes AfModeConverter
        {
            get { return afModeConverter; }
            set { afModeConverter = value; }
        }

        public MeteringModes MeteringModeConverter
        {
            get { return meteringModeConverter; }
            set { meteringModeConverter = value; }
        }
        public DriveModes DriveModeConverter
        {
            get { return driveModeConverter; }
            set { driveModeConverter = value; }
        }
        public PropertyCodes PropertyCodes
        {
            get { return propertyCodesConverter; }
            set
            {
                propertyCodesConverter = value;
                update("PropertyCodes");
            }
        }
        public ExposureCompensation EbvConverter
        {
            get { return ebvConverter; }
            set
            {
                ebvConverter = value;
                update("EbvConverter");
            }
        }
        public AEModes AeModeConverter
        {
            get { return aeModeConverter; }
            set
            {
                aeModeConverter = value;
                update("AeModeConverter");
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

        #region Commands
        public Command_DelScript_LastCommand CommandDelLastCommandScript
        {
            get { return commandDelLastCommandScript; }
            set
            {
                commandDelLastCommandScript = value;
                update("CommandDelLastCommandScript");
            }
        }

        public Command_DelScript CommandDelScript
        {
            get { return commandDelScript; }
            set
            {
                commandDelScript = value;
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
        public CommandHDR CommandHDR
        {
            get { return commandHDR; }
            set
            {
                commandHDR = value;
                update("CommandHDR");
            }
        }
        public Command_DriveLensNearOne CommandDriveLensNearOne
        {
            get { return commandDriveLensNearOne; }
            set
            {
                commandDriveLensNearOne = value;
                update("CommandDriveLensNearOne");
            }
        }

        public Command_DriveLensNearTwo CommandDriveLensNearTwo
        {
            get { return commandDriveLensNearTwo; }
            set
            {
                commandDriveLensNearTwo = value;
                update("CommandDriveLensNearTwo");
            }
        }

        public Command_DriveLensFarThree CommandDriveLensFarThree
        {
            get { return commandDriveLensFarThree; }
            set
            {
                commandDriveLensFarThree = value;
                update("CommandDriveLensFarThree");
            }
        }

        public Command_DriveLensFarTwo CommandDriveLensFarTwo
        {
            get { return commandDriveLensFarTwo; }
            set
            {
                commandDriveLensFarTwo = value;
                update("CommandDriveLensFarTwo");
            }
        }

        public Command_DriveLensFarOne CommandDriveLensFarOne
        {
            get { return commandDriveLensFarOne; }
            set
            {
                commandDriveLensFarOne = value;
                update("CommandDriveLensFarOne");
            }
        }

        public Command_DriveLensNearThree CommandDriveLensNearThree
        {
            get { return commandDriveLensNearThree; }
            set
            {
                commandDriveLensNearThree = value;
                update("CommandDriveLensNearThree");
            }
        }

        public CommandChangeISO CommandChangeIso
        {
            get { return commandChangeIso; }
            set
            {
                commandChangeIso = value;
                update("CommandChangeIso");
            }
        }

        public Command_TakePhoto CommandTakePhoto
        {
            get { return commandTakePhoto; }
            set
            {
                commandTakePhoto = value;
                update("CommandTakePhoto");
            }
        }

        #endregion

        #endregion

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public ViewModelCurrentCamera()
        {
            init();
        }

        /// <summary>
        /// Erstellt von allen Klassenfeldern eine Instanz
        /// </summary>
        private void instance()
        {
            instanceCommands();
            instanceObservableCollections();
            instanceCollectionViews();
            instanceConverter();
        }

        private void instanceObservableCollections()
        {
            // Instance ObservableCollections
            this.AvailableISOListCollection = new ObservableCollection<string>();
            this.AvailableShutterTimesCollection = new ObservableCollection<string>();
            this.AECollection = new ObservableCollection<string>();
            this.AvailableEVBCollection = new ObservableCollection<string>();
            this.ApertureCollection = new ObservableCollection<string>();
            this.DriveModeCollection = new ObservableCollection<string>();
            this.MeteringModeCollection = new ObservableCollection<string>();
            this.AfModeCollection = new ObservableCollection<string>();
        }

        private void instanceCommands()
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
        }

        private void instanceCollectionViews()
        {
            //Instance CollectionViews
            this.AvailableISOListView = new CollectionView(this.AvailableISOListCollection);
            this.AvailableShutterTimesView = new CollectionView(this.AvailableShutterTimesCollection);
            this.AEView = new CollectionView(this.AECollection);
            this.AvailableEBVView = new CollectionView(this.AvailableEVBCollection);
            this.ApertureView = new CollectionView(this.ApertureCollection);
            this.DriveModeView = new CollectionView(this.DriveModeCollection);
            this.MeteringModeView = new CollectionView(this.MeteringModeCollection);
            this.AfModeView = new CollectionView(this.AfModeCollection);
            //Initialise CollectionViews for Scriptpanel
            this.ScriptAperture = new CollectionView(this.apertureCollection);
            this.ScriptTv = new CollectionView(this.AvailableShutterTimesCollection);
            this.ScriptIso = new CollectionView(this.AvailableISOListCollection);
            this.ScriptEbv = new CollectionView(this.AvailableEVBCollection);
        }

        private void instanceConverter()
        {
            this.IsoConverter = new ISOSpeeds();
            this.ShutterTimeConverter = new ShutterTimes();
            this.AeModeConverter = new classes.AEModes();
            this.PropertyCodes = new PropertyCodes();
            this.ApertureConverter = new Apertures();
            this.EbvConverter = new ExposureCompensation();
            this.AeModeConverter = new AEModes();
            this.DriveModeConverter = new DriveModes();
            this.MeteringModeConverter = new MeteringModes();
            this.AfModeConverter = new AFModes();
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
            initFields();
            instance();
            setEvents();
        }

        private void initFields()
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
            this.DriveMode = "";
            this.MeteringMode = "";
            this.AfMode = "";
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
                this.Script.LastIndexOf(";") - 1);
                this.Script.Remove(this.Script.LastIndexOf(";"));
            }
        }

        private void addCommandToScript(string e)
        {
            if (e == "ChangeISO")
            {
                this.Script += "Ändere ISO nach : " + this.ScriptIso.CurrentItem + ";\n";
                this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr, EDSDK.PropID_ISOSpeed, sizeof(int), this.IsoConverter.getISOSpeedFromDec((string)this.ScriptIso.CurrentItem)));
            }
            if (e == "TakePhoto")
            {
                this.Script += "Foto Aufnahme;\n";
                this.ScriptCommand.ScriptCommands.Add(new ScriptCommand(this.CurrentCamera.Ptr, EDSDK.CameraCommand_TakePicture, 0, 0));
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
            updatePropertyDescIsoSpeed();
            updatePropertyDescTv();
            updatePropertyDescAeModes();
            updatePropertyDescAv();
            updatePropertyDescEBV();
            updatePropertyDescDriveMode();
            updatePropertyDescMeteringMode();
            updatePropertyDescAFMode();
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

        private void updatePropertyDescAeModes()
        {
            this.CurrentCamera.getpropertyDescAeModes();
            this.PropertyDescAE = this.CurrentCamera.AvailableAEModes;
            copyPropertyDescAEModesToCollection();
        }

        private void updatePropertyDescMeteringMode()
        {
            this.CurrentCamera.getpropertyDescMeteringModes();
            this.PropertyDescMeteringMode = this.CurrentCamera.AvailableMeteringModes;
            copyPropertyDescMeteringModeToCollection();
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

        private void updatePropertyDescDriveMode()
        {
            this.CurrentCamera.getPropertyDescDriveModes();
            this.PropertyDescDriveMode = this.CurrentCamera.AvailableDriveModes;
            copyPropertyDescDriveModeToCollection();
        }

        private void updatePropertyDescAFMode()
        {
            this.CurrentCamera.getPropertyDescAFModes();
            this.PropertyDescAfMode = this.CurrentCamera.AvailableAFModes;
            copyPropertyDescAfModeToCollection();
        }

        private void updatePropertyDescIsoSpeed()
        {
            this.CurrentCamera.getPropertyDescIsoSpeed();
            this.PropertyDescISO = this.CurrentCamera.AvailableISOSpeeds;
            copyPropertyDescISOToCollection();
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
            this.DriveMode = this.DriveModeConverter.getDriveModeString(this.CurrentCamera.CameraDriveMode);
            this.MeteringMode = this.MeteringModeConverter.getMeteringModeString(this.CurrentCamera.CameraMeteringMode);
            this.AfMode = this.AfModeConverter.getAfModeString(this.CurrentCamera.CameraAFMode);
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
            this.DriveModeView.CurrentChanged += new EventHandler(sendDriveModeToCamera);
            this.MeteringModeView.CurrentChanged += new EventHandler(sendMeteringModeToCamera);
            this.AfModeView.CurrentChanged += new EventHandler(sendAFModeToCamera);
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
                        if (this.CurrentCamera.CameraAEMode == 1)
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
                case EDSDK.PropID_DriveMode:
                    {
                        this.CurrentCamera.getDriveMode();
                        this.DriveMode = this.DriveModeConverter.getDriveModeString(this.CurrentCamera.CameraDriveMode);
                        break;
                    }
                case EDSDK.PropID_MeteringMode:
                    {
                        this.CurrentCamera.getMeteringMode();
                        this.MeteringMode = this.MeteringModeConverter.getMeteringModeString(this.CurrentCamera.CameraMeteringMode);
                        break;
                    }
                case EDSDK.PropID_AFMode:
                    {
                        this.CurrentCamera.getAfMode();
                        this.AfMode = this.AfModeConverter.getAfModeString(this.CurrentCamera.CameraAFMode);
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

        private void copyPropertyDescDriveModeToCollection()
        {
            this.DriveModeCollection.Clear();
            for (int i = 0; i < this.PropertyDescDriveMode.NumElements; i++)
            {
                this.DriveModeCollection.Add(this.DriveModeConverter.getDriveModeString((uint)this.PropertyDescDriveMode.PropDesc[i]));
            }
        }

        private void copyPropertyDescMeteringModeToCollection()
        {
            this.MeteringModeCollection.Clear();
            for (int i = 0; i < this.PropertyDescMeteringMode.NumElements; i++)
            {
                this.MeteringModeCollection.Add(this.MeteringModeConverter.getMeteringModeString((uint)this.PropertyDescMeteringMode.PropDesc[i]));
            }
        }

        private void copyPropertyDescAfModeToCollection()
        {
            this.afModeCollection.Clear();
            for (int i = 0; i < this.PropertyDescAfMode.NumElements; i++)
            {
                this.afModeCollection.Add(this.AfModeConverter.getAfModeString((uint)this.PropertyDescAfMode.PropDesc[i]));
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

        private void sendDriveModeToCamera(object sender, EventArgs e)
        {
            if (this.DriveModeView.CurrentItem != null)
            {
                this.CurrentCamera.setDriveModeToCamera((int)this.DriveModeConverter.getDriveModeHex((string)this.DriveModeView.CurrentItem));
            }
        }

        private void sendMeteringModeToCamera(object sender, EventArgs e)
        {
            if (this.MeteringModeView.CurrentItem != null)
            {
                this.CurrentCamera.setMeteringModeToCamera((int)this.MeteringModeConverter.getMeteringModeHex((string)this.MeteringModeView.CurrentItem));
            }
        }

        private void sendAFModeToCamera(object sender, EventArgs e)
        {
            if (this.AfModeView.CurrentItem != null)
            {
                this.CurrentCamera.setAFModeToCamera((int)this.AfModeConverter.getAFModeHex((string)this.AfModeView.CurrentItem));
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
