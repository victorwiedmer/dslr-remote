using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

namespace Canon_EOS_Remote
{
    class Command_TakePhoto :ICommand , INotifyPropertyChanged
    {
        IntPtr camera=IntPtr.Zero;

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

        public void Execute(object parameter)
        {
            Console.WriteLine("Rise command : take photo");
            EDSDKLib.EDSDK.EdsSendCommand(this.Camera, EDSDKLib.EDSDK.CameraCommand_TakePicture, 0);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
