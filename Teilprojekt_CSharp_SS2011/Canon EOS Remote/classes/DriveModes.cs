using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canon_EOS_Remote.classes;

namespace Canon_EOS_Remote
{
    class DriveModes
    {
        private List<TDriveMode> _driveModes;

        public DriveModes()
        {
            this._driveModes = new List<TDriveMode>();
            init();
        }

        private void init()
        {
            this._driveModes.Add(new TDriveMode("Einzelaufnahme", 0x0));
            this._driveModes.Add(new TDriveMode("Reihenaufnahme",0x1));
            this._driveModes.Add(new TDriveMode("Video",0x2));
            this._driveModes.Add(new TDriveMode("High_Speed_Continuous_Shooting",0x4));
            this._driveModes.Add(new TDriveMode("Low_Speed_Continuous_Shooting",0x5));
            this._driveModes.Add(new TDriveMode("Silent_Single_Shooting",0x6));
            this._driveModes.Add(new TDriveMode("TenSec_Self_Timer_Plus_Continuous_Shooting",0x7));
            this._driveModes.Add(new TDriveMode("TenSec_Self_Timer",0x10));
            this._driveModes.Add(new TDriveMode("TwoSec_Self_Timer",0x11));
        }

        //public uint getDriveModeHex(){
        //}
        public string getDriveModeString(UInt32 tmpDriveModeHex){
            for (int i = 0; i < this._driveModes.Count; i++)
            {
                if (this._driveModes.ElementAt(i).DriveModeHex == tmpDriveModeHex)
                {
                    return this._driveModes.ElementAt(i).DriveModeName;
                }
            }
            return "unknown";
        }
    }
}
