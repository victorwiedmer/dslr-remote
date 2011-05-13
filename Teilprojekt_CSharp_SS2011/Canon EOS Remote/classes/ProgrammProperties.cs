using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class ProgrammProperties
    {
        private string _pathToSavePictures;
        private string _pathToSaveScripts;

        public string PathToSavePictures
        {
            get { return _pathToSavePictures; }
            set { _pathToSavePictures = value; }
        }
        
        public string PathToSaveScripts
        {
            get { return _pathToSaveScripts; }
            set { _pathToSaveScripts = value; }
        }
    }
}
