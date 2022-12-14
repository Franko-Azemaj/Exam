using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FinalTest2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;

    }

    public IActionResult Index()
    {

        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return RedirectToAction("Register");
        }
        int id = (int)HttpContext.Session.GetInt32("userId");

        List<User> LIST4 = _context.Users
                        .Include(e => e.ReciverRequests)
                        .Include(e => e.SenderRequests)
                        .Where(e => e.UserId != id)
                        .Where(e =>
                                     (e.SenderRequests.Any(f => f.ReciverId == id) == false)
                                    && (e.ReciverRequests.Any(f => f.SenderId == id) == false)
                        ).ToList();



        ViewBag.perdoruesit = LIST4;

        ViewBag.iLoguari = _context.Users.FirstOrDefault(e => e.UserId == id);

        ViewBag.Posts = _context.Posts.Include(e => e.Creator).Include(e => e.Likers).ThenInclude(e => e.User).ToList();

        ViewBag.Comment = _context.Comments.Include(e => e.Post).ThenInclude(e => e.Creator).ToList();

        return View();
    }

    [HttpGet("Register")]
    public IActionResult Register()
    {

        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return View();
        }

        return RedirectToAction("Index");

    }
    [HttpPost("Register")]
    public IActionResult Register(User user)
    {
        // Check initial ModelState
        if (ModelState.IsValid)
        {
            // If a User exists with provided email
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                // Manually add a ModelState error to the Email field, with provided
                // error message
                ModelState.AddModelError("UserName", "UserName already in use!");

                return View();
                // You may consider returning to the View at this point
            }
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId", user.UserId);

            return RedirectToAction("Index");
        }
        return View();
    }

    [HttpPost("Login")]
    public IActionResult LoginSubmit(LoginUser userSubmission)
    {
        if (ModelState.IsValid)
        {
            // If initial ModelState is valid, query for a user with provided email
            var userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
            // If no user exists with provided email
            if (userInDb == null)
            {
                // Add an error to ModelState and return to View!
                ModelState.AddModelError("User", "Invalid UserName/Password");
                return View("Register");
            }

            // Initialize hasher object
            var hasher = new PasswordHasher<LoginUser>();

            // verify provided password against hash stored in db
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);

            // result can be compared to 0 for failure
            if (result == 0)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return View("Register");
                // handle failure (this should be similar to how "existing email" is handled)
            }
            HttpContext.Session.SetInt32("userId", userInDb.UserId);

            return RedirectToAction("Index");
        }

        return View("Register");
    }
    [HttpGet("SendR/{id}")]
    public IActionResult SendR(int id)
    {
        int idFromSession = (int)HttpContext.Session.GetInt32("userId");
        Request newRequest = new Request()
        {
            SenderId = idFromSession,
            ReciverId = id,

        };
        _context.Requests.Add(newRequest);
        _context.SaveChanges();
        // User dbUser = _context.Users.Include(e=>e.Requests).First(e=> e.UserId == idFromSession);
        // dbUser.Requests.Add(newRequest);
        _context.SaveChanges();
        return RedirectToAction("index");

    }

    [HttpGet("AcceptR/{id}")]
    public IActionResult AcceptR(int id)
    {

        Request requestii = _context.Requests.First(e => e.RequestId == id);
        requestii.Accepted = true;
        // _context.Remove(hiqFans);
        _context.SaveChanges();
        return RedirectToAction("myProfile");
    }
    [HttpGet("DeclineR/{id}")]
    public IActionResult Decline(int id)
    {

        Request requestii = _context.Requests.First(e => e.RequestId == id);
        _context.Remove(requestii);
        _context.SaveChanges();
        return RedirectToAction("myProfile");
    }
    [HttpGet("RemoveF/{id}")]
    public IActionResult RemoveF(int id)
    {

        Request requestii = _context.Requests.First(e => e.RequestId == id);
        _context.Remove(requestii);
        _context.SaveChanges();
        return RedirectToAction("myProfile");
    }

    [HttpGet("professional_profile")]
    public IActionResult myProfile()
    {
        int id = (int)HttpContext.Session.GetInt32("userId");

        ViewBag.requests = _context.Requests.Include(e => e.Reciver).Include(e => e.Sender).Where(e => e.ReciverId == id).Where(e => e.Accepted == false).ToList();
        ViewBag.miqte = _context.Requests.Where(e => (e.SenderId == id) || (e.ReciverId == id)).Include(e => e.Reciver).Include(e => e.Sender).Where(e => e.Accepted == true).ToList();

        ViewBag.LogedInUser = _context.Users.First(e => e.UserId == id);

        return View("ProfesionalProfile");
    }

    [HttpGet("users/{id}")]
    public IActionResult users(int id)
    {

        ViewBag.CurrentUser = _context.Users.First(e => e.UserId == id);
        return View("Users");

    }

    [HttpGet("CreatePost")]
    public IActionResult CreatePost()
    {

        return View("CreatePost");
    }

    [HttpPost("newPost")]
    public IActionResult newPost(Post post)
    {
        post.UserId = (int)HttpContext.Session.GetInt32("userId");

        if (ModelState.IsValid)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        return View("CreatePost");

    }

    [HttpGet("/Post/Like/{id}")]
    public IActionResult Like(int id)
    {

        int idFromSession = (int)HttpContext.Session.GetInt32("userId");
        Like newPost = new Like
        {
            UserId = idFromSession,
            PostId = id
        };

        _context.Add(newPost);
        _context.SaveChanges();
        return RedirectToAction("index");

    }

    [HttpGet("/Post/Un-Like/{id}")]
    public IActionResult UnLike(int id)
    {

        Like remodedLike = _context.Likes.First(e => e.LikeId == id);

        _context.Remove(remodedLike);
        _context.SaveChanges();
        return RedirectToAction("index");

    }

    [HttpGet("/Post/Delete/{id}")]
    public IActionResult Delete(int id)
    {

        int idFromSession = (int)HttpContext.Session.GetInt32("userId");
        Post DeletingPost = _context.Posts.First(e => e.PostId == id);

        _context.Remove(DeletingPost);
        _context.SaveChanges();
        return RedirectToAction("index");

    }

    [HttpPost("addCommnet")]
    public IActionResult newCommnet(Comment commnet)
    {
        if (ModelState.IsValid)
        {

            _context.Comments.Add(commnet);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

    return View("index");

    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {

        HttpContext.Session.Clear();
        return RedirectToAction("register");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
