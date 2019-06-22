using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class BookIndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public BookIndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelet(int id)
        {
            var Book = await _db.Book.FindAsync(id);
            
            if(Book == null)
            {
                return NotFound();
            }
            _db.Book.Remove(Book);
            await _db.SaveChangesAsync();
            Message = "Book Item Deleted Successfully !";
            return RedirectToPage("BookIndex");
        }
    }
}