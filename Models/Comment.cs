using System.ComponentModel.DataAnnotations;
using GuacAPI.Entities;

namespace GuacAPI.Models;

using System.Text.Json.Serialization;

 
public class Comment

{
    #region Properties


    public int CommentId { get; set; }

    [Range(0, 5)]
    public int Rate {get; set;} // 1 to 5
    public string Message {get; set;}

    public int PreviousCommentId {get; set;}

    public int UserId {get; set;}
    public User user {get; set;}

    public int OfferId {get; set;}
    public Offer offer {get; set;}

    #endregion

}
public class CommentRegister

{
    #region Properties

    [Range(0, 5)]
    public int Rate {get; set;} // 1 to 5
    public string Message {get; set;}

    public int PreviousCommentId {get; set;}

    public int UserId {get; set;}
    public int OfferId {get; set;}

    #endregion

}