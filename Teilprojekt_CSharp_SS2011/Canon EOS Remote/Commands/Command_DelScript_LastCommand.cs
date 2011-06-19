using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Canon_EOS_Remote.Commands
{
    class Command_DelScript_LastCommand:ICommand
    {

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public delegate void scriptEventHandler(string e);
        public event scriptEventHandler delLastCommand;

        public void Execute(object parameter)
        {
            if (delLastCommand != null)
            {
                delLastCommand("DelLastCommand");
            }
        }
    }
}
