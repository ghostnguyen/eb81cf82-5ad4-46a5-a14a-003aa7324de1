using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public interface ITimeRange
    {
        DateTime? FromDate { get; set; }
        DateTime? ToDate { get; set; }
    }

    public class TimeRange : ITimeRange
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public static class TimeRangeFunction
    {
        public static bool IsJoin(this ITimeRange value1, ITimeRange value)
        {
            return !value.Join(value1).IsEmpty();
        }

        public static bool IsValid(this ITimeRange value)
        {
            return (value != null
                && value.FromDate.HasValue
                &&
                (
                    !value.FromDate.HasValue
                    ||
                    value.ToDate >= value.FromDate
                )
                );
        }

        public static bool IsEmpty(this ITimeRange value)
        {
            return (value != null
                && !value.FromDate.HasValue
                && !value.ToDate.HasValue
                );
        }

        public static ITimeRange Join(this ITimeRange value1, ITimeRange value)
        {
            if (!value.IsValid()
                || !value1.IsValid())
                throw new Exception("Invalid time range.");

            DateTime? newFromDate = value.FromDate > value1.FromDate ? value.FromDate : value1.FromDate;
            DateTime? newToDate = value.ToDate.HasValue ?
                (value1.ToDate.HasValue ?
                    (value.ToDate < value1.ToDate ? value.ToDate : value1.ToDate)
                    : value.ToDate)
                : value1.ToDate;

            TimeRange range = new TimeRange() { FromDate = newFromDate, ToDate = newToDate };

            return range.IsValid() ? range : new TimeRange();
        }

        public static bool Contains(this ITimeRange timerange, DateTime? datetime)
        {
            return timerange.IsValid()
                && datetime.HasValue
                && timerange.FromDate <= datetime
                &&
                (
                    !timerange.ToDate.HasValue
                    || (timerange.ToDate.Ceiling() >= datetime)
                );
        }
    }
}