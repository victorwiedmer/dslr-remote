using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canon_EOS_Remote.Enumerations;

namespace Canon_EOS_Remote
{
    /**
     * Added 05-05-2011 12:37 
     * represents the ISOSpeed
     * this class hold a list with the available iso speeds from camera and 
     * a member with the currently setted iso speed, the iso speed can only be setted
     * with values from the list with the available iso speed
     */
    class ISOSpeed
    {
        private List<UInt32> _availableISOSpeeds;
        private UInt32 _currentlySettedISOSpeed;
        private ISOSpeeds _convertISOSpeeds;

        /**
         * Setter and Getter of the class member _availableISOSpeed
         */
        public List<UInt32> AvailableISOSpeed
        {
            get { return _availableISOSpeeds; }
            set
            {
                try
                {
                    _availableISOSpeeds = value;
                }
                catch (FormatException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        /**
         * Setter and Getter of the class member _currentlySettedAEMode
         */
        public UInt32 CurrentlySettedISOSpeed
        {
            get { return _currentlySettedISOSpeed; }
            set
            {
                try
                {
                    if (checkISOSpeedInList(value)) { _currentlySettedISOSpeed = value; }
                    else { throw new Exception("This ISO Speed are not supported"); }
                }
                catch (FormatException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        /**
         * Check the inputed value of iso speed, if the value are included in the list
         * with the available iso speed values
         * if there isn't in the list false are returned , else true
         */
        private bool checkISOSpeedInList(UInt32 _expectedISOSpeed)
        {
            bool includedInList = false;
            return includedInList;
        }
    }
}
