using System.ComponentModel.DataAnnotations;
using GuacAPI.Entities;

namespace GuacAPI.Models;

using System.Text.Json.Serialization;

 
public class Comment

{
    #region Properties
///TODO Finir le model et regarder ONE TO ONE
// 1 commentaire à 1 offre et 1 user

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