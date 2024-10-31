using AvaloniaNavigationView.ViewModel;

namespace AvaloniaNavigationView;

public interface INavigationViewModel
{
    public List<NavigationViewModel> NaviTapVM { get; set; }
}