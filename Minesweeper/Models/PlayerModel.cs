using System.ComponentModel.DataAnnotations;

namespace Minesweeper.Models
{
    public class PlayerModel
    {
        //Unique identifier for database primary key
        public int Id { get; set; }
        //First Name, Last Name, Sex, Age, State, Email Address, Username, and Password as form fields
        [Required]
        [StringLength(80, MinimumLength = 1)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(80, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 1)]
        public string Sex { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string State { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255, MinimumLength = 1)]
        public string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public PlayerModel()
        {
        }

        public PlayerModel(int id, string firstName, string lastName, string sex, int age, string state, string email, string username, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Age = age;
            State = state;
            Email = email;
            Username = username;
            Password = password;
        }

    }
}
