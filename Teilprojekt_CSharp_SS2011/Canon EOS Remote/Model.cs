using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canon_EOS_Remote.classes;
using System.ComponentModel;

namespace Canon_EOS_Remote
{
    class Model : INotifyPropertyChanged
    {
        private SDK _sdk;

        private string _modelBla = "Model";

        public string ModelBla
        {
            get {return _modelBla; }
            set { _modelBla = value; }
        }

        public SDK Sdk
        {
            get { return _sdk; }
            set { _sdk = value;}
        }

        public Model()
        {
         Sdk = new SDK();
         //System.Windows.MessageBox.Show("Model created");
        }

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                System.Windows.MessageBox.Show("Property has changed from : " + this + " : " + property);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
