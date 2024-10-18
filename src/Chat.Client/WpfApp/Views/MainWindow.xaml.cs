using System.Windows;
using System.Windows.Controls;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsLbScrollable { get; set; } = false;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void ScrollToBottom(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            if (lvMessages.Items.Count > 0 && !IsLbScrollable)
            {
                lvMessages.ScrollIntoView(lvMessages.Items[^1]);
                IsLbScrollable = false;
            }
        }

        private void ChangeScrollableTrue(object sender, System.Windows.Input.MouseEventArgs e)
        {
            IsLbScrollable = true;
        }

        private void ChangeScrollableFalse(object sender, System.Windows.Input.MouseEventArgs e)
        {
            IsLbScrollable = false;
        }
    }
}
