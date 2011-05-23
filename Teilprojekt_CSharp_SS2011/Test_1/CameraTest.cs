using Canon_EOS_Remote;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test_1
{
    
    
    /// <summary>
    ///Dies ist eine Testklasse für "CameraTest" und soll
    ///alle CameraTest Komponententests enthalten.
    ///</summary>
    [TestClass()]
    public class CameraTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Testkontext auf, der Informationen
        ///über und Funktionalität für den aktuellen Testlauf bietet, oder legt diesen fest.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Zusätzliche Testattribute
        // 
        //Sie können beim Verfassen Ihrer Tests die folgenden zusätzlichen Attribute verwenden:
        //
        //Mit ClassInitialize führen Sie Code aus, bevor Sie den ersten Test in der Klasse ausführen.
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Mit ClassCleanup führen Sie Code aus, nachdem alle Tests in einer Klasse ausgeführt wurden.
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen.
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Ein Test für "Camera-Konstruktor"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraConstructorTest()
        {
            IntPtr cameraPtr = new IntPtr(); // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(cameraPtr);
            Assert.Inconclusive("TODO: Code zum Überprüfen des Ziels implementieren");
        }

        /// <summary>
        ///Ein Test für "getCameraBatteryLevelFromBody"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void getCameraBatteryLevelFromBodyTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            target.getCameraBatteryLevelFromBody();
            Assert.Inconclusive("Eine Methode, die keinen Wert zurückgibt, kann nicht überprüft werden.");
        }

        /// <summary>
        ///Ein Test für "getCameraBodyIDFromBody"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void getCameraBodyIDFromBodyTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            target.getCameraBodyIDFromBody();
            Assert.Inconclusive("Eine Methode, die keinen Wert zurückgibt, kann nicht überprüft werden.");
        }

        /// <summary>
        ///Ein Test für "getCameraFirmwareFromBody"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void getCameraFirmwareFromBodyTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            target.getCameraFirmwareFromBody();
            Assert.Inconclusive("Eine Methode, die keinen Wert zurückgibt, kann nicht überprüft werden.");
        }

        /// <summary>
        ///Ein Test für "getCameraNameFromBody"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void getCameraNameFromBodyTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            target.getCameraNameFromBody();
            Assert.Inconclusive("Eine Methode, die keinen Wert zurückgibt, kann nicht überprüft werden.");
        }

        /// <summary>
        ///Ein Test für "getCameraOwnerFromBody"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void getCameraOwnerFromBodyTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            target.getCameraOwnerFromBody();
            Assert.Inconclusive("Eine Methode, die keinen Wert zurückgibt, kann nicht überprüft werden.");
        }

        /// <summary>
        ///Ein Test für "getCameraTime"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void getCameraTimeTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            target.getCameraTime();
            Assert.Inconclusive("Eine Methode, die keinen Wert zurückgibt, kann nicht überprüft werden.");
        }

        /// <summary>
        ///Ein Test für "getErrorString"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void getErrorStringTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            uint errorCodeNumber = 0; // TODO: Passenden Wert initialisieren
            string expected = string.Empty; // TODO: Passenden Wert initialisieren
            string actual;
            actual = target.getErrorString(errorCodeNumber);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "update"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void updateTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            target.update();
            Assert.Inconclusive("Eine Methode, die keinen Wert zurückgibt, kann nicht überprüft werden.");
        }

        /// <summary>
        ///Ein Test für "CameraAEMode"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraAEModeTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            AEMode_Accessor expected = null; // TODO: Passenden Wert initialisieren
            AEMode_Accessor actual;
            target.CameraAEMode = expected;
            actual = target.CameraAEMode;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraAFMode"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraAFModeTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            uint expected = 0; // TODO: Passenden Wert initialisieren
            uint actual;
            target.CameraAFMode = expected;
            actual = target.CameraAFMode;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraAperture"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraApertureTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            uint expected = 0; // TODO: Passenden Wert initialisieren
            uint actual;
            target.CameraAperture = expected;
            actual = target.CameraAperture;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraAvailableShots"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraAvailableShotsTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            uint expected = 0; // TODO: Passenden Wert initialisieren
            uint actual;
            target.CameraAvailableShots = expected;
            actual = target.CameraAvailableShots;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraBatteryLevel"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraBatteryLevelTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            uint expected = 0; // TODO: Passenden Wert initialisieren
            uint actual;
            target.CameraBatteryLevel = expected;
            actual = target.CameraBatteryLevel;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraBodyID"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraBodyIDTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            string expected = string.Empty; // TODO: Passenden Wert initialisieren
            string actual;
            target.CameraBodyID = expected;
            actual = target.CameraBodyID;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraDriveMode"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraDriveModeTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            uint expected = 0; // TODO: Passenden Wert initialisieren
            uint actual;
            target.CameraDriveMode = expected;
            actual = target.CameraDriveMode;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraExposureCompensation"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraExposureCompensationTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            uint expected = 0; // TODO: Passenden Wert initialisieren
            uint actual;
            target.CameraExposureCompensation = expected;
            actual = target.CameraExposureCompensation;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraFirmware"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraFirmwareTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            string expected = string.Empty; // TODO: Passenden Wert initialisieren
            string actual;
            target.CameraFirmware = expected;
            actual = target.CameraFirmware;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraISOSpeed"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraISOSpeedTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            ISOSpeed_Accessor expected = null; // TODO: Passenden Wert initialisieren
            ISOSpeed_Accessor actual;
            target.CameraISOSpeed = expected;
            actual = target.CameraISOSpeed;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraMeteringMode"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraMeteringModeTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            uint expected = 0; // TODO: Passenden Wert initialisieren
            uint actual;
            target.CameraMeteringMode = expected;
            actual = target.CameraMeteringMode;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraName"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraNameTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            string expected = string.Empty; // TODO: Passenden Wert initialisieren
            string actual;
            target.CameraName = expected;
            actual = target.CameraName;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraOwner"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraOwnerTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            string expected = string.Empty; // TODO: Passenden Wert initialisieren
            string actual;
            target.CameraOwner = expected;
            actual = target.CameraOwner;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraPtr"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraPtrTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            IntPtr expected = new IntPtr(); // TODO: Passenden Wert initialisieren
            IntPtr actual;
            target.CameraPtr = expected;
            actual = target.CameraPtr;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraShutterTime"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraShutterTimeTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            uint expected = 0; // TODO: Passenden Wert initialisieren
            uint actual;
            target.CameraShutterTime = expected;
            actual = target.CameraShutterTime;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CameraTime"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CameraTimeTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            EdsTime_Accessor expected = null; // TODO: Passenden Wert initialisieren
            EdsTime_Accessor actual;
            target.CameraTime = expected;
            actual = target.CameraTime;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "CurrentStorage"
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Canon EOS Remote.exe")]
        public void CurrentStorageTest()
        {
            PrivateObject param0 = null; // TODO: Passenden Wert initialisieren
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Passenden Wert initialisieren
            string expected = string.Empty; // TODO: Passenden Wert initialisieren
            string actual;
            target.CurrentStorage = expected;
            actual = target.CurrentStorage;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }
    }
}
