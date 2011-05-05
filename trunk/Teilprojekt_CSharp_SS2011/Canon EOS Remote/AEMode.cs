using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote
{
    /**
     * Added 05-05-2011 12:02 
     * represents the AEMode
     * this class hold a list with the available ae modes from camera and 
     * a member with the currently setted ae mode, the aemode can only be setted
     * with values from the list with the available ae modes
     */
    class AEMode
    {
        private List<UInt32> _availableAEModes;

        /**
         * Setter and Getter of the class member _availableAEModes
         */
        public List<UInt32> AvailableAEModes
        {
            get { return _availableAEModes; }
            set { _availableAEModes = value; }
        }
        private UInt32 _currentlySettedAEMode;

        /**
         * Setter and Getter of the class member _currentlySettedAEMode
         */
        public UInt32 CurrentlySettedAEMode
        {
            get { return _currentlySettedISOSpeed; }
            set
            {
                if (checkAEModeInList(value)) { _currentlySettedISOSpeed = value; }
                else { throw new Exception("This AEMode are not supported"); }
            }
        }

        /**
         * Check the inputed value of AE Mode, if the value are included in the list
         * with the available ae mode values
         * if there isn't in the list false are returned , else true
         */
        private bool checkAEModeInList(UInt32 _expectedAEMode)
        {
            bool includedInList = false;
            return includedInList;
        }
    }
}
