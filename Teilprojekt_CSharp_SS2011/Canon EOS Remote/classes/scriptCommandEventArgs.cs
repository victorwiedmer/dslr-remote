using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class scriptCommandEventArgs
    {
        private string command;

        public string Command
        {
            get { return command; }
            set { command = value; }
        }

        public scriptCommandEventArgs(string eventString)
        {

        }
    }
}
