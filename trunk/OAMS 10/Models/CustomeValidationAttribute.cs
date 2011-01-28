using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{
    [AttributeUsage(AttributeTargets.Class ,
                AllowMultiple = false,
                Inherited = true)]
    public sealed class ValidateTimeRangeAttribute
        : ValidationAttribute
    {
        //private const string _defaultErrorMessage =
        //    "’{0}’ must be at least {1} characters long.";

        private const string _defaultErrorMessage =
            "'FromDate' is greater than 'ToDate'";

        public ValidateTimeRangeAttribute()
            : base(_defaultErrorMessage)
        {
        }

        //public override string FormatErrorMessage(string name)
        //{
        //    return String.Format(CultureInfo.CurrentUICulture,
        //        ErrorMessageString,
        //        name, _minCharacters);
        //}

        public override bool IsValid(object value)
        {
            var range = value as ITimeRange;
            return range.IsValid();
        }
    }
}