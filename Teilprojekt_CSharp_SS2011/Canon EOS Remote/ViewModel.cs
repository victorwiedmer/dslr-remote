using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Canon_EOS_Remote.ViewModel
{
    class ViewModel :INotifyPropertyChanged, IDisposable
    {
        private Model model;

        private string _viewModelBla = "ViewModel";

        public string ViewModelBla
        {
            get { return _viewModelBla; }
            set { _viewModelBla = value; }
        }
        
        public ViewModel()
        {
            Model = new Model();
        }

        public Model Model
        {
            get { return model; }
            set { model = value;}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                System.Windows.MessageBox.Show("Property has changed : " + property);
            }
        }
    }
}
