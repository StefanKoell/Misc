using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using AvaMarkupExtensionTranslate.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace AvaMarkupExtensionTranslate.MarkupExtensions;

public class UIStringExtension
{
    private static readonly ConditionalWeakTable<Control, Dictionary<AvaloniaProperty, Localize>> References = new ();
    
    public string Text { get; init; } = string.Empty;
    
    [SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "Markup extension contract")]
    public object? ProvideValue(IServiceProvider serviceProvider)
    {
        var provideTarget = serviceProvider.GetService<IProvideValueTarget>();
        if (provideTarget?.TargetObject is not Control control || 
            provideTarget.TargetProperty is not AvaloniaProperty avaloniaProperty)
            return null;
        
        if (!References.TryGetValue(control, out var props))
        {
            props = new Dictionary<AvaloniaProperty, Localize>();
            References.Add(control, props);
        }
        
        if (!props.TryGetValue(avaloniaProperty, out var localize))
        {
            localize = new Localize(Text);
            props.Add(avaloniaProperty, localize);
        }

        var binding = new Binding
        {
            Source = localize,
            Path = nameof(Localize.Value)
        };
        
        return binding;
    }
}