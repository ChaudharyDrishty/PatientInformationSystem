namespace PatientInformationSystem.Models
{
    public class Patient
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Diagnosis { get; set; }
        public string SurgeryStatus { get; set; }
        public string Consultant { get; set; }
    }
}
