using System;
namespace ReCircle.Model.Adapter
{
    public class Client
    {
        public int UserId { get; set; }
        public int Role { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string Calification { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }
        public bool IsActive { get; set; }
    }
}
