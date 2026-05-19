using PatientInformationSystem.Models;
using System.Collections.ObjectModel;
using System.Windows;

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
            
        }

        

        
    }
}