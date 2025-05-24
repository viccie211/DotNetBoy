using Avalonia.Controls;
using DotNetBoy.AvaloniaFrontend.ViewModels;

namespace DotNetBoy.AvaloniaFrontend.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        var dataContext = new MainViewModel();
        DataContext = dataContext;
       
    }
}