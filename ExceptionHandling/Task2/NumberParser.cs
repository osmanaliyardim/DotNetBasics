using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            int intValue = 0;
            string trimmedStringValue = "";
            bool isValueNegative = false;

            if (!string.IsNullOrEmpty(stringValue) && !string.IsNullOrWhiteSpace(stringValue))
            {
                trimmedStringValue = stringValue.Trim();
            }
            else if (stringValue == null)
            {
                throw new ArgumentNullException();
            }
            else throw new FormatException();

            if (trimmedStringValue.StartsWith('+'))
            {
                trimmedStringValue = trimmedStringValue.Substring(1);
            }
            else if (trimmedStringValue.StartsWith("-"))
            {
                trimmedStringValue = trimmedStringValue.Substring(1);
                isValueNegative = true;
            }

            for (int i = 0; i < trimmedStringValue.Length; i++)
            {
                char ch = trimmedStringValue[i];

                if (char.IsDigit(ch))
                {
                    intValue *= 10;
                    int asciiValueDifference = intValue + (ch - '0');

                    if (asciiValueDifference < 0 && !isValueNegative)
                        throw new OverflowException();
                    else if (asciiValueDifference < 0 && asciiValueDifference > Int32.MinValue)
                        throw new OverflowException();
                    else intValue += ch - '0';
                }
                else throw new FormatException();
            }

            return isValueNegative ? (-1) * intValue : intValue;
        }
    }
}