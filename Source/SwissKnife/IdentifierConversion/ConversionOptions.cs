namespace SwissKnife.IdentifierConversion
{
    /// <summary>
    /// // TODO-iG: Combine these two comments: Represents the parameters used to customize the conversion of identifier expressions into their string representations.
    /// Represents the parameters used to customize the output of the <see cref="Identifier.ToString{T}(System.Linq.Expressions.Expression{System.Func{T,object}})"/> methods.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    public sealed class ConversionOptions // TODO-IG: Add options for conversion of arrays and methods.
    {
        public string Separator { get; set; }
        public StaticMemberConversion StaticMemberConversion { get; set; }

        public ConversionOptions()
        {
            Separator = ".";
            StaticMemberConversion = StaticMemberConversion.MemberNameOnly;
        }
    }
}
