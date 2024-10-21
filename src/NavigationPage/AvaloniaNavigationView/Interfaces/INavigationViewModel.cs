using AvaloniaNavigationView.ViewModels;

namespace AvaloniaNavigationBar;

public interface INavigationViewModel
{
    public List<ViewModelBase> NaviTapVM { get; set; }
}