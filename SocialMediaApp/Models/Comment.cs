using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SocialMediaApp.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;

public class Comment
{

    [Key]
    public int CommentId { get; set; }

    public string Content { get; set; }
    public int NrLikes { get; set; }

    public int PostId { get; set; }
    public Post? BelongingPost { get; set;}


}
