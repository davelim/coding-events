using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    // GET: /Tags/Detail/{id}
    [HttpGet("/Tags/Detail/{id}")]
    public IActionResult Detail(int id)
    {
        Tag theTag = context.Tags
                     .Include(e => e.Events)
                     .Where(t => t.Id == id).First();
        return View(theTag);
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
    // GET: /Tags/AddEvent/{id}
    [HttpGet("/Tags/AddEvent/{id}")]
    public IActionResult AddEvent(int id)
    {
        Event theEvent = context.Events.Find(id);
        List<Tag> possibleTags = context.Tags.ToList();
        AddEventTagViewModel viewModel = new AddEventTagViewModel(theEvent, possibleTags);
        return View(viewModel);
    }
    // POST: /Tags/AddEvent/{id}
    [HttpPost("/Tags/AddEvent/{id}")]
    public IActionResult AddEvent(AddEventTagViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            int eventId = viewModel.EventId;
            int tagId = viewModel.TagId;
            Event theEvent = context.Events.Include(e => e.Tags).Where(e => e.Id == eventId).First();
            Tag theTag = context.Tags.Where(t => t.Id == tagId).First();
            theEvent.Tags.Add(theTag);
            context.SaveChanges();
            return Redirect("/Events/Detail/" + eventId);
        }
        return View(viewModel);
    }
}