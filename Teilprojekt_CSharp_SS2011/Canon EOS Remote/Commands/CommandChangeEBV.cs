using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

namespace Canon_EOS_Remote.Commands
{
    class CommandChangeEBV : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            changeEBV("ChangeEBV");
        }

        public delegate void scriptEventHandler(string e);
        public event scriptEventHandler changeEbvCommand;

        private void changeEBV(string e){
            if (changeEbvCommand != null)
            {
                changeEbvCommand(e);
            }
        }
    }
}
