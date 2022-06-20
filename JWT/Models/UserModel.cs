using System.Collections.Generic;

namespace JWT.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public string Surtname { get; set; }
        public string GivenName { get; set; }
    }

    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel(){UserName = "hebertpena88", Role = "Admin", EmailAddress = "hebertpena88@gmail.com", Password = "123456", GivenName ="hebert", Surtname="Pena" },
            new UserModel(){UserName = "hebertpena1988", Role = "Seller", EmailAddress = "hebertpena1988@gmail.com", Password = "123456", GivenName ="hebert2", Surtname="Pena2" }
        };

    }
}
