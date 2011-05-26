using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canon_EOS_Remote.classes;

namespace Canon_EOS_Remote
{
    class Model
    {
        private SDK _sdk;

        internal SDK Sdk
        {
            get { return _sdk; }
            set { _sdk = value; }
        }
        public Model()
        {
            _sdk = new SDK();
         System.Windows.MessageBox.Show("Model created");
        }

    }
}
