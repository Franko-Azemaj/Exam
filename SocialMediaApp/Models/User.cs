using System.ComponentModel.DataAnnotations;
namespace SocialMediaApp.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;


public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required]
    [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
    public string Password { get; set; }

    [Required]
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    [InverseProperty("Reciver")]
    public List<Request> ReciverRequests {get;set;} = new List<Request>();
    [InverseProperty("Sender")]
    public List<Request> SenderRequests {get;set;} = new List<Request>();


    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string Confirm { get; set; }

    public List<Post> CreatedPosts { get; set; } =  new List<Post>();

    public List<Like> PostsLiked { get; set; } = new List<Like>(); 

}
public class LoginUser
{
    // No other fields!
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}


