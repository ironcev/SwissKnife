namespace SwissKnife.IdentifierConversion
{
    /// <summary>
    /// Represents various options that can be used to customize the conversion of identifier expressions into their string representations.
    /// In other words, <see cref="ConversionOptions"/> define the output of the <see cref="Identifier"/> class methods.
    /// They are meant to be used in advanced scenarios when the default output of the <see cref="Identifier"/> class methods needs to be customized.
    /// </summary>
    /// <remarks>
    /// To learn more about identifier expressions, see the <see cref="Identifier"/> class.
    /// </remarks>
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
