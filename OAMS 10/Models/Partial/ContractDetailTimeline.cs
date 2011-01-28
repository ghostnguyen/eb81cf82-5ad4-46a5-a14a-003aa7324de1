using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{
    [MetadataType(typeof(ContractDetailTimelineNotaion))]
    [ValidateTimeRange]
    [ValidateContractDetailTimeline]
    public partial class ContractDetailTimeline : ITimeRange
    {
    }

    public class ContractDetailTimelineNotaion
    {
        [Required]
        public DateTime? FromDate { get; set; }

        [Required]
        public DateTime? ToDate { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class,
                AllowMultiple = false,
                Inherited = true)]
    public sealed class ValidateContractDetailTimelineAttribute
        : ValidationAttribute
    {
        private const string _defaultErrorMessage =
            "Timeline is conflict.";

        public ValidateContractDetailTimelineAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            bool isValid = false;
            var e = value as ContractDetailTimeline;
            
            if (e != null)
            {
                ContractDetailTimelineRepository repo = new ContractDetailTimelineRepository();
                isValid = repo.ValidConflictTimeline(e); 
            }

            return isValid;
        }
    }
}