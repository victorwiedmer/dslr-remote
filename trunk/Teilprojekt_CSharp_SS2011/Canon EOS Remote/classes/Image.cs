using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDSDKLib;

namespace Canon_EOS_Remote.classes
{
    class Image
    {
        private IntPtr _ptr;

        public IntPtr Ptr
        {
            get { return _ptr; }
            set { _ptr = value; }
        }
        private EDSDK.EdsDirectoryItemInfo _imageItemInfo;

        public EDSDK.EdsDirectoryItemInfo ImageItemInfo
        {
            get { return _imageItemInfo; }
            set { _imageItemInfo = value; }
        }

        public Image(IntPtr ptr)
        {
            this.Ptr = ptr;
            EDSDK.EdsGetDirectoryItemInfo(this.Ptr, out _imageItemInfo);
        }

        public string ToString()
        {
            return
                   "Filename : " + this._imageItemInfo.szFileName + "\n";
        }
    }
}
