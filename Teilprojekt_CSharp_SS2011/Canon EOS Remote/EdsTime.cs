using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote
{
    class EdsTime
    {
        /**
         * Added 05-05-2011 11:53
         * represents the EdsTime for property DateTime
         * SDK property ID is kEdsPropID_DateTime
         * */
        private UInt32 year; // year
        private UInt32 month; // month 1=January, 2=February, ...
        private UInt32 day; // day
        private UInt32 hour; // hour
        private UInt32 minute; // minute
        private UInt32 second; // second
        private UInt32 milliseconds; // reserved
    }
}
