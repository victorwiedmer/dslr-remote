using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

namespace Canon_EOS_Remote
{
    class CommandScriptPhoto : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Console.WriteLine("Script take photo clicked");
            takePhoto("TakePhoto");
        }

        public delegate void scriptEventHandler(string e);
        public event scriptEventHandler takePhotoCommand;

        private void takePhoto(string e){
            if (takePhotoCommand != null)
            {
                takePhotoCommand(e);
            }
        }
    }
}
