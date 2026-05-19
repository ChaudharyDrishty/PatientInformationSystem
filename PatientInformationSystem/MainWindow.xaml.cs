using PatientInformationSystem.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PatientInformationSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Property the DataGrid binds to
        public ObservableCollection<Patient> Patients { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadSampleData();
            DataContext = this;
        }

        private void LoadSampleData()
        {
            Patients = new ObservableCollection<Patient>
            {
                new Patient { Name = "John Doe", DOB = new DateTime(1985, 5, 12), Email = "john.doe@email.com", PhoneNo = "555-0192", Address = "123 Health Ave, NY", Diagnosis = "Acute Appendicitis", SurgeryStatus = "Scheduled", Consultant = "Dr. Alice Smith" },
                new Patient { Name = "Jane Smith", DOB = new DateTime(1992, 10, 24), Email = "jane.smith@email.com", PhoneNo = "555-0143", Address = "456 Care Blvd, CA", Diagnosis = "Cholecystitis", SurgeryStatus = "Completed", Consultant = "Dr. Robert Lee" },
                new Patient { Name = "Robert Johnson", DOB = new DateTime(1970, 1, 15), Email = "robert.j@email.com", PhoneNo = "555-0177", Address = "789 Clinic Rd, TX", Diagnosis = "Inguinal Hernia", SurgeryStatus = "None", Consultant = "Dr. Sarah Jenkins" }
            };
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