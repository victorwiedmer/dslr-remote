using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

namespace Canon_EOS_Remote.Commands
{
    class CommandChangeTv : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            changeTv("ChangeTv");
        }

        public delegate void scriptEventHandler(string e);
        public event scriptEventHandler changeTvCommand;

        private void changeTv(string e){
            if (changeTvCommand != null)
            {
                changeTvCommand(e);
            }
        }
    }
}
