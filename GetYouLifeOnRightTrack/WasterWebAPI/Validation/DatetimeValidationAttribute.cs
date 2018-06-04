using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WasterWebAPI.Validation
{
    using System.ComponentModel.DataAnnotations;

    public class DatetimeValidationAttribute : ValidationAttribute
    {
        private readonly string _fieldName;

        public DatetimeValidationAttribute(string FieldName)
        {
            _fieldName = FieldName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime cast;
            if(DateTime.TryParse(value.ToString(), out cast))
            {
                if(cast >= DateTime.Today)
                {
                    return ValidationResult.Success;
                }    
            }
            
            return new ValidationResult(string.Format("Pole {0} nie może być datą przeszłą", validationContext.DisplayName));

        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }
    }
}