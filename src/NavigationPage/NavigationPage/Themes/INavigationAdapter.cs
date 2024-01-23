using System;

namespace NavigationPage.Themes;

public interface INavigationAdapter
{
    Action<int> ChangedSelectedIndex { get; set; }
}