using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDSDKLib;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Canon_EOS_Remote
{
    class ModelCurrentCamera : INotifyPropertyChanged
    {

        private EDSDK.EdsPropertyDesc propertyDescISO;
        public EDSDK.EdsPropertyDesc PropertyDescISO
        {
            get { return propertyDescISO; }
            set { propertyDescISO = value;
            update("PropertyDescISO");
            }
        }

        private EDSDK.EdsPropertyDesc propertyDescTv;

        public EDSDK.EdsPropertyDesc PropertyDescTv
        {
            get { return propertyDescTv; }
            set { propertyDescTv = value;
            update("PropertyDescTv");
            }
        }

        private EDSDK.EdsPropertyDesc propertyDescEBV;
        public EDSDK.EdsPropertyDesc PropertyDescEBV
        {
            get { return propertyDescEBV; }
            set { propertyDescEBV = value; }
        }

        private EDSDK.EdsPropertyDesc propertyDescAE;
        public EDSDK.EdsPropertyDesc PropertyDescAE
        {
            get { return propertyDescAE; }
            set { propertyDescAE = value;
            update("PropertyDescAE");
            }
        }

        private EDSDK.EdsPropertyDesc apertureDesc;
        public EDSDK.EdsPropertyDesc ApertureDesc
        {
            get { return apertureDesc; }
            set { apertureDesc = value;
            update("ApertureDesc");
            }
        }

        #region ObservableCollections fuer die CollectionViews der GUI
        private ObservableCollection<string> availableISOListCollection;
        public ObservableCollection<string> AvailableISOListCollection
        {
            get { return availableISOListCollection; }
            set
            {
                availableISOListCollection = value;
                update("AvailableISOListCollection");
            }
        }

        private ObservableCollection<string> availableShutterTimesCollection;
        public ObservableCollection<string> AvailableShutterTimesCollection
        {
            get { return availableShutterTimesCollection; }
            set
            {
                availableShutterTimesCollection = value;
                update("AvailableShutterTimesCollection");
            }
        }

        private ObservableCollection<string> availableEVBCollection;
        public ObservableCollection<string> AvailableEVBCollection
        {
            get { return availableEVBCollection; }
            set
            {
                availableEVBCollection = value;
                update("AvailableEVBCollection");
            }
        }

        private ObservableCollection<string> apertureCollection;
        public ObservableCollection<string> ApertureCollection
        {
            get { return apertureCollection; }
            set
            {
                apertureCollection = value;
                update("ApertureCollection");
            }
        }

        private ObservableCollection<string> aECollection;
        public ObservableCollection<string> AECollection
        {
            get { return aECollection; }
            set
            {
                aECollection = value;
                update("AECollection");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public ModelCurrentCamera()
        {
            initObservableCollections();
        }

        private void initObservableCollections()
        {
            this.AvailableISOListCollection = new ObservableCollection<string>();
            this.AvailableShutterTimesCollection = new ObservableCollection<string>();
            this.AvailableEVBCollection = new ObservableCollection<string>();
            this.ApertureCollection = new ObservableCollection<string>();
            this.AECollection = new ObservableCollection<string>();
        }

        private void update(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
