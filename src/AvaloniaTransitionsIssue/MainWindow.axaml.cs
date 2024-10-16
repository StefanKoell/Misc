using System;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaTransitions;

public partial class MainWindow : Window
{
    private readonly PageSlide _pageSlide = new(TimeSpan.FromMilliseconds(500), PageSlide.SlideAxis.Vertical)
    {
        SlideInEasing = new ExponentialEaseOut(),
        SlideOutEasing = new CubicEaseIn()
    };

    private readonly CompositePageTransition _pageTransition = new()
    {
        PageTransitions =
        {
            new CrossFade(TimeSpan.FromMilliseconds(300))
            {
                FadeInEasing = new ExponentialEaseIn(),
                FadeOutEasing = new CubicEaseIn()
            },
        }
    };

    public MainWindow()
    {
        InitializeComponent();
        _pageTransition.PageTransitions.Add(_pageSlide);
        TransitioningContentControl.PageTransition = _pageTransition;
        TransitioningContentControl.Content = CreateControl(typeof(UserControl1));
    }

    private void Button1_OnClick(object? sender, RoutedEventArgs e)
    {
        TransitioningContentControl.Content = CreateControl(typeof(UserControl1));
    }

    private void Button2_OnClick(object? sender, RoutedEventArgs e)
    {
        TransitioningContentControl.Content = CreateControl(typeof(UserControl2));
    }

    private Control CreateControl(Type controlType)
    {
        var control = (Control)Activator.CreateInstance(controlType)!;
        control.DataContext = DataContext;
        return control;
    }
}