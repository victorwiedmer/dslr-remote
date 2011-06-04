using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote
{
    /**
     * Version 0.1
     * */
    class ISOSpeeds
    {
        private List<ISOValue> _isoSpeeds;

        public ISOSpeeds()
        {
            _isoSpeeds = new List<ISOValue>();
            addValuesToList();
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
            _isoSpeeds.Add(new ISOValue(200,0x50));
            _isoSpeeds.Add(new ISOValue(250,0x53));
            _isoSpeeds.Add(new ISOValue(320,0x55));
            _isoSpeeds.Add(new ISOValue(400,0x58));
            _isoSpeeds.Add(new ISOValue(500,0x5b));
            _isoSpeeds.Add(new ISOValue(640,0x5d));
            _isoSpeeds.Add(new ISOValue(800,0x60));
            _isoSpeeds.Add(new ISOValue(1000,0x63));
            _isoSpeeds.Add(new ISOValue(1250,0x65));
            _isoSpeeds.Add(new ISOValue(1600,0x68));
            _isoSpeeds.Add(new ISOValue(3200,0x70));
            _isoSpeeds.Add(new ISOValue(6400,0x78));
            _isoSpeeds.Add(new ISOValue(12800,0x80));
            _isoSpeeds.Add(new ISOValue(25600,0x88));
            _isoSpeeds.Add(new ISOValue(51200,0x90));
            _isoSpeeds.Add(new ISOValue(102400,0x98));
            _isoSpeeds.Add(new ISOValue(0,0xff));
        }

        public UInt32 getISOSpeedFromHex(UInt32 isoHexvalue){
            for (int i = 0; i < _isoSpeeds.Count; i++)
            {
                if (_isoSpeeds.ElementAt(i).HexValue == isoHexvalue)
                {
                    return _isoSpeeds.ElementAt(i).DecValue;
                }
            }
            return 0;
        }

        public UInt32 getISOSpeedFromDec(UInt32 isoDecvalue)
        {
            for (int i = 0; i < _isoSpeeds.Count; i++)
            {
                if (_isoSpeeds.ElementAt(i).DecValue == isoDecvalue)
                {
                    return _isoSpeeds.ElementAt(i).HexValue;
                }
            }
            return 0x0;
        }
    }
}
