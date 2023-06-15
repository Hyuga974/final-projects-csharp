using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Weather.Views;

public partial class SearchCity : UserControl
{
    public SearchCity()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}