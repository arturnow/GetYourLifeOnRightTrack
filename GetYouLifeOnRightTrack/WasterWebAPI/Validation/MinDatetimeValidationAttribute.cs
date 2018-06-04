using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WasterWebAPI.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    [AttributeUsage(AttributeTargets.Property |AttributeTargets.Field, AllowMultiple = false)]
    public class NowDatetimeValidationAttribute : ValidationAttribute, IClientValidatable
    {

        public NowDatetimeValidationAttribute(string errorMessage = null):base(errorMessage){}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value==null)
                return new ValidationResult(string.Format(this.ErrorMessageString, validationContext.DisplayName)); 

            DateTime cast;
            if(DateTime.TryParse(value.ToString(), out cast))
            {
                if(cast >= DateTime.Today)
                {
                    return ValidationResult.Success;
                }    
            }
            
            return new ValidationResult(string.Format(this.ErrorMessageString, validationContext.DisplayName));

        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string errorMessage = this.ErrorMessageString;

            // The value we set here are needed by the jQuery adapter
            ModelClientValidationRule dateGreaterThanRule = new ModelClientValidationRule();
            dateGreaterThanRule.ErrorMessage = errorMessage;
            dateGreaterThanRule.ValidationType = "dategreaterthannow"; // This is the name the jQuery adapter will use
            //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
            dateGreaterThanRule.ValidationParameters.Add("mindate", DateTime.Today);

            yield return dateGreaterThanRule;

        }
    }
}