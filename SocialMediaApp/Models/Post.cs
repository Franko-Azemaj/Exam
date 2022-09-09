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
    public string Comment { get; set; }
    public int NrLikes { get; set; }
    public int UserId { get; set; }
    public User? Creator { get; set;}

    public List<Comment> CreatedComnets { get; set; } =  new List<Comment>();

}


