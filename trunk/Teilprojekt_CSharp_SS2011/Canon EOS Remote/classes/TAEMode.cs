
namespace Canon_EOS_Remote.classes
{
    class TAEMode
    {
        private string aeModeString;

        public string AeModeString
        {
            get { return aeModeString; }
            set { aeModeString = value; }
        }
        private uint aeModeHex;

        public uint AeModeHex
        {
            get { return aeModeHex; }
            set { aeModeHex = value; }
        }

        public TAEMode(string AEString, uint AEHex)
        {
            this.AeModeString = AEString;
            this.AeModeHex = AEHex;
        }
    }
}
