using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDSDKLib;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Canon_EOS_Remote
{
    class LiveView
    {
        private IntPtr cameraPtr;

        public IntPtr CameraPtr
        {
            get { return cameraPtr; }
            set { cameraPtr = value; }
        }

        private IntPtr memStreamPtr;

        public IntPtr MemStreamPtr
        {
            get { return memStreamPtr; }
            set { memStreamPtr = value; }
        }
        private IntPtr evfImgPtr;

        public IntPtr EvfImgPtr
        {
            get { return evfImgPtr; }
            set { evfImgPtr = value; }
        }
        private bool lvActive;

        public bool LvActive
        {
            get { return lvActive; }
            set { lvActive = value; }
        }

        private int frames;

        public int Frames
        {
            get { return frames; }
            set { frames = value; }
        }

        public LiveView(IntPtr cameraPtr)
        {
            this.CameraPtr = cameraPtr;
        }

        private BitmapImage bmp;

        public BitmapImage Bmp
        {
            get { return bmp; }
            set { bmp = value; }
        }

        public bool startLV()
        {
            uint err = 0;
            uint device = 0;
            EvfImgPtr = new IntPtr(0);
            MemStreamPtr = new IntPtr(0);
            Bmp = new BitmapImage();
            err=EDSDK.EdsGetPropertyData(this.CameraPtr,EDSDK.PropID_Evf_OutputDevice, 0, out device);
            Console.WriteLine("Cant get output device because : " + err);
            Thread.Sleep(1000);
            err = EDSDK.EdsSetPropertyData(this.CameraPtr, EDSDK.PropID_Evf_OutputDevice, 0, Marshal.SizeOf(EDSDK.EvfOutputDevice_PC), EDSDK.EvfOutputDevice_PC);
            Console.WriteLine("Cant set output device because : " + err);
            err = EDSDK.EdsCreateMemoryStream(0, out memStreamPtr);
            Console.WriteLine("Cant create memory stream because : " + err);
            err = EDSDK.EdsCreateEvfImageRef(memStreamPtr, out evfImgPtr);
            Console.WriteLine("Cant create evfimagestream because : " + err);
            Thread.Sleep(2000);
            return true;
        }

        public void stopLV()
        {
            this.LvActive = false;
            Thread.Sleep(500);
            EDSDK.EdsRelease(this.MemStreamPtr);
            EDSDK.EdsRelease(this.EvfImgPtr);
        }

        public void pauseLV()
        {
            LvActive = false;
        }

        public void Run()
        {
            LvActive = true;
            while (LvActive)
            {
                frames++;

            }
        }

        public void updatePic()
        {
            EDSDK.EdsDownloadEvfImage(this.CameraPtr, this.EvfImgPtr);
            IntPtr ptr;
            EDSDK.EdsGetPointer(MemStreamPtr, out ptr);
            uint len;
            EDSDK.EdsGetLength(MemStreamPtr, out len);
            Byte[] data = new Byte[len];
            Marshal.Copy(ptr, data, 0, (int)len);
            System.IO.MemoryStream memStream = new System.IO.MemoryStream(data);
            bmp.BeginInit();
            bmp.StreamSource = memStream;
            bmp.EndInit();
            memStream.Dispose();
            EDSDK.EdsRelease(ptr);
        }
    }
}
