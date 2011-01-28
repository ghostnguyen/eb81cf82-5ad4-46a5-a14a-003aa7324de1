using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{
    [MetadataType(typeof(ContractTimelineNotaion))]
    [ValidateTimeRange]
    [ValidateContractTimeline]
    public partial class ContractTimeline : ITimeRange
    {
    }

    public class ContractTimelineNotaion
    {
        [Required]
        public DateTime? FromDate { get; set; }

        [Required]
        public DateTime? ToDate { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class,
                AllowMultiple = false,
                Inherited = true)]
    public sealed class ValidateContractTimelineAttribute
        : ValidationAttribute
    {
        private const string _defaultErrorMessage =
            "Timeline is conflict.";

        public ValidateContractTimelineAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            bool isValid = false;
            var e = value as ContractTimeline;
            
            if (e != null)
            {
                ContractTimelineRepository repo = new ContractTimelineRepository();
                isValid = repo.ValidConflictTimeline(e); 
            }

            return isValid;
        }
    }
}