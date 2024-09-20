
using Microsoft.EntityFrameworkCore;
using TypicalTechTools.Models.Data;

namespace TypicalTechTools.Models.Repositories
{
    public class CommentRepository : ICommentRepository
    {

        // Database context for accessing Comment data
        private readonly TechToolsDBContext _context;

        // Constructor to initialize the repository with the database context
        public CommentRepository(TechToolsDBContext context)
        {
            _context = context;
        }

        // Method to create and save a new comment
        public void CreateComment(Comment comment)
        {
            // Add the new comment to the Comments DbSet
            _context.Comments.Add(comment);
            // Save changes to the database
            _context.SaveChanges();
        }

        // Method to delete a comment by its ID
        public void DeleteCommentById(int id)
        {
            // Find the comment with the specified ID and remove it from the DbSet
            // ExecuteDelete() is used for direct database deletion
            _context.Comments.Where(c => c.Id == id).ExecuteDelete();
        }

        // Method to retrieve a single comment by its ID
        public Comment GetCommentById(int id)
        {
            // Find the comment with the specified ID or return a new Comment object if not found
            //New comment will only have an ID associated
            return _context.Comments.Where(c => c.Id == id)
                .FirstOrDefault()?? new Comment();
        }

        // Method to retrieve comments associated with a specific product code
        public List<Comment> GetCommentsByProductCode(int code)
        {
            // Retrieve all comments with the specified product code and return as a list
            return _context.Comments.Where(c => c.Code == code).ToList();
        }

        // Method to update an existing comment
        public void UpdateComment(Comment comment)
        {
            // Mark the comment entity as modified
            _context.Comments.Update(comment);
            // Save changes to the database
            _context.SaveChanges();
        }
    }
}
