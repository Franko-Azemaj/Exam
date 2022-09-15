using System.ComponentModel.DataAnnotations;
namespace SocialMediaApp.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;


public class Like{

    [Key]
    public int LikeId {get;set;}
    public int UserId {get;set;}
    public int PostId { get; set; }
    public User? User {get;set;}
    
}
