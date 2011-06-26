using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDSDKLib;

namespace Canon_EOS_Remote.classes
{
    class Folder
    {
        private IntPtr _ptr;

        public IntPtr Ptr
        {
            get { return _ptr; }
            set { _ptr = value; }
        }
        private EDSDK.EdsDirectoryItemInfo _folderInfo;

        public EDSDK.EdsDirectoryItemInfo FolderInfo
        {
            get { return _folderInfo; }
            set { _folderInfo = value; }
        }

        public Folder(IntPtr ptr)
        {
            this.Ptr = ptr;
            EDSDK.EdsGetDirectoryItemInfo(this.Ptr, out _folderInfo);
        }

        public string ToString()
        {
            return
                    "Foldername : " + this._folderInfo.szFileName + "\n";
        }
    }
}
