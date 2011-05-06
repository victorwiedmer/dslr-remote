using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote
{
    class EdsError
    {
        private uint _errorCodeNumber;
        private string _errorCodeString;
        private string _errorDescription;

        public EdsError(uint _errorCodeNumber)
        {
            this._errorCodeNumber = _errorCodeNumber;
        }

        public EdsError(uint _errorCodeNumber, string _errorCodeString, string _errorDescription)
        {
            this._errorCodeNumber = _errorCodeNumber;
            this._errorCodeString = _errorCodeString;
            this._errorDescription = _errorDescription;
        }
    }
}
