using System.ComponentModel.DataAnnotations;

namespace Kyrsova
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true; 
            }

            if (DateTime.TryParse(Convert.ToString(value), out DateTime dateValue))
            {
                return dateValue > DateTime.Now;
            }

            return false;
        }
    }

}
