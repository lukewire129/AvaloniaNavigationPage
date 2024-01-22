using System;
using System.Collections.Generic;
using System.Threading;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;

namespace NavigationPage.Themes;

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
               
                var nextItem = ((MagicBar)this.PART_Navigator.Content).Items[i];
                var ctrl = new ViewLocator().Build(nextItem);
                if (this.PART_Content.Content == null)
                {
                    this.PART_Content.Content = ctrl;
                    currentIndx = i;
                    return;
                }

                var pageSlide = new PageSlide(new TimeSpan(0, 0, 0, 0, 300));
                bool isForwad = currentIndx > i? false : true;
                
                pageSlide.Start((Visual)this.PART_Content.Content, ctrl,isForwad, new CancellationToken());
                this.PART_Content.Content = ctrl;
                currentIndx = i;
            });
        }
    }
}