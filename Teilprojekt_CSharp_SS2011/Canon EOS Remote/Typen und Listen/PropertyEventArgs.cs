using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class PropertyEventArgs : EventArgs
    {
        private uint propertyName;

        public uint PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        public PropertyEventArgs(uint propertyName)
        {
            this.PropertyName = propertyName;
        }
    }
}
