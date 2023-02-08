using GuacAPI.Models;

namespace GuacAPI.Services;
 
public interface ICommentService

{
    //Get all product in Database
    // ICollection<Product> GetAll();

    //Ad one Product in database
    // Product AddOne(Product item);

    Task<List<Comment>> GetAll();
    Task<Comment> GetCommentById(int id);

    Task<Comment> AddComment(Comment comment);

    Task<Comment> UpdateComment(int id, Comment comment);
    Task<Comment> DeleteComment(int id);
    Task<Comment> OwnerDeleteComment(int id, int userId);
}