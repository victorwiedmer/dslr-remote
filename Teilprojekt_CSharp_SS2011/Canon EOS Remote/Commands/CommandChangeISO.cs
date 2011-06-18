using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

namespace Canon_EOS_Remote.Commands
{
    class CommandChangeISO : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            changeISO("ChangeISO");
        }

        public delegate void scriptEventHandler(string e);
        public event scriptEventHandler changeIsoCommand;

        private void changeISO(string e){
            if (changeIsoCommand != null)
            {
                changeIsoCommand(e);
            }
        }
    }
}
