using PatientInformationSystem.Infrastructure;
using PatientInformationSystem.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace PatientInformationSystem.AddPatient
{
    public class AddPatientViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<Patient> _patients;
        private readonly Action _closeWindow;

        // Form Fields
        private string _patientName = string.Empty;
        private DateTime? _dob;
        private string _gender = "I Prefer Not To Say";
        private string _email = string.Empty;
        private string _phoneNo = string.Empty;
        private string _address = string.Empty;
        private string _diagnosis = string.Empty;
        private string _surgeryStatus = "Not Required";
        private string _consultant = string.Empty;

        // Per-field error messages (shown under each field)
        private string _patientNameError = string.Empty;
        private string _dobError = string.Empty;
        private string _genderError = string.Empty;
        private string _emailError = string.Empty;
        private string _phoneNoError = string.Empty;
        private string _addressError = string.Empty;
        private string _diagnosisError = string.Empty;
        private string _surgeryStatusError = string.Empty;
        private string _consultantError = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<string> GenderOptions { get; }
        public ObservableCollection<string> SurgeryStatusOptions { get; }
        public ObservableCollection<string> ConsultantList { get; }

        public ICommand AddPatientCommand { get; }
        public ICommand CancelCommand { get; }

        public AddPatientViewModel(ObservableCollection<Patient> patients, Action closeWindow)
        {
            _patients = patients ?? throw new ArgumentNullException(nameof(patients));
            _closeWindow = closeWindow ?? throw new ArgumentNullException(nameof(closeWindow));

            GenderOptions = new ObservableCollection<string>
            {
                "Male",
                "Female",
                "I Prefer Not To Say"
            };

            // ✅ You said "4 options" but listed 5; I included ALL listed values.
            SurgeryStatusOptions = new ObservableCollection<string>
            {
                "Not Required",
                "Scheduled",
                "In Progress",
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

        // ---------------------------
        // Bindable Properties (Fields)
        // ---------------------------
        public string PatientName
        {
            get => _patientName;
            set
            {
                SetProperty(ref _patientName, value);
                ValidatePatientName();
            }
        }

        public DateTime? DOB
        {
            get => _dob;
            set
            {
                SetProperty(ref _dob, value);
                ValidateDOB();
            }
        }

        public string Gender
        {
            get => _gender;
            set
            {
                SetProperty(ref _gender, value);
                ValidateGender();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
                ValidateEmail();
            }
        }

        public string PhoneNo
        {
            get => _phoneNo;
            set
            {
                SetProperty(ref _phoneNo, value);
                ValidatePhone();
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                SetProperty(ref _address, value);
                ValidateAddress();
            }
        }

        public string Diagnosis
        {
            get => _diagnosis;
            set
            {
                SetProperty(ref _diagnosis, value);
                ValidateDiagnosis();
            }
        }

        public string SurgeryStatus
        {
            get => _surgeryStatus;
            set
            {
                SetProperty(ref _surgeryStatus, value);
                ValidateSurgeryStatus();
            }
        }

        public string Consultant
        {
            get => _consultant;
            set
            {
                SetProperty(ref _consultant, value);
                ValidateConsultant();
            }
        }

        // -------------------------------------
        // Bindable Properties (Field Error Text)
        // -------------------------------------
        public string PatientNameError { get => _patientNameError; private set => SetProperty(ref _patientNameError, value); }
        public string DOBError { get => _dobError; private set => SetProperty(ref _dobError, value); }
        public string GenderError { get => _genderError; private set => SetProperty(ref _genderError, value); }
        public string EmailError { get => _emailError; private set => SetProperty(ref _emailError, value); }
        public string PhoneNoError { get => _phoneNoError; private set => SetProperty(ref _phoneNoError, value); }
        public string AddressError { get => _addressError; private set => SetProperty(ref _addressError, value); }
        public string DiagnosisError { get => _diagnosisError; private set => SetProperty(ref _diagnosisError, value); }
        public string SurgeryStatusError { get => _surgeryStatusError; private set => SetProperty(ref _surgeryStatusError, value); }
        public string ConsultantError { get => _consultantError; private set => SetProperty(ref _consultantError, value); }

        // ------------
        // Button Logic
        // ------------
        private void AddPatient()
        {
            // Validate everything on click
            ValidateAll();

            // If any error exists, do not add
            if (HasAnyError())
                return;

            var newPatient = new Patient
            {
                Name = PatientName.Trim(),
                DOB = DOB!.Value,
                Gender = Gender,
                Email = Email.Trim(),
                PhoneNo = PhoneNo.Trim(),
                Address = Address.Trim(),
                Diagnosis = Diagnosis.Trim(),
                SurgeryStatus = SurgeryStatus,
                Consultant = Consultant
            };

            _patients.Add(newPatient);

            // ✅ Close immediately so user returns to main window
            _closeWindow();
        }

        private void Cancel()
        {
            ClearForm();
            _closeWindow();
        }

        private void ClearForm()
        {
            PatientName = string.Empty;
            DOB = null;
            Gender = "I Prefer Not To Say";
            Email = string.Empty;
            PhoneNo = string.Empty;
            Address = string.Empty;
            Diagnosis = string.Empty;
            SurgeryStatus = "Not Required";
            Consultant = string.Empty;

            ClearErrors();
        }

        // ----------------
        // Validation Rules
        // ----------------
        private void ValidateAll()
        {
            ValidatePatientName();
            ValidateDOB();
            ValidateGender();
            ValidateEmail();
            ValidatePhone();
            ValidateAddress();
            ValidateDiagnosis();
            ValidateSurgeryStatus();
            ValidateConsultant();
        }

        private bool HasAnyError()
        {
            return !string.IsNullOrWhiteSpace(PatientNameError)
                || !string.IsNullOrWhiteSpace(DOBError)
                || !string.IsNullOrWhiteSpace(GenderError)
                || !string.IsNullOrWhiteSpace(EmailError)
                || !string.IsNullOrWhiteSpace(PhoneNoError)
                || !string.IsNullOrWhiteSpace(AddressError)
                || !string.IsNullOrWhiteSpace(DiagnosisError)
                || !string.IsNullOrWhiteSpace(SurgeryStatusError)
                || !string.IsNullOrWhiteSpace(ConsultantError);
        }

        private void ClearErrors()
        {
            PatientNameError = string.Empty;
            DOBError = string.Empty;
            GenderError = string.Empty;
            EmailError = string.Empty;
            PhoneNoError = string.Empty;
            AddressError = string.Empty;
            DiagnosisError = string.Empty;
            SurgeryStatusError = string.Empty;
            ConsultantError = string.Empty;
        }

        private void ValidatePatientName()
        {
            if (string.IsNullOrWhiteSpace(PatientName))
            {
                PatientNameError = "Patient Name is required.";
                return;
            }

            if (PatientName.Trim().Length > 20)
            {
                PatientNameError = "Patient Name cannot be more than 20 characters.";
                return;
            }

            PatientNameError = string.Empty;
        }

        private void ValidateDOB()
        {
            if (!DOB.HasValue)
            {
                DOBError = "Date of Birth is required.";
                return;
            }

            if (DOB.Value.Date > DateTime.Today)
            {
                DOBError = "Date of Birth cannot be greater than today's date.";
                return;
            }

            DOBError = string.Empty;
        }

        private void ValidateGender()
        {
            // Gender dropdown has default; no strict requirement stated.
            // If you want it required, uncomment below.
            // if (string.IsNullOrWhiteSpace(Gender)) { GenderError = "Please select Gender."; return; }

            GenderError = string.Empty;
        }

        private void ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email is required.";
                return;
            }

            // Must contain @ and a valid domain.
            // Simple robust validation: local@domain.tld (tld min 2 chars)
            var email = Email.Trim();
            bool ok = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$");

            EmailError = ok ? string.Empty : "Email must contain '@' and a valid domain (example: name@domain.com).";
        }

        private void ValidatePhone()
        {
            if (string.IsNullOrWhiteSpace(PhoneNo))
            {
                PhoneNoError = "Phone Number is required.";
                return;
            }

            // Accept: 10 digits OR +<countrycode><9-12 digits total after +>
            var phone = PhoneNo.Trim();
            bool ok = Regex.IsMatch(phone, @"^\d{10}$|^\+\d{1,3}\d{9,10}$");

            PhoneNoError = ok ? string.Empty : "Enter a valid phone number (10 digits or +<countrycode><number>).";
        }

        private void ValidateAddress()
        {
            if (string.IsNullOrWhiteSpace(Address))
            {
                AddressError = "Address is required.";
                return;
            }

            if (Address.Trim().Length > 200)
            {
                AddressError = "Address cannot be more than 200 characters.";
                return;
            }

            AddressError = string.Empty;
        }

        private void ValidateDiagnosis()
        {
            if (string.IsNullOrWhiteSpace(Diagnosis))
            {
                DiagnosisError = "Diagnosis is required.";
                return;
            }

            if (Diagnosis.Trim().Length > 500)
            {
                DiagnosisError = "Diagnosis cannot be more than 500 characters.";
                return;
            }

            DiagnosisError = string.Empty;
        }

        private void ValidateSurgeryStatus()
        {
            if (string.IsNullOrWhiteSpace(SurgeryStatus))
            {
                SurgeryStatusError = "Please select Surgery Status.";
                return;
            }

            SurgeryStatusError = string.Empty;
        }

        private void ValidateConsultant()
        {
            if (string.IsNullOrWhiteSpace(Consultant))
            {
                ConsultantError = "Consultant selection is required.";
                return;
            }

            ConsultantError = string.Empty;
        }

        // -------------------
        // Helper for INotifyPropertyChanged
        // -------------------
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
































// using PatientInformationSystem.Models;
// using System;
// using System.Collections.ObjectModel;
// using System.ComponentModel;
// using System.Runtime.CompilerServices;
// using System.Windows.Input;

// namespace PatientInformationSystem.AddPatient
// {
//     public class AddPatientViewModel : INotifyPropertyChanged
//     {
//         private string _patientName = string.Empty;
//         private DateTime? _dob;
//         private string _email = string.Empty;
//         private string _phoneNo = string.Empty;
//         private string _address = string.Empty;
//         private string _diagnosis = string.Empty;
//         private string _surgeryStatus = "Not Required";
//         private string _consultant = string.Empty;
//         private string _errorMessage = string.Empty;
//         private string _successMessage = string.Empty;
//         private Action? _cancelCallback;
//         private Action<Patient>? _addPatientCallback;

//         public string PatientName
//         {
//             get { return _patientName; }
//             set { SetProperty(ref _patientName, value); }
//         }

//         public DateTime? DOB
//         {
//             get { return _dob; }
//             set { SetProperty(ref _dob, value); }
//         }

//         public string Email
//         {
//             get { return _email; }
//             set { SetProperty(ref _email, value); }
//         }

//         public string PhoneNo
//         {
//             get { return _phoneNo; }
//             set { SetProperty(ref _phoneNo, value); }
//         }

//         public string Address
//         {
//             get { return _address; }
//             set { SetProperty(ref _address, value); }
//         }

//         public string Diagnosis
//         {
//             get { return _diagnosis; }
//             set { SetProperty(ref _diagnosis, value); }
//         }

//         public string SurgeryStatus
//         {
//             get { return _surgeryStatus; }
//             set { SetProperty(ref _surgeryStatus, value); }
//         }

//         public string Consultant
//         {
//             get { return _consultant; }
//             set { SetProperty(ref _consultant, value); }
//         }

//         public string ErrorMessage
//         {
//             get { return _errorMessage; }
//             set { SetProperty(ref _errorMessage, value); }
//         }

//         public string SuccessMessage
//         {
//             get { return _successMessage; }
//             set { SetProperty(ref _successMessage, value); }
//         }

//         public ObservableCollection<string> SurgeryStatusOptions { get; }
//         public ObservableCollection<string> ConsultantList { get; }

//         public ICommand AddPatientCommand { get; }
//         public ICommand CancelCommand { get; }

//         public AddPatientViewModel()
//         {
//             SurgeryStatusOptions = new ObservableCollection<string>
//             {
//                 "Not Required",
//                 "Scheduled",
//                 "Completed",
//                 "Cancelled"
//             };

//             ConsultantList = new ObservableCollection<string>
//             {
//                 "Dr. Smith",
//                 "Dr. Johnson",
//                 "Dr. Williams",
//                 "Dr. Brown",
//                 "Dr. Davis"
//             };

//             AddPatientCommand = new RelayCommand(AddPatient);
//             CancelCommand = new RelayCommand(Cancel);
//         }

//         public void SetCallbacks(Action<Patient> onAddPatient, Action onCancel)
//         {
//             _addPatientCallback = onAddPatient;
//             _cancelCallback = onCancel;
//         }

//         private void AddPatient()
//         {
//             ErrorMessage = string.Empty;
//             SuccessMessage = string.Empty;

//             // Validation
//             if (string.IsNullOrWhiteSpace(PatientName))
//             {
//                 ErrorMessage = "Patient Name is required.";
//                 return;
//             }

//             if (PatientName.Length > 20)
//             {
//                 ErrorMessage = "Patient Name should not be more than 20 characters.";
//                 return;
//             }

//             if (!DOB.HasValue)
//             {
//                 ErrorMessage = "Date of Birth is required.";
//                 return;
//             }

//             if (DOB.Value.Date > DateTime.Today)
//             {
//                 ErrorMessage = "Date of Birth cannot be greater than today's date.";
//                 return;
//             }

//             if (string.IsNullOrWhiteSpace(Email))
//             {
//                 ErrorMessage = "Email is required.";
//                 return;
//             }

//             if (!IsValidEmail(Email))
//             {
//                 ErrorMessage = "Email format should be correct(@ symbol and a valid domain).";
//                 return;
//             }

//             if (string.IsNullOrWhiteSpace(PhoneNo))
//             {
//                 ErrorMessage = "Phone Number is required.";
//                 return;
//             }

//             if (!IsValidPhoneNumber(PhoneNo))
//             {
//                 ErrorMessage = "Valid phone number is required.";
//                 return;
//             }

//             if (!string.IsNullOrWhiteSpace(Diagnosis) && Diagnosis.Length > 500)
//             {
//                 ErrorMessage = "Diagnosis should not exceed 500 characters.";
//                 return;
//             }

//             // Create patient object
//             var newPatient = new Patient
//             {
//                 Name = PatientName,
//                 DOB = DOB.Value,
//                 Email = Email,
//                 PhoneNo = PhoneNo,
//                 Address = Address ?? string.Empty,
//                 Diagnosis = Diagnosis ?? string.Empty,
//                 SurgeryStatus = SurgeryStatus ?? "Not Required",
//                 Consultant = Consultant ?? string.Empty
//             };

//             // Call the callback to add patient to main list
//             _addPatientCallback?.Invoke(newPatient);

//             // Show success message briefly before closing
//             SuccessMessage = $"Patient '{PatientName}' added successfully!";

//             // Clear the form
//             ClearForm();

//             // Close the window after a short delay to show the success message
//             System.Threading.Tasks.Task.Delay(1000).ContinueWith(_ =>
//             {
//                 _cancelCallback?.Invoke();
//             });
//         }

//         private void Cancel()
//         {
//             ClearForm();
//             _cancelCallback?.Invoke();
//         }

//         private void ClearForm()
//         {
//             PatientName = string.Empty;
//             DOB = null;
//             Email = string.Empty;
//             PhoneNo = string.Empty;
//             Address = string.Empty;
//             Diagnosis = string.Empty;
//             SurgeryStatus = "Not Required";
//             Consultant = string.Empty;
//             ErrorMessage = string.Empty;
//             SuccessMessage = string.Empty;
//         }

//         private bool IsValidEmail(string email)
//         {
//             try
//             {
//                 var addr = new System.Net.Mail.MailAddress(email);
//                 return addr.Address == email;
//             }
//             catch
//             {
//                 return false;
//             }
//         }

//         private bool IsValidPhoneNumber(string phoneNo)
//         {
//             return System.Text.RegularExpressions.Regex.IsMatch(phoneNo, @"^\d{10}$|^\d{3}-\d{3}-\d{4}$|^\+\d{1,3}\d{9,10}$");
//         }

//         public event PropertyChangedEventHandler PropertyChanged;

//         protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
//         {
//             if (!Equals(field, value))
//             {
//                 field = value;
//                 PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//             }
//         }
//     }

//     public class RelayCommand : ICommand
//     {
//         private readonly Action _execute;
//         private readonly Func<bool> _canExecute;

//         public RelayCommand(Action execute, Func<bool> canExecute = null)
//         {
//             _execute = execute;
//             _canExecute = canExecute;
//         }

//         public event EventHandler CanExecuteChanged
//         {
//             add { CommandManager.RequerySuggested += value; }
//             remove { CommandManager.RequerySuggested -= value; }
//         }

//         public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

//         public void Execute(object parameter) => _execute?.Invoke();
//     }
// }
