#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using Microsoft.AspNetCore.Identity;


namespace Projekt.Controllers
{
    public class SitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;


        public SitesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Sites
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sites.Include(s => s.Categories);
            return View(await applicationDbContext.ToListAsync());
        }
        

        // GET: Sites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sites = await _context.Sites
                .Include(s => s.Categories)
                .Include(s => s.Comments)
                    .ThenInclude(s => s.ApplicationUser) //get related users to comments
                .Include(s => s.ApplicationUser)
                .FirstOrDefaultAsync(m => m.SitesId == id);
            if (sites == null)
            {
                return NotFound();
            }

            return View(sites);
        }

        // GET: Sites/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "CategoriesId", "Category");
            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("SitesId,Title,Description,CategoriesId,Url,UserID,Created")] Sites sites)
        public async Task<IActionResult> Create(Sites sites)
        {

            var userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                _context.Add(new Sites
                {
                    Title = sites.Title,
                    Description = sites.Description,
                    CategoriesId = sites.CategoriesId,
                    Url = sites.Url,
                    ApplicationUserId = userId,
                });

                await _context.SaveChangesAsync();
                return RedirectToAction("UserSites");
            }
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "CategoriesId", "CategoriesId", sites.CategoriesId);
            return View(sites);
        }

        // GET: Sites/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var sites = await _context.Sites.FindAsync(id);
            if (sites == null)
            {
                return NotFound();
            }
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "CategoriesId", "Category", sites.CategoriesId);
            return View(sites);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SitesId,Title,Description,CategoriesId,Url,ApplicationUserId,Created")] Sites sites)
        {
            if (id != sites.SitesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sites);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SitesExists(sites.SitesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("UserSites");
            }
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "CategoriesId", "CategoriesId", sites.CategoriesId);

            return RedirectToAction("UserSites");
        }

        // GET: Sites/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sites = await _context.Sites
                .Include(s => s.Categories)
                .Include(s => s.ApplicationUser)
                .FirstOrDefaultAsync(m => m.SitesId == id);
            if (sites == null)
            {
                return NotFound();
            }

            return View(sites);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sites = await _context.Sites.FindAsync(id);
            _context.Sites.Remove(sites);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserSites", "Sites");
        }

        // Post comment to a Site
        public async Task<IActionResult> postComment(Comments com)
        {
            var userId = _userManager.GetUserId(User);

            _context.Comments.Add(new Comments
            {
                SitesId = com.SitesId,
                Comment = com.Comment,
                ApplicationUserId = userId,
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new
            {
                id = com.SitesId
            });

        }

        // GET: Sites for logged in user. 
        [Authorize]
        public async Task<IActionResult> UserSites()
        {
            var userId = _userManager.GetUserId(User);
            var applicationDbContext = _context.Sites
                .Include(s => s.Categories)
                .Where(t => t.ApplicationUserId == userId);

            return View(await applicationDbContext.ToListAsync());
        }

        //GET: Category for menu
        public async Task<IActionResult> ViewByCategory(int CategoryId)
        {

            var getCategory = _context.Categories.SingleOrDefault(t => t.CategoriesId == CategoryId);
            ViewBag.currentCategory= getCategory;
            
            var applicationDbContext = _context.Sites
                .Include(s => s.Categories)
                .Include(s => s.ApplicationUser)
                .OrderByDescending(s => s.Created)
                .Where(t => t.CategoriesId == CategoryId);

            return View(await applicationDbContext.ToListAsync());
        }

        private bool SitesExists(int id)
        {
            return _context.Sites.Any(e => e.SitesId == id);
        }
    }
}