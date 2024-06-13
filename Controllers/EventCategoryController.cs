using Microsoft.AspNetCore.Mvc;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;

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
    // GET: /EventCategory/Create
    [HttpGet("/EventCategory/Create")]
    public IActionResult Create()
    {
        AddEventCategoryViewModel addEventCategoryViewModel = new();
        return View("create", addEventCategoryViewModel);
    }
    // POST: /EventCategory/Create
    [HttpPost("/EventCategory/Create")]
    public IActionResult ProcessCreateEventCategoryForm(AddEventCategoryViewModel addEventCategoryViewModel)
    {
        if (ModelState.IsValid)
        {
            EventCategory category = new(addEventCategoryViewModel.Name);
            context.Categories.Add(category);
            context.SaveChanges();
            return Redirect("index");
        }
        return View("create", addEventCategoryViewModel);
    }
}