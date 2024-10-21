using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using AvaloniaNavigationBar;
using AvaloniaNavigationBar.Interface;
using AvaloniaNavigationView.ViewModels;

namespace AvaloniaNavigationView;

public partial class NavigationView : UserControl
{
    public static readonly DirectProperty<ListBox, ListBox> NavigtorContentProperty =
        AvaloniaProperty.RegisterDirect<ListBox, ListBox>(
            nameof(NavigtorContent),
            o => o,
            (o, v) => o = v);

    private ListBox _navigtorContent;

    public ListBox NavigtorContent
    {
        get => _navigtorContent;
        set => SetAndRaise(NavigtorContentProperty, ref _navigtorContent, value);
    }

    private readonly IList<UserControl> _pagesitems = new List<UserControl>();
    public IList<UserControl> PageItems =>_pagesitems;
    
    public NavigationView()
    {
        InitializeComponent();
    }

    private int currentIndx = 0;
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        
        if (change.Property.Name == nameof(NavigtorContent))
        {
            this.PART_Navigator.Content = NavigtorContent;

            ((INavigationAdapter)NavigtorContent).ChangedSelectedIndex += (i =>
            {
                if (i < 0) return;
                if (PART_Content == null) return;

                ChangeContent(i);
            });
        }
    }
    
    private void ChangeContent(int index)
    {
        var naviVVM = (INavigationViewModel)this.DataContext;
        var naviTap = (ViewModelBase)naviVVM.NaviTapVM[index];
        
        PageItems[index].DataContext = naviTap;
        if (this.PART_Content.Content == null)
        {
            this.PART_Content.Content = PageItems[index];
            currentIndx = index;
            return;
        }

        var pageSlide = new PageSlide(new TimeSpan(0, 0, 0, 0, 300));
        bool isForwad = currentIndx > index? false : true;
                
        pageSlide.Start((Visual)this.PART_Content.Content, PageItems[index],isForwad, new CancellationToken());
        this.PART_Content.Content = PageItems[index];
        currentIndx = index;
    }
    
    public Action<Control?> VMChange { get; set; }
}