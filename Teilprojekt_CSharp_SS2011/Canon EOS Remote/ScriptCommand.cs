using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote
{
    class ScriptCommand
    {
        private IntPtr commandDestination;

        public IntPtr CommandDestination
        {
            get { return commandDestination; }
            set { commandDestination = value; }
        }
        private UInt32 command;

        public UInt32 Command
        {
            get { return command; }
            set { command = value; }
        }
        private UInt32 commandParam;

        public UInt32 CommandParam
        {
            get { return commandParam; }
            set { commandParam = value; }
        }

        private int paramSize;
        private IntPtr intPtr;
        private uint p;
        private int p_2;
        private object p_3;

        public int ParamSize
        {
            get { return paramSize; }
            set { paramSize = value; }
        }



        public ScriptCommand(IntPtr ptr, UInt32 command, int paramSize ,  UInt32 param)
        {
            this.CommandDestination = ptr;
            this.Command = command;
            this.ParamSize = paramSize;
            this.CommandParam = param;
        }

        public ScriptCommand(IntPtr intPtr, uint p, int p_2, object p_3)
        {
            // TODO: Complete member initialization
            this.intPtr = intPtr;
            this.p = p;
            this.p_2 = p_2;
            this.p_3 = p_3;
        }
    }
}
