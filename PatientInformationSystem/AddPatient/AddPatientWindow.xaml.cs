using PatientInformationSystem.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace PatientInformationSystem.AddPatient
{
    public partial class AddPatientWindow : Window
    {
        public AddPatientWindow(ObservableCollection<Patient> patients)
        {
            InitializeComponent();
            DataContext = new AddPatientViewModel(patients, Close);
        }
    }
}





// using PatientInformationSystem.Models;
// using System.Collections.ObjectModel;
// using System.Windows;

// namespace PatientInformationSystem.AddPatient
// {
//     public partial class AddPatientWindow : Window
//     {
//         public AddPatientWindow(ObservableCollection<Patient> patients)
//         {
//             InitializeComponent();

//             // Pass shared collection + close action
//             DataContext = new AddPatientViewModel(patients, Close);
//         }
//     }
// }






















// using System.Windows;
// using PatientInformationSystem.Models;

// namespace PatientInformationSystem.AddPatient
// {
//     /// <summary>
//     /// Interaction logic for AddPatientWindow.xaml
//     /// </summary>
//     public partial class AddPatientWindow : Window
//     {
//         private readonly AddPatientViewModel _viewModel;

//         public AddPatientWindow()
//         {
//             InitializeComponent();
//             _viewModel = new AddPatientViewModel();
//             DataContext = _viewModel;
//         }

//         public void SetCallbacks(System.Action<Patient> onAddPatient, System.Action onCancel)
//         {
//             _viewModel.SetCallbacks(onAddPatient, () => 
//             {
//                 onCancel?.Invoke();
//                 this.Close();
//             });
//         }
//     }
// }
