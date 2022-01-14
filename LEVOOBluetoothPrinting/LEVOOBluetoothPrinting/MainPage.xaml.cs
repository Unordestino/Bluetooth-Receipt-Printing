using Xamarin.Forms;

namespace LEVOOBluetoothPrinting
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new PrintPageViewModel();
        }
    }
}
