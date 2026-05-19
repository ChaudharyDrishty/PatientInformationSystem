using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PatientInformationSystem.SearchBar
{
    /// <summary>
    /// Interaction logic for SearchBarView.xaml
    /// </summary>
    public partial class SearchBarView : UserControl
    {
        public SearchBarView()
        {
            InitializeComponent();
        }
        // Searchbox Placeholder Logic
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "Search patients...")
            {
                SearchBox.Text = "";
                SearchBox.Foreground = Brushes.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = "Search patients...";
                SearchBox.Foreground = Brushes.Gray;
            }
        }
    }
}
