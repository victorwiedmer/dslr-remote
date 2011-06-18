using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.Types
{
    /// <summary>
    /// Stellt einen ISO-Wert dar, mit den Feldern decValue der den ISO-Wert als String repräsentiert 
    /// und dem Feld hexValue der den ISO-Wert in hexadezimalen Schreibweise darstellt.
    /// </summary>
    class TISOValue
    {
        #region Declaration of class members

        private string decValue;
        private UInt32 hexValue;

        #endregion

        #region Getter and Setter of class members

        public UInt32 HexValue
        {
            get { return hexValue; }
            set { hexValue = value; }
        }

        /// <summary>
        /// Getter und Setter des Klassenfeldes decValue
        /// </summary>
        /// <exception cref="FormatException">Wird geworfen wenn das Format der zu setzenden Wertes mit dem Klassenfeld nicht übereinstimmt</exception>
        public string DecValue
        {
            get { return decValue; }
            set {
                try
                {
                    decValue = value;
                }
                catch (FormatException e)
                {
                    System.Windows.MessageBox.Show("FormatException at TISOValue:decValue:" + this + "\n" + e);
                }
            }
        }

        #endregion

        #region Construtors

        public TISOValue(string decValue, UInt32 hexValue)
        {
            this.DecValue = decValue;
            this.HexValue = hexValue;
        }

        public TISOValue(UInt32 hexValue, string decValue)
        {
            this.DecValue = decValue;
            this.HexValue = hexValue;
        }

        #endregion
    }
}
