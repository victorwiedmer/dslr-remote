using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Canon_EOS_Remote.Commands
{
    class Command_DelScript:ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public delegate void scriptEventHandler(string e);
        public event scriptEventHandler delscriptHandler;

        private void sendDelScriptEvent(string e)
        {
            if (delscriptHandler != null)
            {
                delscriptHandler("DelScript");
            }
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
