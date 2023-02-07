using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;

using System.Text.Json.Serialization;

 
public class Comment

{
    #region Properties
///TODO Finir le model et regarder ONE TO ONE
    public int CommentId { get; set; }
    public int Rate {get; set;} // 1 to 5
    public string Message {get; set;}
    public double Price {get; set;}
    public string ImageUrl {get; set;}

    public int UserId;
    public int OfferId;

    #endregion

}