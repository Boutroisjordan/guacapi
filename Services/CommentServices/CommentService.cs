using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Services;
 
public class CommentService : ICommentService
{
    #region Fields

    private readonly DataContext _context;

    #endregion

    // #region Constructors
    public CommentService(DataContext context)
    {
        this._context = context;
    }


    public async Task<List<Comment>> GetAll()
    {
        var comment = await _context.Comments.ToListAsync();
        return comment;
    }

    public async Task<Comment> GetCommentById(int id)
    {
        var commentById = await _context.Comments.Include(x => x.user).Where(x => x.CommentId == id).FirstOrDefaultAsync();
        if (commentById == null)
        {
            throw new Exception("Alcohol not found");
        }

        return commentById;
    }

    public async Task<Comment> AddComment(Comment request)
    {
        if (request is null)
        {
            return null;
        }

        if(_context.Comments.Any(o => o.CommentId == request.PreviousCommentId) == false && request.PreviousCommentId != 0) {
            throw new Exception("bad PreviousCommentId");
        }

        var addedComment= new Comment {
            Rate = request.Rate,
            UserId = request.UserId,
            OfferId = request.OfferId,
            PreviousCommentId = request.PreviousCommentId,
            Message = request.Message
        };
        

        var savedComment = _context.Comments.Add(addedComment).Entity;
        await _context.SaveChangesAsync();
        return savedComment;
    }

    public async Task<Comment> UpdateComment(int id, Comment request)
    {

        // return await Task.Run(() =>
        // {}
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }

            comment.Rate = request.Rate;
            comment.Message = request.Message;

            await _context.SaveChangesAsync();
            return comment;
    }

    public async Task<Comment> DeleteComment(int id)
    {
        // return await Task.Run(() =>
        // {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
                // throw new ArgumentException("Alcohol not found");

            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        // });
    }
    public async Task<Comment> OwnerDeleteComment(int id, int userId)
    {
        // return await Task.Run(() =>
        // {
            var comment = await _context.Comments.Where(x => x.CommentId == 1 && x.UserId == userId).FirstAsync();
            if (comment == null)
            {
                return null;
                // throw new ArgumentException("Alcohol not found");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        // });
    }
}