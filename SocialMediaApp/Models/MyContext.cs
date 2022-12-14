#pragma warning disable CS8618
/* 
Disabled Warning: "Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable."
We can disable this safely because we know the framework will assign non-null values when it constructs this class for us.
*/
using Microsoft.EntityFrameworkCore;
namespace SocialMediaApp.Models;
// the MyContext class representing a session with our MySQL database, allowing us to query for or save data
public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }
    // the "Monsters" table name will come from the DbSet property name
    public DbSet<User> Users { get; set; } 
    public DbSet<Request> Requests {get;set;}
    public DbSet<Post> Posts {get;set;}
    public DbSet<Like> Likes {get;set;}
    public DbSet<Comment> Comments {get;set;}


    //  protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
        
    //     modelBuilder.Entity<Request>()
    //         .HasOne(r => r.Reciver)
    //         .WithMany(u => u.ReciverRequests)
    //         .HasForeignKey(r => r.ReciverId);

    //     modelBuilder.Entity<Request>()
    //         .HasOne(r => r.Sender)
    //         .WithMany(u => u.SenderRequests)
    //         .HasForeignKey(r => r.SenderId);
    // }
}
