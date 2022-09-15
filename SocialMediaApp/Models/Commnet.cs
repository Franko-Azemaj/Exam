using System.ComponentModel.DataAnnotations;
namespace SocialMediaApp.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;


public class Comment{

    [Key]
    public int CommentId {get;set;}
    public string Content { get; set; }
    public int PostId { get; set; }
    public Post? Post {get;set;}


    
}
