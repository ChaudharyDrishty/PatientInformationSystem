using PatientInformationSystem.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PatientInformationSystem.AddPatient
{
    public class AddPatientViewModel : INotifyPropertyChanged
    {
        private string _patientName = string.Empty;
        private DateTime? _dob;
        private string _email = string.Empty;
        private string _phoneNo = string.Empty;
        private string _address = string.Empty;
        private string _diagnosis = string.Empty;
        private string _surgeryStatus = "Not Required";
        private string _consultant = string.Empty;
        private string _errorMessage = string.Empty;
        private string _successMessage = string.Empty;
        private Action? _cancelCallback;
        private Action<Patient>? _addPatientCallback;

        public string PatientName
        {
            get { return _patientName; }
            set { SetProperty(ref _patientName, value); }
        }

        public DateTime? DOB
        {
            get { return _dob; }
            set { SetProperty(ref _dob, value); }
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public string PhoneNo
        {
            get { return _phoneNo; }
            set { SetProperty(ref _phoneNo, value); }
        }

        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        public string Diagnosis
        {
            get { return _diagnosis; }
            set { SetProperty(ref _diagnosis, value); }
        }

        public string SurgeryStatus
        {
            get { return _surgeryStatus; }
            set { SetProperty(ref _surgeryStatus, value); }
        }

        public string Consultant
        {
            get { return _consultant; }
            set { SetProperty(ref _consultant, value); }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public string SuccessMessage
        {
            get { return _successMessage; }
            set { SetProperty(ref _successMessage, value); }
        }

        public ObservableCollection<string> SurgeryStatusOptions { get; }
        public ObservableCollection<string> ConsultantList { get; }

        public ICommand AddPatientCommand { get; }
        public ICommand CancelCommand { get; }

        public AddPatientViewModel()
        {
            SurgeryStatusOptions = new ObservableCollection<string>
            {
                "Not Required",
                "Scheduled",
                "Completed",
                "Cancelled"
            };

            ConsultantList = new ObservableCollection<string>
            {
                "Dr. Smith",
                "Dr. Johnson",
                "Dr. Williams",
                "Dr. Brown",
                "Dr. Davis"
            };

            AddPatientCommand = new RelayCommand(AddPatient);
            CancelCommand = new RelayCommand(Cancel);
        }

        public void SetCallbacks(Action<Patient> onAddPatient, Action onCancel)
        {
            _addPatientCallback = onAddPatient;
            _cancelCallback = onCancel;
        }

        private void AddPatient()
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;

            // Validation
            if (string.IsNullOrWhiteSpace(PatientName))
            {
                ErrorMessage = "Patient Name is required.";
                return;
            }

            if (PatientName.Length > 20)
            {
                ErrorMessage = "Patient Name should not be more than 20 characters.";
                return;
            }

            if (!DOB.HasValue)
            {
                ErrorMessage = "Date of Birth is required.";
                return;
            }

            if (DOB.Value.Date > DateTime.Today)
            {
                ErrorMessage = "Date of Birth cannot be greater than today's date.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Email is required.";
                return;
            }

            if (!IsValidEmail(Email))
            {
                ErrorMessage = "Email format should be correct(@ symbol and a valid domain).";
                return;
            }

            if (string.IsNullOrWhiteSpace(PhoneNo))
            {
                ErrorMessage = "Phone Number is required.";
                return;
            }

            if (!IsValidPhoneNumber(PhoneNo))
            {
                ErrorMessage = "Valid phone number is required.";
                return;
            }

            if (!string.IsNullOrWhiteSpace(Diagnosis) && Diagnosis.Length > 500)
            {
                ErrorMessage = "Diagnosis should not exceed 500 characters.";
                return;
            }

            // Create patient object
            var newPatient = new Patient
            {
                Name = PatientName,
                DOB = DOB.Value,
                Email = Email,
                PhoneNo = PhoneNo,
                Address = Address ?? string.Empty,
                Diagnosis = Diagnosis ?? string.Empty,
                SurgeryStatus = SurgeryStatus ?? "Not Required",
                Consultant = Consultant ?? string.Empty
            };

            // Call the callback to add patient to main list
            _addPatientCallback?.Invoke(newPatient);

            // Show success message briefly before closing
            SuccessMessage = $"Patient '{PatientName}' added successfully!";

            // Clear the form
            ClearForm();

            // Close the window after a short delay to show the success message
            System.Threading.Tasks.Task.Delay(1000).ContinueWith(_ =>
            {
                _cancelCallback?.Invoke();
            });
        }

        private void Cancel()
        {
            ClearForm();
            _cancelCallback?.Invoke();
        }

        private void ClearForm()
        {
            PatientName = string.Empty;
            DOB = null;
            Email = string.Empty;
            PhoneNo = string.Empty;
            Address = string.Empty;
            Diagnosis = string.Empty;
            SurgeryStatus = "Not Required";
            Consultant = string.Empty;
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNo)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNo, @"^\d{10}$|^\d{3}-\d{3}-\d{4}$|^\+\d{1,3}\d{9,10}$");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute?.Invoke();
    }
}
