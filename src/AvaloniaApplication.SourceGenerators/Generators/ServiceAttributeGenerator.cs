using System.Text;
using AvaloniaApplication.SourceGenerators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AvaloniaApplication.SourceGenerators;

[Generator]
public sealed class ServiceAttributeGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext initContext)
    {
        IncrementalValuesProvider<PropertyData> provider = initContext.SyntaxProvider.ForAttributeWithMetadataName(
            "AvaloniaApplication.Attributes.ServiceAttribute",
            static (node, _) => node is PropertyDeclarationSyntax,
            static (context, _) => context.SemanticModel.GetDeclaredSymbol((PropertyDeclarationSyntax)context.TargetNode) is IPropertySymbol {IsStatic: true, IsReadOnly: true} propertySymbol
                                       ? new PropertyData(propertySymbol.Name, $"_lazy{propertySymbol.Name}", propertySymbol.Type.ToDisplayString(), AccessibilityMap[propertySymbol.DeclaredAccessibility], propertySymbol.ContainingType.Name, propertySymbol.ContainingNamespace)
                                       : null
        ).Where(static propertyData => propertyData is not null)!;

        initContext.RegisterSourceOutput(provider, static (context, propertyData) =>
        {
            StringBuilder sourceCodeBuilder = StringBuilder.CreateSourceCode(propertyData.ContainingNamespaceSymbol);
            sourceCodeBuilder.AppendLine($$"""
                                           partial class {{propertyData.ContainingClassName}}
                                           {
                                               private static readonly Lazy<{{propertyData.TypeQualifiedName}}> {{propertyData.LazyName}} = new(static () => AvaloniaApplication.Program.GetService<{{propertyData.TypeQualifiedName}}>());
                                               {{propertyData.AccessModifiers}} static partial {{propertyData.TypeQualifiedName}} {{propertyData.Name}} { get => {{propertyData.LazyName}}.Value; }
                                           }
                                           """);

            context.AddSource($"{propertyData.ContainingNamespaceSymbol.ToDisplayString()}.{propertyData.ContainingClassName}.{propertyData.Name}{SourceFileNameSuffix}", sourceCodeBuilder.ToString());
        });
    }

    private sealed record class PropertyData(string Name, string LazyName, string TypeQualifiedName, string AccessModifiers, string ContainingClassName, INamespaceSymbol ContainingNamespaceSymbol);
}
