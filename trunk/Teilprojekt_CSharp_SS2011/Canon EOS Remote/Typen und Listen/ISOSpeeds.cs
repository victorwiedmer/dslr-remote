using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canon_EOS_Remote.Types;

namespace Canon_EOS_Remote
{
    /**
     * Version 0.1
     * */
    class ISOSpeeds
    {
        private List<TISOValue> _isoSpeeds;

        public ISOSpeeds()
        {
            _isoSpeeds = new List<TISOValue>();
            addValuesToList();
        }

        private void addValuesToList()
        {
            _isoSpeeds.Add(new TISOValue("6",0x28));
            _isoSpeeds.Add(new TISOValue("12",0x30));
            _isoSpeeds.Add(new TISOValue("25",0x38));
            _isoSpeeds.Add(new TISOValue("50",0x40));
            _isoSpeeds.Add(new TISOValue("100",0x48));
            _isoSpeeds.Add(new TISOValue("125",0x4b));
            _isoSpeeds.Add(new TISOValue("160",0x4d));
            _isoSpeeds.Add(new TISOValue("200",0x50));
            _isoSpeeds.Add(new TISOValue("250",0x53));
            _isoSpeeds.Add(new TISOValue("320",0x55));
            _isoSpeeds.Add(new TISOValue("400",0x58));
            _isoSpeeds.Add(new TISOValue("500",0x5b));
            _isoSpeeds.Add(new TISOValue("640",0x5d));
            _isoSpeeds.Add(new TISOValue("800",0x60));
            _isoSpeeds.Add(new TISOValue("1000",0x63));
            _isoSpeeds.Add(new TISOValue("1250",0x65));
            _isoSpeeds.Add(new TISOValue("1600",0x68));
            _isoSpeeds.Add(new TISOValue("2000",0x6B));
            _isoSpeeds.Add(new TISOValue("2500",0x6D));
            _isoSpeeds.Add(new TISOValue("3200",0x70));
            _isoSpeeds.Add(new TISOValue("4000", 0x73));
            _isoSpeeds.Add(new TISOValue("5000",0x75));
            _isoSpeeds.Add(new TISOValue("6400",0x78));
            _isoSpeeds.Add(new TISOValue("12800",0x80));
            _isoSpeeds.Add(new TISOValue("25600",0x88));
            _isoSpeeds.Add(new TISOValue("51200",0x90));
            _isoSpeeds.Add(new TISOValue("102400",0x98));
            _isoSpeeds.Add(new TISOValue("0",0xff));
        }

        public string getISOSpeedFromHex(uint isoHexvalue)
        {
            if (isoHexvalue == 0)
            {
                return "Auto";
            }
            for (int i = 0; i < _isoSpeeds.Count; i++)
            {
                if (_isoSpeeds.ElementAt(i).HexValue == isoHexvalue)
                {
                    return Convert.ToString(_isoSpeeds.ElementAt(i).DecValue);
                }
            }
            return "unknown : " + isoHexvalue;
        }

        public UInt32 getISOSpeedFromDec(string isoDecvalue)
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
