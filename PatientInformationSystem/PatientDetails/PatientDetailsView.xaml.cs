using System.Collections.ObjectModel;
using System.Windows.Controls;
using PatientInformationSystem.Models;

namespace PatientInformationSystem.PatientDetails
{
    public partial class PatientDetailsView : UserControl
    {
        public PatientDetailsView()
        {
            InitializeComponent();
            DataContext = this;
            LoadSampleData();
        }

        public ObservableCollection<Patient> Patients { get; } = new ObservableCollection<Patient>();

        private void LoadSampleData()
        {
            Patients.Clear();

            Patients.Add(new Patient { Name = "John Doe", DOB = new DateTime(1985, 5, 12), Email = "john.doe@email.com", PhoneNo = "555-0192", Address = "123 Health Ave, NY", Diagnosis = "Acute Appendicitis", SurgeryStatus = "Scheduled", Consultant = "Dr. Alice Smith" });
            Patients.Add(new Patient { Name = "Jane Smith", DOB = new DateTime(1992, 10, 24), Email = "jane.smith@email.com", PhoneNo = "555-0143", Address = "456 Care Blvd, CA", Diagnosis = "Cholecystitis", SurgeryStatus = "Completed", Consultant = "Dr. Robert Lee" });
            Patients.Add(new Patient { Name = "Robert Johnson", DOB = new DateTime(1970, 1, 15), Email = "robert.j@email.com", PhoneNo = "555-0177", Address = "789 Clinic Rd, TX", Diagnosis = "Inguinal Hernia", SurgeryStatus = "None", Consultant = "Dr. Sarah Jenkins" });
        }
    }
}

