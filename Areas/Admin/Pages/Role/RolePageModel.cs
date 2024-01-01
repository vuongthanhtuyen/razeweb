using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTT.models;

namespace App.Admin.Role
{
    public class RolePageModel : PageModel
    {
          protected readonly RoleManager<IdentityRole> _roleManager;
          protected readonly MyBlogContent _context;

          [TempData]
          public string StatusMessage { get; set; }
          public RolePageModel(RoleManager<IdentityRole> roleManager, MyBlogContent myBlogContext)
          {
              _roleManager = roleManager;
              _context = myBlogContext;
          }
    }
}