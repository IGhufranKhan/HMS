using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class MembersController : Controller
    {
        private static List<Member> _members = new List<Member>();
        public MembersController()
        {
            SeedMembers();
        }
        public IActionResult Index()
        {
            return View(_members);
        }
        public void SeedMembers()
        {
            var members = new List<Member>()
            {
                new Member { Id = 1, Name = "Ghufran", Email = "ghfuran@gmail.com" },
                new Member { Id = 2, Name = "Saad", Email = "saad@gmail.com" },
            };
            _members.AddRange(members);

        }
    }
}
