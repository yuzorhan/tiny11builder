namespace Tiny11Builder.Core;

internal static class WindowsImageIdentity
{
    // Enablement releases share the servicing base of the release they enable. 21H2 (22000) is a
    // servicing base of its own, so 22621+ tools satisfy it and no folding entry is needed here.
    public static int ServicingBase(int build) => build switch
    {
        22631 => 22621,
        26200 or 26300 => 26100,
        _ => build
    };

    public static bool Matches(WindowsImage actual, WindowsImage expected) =>
        actual.Architecture.Equals(expected.Architecture, StringComparison.OrdinalIgnoreCase) &&
        actual.Edition.Equals(expected.Edition, StringComparison.OrdinalIgnoreCase) &&
        actual.Installation.Equals(expected.Installation, StringComparison.OrdinalIgnoreCase) &&
        actual.Version.Major == expected.Version.Major && actual.Version.Minor == expected.Version.Minor &&
        actual.Version.Revision == expected.Version.Revision &&
        ServicingBase(actual.Version.Build) == ServicingBase(expected.Version.Build);
}
