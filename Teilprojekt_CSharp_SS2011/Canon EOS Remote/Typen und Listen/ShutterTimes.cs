using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class ShutterTimes
    {
        private List<TShutterTime> shutterTimes;

        public ShutterTimes()
        {
            this.shutterTimes=new List<TShutterTime>();
            init();
        }

        private void init()
        {
            this.shutterTimes.Add(new TShutterTime("Not available", 0x00));
            this.shutterTimes.Add(new TShutterTime("Bulb", 0x0C));
            this.shutterTimes.Add(new TShutterTime("30s", 0x10));
            this.shutterTimes.Add(new TShutterTime("25s", 0x13));
            this.shutterTimes.Add(new TShutterTime("20s", 0x14));
            this.shutterTimes.Add(new TShutterTime("20s", 0x15));
            this.shutterTimes.Add(new TShutterTime("15s", 0x18));
            this.shutterTimes.Add(new TShutterTime("13s", 0x1B));
            this.shutterTimes.Add(new TShutterTime("10s", 0x1C));
            this.shutterTimes.Add(new TShutterTime("10s", 0x1D));
            this.shutterTimes.Add(new TShutterTime("8s", 0x20));
            this.shutterTimes.Add(new TShutterTime("6s", 0x23));
            this.shutterTimes.Add(new TShutterTime("6s", 0x24));
            this.shutterTimes.Add(new TShutterTime("5s", 0x25));
            this.shutterTimes.Add(new TShutterTime("4s", 0x28));
            this.shutterTimes.Add(new TShutterTime("3.2s", 0x2B));
            this.shutterTimes.Add(new TShutterTime("3s", 0x2C));
            this.shutterTimes.Add(new TShutterTime("2.5s", 0x2D));
            this.shutterTimes.Add(new TShutterTime("2s", 0x30));
            this.shutterTimes.Add(new TShutterTime("1.6s", 0x33));
            this.shutterTimes.Add(new TShutterTime("1.5s", 0x34));
            this.shutterTimes.Add(new TShutterTime("1.3s", 0x35));
            this.shutterTimes.Add(new TShutterTime("1s", 0x38));
            this.shutterTimes.Add(new TShutterTime("0.8s", 0x3B));
            this.shutterTimes.Add(new TShutterTime("0.7s", 0x3C));
            this.shutterTimes.Add(new TShutterTime("0.6s", 0x3D));
            this.shutterTimes.Add(new TShutterTime("0.5s", 0x40));
            this.shutterTimes.Add(new TShutterTime("0.4s", 0x43));
            this.shutterTimes.Add(new TShutterTime("0.3s", 0x44));
            this.shutterTimes.Add(new TShutterTime("0.3s", 0x45));
            this.shutterTimes.Add(new TShutterTime("1/4", 0x48));
            this.shutterTimes.Add(new TShutterTime("1/5", 0x4B));
            this.shutterTimes.Add(new TShutterTime("1/6", 0x4C));
            this.shutterTimes.Add(new TShutterTime("1/6", 0x4D));
            this.shutterTimes.Add(new TShutterTime("1/8", 0x50));
            this.shutterTimes.Add(new TShutterTime("1/10", 0x53));
            this.shutterTimes.Add(new TShutterTime("1/10", 0x54));
            this.shutterTimes.Add(new TShutterTime("1/13", 0x55));
            this.shutterTimes.Add(new TShutterTime("1/15", 0x58));
            this.shutterTimes.Add(new TShutterTime("1/20", 0x5B));
            this.shutterTimes.Add(new TShutterTime("1/20", 0x5C));
            this.shutterTimes.Add(new TShutterTime("1/25", 0x5D));
            this.shutterTimes.Add(new TShutterTime("1/30", 0x60));
            this.shutterTimes.Add(new TShutterTime("1/40", 0x63));
            this.shutterTimes.Add(new TShutterTime("1/45", 0x64));
            this.shutterTimes.Add(new TShutterTime("1/50", 0x65));
            this.shutterTimes.Add(new TShutterTime("1/60", 0x68));
            this.shutterTimes.Add(new TShutterTime("1/80", 0x6B));
            this.shutterTimes.Add(new TShutterTime("1/90", 0x6C));
            this.shutterTimes.Add(new TShutterTime("1/100", 0x6D));
            this.shutterTimes.Add(new TShutterTime("1/125", 0x70));
            this.shutterTimes.Add(new TShutterTime("1/160", 0x73));
            this.shutterTimes.Add(new TShutterTime("1/180", 0x74));
            this.shutterTimes.Add(new TShutterTime("1/200", 0x75));
            this.shutterTimes.Add(new TShutterTime("1/250", 0x78));
            this.shutterTimes.Add(new TShutterTime("1/320", 0x7B));
            this.shutterTimes.Add(new TShutterTime("1/350", 0x7C));
            this.shutterTimes.Add(new TShutterTime("1/400", 0x7D));
            this.shutterTimes.Add(new TShutterTime("1/500", 0x80));
            this.shutterTimes.Add(new TShutterTime("1/640", 0x83));
            this.shutterTimes.Add(new TShutterTime("1/750", 0x84));
            this.shutterTimes.Add(new TShutterTime("1/800", 0x85));
            this.shutterTimes.Add(new TShutterTime("1/1000", 0x88));
            this.shutterTimes.Add(new TShutterTime("1/1250", 0x8B));
            this.shutterTimes.Add(new TShutterTime("1/1500", 0x8C));
            this.shutterTimes.Add(new TShutterTime("1/1600", 0x8D));
            this.shutterTimes.Add(new TShutterTime("1/2000", 0x90));
            this.shutterTimes.Add(new TShutterTime("1/2500", 0x93));
            this.shutterTimes.Add(new TShutterTime("1/3000", 0x94));
            this.shutterTimes.Add(new TShutterTime("1/3200", 0x95));
            this.shutterTimes.Add(new TShutterTime("1/4000", 0x98));
            this.shutterTimes.Add(new TShutterTime("1/5000", 0x9B));
            this.shutterTimes.Add(new TShutterTime("1/6000", 0x9C));
            this.shutterTimes.Add(new TShutterTime("1/6400", 0x9D));
            this.shutterTimes.Add(new TShutterTime("1/8000", 0xA0));
            this.shutterTimes.Add(new TShutterTime("NotValidNoSettingsChanged", 0xFFFFFFFF));
        }

        public string getShutterTimeStringFromHex(uint hexvalue){
            for (int i = 0; i < this.shutterTimes.Count; i++)
            {
                if (this.shutterTimes.ElementAt(i).ShutterTimeHex == hexvalue)
                {
                    return this.shutterTimes.ElementAt(i).ShutterTimeDec;
                }
            }
            return "unknown" + hexvalue;
        }

        public uint getShutterTimeStringFromDec(string decvalue)
        {
            for (int i = 0; i < this.shutterTimes.Count; i++)
            {
                if (this.shutterTimes.ElementAt(i).ShutterTimeDec == decvalue)
                {
                    return this.shutterTimes.ElementAt(i).ShutterTimeHex;
                }
            }
            return 0x0;
        }
    }
}
