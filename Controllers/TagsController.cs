using Microsoft.AspNetCore.Mvc;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;

namespace CodingEvents.Controllers;

public class TagsController: Controller
{
    private EventDbContext context;
    public TagsController(EventDbContext dbContext)
    {
        context = dbContext;
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<Tag> tags = context.Tags.ToList();
        return View(tags);
    }
    // GET: /Tags/Create
    [HttpGet("/Tags/Create")]
    public IActionResult Create()
    {
        AddTagViewModel addTagViewModel = new();
        return View("create", addTagViewModel);
    }
    // POST: /Tags/Create
    [HttpPost("/Tags/Create")]
    public IActionResult ProcessCreateTagForm(AddTagViewModel addTagViewModel)
    {
        if (ModelState.IsValid)
        {
            Tag tag = new(addTagViewModel.Name);
            context.Tags.Add(tag);
            context.SaveChanges();
            return Redirect("index");
        }
        return View("create", addTagViewModel);
    }
}