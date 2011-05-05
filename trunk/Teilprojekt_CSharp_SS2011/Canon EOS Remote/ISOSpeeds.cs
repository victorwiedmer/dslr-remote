using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.Enumerations
{
    class ISOSpeeds
    {
        private List<ISOValue> _isoSpeeds;

        public ISOSpeeds()
        {
            _isoSpeeds = new List<ISOValue>();
        }

        private void addValuesToList()
        {
            _isoSpeeds.Add(new ISOValue(6,0x28));
            _isoSpeeds.Add(new ISOValue(12,0x30));
            _isoSpeeds.Add(new ISOValue(25,0x38));
            _isoSpeeds.Add(new ISOValue(50,0x40));
            _isoSpeeds.Add(new ISOValue(100,0x48));
            _isoSpeeds.Add(new ISOValue(125,0x4b));
            _isoSpeeds.Add(new ISOValue(160,0x4d));
            ISO_200 = 0x50,
            ISO_250 = 0x53,
            ISO_320 = 0x55,
            ISO_400 = 0x58,
            ISO_500 = 0x5b,
            ISO_640 = 0x5d,
            ISO_800 = 0x60,
            ISO_1000 = 0x63,
            ISO_1250 = 0x65,
            ISO_1600 = 0x68,
            ISO_3200 = 0x70,
            ISO_6400 = 0x78,
            ISO_12800 = 0x80,
            ISO_25600 = 0x88,
            ISO_51200 = 0x90,
            ISO_102400 = 0x98,
            ISO_INVALID = 0xff
        }

        public UInt32 getISOSpeedFromHex(UInt32 isoHexvalue){
            UInt32 returnValue=0;

            return returnValue;
        }
    }
}
