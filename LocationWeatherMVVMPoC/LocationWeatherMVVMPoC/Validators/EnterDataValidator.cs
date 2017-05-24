using System;
using System.Text.RegularExpressions;

namespace LocationWeatherMVVMPoC
{
    public class EnterDataValidator
    {
        public void ValidateData(string stringToValidate, Action<bool, string> callback)
        {
            bool isValid = true;
            var validateToResult = stringToValidate;
            if (IsDataNotValid(validateToResult))
            {
                isValid = false;
                validateToResult = string.Empty;
            }
            InvokeCallback(isValid, validateToResult, callback);
        }

        private bool IsDataNotValid(string data)
        {
            if (!string.IsNullOrWhiteSpace(data))
            {
                bool isThereAnyLetters = Regex.IsMatch(data, @"[a-zA-Z]");
                return isThereAnyLetters;
            }
            else
            {
                return true;
            }
        }

        private void InvokeCallback(bool isValid, string result, Action<bool, string> callback)
        {
            callback?.Invoke(isValid, result);
        }
    }
}
