using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading;

namespace Canon_EOS_Remote
{
    class Command_TakePhoto :ICommand , INotifyPropertyChanged
    {
        IntPtr camera=IntPtr.Zero;
        private int photoCount = 1;

        public int PhotoCount
        {
            get { return photoCount; }
            set { photoCount = value; }
        }

        public IntPtr Camera
        {
            get { return camera; }
            set { camera = value;
            update("Camera");
            }
        }

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                Console.WriteLine("Command_TakePhoto say : PropertyChanged : " + property);
            }
        }

        public bool CanExecute(object parameter)
        {
                return true;
        }

        public event EventHandler CanExecuteChanged;

        public void takePhoto()
        {
            uint tmpError = 0;
            for (int i = 0; i < this.photoCount; )
            {
                tmpError = EDSDKLib.EDSDK.EdsSendCommand(this.camera, EDSDKLib.EDSDK.CameraCommand_TakePicture, 0);
                if (tmpError == 0) { i++; }
                else
                {
                    Thread.Sleep(1);
                }
            }

        }

        public void Execute(object parameter)
        {
            Thread photoThread = new Thread(new ThreadStart(this.takePhoto));
            photoThread.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
