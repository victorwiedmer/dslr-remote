using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Canon_EOS_Remote.Commands
{
    class Command_SaveAllPictures : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public delegate void clickEvent();

        public event clickEvent clickEventHandler;

        public void Execute(object parameter)
        {
            if (clickEventHandler != null)
            {
                clickEventHandler();
            }
        }
    }
}
