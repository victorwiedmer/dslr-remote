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
        private UInt32 _currentlySettedAEMode;
    }
}
