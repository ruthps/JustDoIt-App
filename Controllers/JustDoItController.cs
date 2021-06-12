using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustDoIt.Infrastructure;
using JustDoIt.Models;
using Microsoft.EntityFrameworkCore;

namespace JustDoIt.Controllers
{
    public class JustDoItController : Controller
    {
        private readonly ToDoContext context;
        public JustDoItController(ToDoContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult> Index()
        {
            IQueryable<ToDoList> items = (IQueryable<ToDoList>)(from i in context.JustDoIt orderby i.Id select i);
            List<ToDoList> todoList = await items.ToListAsync();

            return View(todoList);
        }


        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        

        // Create new ToDo
        public async Task<ActionResult> Create(ToDoList item)
        {
            if (ModelState.IsValid)
            {
                context.Add(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The To-Do has been added \n Have a good day ^^";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        
        // Edit ToDo
        public async Task<ActionResult> Edit(int id)
        {
            ToDoList item = await context.JustDoIt.FirstAsync();
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit(ToDoList item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The To-Do has been updated";
                return RedirectToAction("Index");
            }
            return View(item);
        }

        //Delete ToDo
        public async Task<ActionResult> Delete(int id)
        {
            ToDoList item = await context.JustDoIt.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The To-Do doesn't exist";
            }
            else
            {
                context.JustDoIt.Remove(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "The To-Do has been deleted, \n Have a good day ^^!";

            }
            return RedirectToAction("Index");
        }
    }
}
