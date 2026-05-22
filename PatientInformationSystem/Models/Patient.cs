using System;

namespace PatientInformationSystem.Models
{
    public class Patient
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DOB { get; set; }

        public string Gender { get; set; } = "I Prefer Not To Say";

        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Diagnosis { get; set; } = string.Empty;

        public string SurgeryStatus { get; set; } = "Not Required";
        public string Consultant { get; set; } = string.Empty;
    }
}













// namespace PatientInformationSystem.Models
// {
//     public class Patient
//     {
//         public string Name { get; set; }
//         public DateTime DOB { get; set; }
//         public string Email { get; set; }
//         public string PhoneNo { get; set; }
//         public string Address { get; set; }
//         public string Diagnosis { get; set; }
//         public string SurgeryStatus { get; set; }
//         public string Consultant { get; set; }
//     }
// }
