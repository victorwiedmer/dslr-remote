using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

namespace Canon_EOS_Remote
{
    class CommandChangeAv : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            changeAv("ChangeAv");
        }

        public delegate void scriptEventHandler(string e);
        public event scriptEventHandler changeAvCommand;

        private void changeAv(string e){
            if (changeAvCommand != null)
            {
                changeAvCommand(e);
            }
        }
    }
}
