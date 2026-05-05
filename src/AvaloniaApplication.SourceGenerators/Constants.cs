using Microsoft.CodeAnalysis;

namespace AvaloniaApplication.SourceGenerators;

public static class Constants
{
    public const string SourceFileNameSuffix = ".g.cs";

    public static readonly Dictionary<Accessibility, string> AccessibilityMap = new()
    {
        [Accessibility.NotApplicable] = "<unknown>",
        [Accessibility.Public] = "public",
        [Accessibility.Protected] = "protected",
        [Accessibility.Private] = "private",
        [Accessibility.Internal] = "internal",
        [Accessibility.ProtectedOrInternal] = "protected internal",
        [Accessibility.ProtectedAndInternal] = "private protected",
    };
}
