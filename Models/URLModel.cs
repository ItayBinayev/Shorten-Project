using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ShortenProject.Models
{
    public class URLModel
    {
        [Required]
        [Key]
        public string ShortURL { get; set; }
        [Required]
        public string FullURL { get; set; }
        [Required]
        public int CounterOfRequests { get; set; }
        [Required]
        public DateTime URLCreated { get; set; }
        public IdentityUser UrlUser { get; set; }

        public URLModel()
        {
            URLCreated = DateTime.Now;
        }
    }
}
