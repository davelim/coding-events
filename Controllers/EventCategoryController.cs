using Microsoft.AspNetCore.Mvc;
using CodingEvents.Data;
using CodingEvents.Models;

namespace CodingEvents.Controllers;

public class EventCategoryController: Controller
{
    private EventDbContext context;
    public EventCategoryController(EventDbContext dbContext)
    {
        context = dbContext;
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<EventCategory> categories = context.Categories.ToList();
        return View(categories);
    }
}