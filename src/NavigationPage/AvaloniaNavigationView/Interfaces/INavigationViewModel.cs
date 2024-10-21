using AvaloniaNavigationView.ViewModels;

namespace AvaloniaNavigationBar;

public interface INavigationViewModel
{
    public List<NavigationViewModel> NaviTapVM { get; set; }
}