using System.Windows;
using PatientInformationSystem.Models;

namespace PatientInformationSystem.AddPatient
{
    /// <summary>
    /// Interaction logic for AddPatientWindow.xaml
    /// </summary>
    public partial class AddPatientWindow : Window
    {
        private readonly AddPatientViewModel _viewModel;

        public AddPatientWindow()
        {
            InitializeComponent();
            _viewModel = new AddPatientViewModel();
            DataContext = _viewModel;
        }

        public void SetCallbacks(System.Action<Patient> onAddPatient, System.Action onCancel)
        {
            _viewModel.SetCallbacks(onAddPatient, () => 
            {
                onCancel?.Invoke();
                this.Close();
            });
        }
    }
}
