using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canon_EOS_Remote.classes;

namespace Canon_EOS_Remote
{
    class DriveModes
    {
        private List<DriveMode> _driveModes;

        private void init()
        {
            this._driveModes.Add(new DriveMode("Single_Frame_Shooting", 0x0));
            this._driveModes.Add(new DriveMode("Continuous_Shooting",0x1));
            this._driveModes.Add(new DriveMode("Video",0x2));
            this._driveModes.Add(new DriveMode("High_Speed_Continuous_Shooting",0x4));
            this._driveModes.Add(new DriveMode("Low_Speed_Continuous_Shooting",0x5));
            this._driveModes.Add(new DriveMode("Silent_Single_Shooting",0x6));
            this._driveModes.Add(new DriveMode("TenSec_Self_Timer_Plus_Continuous_Shooting",0x7));
            this._driveModes.Add(new DriveMode("TenSec_Self_Timer",0x10));
            this._driveModes.Add(new DriveMode("TwoSec_Self_Timer",0x11));
        }

        //public uint getDriveModeHex(){
        //}
        //public string getDriveModeString(){}
    }
}
