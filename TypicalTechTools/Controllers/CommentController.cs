using Microsoft.AspNetCore.Mvc;
using TypicalTechTools.Models;
using TypicalTechTools.Models.Repositories;

namespace TypicalTools.Controllers
{
    public class CommentController : Controller
    {
        // Repository for managing comment data
        private readonly ICommentRepository _repository;

        // Constructor to inject the comment repository
        public CommentController(ICommentRepository repository)
        {
            _repository = repository;
        }

        // GET: /Comment/CommentList
        // Displays a list of comments for a specific product
        [HttpGet]
        public ActionResult CommentList(int id)
        {
            //Get a list of comments with matching product codes (id)
            List<Comment> comments = _repository.GetCommentsByProductCode(id);

            // Redirect to the product index if no comments are found
            if (comments == null)
            {
                return RedirectToAction("Index", "Product");
            }

            // Return the view with the list of comments
            return View(comments);
        }


        // GET: /Comment/AddComment
        // Displays a form to add a new comment for a specific product
        [HttpGet]
        public IActionResult AddComment(int productCode)
        {
            Comment comment = new Comment();
            // Set the product code for the new comment
            comment.Code = productCode;
            // Return the view with the empty comment model (+ product code)
            return View(comment);
        }

        // POST: /Comment/AddComment
        // Handles the form submission to create a new comment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(Comment comment)
        {
            try
            {
                // Check if the form data is valid
                if (ModelState.IsValid)
                {
                    // Store the session ID in session data
                    HttpContext.Session.SetString("SessionId", HttpContext.Session.Id);

                    // Add the session ID and a created date to the comment
                    comment.SessionId = HttpContext.Session.Id;
                    comment.CreatedDate = DateTime.Now;

                    // Save the comment to the repository
                    _repository.CreateComment(comment);

                    // Store the comment text in session (for tracking purposes)
                    HttpContext.Session.SetString("CommentText", comment.Text);

                    // Redirect to the product index page
                    return RedirectToAction("Index", "Product");
                }

                // Return the view with the invalid comment model
                return View(comment);
            }catch
            {
                // Return the view with the comment model in case of an error
                return View(comment);
            }
        }

        // GET: /Comment/RemoveComment
        // Displays a confirmation form to delete a comment
        [HttpGet]
        public ActionResult RemoveComment(int commentId)
        {
            // Retrieve the comment by ID
            var comment = _repository.GetCommentById(commentId);
            // Return the view with the comment to be deleted
            return View(comment);
        }


        // POST: /Comment/RemoveComment
        // Handles the form submission to delete a comment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveComment(Comment comment)
        {
            try
            {
                // Check if the form data is valid
                if (ModelState.IsValid)
                {
                    // Delete the comment from the repository
                    _repository.DeleteCommentById(comment.Id);
                    // Redirect to the product index page
                    return RedirectToAction("Index", "Product");
                }
                // Redirect if the data is not valid
                return RedirectToAction("Index", "Product");
            }
            catch
            {
                // Redirect in case of an error
                return RedirectToAction("Index", "Product");
            }
        }

        // GET: /Comment/EditComment
        // Displays a form to edit an existing comment
        [HttpGet]
        public ActionResult EditComment(int commentId)
        {
            // Retrieve the comment by ID
            Comment comment = _repository.GetCommentById(commentId);
            // Return the view with the comment to be edited
            return View(comment);
        }

        // POST: /Comment/EditComment
        // Handles the form submission to update an existing comment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(Comment comment)
        {
            // Check if the comment is null
            if (comment == null)
            {
                // Redirect to the product index page
                return RedirectToAction("Index", "Product");
            }
            try
            {
                // Check if the form data is valid
                if (ModelState.IsValid)
                {
                    // Store the session ID in session data
                    HttpContext.Session.SetString("SessionId", HttpContext.Session.Id);
                    // Add the session ID to the comment
                    comment.SessionId = HttpContext.Session.Id;
                    // Update the comment in the repository
                    _repository.UpdateComment(comment);

                    // Redirect to the list of comments for the product
                    return RedirectToAction("CommentList", "Comment", new { id = comment.Code });
                }

                // Return the view with the invalid comment model
                return View(comment);
            }
            catch
            {
                // Return the view with the comment model in case of an error
                return View(comment);
            }

        }
    }
}
