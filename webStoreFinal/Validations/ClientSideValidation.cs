using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webStoreFinal.Validations
{
    public class ClientSideValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string numberCheck = value.ToString();
            bool IsNum = double.TryParse(numberCheck, out double price);
            if (IsNum && price > 0)
            {
                return true;
            }
            return false;

        }
        public override string FormatErrorMessage(string name)
        {
            return string.Format("please make sure to enter a positive number");
        }
    }
}
