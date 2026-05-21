using System;
using System.Windows.Controls;
using PatientInformationSystem.AddPatient;

namespace PatientInformationSystem.AddPatient
{
    /// <summary>
    /// Interaction logic for AddPatientView.xaml
    /// </summary>
    public partial class AddPatientView : UserControl
    {
        public AddPatientView()
        {
            InitializeComponent();
            DataContext = new AddPatientViewModel();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Set max date to today when the control is loaded
            if (sender is UserControl uc)
            {
                if (uc.FindName("DOBPicker") is DatePicker dobPicker)
                {
                    dobPicker.DisplayDateEnd = DateTime.Today;
                }
            }
        }
    }
}
