namespace AvaloniaApplication.Attributes;

/// <summary>
/// Marks the decorated property as a service, provided that it is <c>static</c>
/// and has only a <c>get</c> accessor.
/// </summary>
/// <remarks>
/// Decorated properties use a <see cref="Lazy{T}"/> backing field.
/// </remarks>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ServiceAttribute : Attribute;
