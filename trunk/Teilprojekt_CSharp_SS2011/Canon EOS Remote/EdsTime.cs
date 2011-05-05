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

        public UInt32 Year
        {
            get { return year; }
            set
            {
                try
                {
                    year = value;
                }
                catch (FormatException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public UInt32 Month
        {
            get { return month; }
            set
            {
                try
                {
                    month = value;
                }
                catch (FormatException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public UInt32 Day
        {
            get { return day; }
            set
            {
                try
                {
                    day = value;
                }
                catch (FormatException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public UInt32 Hour
        {
            get { return hour; }
            set
            {
                try
                {
                    hour = value;
                }
                catch(FormatException e){
                    throw new Exception(e.Message);
                }
            }
        }
        public UInt32 Minute
        {
            get { return minute; }
            set
            {
                try
                {
                    minute = value;
                }
                catch (FormatException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public UInt32 Second
        {
            get { return second; }
            set
            {
                try
                {
                    second = value;
                }
                catch (FormatException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public UInt32 Milliseconds
        {
            get { return milliseconds; }
            set
            {
                try
                {
                    milliseconds = value;
                }
                catch (FormatException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
