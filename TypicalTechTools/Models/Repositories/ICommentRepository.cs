namespace TypicalTechTools.Models.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetCommentsByProductCode(int code);
        Comment GetCommentById(int id);
        void CreateComment(Comment comment);
        void DeleteCommentById(int id);
        void UpdateComment(Comment comment);
    }
}
