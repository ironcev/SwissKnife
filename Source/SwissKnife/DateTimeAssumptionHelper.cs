using System.Globalization;

namespace SwissKnife
{
    internal static class DateTimeAssumptionHelper
    {
        internal static DateTimeStyles ToDateTimeStyles(DateTimeAssumption dateTimeAssumption)
        {
            System.Diagnostics.Debug.Assert(dateTimeAssumption == DateTimeAssumption.AssumeLocal || dateTimeAssumption == DateTimeAssumption.AssumeUniversal);

            switch (dateTimeAssumption)
            {
                case DateTimeAssumption.AssumeLocal: return DateTimeStyles.AssumeLocal;
                case DateTimeAssumption.AssumeUniversal: return DateTimeStyles.AssumeUniversal;                
            }

            return DateTimeStyles.AssumeUniversal; // Just to make it compile. This line will never be reached.
        }
    }
}