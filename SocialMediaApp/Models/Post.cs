using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SocialMediaApp.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;

public class Post
{

    [Key]
    public int PostId { get; set; }
    [Required]
    public string Description { get; set; }
    public int UserId { get; set; }
    public User? Creator { get; set;}
    public List<Like> Likers { get; set; } = new List<Like>(); 
    public List<Comment> Commnets { get; set; } = new List<Comment>(); 

}
