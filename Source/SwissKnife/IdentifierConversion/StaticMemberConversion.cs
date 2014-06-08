namespace SwissKnife.IdentifierConversion
{
    public enum StaticMemberConversion //TODO-IG: This should be available not only for static members, but for all members. E.g. MemberNameConversion
    {
        MemberNameOnly,
        ParentTypeName, // TODO-IG: Rename to WithParentTypeName or IncludeParentTypeName
        ParentTypeFullName // TODO-IG: Rename to WithParentTypeFullName or IncludeParentTypeFullName
    }
}
