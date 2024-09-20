using TypicalTechTools.Models.DTOs;
using TypicalTechTools.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace TypicalTechTools.Controllers
{
    public class UserController : Controller
    {
        // Repository interface for user operations
        private readonly IUserRepository _repository;

        // Constructor to inject the user repository
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET: /User/Login
        // Displays the login view
        public IActionResult Login()
        {
            return View();
        }

        // POST: /User/Login
        // Handles the form submission for user login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginDTO loginDTO)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                // Return the view with validation errors
                return View(loginDTO);
            }

            // Authenticate the user using the repository
            var user = _repository.Authenticate(loginDTO);
            if(user == null)
            {
                // If authentication fails, display an error message
                ViewBag.LoginMessage = "Username or Password is Invalid";
                // Return the view with the error message and user input prefilled
                return View(loginDTO);
            }

            // If authentication succeeds, set the session to indicate the user is authenticated
            HttpContext.Session.SetString("Authenticated", "true");
            // Redirect to Product/Index
            return RedirectToAction("Index", "Product");
        }

        // GET: /User/Logout
        // Handles user logout
        public ActionResult Logout()
        {
            // Set the session to indicate the user is not authenticated
            HttpContext.Session.SetString("Authenticated", "false");
            // Redirect to Product/Index
            return RedirectToAction("Index", "Product");
        }

        // GET: /User/Create
        // Displays the user creation view
        public ActionResult Create()
        {
            // Check if the user is authenticated by reading from the session
            string Authenticated = HttpContext.Session.GetString("Authenticated") ?? "false";

            // If not authenticated, redirect to the Login action
            if (Authenticated.Equals("false")) 
            {
                return RedirectToAction("Login", "User");
            }

            // Return the Create view
            return View();
        }


        // POST: /User/Create
        // Handles the form submission for creating a new user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserDTO userDTO)
        {
            // Check if the password and confirmation password match
            if (!userDTO.Password.Equals(userDTO.PasswordConfirmation)) 
            {
                // If passwords do not match, display an error message
                ViewBag.CreateUserError = "Passwords Do not Match";
                // Return the view with the error message and user input prefilled
                return View(userDTO);
            }

            // If the model state is not valid
            if (!ModelState.IsValid)
            {
                // Return the view with validation errors
                return View(userDTO);
            }

            // Create a new user using the repository
            var user = _repository.CreateUser(userDTO);
            if (user == null) 
            {
                // If the user creation fails (e.g., username already exists), display an error message
                ViewBag.CreateUserError = "Username already exists. Please choose a different Username.";
                // Return the view with the error message and user input prefilled
                return View(userDTO);
            }

            // If user creation succeeds, display a confirmation message
            ViewBag.CreateUserConfirmation = "User Account Created.";
            // Clear the model state to reset form inputs
            ModelState.Clear();
            // Return the view to indicate successful user creation
            return View();
        }
    }
}
