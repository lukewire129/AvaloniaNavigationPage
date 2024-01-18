using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;

namespace AvaloniaNavigationPage.Themes;

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
                if (currentIndx < i)
                {
                    
                }
                else
                {
                    
                }
                this.PART_Content.Content = PageItems[i];
                currentIndx = i;
            });
        }
    }
}