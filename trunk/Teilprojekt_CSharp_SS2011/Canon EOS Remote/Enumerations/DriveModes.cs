using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.Enumerations
{
    class DriveModes
    {
        public enum _driveModes {
        Single_Frame_Shooting=0x0,
        Continuous_Shooting=0x1,
        Video=0x2,
        High_Speed_Continuous_Shooting=0x4,
        Low_Speed_Continuous_Shooting=0x5,
        Silent_Single_Shooting=0x6,
        TenSec_Self_Timer_Plus_Continuous_Shooting=0x7,
        TenSec_Self_Timer=0x10,
        TwoSec_Self_Timer=0x11
        };
    }
}
