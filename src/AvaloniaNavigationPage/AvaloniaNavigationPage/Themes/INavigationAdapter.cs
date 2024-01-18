using System;

namespace AvaloniaNavigationPage.Themes;

public interface INavigationAdapter
{
    Action<int> ChangedSelectedIndex { get; set; }
}