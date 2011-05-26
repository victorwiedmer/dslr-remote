using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Canon_EOS_Remote.ViewModel
{
    class ViewModel :INotifyPropertyChanged, IDisposable
    {
        Model model;
        
        public ViewModel()
        {
            model = new Model();
        }

        internal Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
