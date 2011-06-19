using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading;

namespace Canon_EOS_Remote.Commands
{
    class Command_RunScript :ICommand , INotifyPropertyChanged
    {
        private List<ScriptCommand> scriptCommands;

        public List<ScriptCommand> ScriptCommands
        {
            get { return scriptCommands; }
            set { scriptCommands = value; }
        }

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                Console.WriteLine(
                    "|------------------------------------------------------|\n" + 
                    "|Command_RunScript say : PropertyChanged : " + property + "|\n" +
                    "|------------------------------------------------------|\n");
            }
        }

        public Command_RunScript()
        {
            this.ScriptCommands = new List<ScriptCommand>();
        }

        public bool CanExecute(object parameter)
        {
                return true;
        }

        public event EventHandler CanExecuteChanged;

        public void runScript()
        {
            uint tmperror = 0;
            Console.WriteLine("Will run : " + this.ScriptCommands.Count + " commands");
            for (int i = 0; i < this.ScriptCommands.Count; i++)
            {
                Console.WriteLine("Run the " + i + " command : " + this.ScriptCommands.ElementAt(i).Command);
                if (this.ScriptCommands.ElementAt(i).Command == EDSDKLib.EDSDK.CameraCommand_TakePicture)
                {
                    tmperror=EDSDKLib.EDSDK.EdsSendCommand(this.ScriptCommands.ElementAt(i).CommandDestination, EDSDKLib.EDSDK.CameraCommand_TakePicture, 0);
                    if (tmperror != 0)
                    {
                        Console.WriteLine("Error at taking photo in script : " + tmperror);
                    }
                    Console.WriteLine("Nehme Foto auf ...");
                }
                else
                {
                    tmperror=EDSDKLib.EDSDK.EdsSetPropertyData(this.ScriptCommands.ElementAt(i).CommandDestination,
                        this.ScriptCommands.ElementAt(i).Command, 0, this.ScriptCommands.ElementAt(i).ParamSize, this.ScriptCommands.ElementAt(i).CommandParam);
                    if (tmperror != 0)
                    {
                        Console.WriteLine("Error at Change Property via script : " + tmperror);
                    }
                    Console.WriteLine("Change Property via script");
                }
                if (tmperror != 0)
                {
                    i--;
                }
                Thread.Sleep(1000);
            }
        }

        public void Execute(object parameter)
        {
            Console.WriteLine(
                "|--------------------------|\n" + 
                "|Command run script clicked|\n" + 
                "|--------------------------|\n"
                );
            Thread scriptThread = new Thread(new ThreadStart(this.runScript));
            scriptThread.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
