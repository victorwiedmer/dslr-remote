using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDSDKLib;

namespace Canon_EOS_Remote.classes
{
    class MemoryCard
    {
        private IntPtr _ptr;

        public IntPtr Ptr
        {
            get { return _ptr; }
            set { _ptr = value; }
        }
        private EDSDK.EdsVolumeInfo _memoryCardInfo;

        public EDSDK.EdsVolumeInfo MemoryCardInfo
        {
            get { return _memoryCardInfo; }
            set { _memoryCardInfo = value; }
        }

        public MemoryCard(IntPtr ptr)
        {
            this.Ptr = ptr;
            EDSDK.EdsGetVolumeInfo(this.Ptr, out _memoryCardInfo);
        }

        public string toString()
        {
            return 
                    "Storage Type : " + this.MemoryCardInfo.StorageType + "\n" +
                    "Access : " + this.MemoryCardInfo.Access + "\n" +
                    "Max Capacity : " + this.MemoryCardInfo.MaxCapacity + "\n" +
                    "Free Space in Bytes : " + this.MemoryCardInfo.FreeSpaceInBytes + "\n" +
                    "Volume Label : " + this.MemoryCardInfo.szVolumeLabel + "\n";
        }
    }
}
