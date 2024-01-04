using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VTT.models;

namespace CS_58_TichHop_EntityFramework.Pages_Blog
{
    [Authorize] // gioi han quyen khi chua dang nhap, khong the truy cap
    public class IndexModel : PageModel
    {
        private readonly VTT.models.AppDbContext _context;

        public IndexModel(VTT.models.AppDbContext context)
        {
            _context = context;
        }
        public const int ITEMS_PER_PAGE = 10;
        [BindProperty(SupportsGet =true, Name = "p")]
        public int currentpage {get; set;}
        public int countpage {get; set;}


        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync(string SearchString)
        {
            int totalArticle = await _context.articles.CountAsync();
            countpage = (int)Math.Ceiling((double)totalArticle/ITEMS_PER_PAGE);
             
            if(currentpage<1){
                currentpage = 1;
            }
            if(currentpage>countpage)
                currentpage = countpage;

            

            // Article = await _context.articles.ToListAsync();
            var qr = (from a in _context.articles
                orderby a.Created descending
                select a)
                .Skip((currentpage -1)*ITEMS_PER_PAGE)
                .Take(ITEMS_PER_PAGE);

            if(!string.IsNullOrEmpty(SearchString)){
                Article = qr.Where(a=> a.Title.Contains(SearchString)).ToList();
            }
            else{
                Article =  await qr.ToListAsync();
            }

            
        }
    }
}
