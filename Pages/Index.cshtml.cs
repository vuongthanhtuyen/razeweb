using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTT.models;

namespace CS_58_TichHop_EntityFramework.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MyBlogContent myBlogContent;

        public IndexModel(ILogger<IndexModel> logger, MyBlogContent _myBlogContent)
        {
            _logger = logger;
            myBlogContent = _myBlogContent;
        }

        public void OnGet()
        {
            var posts = (from a in myBlogContent.articles
                        orderby a.Created descending
                        select a).ToList();

            ViewData["posts"] = posts;

                        

        }
    }
}
