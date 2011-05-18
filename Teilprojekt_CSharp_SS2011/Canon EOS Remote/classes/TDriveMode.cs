using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class TDriveMode
    {
        private string _driveModeName;
        private uint _driveModeHex;

        public uint DriveModeHex
        {
            get { return _driveModeHex; }
            set { _driveModeHex = value; }
        }

        public string DriveModeName
        {
            get { return _driveModeName; }
            set { _driveModeName = value; }
        }

        public TDriveMode(string driveModeName, uint driveModeHex)
        {
            this.DriveModeName = driveModeName;
            this.DriveModeHex = driveModeHex;
        }

    }
}
