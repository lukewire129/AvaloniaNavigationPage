using System.Collections.Generic;
using AvaloniaNavigationView;
using AvaloniaNavigationView.ViewModel;

namespace DemoApp.ViewModels;

public class MainViewModel : ViewModelBase,INavigationViewModel
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static
    public MainViewModel()
    {
        NaviTapVM = new List<NavigationViewModel>()
        {
            new AContentViewModel(),
            new BContentViewModel(),
            new CContentViewModel(),
            new DContentViewModel(),
            new EContentViewModel(),
        };
    }

    public List<NavigationViewModel> NaviTapVM { get; set; }
}