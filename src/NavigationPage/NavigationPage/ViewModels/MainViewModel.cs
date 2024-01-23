using System.Collections.Generic;

namespace NavigationPage.ViewModels;

public class MainViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    public List<NavigationViewModelBase> ContentVM { get; set; }

    public MainViewModel()
    {
        ContentVM = new List<NavigationViewModelBase>()
        {
            new AContentViewModel(),
            new BContentViewModel(),
            new CContentViewModel(),
            new DContentViewModel(),
            new EContentViewModel(),
        };
    }
}