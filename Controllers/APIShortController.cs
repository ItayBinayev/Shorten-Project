using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShortenProject.Database;
using ShortenProject.Models;
using System;
using System.Security.Claims;

namespace ShortenProject.Controllers
{
    [Route("api")]
    [ApiController]
    public class APIShortController : ControllerBase
    {
        private readonly UrlDbcontext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public APIShortController(UrlDbcontext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }
        [HttpPost("shorten")]
        public ActionResult<string> Shorten([FromQuery] string fullurl)
        {
            string shorturl = @"https://localhost:7235/w/";
            string tempforcheck = "";
            var currentUser = HttpContext.User;
            var userId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
            var tempUser = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (!string.IsNullOrEmpty(fullurl))
            {
                Uri tmp;
                if (!Uri.IsWellFormedUriString(fullurl, UriKind.Absolute))
                    return BadRequest("Error, Invalid URL");
                if (!Uri.TryCreate(fullurl, UriKind.Absolute, out tmp))
                    return BadRequest("Error, Invalid URL");
                if (!(tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps))
                    return BadRequest("Error, Invalid URL");
                //todo: need to check if user logged in before this logic
                if (!(_signInManager.IsSignedIn(User)) && _context.URLs.Any(u => u.FullURL == fullurl && u.UrlUser == null)) 
				{
                    shorturl += _context.URLs.First(u => u.FullURL == fullurl && u.UrlUser == null).ShortURL;
                    return Ok(shorturl);
                }
                else if (_signInManager.IsSignedIn(User) && _context.URLs.Any(u => u.FullURL == fullurl && u.UrlUser.Equals(tempUser)))
                {
                    shorturl += _context.URLs.First(u => u.FullURL == fullurl && u.UrlUser.Equals(tempUser)).ShortURL;
                    return Ok(shorturl);
                }
                else
                {
                    URLModel newURL = new URLModel();
                    Random rnd = new Random();
                    do
                    {
                        tempforcheck = "";
                        const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
                        var chars = Enumerable.Range(0, rnd.Next(6, 10))
                            .Select(x => pool[rnd.Next(0, pool.Length)]);
                        tempforcheck += new string(chars.ToArray());

                    } while (_context.URLs.Any(u => u.ShortURL == tempforcheck));
                    newURL.ShortURL = tempforcheck;
                    newURL.FullURL = fullurl;
                    if (_signInManager.IsSignedIn(User))
                    {
                        if(tempUser != null)
                        newURL.UrlUser = tempUser;
                    }
                    _context.URLs.Add(newURL);
                    _context.SaveChanges();
                    return Ok(shorturl + tempforcheck);
                }
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpGet("/w/{shortenurl}")]
        public ActionResult RedirectShort(string shortenurl)
        {

            if (!string.IsNullOrEmpty(shortenurl))
            {
                if (_context.URLs.Any(u => u.ShortURL == shortenurl))
                {
                    var webObj = _context.URLs.First(u => u.ShortURL == shortenurl);
                    string website = webObj.FullURL;
                    webObj.CounterOfRequests++;
                    _context.SaveChanges();
                    return Redirect(website);
                }
            }
            return BadRequest();

        }
    }



}
