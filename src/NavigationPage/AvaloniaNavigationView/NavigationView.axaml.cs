using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using AvaloniaNavigationBar.Interface;
using AvaloniaNavigationView.ViewModel;

namespace AvaloniaNavigationView;

public class NavigationView : TemplatedControl
{
    public static readonly DirectProperty<ListBox, ListBox> NavigtorContentProperty =
        AvaloniaProperty.RegisterDirect<ListBox, ListBox>(
            nameof(NavigtorContent),
            o => o,
            (o, v) => o = v);

    private ListBox _navigtorContent;
    
    private readonly IList<UserControl> _pagesitems = new List<UserControl>();
    public IList<UserControl> PageItems =>_pagesitems;
    public ListBox NavigtorContent
    {
        get => _navigtorContent;
        set => SetAndRaise(NavigtorContentProperty, ref _navigtorContent, value);
    }
    
    private int currentIndx = 0;
    
    private void ChangeContent(int index)
    {
        var naviVVM = (INavigationViewModel)this.DataContext;
        var naviTap = (NavigationViewModel)naviVVM.NaviTapVM[index];
        
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

    private ContentControl PART_Content;
    private ContentControl PART_Navigator;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        
        PART_Content = e.NameScope.Get<ContentControl>("PART_Content");
        PART_Navigator = e.NameScope.Get<ContentControl>("PART_Navigator");
        
        this.PART_Navigator.Content = NavigtorContent;

        ((INavigationAdapter)NavigtorContent).ChangedSelectedIndex += (i =>
        {
            if (i < 0) return;
            if (PART_Content == null) return;

            ChangeContent(i);
        });
    }
}