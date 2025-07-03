namespace KafanaTask.DTOs
{

    public class RegisterCustomerDto
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IFormFile Photo { get; set; }
        public string Password { get; set; }

        public string GenderInput { get; set; }

        public DateTime? DateOfBirth { get; set; }



    }
}
