
using CommunityToolkit.Mvvm.ComponentModel;

namespace LoaTool.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "Test";
}

