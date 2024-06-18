using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;


namespace CodingEvents.Controllers;

[Authorize]
public class EventsController : Controller
{
    private EventDbContext context;
    // dependency injection: ASP.NET invokes constructor with an instance of
    //   EventDbContext.
    public EventsController(EventDbContext dbContext)
    {
        context = dbContext;
    }
    [AllowAnonymous]
    // GET: /Events/
    [HttpGet]
    public IActionResult Index()
    {
        // When retrieving events, "eager load" event category.
        List<Event> events = context.Events.Include(e => e.Category).ToList();
        return View(events);
    }
    // GET: /Events/Detail/{id}
    [HttpGet("/Events/Detail/{id}")]
    public IActionResult Detail(int id)
    {
        Event theEvent = context.Events
            .Include(e => e.Category)
            .Include(e => e.Tags)
            .Single(e => e.Id == id);
        EventDetailViewModel viewModel = new EventDetailViewModel(theEvent);
        return View(viewModel);
    }
    // GET: /Events/add/
    [HttpGet()]
    public IActionResult Add()
    {
        List<EventCategory> categories = context.Categories.ToList();
        EventViewModel eventViewModel = new EventViewModel(categories);
        return View(eventViewModel);
    }
    // POST: /Events/add/
    [HttpPost()]
    public IActionResult Add(EventViewModel eventViewModel)
    {
        if (ModelState.IsValid)
        {
            EventCategory theCategory =
                context.Categories.Find(eventViewModel.CategoryId);
            Event newEvent = new Event
            {
                Name = eventViewModel.Name,
                Description = eventViewModel.Description,
                ContactEmail = eventViewModel.ContactEmail,
                Category = theCategory
            };
            context.Events.Add(newEvent);
            context.SaveChanges();
            return Redirect("/Events");
        }
        return View(eventViewModel);
    }
    // GET: /events/delete
    [HttpGet]
    public IActionResult Delete()
    {
        ViewBag.events = context.Events.ToList();
        return View();
    }
    // POST: /events/delete
    [HttpPost]
    public IActionResult Delete(int[] eventIds)
    {
        foreach (int eventId in eventIds)
        {
            Event? theEvent = context.Events.Find(eventId);
            if (theEvent != null)
            {
                context.Events.Remove(theEvent);
            }
        }
        context.SaveChanges();
        return Redirect("/Events");
    }
    // GET: /events/edit/{id}
    [HttpGet("/events/edit/{eventId}")]
    public IActionResult Edit([FromRoute]int eventId)
    {
        Event? editEvent = context.Events.Find(eventId);
        if (editEvent != null)
        {
            List<EventCategory> categories = context.Categories.ToList();
            EventViewModel editEventViewModel = new EventViewModel(categories){
                Name = editEvent.Name,
                Description = editEvent.Description,
                ContactEmail = editEvent.ContactEmail,
                CategoryId = editEvent.CategoryId
            };
            ViewBag.title = "Edit Event " + editEvent.Name + "(id = " + editEvent.Id + ")";
            return View(editEventViewModel);
        }
        return Redirect("/Events");
    }
    // POST: /events/edit/{id}
    [HttpPost("/events/edit/{eventId}")]
    public IActionResult SubmitEditEventForm(int eventId, EventViewModel eventViewModel)
    {
        if (ModelState.IsValid)
        {
            Event? editEvent = context.Events.Find(eventId);
            if (editEvent != null)
            {
                // TODO: handle
                EventCategory theCategory =
                    context.Categories.Find(eventViewModel.CategoryId);
                editEvent.Name = eventViewModel.Name;
                editEvent.Description = eventViewModel.Description;
                editEvent.ContactEmail = eventViewModel.ContactEmail;
                editEvent.Category = theCategory;
                context.SaveChanges();
            }
            return Redirect("/Events");
        }
        return View("edit", eventViewModel);
    }
}