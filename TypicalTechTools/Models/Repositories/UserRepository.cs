
using TypicalTechTools.Models.Data;
using TypicalTechTools.Models;
using TypicalTechTools.Models.DTOs;

namespace TypicalTechTools.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        // Database context for accessing data
        private readonly TechToolsDBContext _context;

        // Constructor to initialize the repository with a database context
        public UserRepository(TechToolsDBContext context)
        {
            _context = context;
        }

        // Method to authenticate a user based on login credentials
        public AdminUser Authenticate(LoginDTO loginDTO)
        {
            // Find a user with the provided username
            var userDetails = _context.AdminUsers.Where(u => u.UserName.Equals(loginDTO.UserName)).FirstOrDefault();

            // If no user is found, return null
            if (userDetails == null)
            {
                return null;
            }

            // Verify the provided password against the stored hash
            if (BCrypt.Net.BCrypt.EnhancedVerify(loginDTO.Password, userDetails.Password)) 
            {
                // If password is correct, return the user details
                return userDetails;
            }

            // If password is incorrect, return null
            return null;
        }

        // Method to create a new user
        public AdminUser CreateUser(CreateUserDTO userDTO)
        {
            // Check if a user with the same username already exists
            var userDetails = _context.AdminUsers.Where(u => u.UserName.Equals(userDTO.UserName)).FirstOrDefault();

            // If a user with the same username exists, return null
            if (userDetails != null)
            {
                return null;
            }

            // Otherwise create a new Admin User with the provided details
            var user = new AdminUser
            {
                Email = userDTO.Email,
                UserName = userDTO.UserName,
                // Hash the password for security
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userDTO.Password)
            };

            // Add the new user to the database context and save changes
            _context.AdminUsers.Add(user);
            _context.SaveChanges();

            // Return the newly created user
            return user;
        }
    }
}
