using System;
namespace RAS.Bootcamp.Mvc.Net.Models
{
    public class RegisterRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Tipe { get; set; } = null!;
        public string Alamat { get; set; }
    }
}

