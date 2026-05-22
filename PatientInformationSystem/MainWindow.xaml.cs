using PatientInformationSystem.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace PatientInformationSystem
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Patient> Patients { get; } = new ObservableCollection<Patient>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Pass Patients list to SearchBar for Add Patient window
            SearchBarControl.Patients = Patients;

            // Optional: Seed data
            Patients.Add(new Patient
            {
                Name = "Sample Patient",
                DOB = new System.DateTime(1995, 5, 10),
                Gender = "I Prefer Not To Say",
                Email = "sample@test.com",
                PhoneNo = "1234567890",
                Address = "Sample Address",
                Diagnosis = "Sample Diagnosis",
                SurgeryStatus = "Not Required",
                Consultant = "Dr. Smith"
            });
        }
    }
}









// using PatientInformationSystem.Models;
// using System.Collections.ObjectModel;
// using System.Windows;

// namespace PatientInformationSystem
// {
//     /// <summary>
//     /// Interaction logic for MainWindow.xaml
//     /// </summary>
//     public partial class MainWindow : Window
//     {
//         // Property the DataGrid binds to
//         public ObservableCollection<Patient> Patients { get; set; }

//         public MainWindow()
//         {
//             InitializeComponent();
//             Patients = new ObservableCollection<Patient>();
//         }

//         public void AddPatientToList(Patient patient)
//         {
//             if (patient != null)
//             {
//                 Patients.Add(patient);
//             }
//         }
//     }
// }