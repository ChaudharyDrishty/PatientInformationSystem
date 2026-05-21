using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PatientInformationSystem.AddPatient;
using PatientInformationSystem.Models;

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

        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the main window
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                var addPatientWindow = new AddPatientWindow();

                // Set callbacks to add patient to the main list
                addPatientWindow.SetCallbacks(
                    onAddPatient: (patient) =>
                    {
                        mainWindow.AddPatientToList(patient);
                    },
                    onCancel: () =>
                    {
                        // Window will close automatically
                    }
                );

                addPatientWindow.Owner = mainWindow;
                addPatientWindow.ShowDialog();
            }
        }
    }
}
