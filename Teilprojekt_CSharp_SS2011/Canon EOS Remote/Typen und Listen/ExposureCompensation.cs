using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canon_EOS_Remote.classes;

namespace Canon_EOS_Remote
{
    class ExposureCompensation
    {
        private List<classes.TExposureCompensation> ebvList;

        internal List<classes.TExposureCompensation> EbvList
        {
            get { return ebvList; }
            set { ebvList = value; }
        }

        public ExposureCompensation()
        {
            this.EbvList = new List<classes.TExposureCompensation>();
            init();
        }

        private void init()
        {
            this.EbvList.Add(new TExposureCompensation("-5",0xd8));
            this.EbvList.Add(new TExposureCompensation("-4,5",0xdc));
            this.EbvList.Add(new TExposureCompensation("-4",0xe0));
            this.EbvList.Add(new TExposureCompensation("-3,5",0xe4));
            this.EbvList.Add(new TExposureCompensation("-3",0xe8));
            this.EbvList.Add(new TExposureCompensation("-2,5",0xec));
            this.EbvList.Add(new TExposureCompensation("-2",0xf0));
            this.EbvList.Add(new TExposureCompensation("-1,5",0xf4));
            this.EbvList.Add(new TExposureCompensation("-1",0xf8));
            this.EbvList.Add(new TExposureCompensation("-0,5",0xfc));
            this.EbvList.Add(new TExposureCompensation("0",0x0));
            this.EbvList.Add(new TExposureCompensation("0,5",0x04));
            this.EbvList.Add(new TExposureCompensation("1",0x08));
            this.EbvList.Add(new TExposureCompensation("1,5",0x0c));
            this.EbvList.Add(new TExposureCompensation("2",0x10));
            this.EbvList.Add(new TExposureCompensation("2,5",0x14));
            this.EbvList.Add(new TExposureCompensation("3",0x18));
            this.EbvList.Add(new TExposureCompensation("3,5",0x1c));
            this.EbvList.Add(new TExposureCompensation("4",0x20));
            this.EbvList.Add(new TExposureCompensation("4,5",0x24));
            this.EbvList.Add(new TExposureCompensation("5",0x28));
        }

        public string getEbvString(uint ebvHex)
        {
            for (int i = 0; i < this.EbvList.Count; i++)
            {
                if (this.EbvList.ElementAt(i).EBVHex == ebvHex)
                {
                    return this.EbvList.ElementAt(i).EBVString;
                }
            }

            return "unknown : " + ebvHex;
        }

        public uint getebvHex(string ebvstring)
        {
            for (int i = 0; i < this.EbvList.Count; i++)
            {
                if (this.EbvList.ElementAt(i).EBVString == ebvstring)
                {
                    return this.EbvList.ElementAt(i).EBVHex;
                }
            }

            return 0;
        }

    }
}
