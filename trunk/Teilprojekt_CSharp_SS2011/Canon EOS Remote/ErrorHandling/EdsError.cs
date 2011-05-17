using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote
{
    class EdsError
    {
        private bool developing = true;

        #region Declaration of class members
        private uint _errorCodeNumber;
        private string _errorCodeString;
        private string _errorDescription;
        #endregion

        #region Setter and Getter of class members
        public uint ErrorCodeNumber
        {
            get { return _errorCodeNumber; }
            set { _errorCodeNumber = value; }
        }

        public string ErrorCodeString
        {
            get { return _errorCodeString; }
            set { _errorCodeString = value; }
        }

        public string ErrorDescription
        {
            get { return _errorDescription; }
            set { _errorDescription = value; }
        }

#endregion

        #region Constructors
        public EdsError()
        {
        }

        public EdsError(uint _errorCodeNumber)
        {
            this._errorCodeNumber = _errorCodeNumber;
            this._errorCodeString = ErrorCodes.getErrorDataWithCodeNumber(_errorCodeNumber).ErrorCodeString;
            this._errorDescription = ErrorCodes.getErrorDataWithCodeNumber(_errorCodeNumber).ErrorDescription;
        }

        public EdsError(uint _errorCodeNumber, string _errorCodeString, string _errorDescription)
        {
            this._errorCodeNumber = _errorCodeNumber;
            this._errorCodeString = _errorCodeString;
            this._errorDescription = _errorDescription;
        }
        #endregion

        public override string ToString()
        {
            if (developing)
            {
                return this.ErrorCodeNumber + "-" + this.ErrorCodeString + "-" + this.ErrorDescription;
            }
            else
            {
                return this._errorDescription;
            }
        }
    }
}
