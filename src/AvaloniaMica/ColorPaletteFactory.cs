using System;
using ActiproSoftware.UI.Avalonia.Themes.Generation;
using Avalonia;
using Avalonia.Media;

namespace AvaloniaMica;

public enum AccentColor
{
    System = 0,
    Custom,
    Red,
    Orange,
    Amber,
    Yellow,
    Lime,
    Green,
    Emerald,
    Teal,
    Cyan,
    Sky,
    Blue,
    Indigo,
    Violet,
    Purple,
    Fuchsia,
    Pink,
}

internal class ColorPaletteFactory : DefaultColorPaletteFactory
{
    public AccentColor AccentColor
    {
        get;
        set
        {
            field = value;
            SetAccentColor(value);
        }
    } = AccentColor.System;

    public Color CustomAccentColor
    {
        get;
        set
        {
            field = value;
            SetAccentColor(AccentColor.Custom);
        }
    } = Colors.Coral;

    public bool BorderAccentColorEnabled
    {
        get;
        set
        {
            field = value;
            SetAccentColor(AccentColor);
        }
    }
    
    public override ColorPalette Create()
    {
        var palette = base.Create();
        var color = AccentColor switch
        {
            AccentColor.System => GetSystemAccentColor(),
            AccentColor.Custom => CustomAccentColor,
            _ => null,
        };

        if (color is null) 
            return palette;
        
        palette.Ramps.Remove(Hue.Red);
        palette.Ramps.Add(CreateColorRamp(Hue.Red, color.Value));

        return palette;
    }

    public Color GetColor(AccentColor accentColor) => accentColor switch
    {
        AccentColor.System => GetSystemAccentColor() ?? Colors.Transparent,
        AccentColor.Custom => CustomAccentColor,
        AccentColor.Red => RedMidtoneColor,
        AccentColor.Orange => OrangeMidtoneColor,
        AccentColor.Amber => AmberMidtoneColor,
        AccentColor.Yellow => YellowMidtoneColor,
        AccentColor.Lime => LimeMidtoneColor,
        AccentColor.Green => GreenMidtoneColor,
        AccentColor.Emerald => EmeraldMidtoneColor,
        AccentColor.Teal => TealMidtoneColor,
        AccentColor.Cyan => CyanMidtoneColor,
        AccentColor.Sky => SkyMidtoneColor,
        AccentColor.Blue => BlueMidtoneColor,
        AccentColor.Indigo => IndigoMidtoneColor,
        AccentColor.Violet => VioletMidtoneColor,
        AccentColor.Purple => PurpleMidtoneColor,
        AccentColor.Fuchsia => FuchsiaMidtoneColor,
        AccentColor.Pink => PinkMidtoneColor,
        _ => throw new ArgumentOutOfRangeException(nameof(accentColor), accentColor, null)
    };

    public void StartSystemAccentColorWatcher()
    {
        App.MainWindow?.PlatformSettings?.ColorValuesChanged += (_, _) =>
        {
            if (AccentColor != AccentColor.System)
                return;
            SetAccentColor(AccentColor);
        };
    }
    
    private Color? GetSystemAccentColor() => App.MainWindow?.PlatformSettings?.GetColorValues().AccentColor1;
    
    private void SetAccentColor(AccentColor accentColor)
    {
        if (App.Theme?.Definition is null)
            return;
        
        Application.Current?.Resources["UIWindowBorderColorActive"] = BorderAccentColorEnabled 
            ? GetColor(accentColor)
            : Colors.Transparent;

        App.Theme.Definition.AccentColorRampName = GetColorRampName(accentColor);
        App.Theme.RefreshResources();
    }

    private string GetColorRampName(AccentColor accentColor) => accentColor switch
    {
        AccentColor.System or AccentColor.Custom => nameof(AccentColor.Red),
        _ => accentColor.ToString()
    };
}