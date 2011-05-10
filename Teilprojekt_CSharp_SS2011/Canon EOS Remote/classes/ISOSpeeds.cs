﻿using System;
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
            _isoSpeeds.Add(new ISOValue(200,0x4d));
            _isoSpeeds.Add(new ISOValue(250,0x4d));
            _isoSpeeds.Add(new ISOValue(320,0x4d));
            _isoSpeeds.Add(new ISOValue(400,0x4d));
            _isoSpeeds.Add(new ISOValue(500,0x4d));
            _isoSpeeds.Add(new ISOValue(640,0x4d));
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
            UInt32 returnValue=0;
            int listLength=_isoSpeeds.Count;
            for (int i = 0; i <= listLength; i++)
            {
                if (_isoSpeeds.ElementAt(i).HexValue == isoHexvalue)
                {
                    returnValue = _isoSpeeds.ElementAt(i).DecValue;
                }
            }
            return returnValue;
        }

        public UInt32 getISOSpeedFromDec(UInt32 isoDecvalue)
        {
            UInt32 returnValue = 0;
            int listLength = _isoSpeeds.Count;
            for (int i = 0; i <= listLength; i++)
            {
                if (_isoSpeeds.ElementAt(i).DecValue == isoDecvalue)
                {
                    returnValue = _isoSpeeds.ElementAt(i).HexValue;
                }
            }
            return returnValue;
        }
    }
}