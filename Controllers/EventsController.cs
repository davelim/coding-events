using Microsoft.AspNetCore.Mvc;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;

namespace CodingEvents.Controllers;

public class EventsController : Controller
{
    private EventDbContext context;
    // dependency injection: ASP.NET invokes constructor with an instance of
    //   EventDbContext.
    public EventsController(EventDbContext dbContext)
    {
        context = dbContext;
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<Event> events = context.Events.ToList();
        return View(events);
    }
    // GET: /Events/add/
    // retrieve form
    [HttpGet()]
    public IActionResult Add()
    {
        EventViewModel eventViewModel = new EventViewModel();
        return View(eventViewModel);
    }
    // POST: /Events/add/
    // process form
    [HttpPost()]
    public IActionResult Add(EventViewModel eventViewModel)
    {
        if (ModelState.IsValid)
        {
            Event newEvent = new Event
            {
                Name = eventViewModel.Name,
                Description = eventViewModel.Description,
                ContactEmail = eventViewModel.ContactEmail,
                Type = eventViewModel.Type
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
        EventViewModel editEventViewModel = new EventViewModel{
            Name = editEvent.Name,
            Description = editEvent.Description,
            ContactEmail = editEvent.ContactEmail,
            Type = editEvent.Type
        };
        ViewBag.title = "Edit Event " + editEvent.Name + "(id = " + editEvent.Id + ")";
        return View(editEventViewModel);
    }
    // POST: /events/edit/{id}
    [HttpPost("/events/edit/{eventId}")]
    public IActionResult SubmitEditEventForm(int eventId, EventViewModel eventViewModel) {
        if (ModelState.IsValid)
        {
            // TODO: handle possible "editEvent" null reference(?)
            Event? editEvent = context.Events.Find(eventId);
            editEvent.Name = eventViewModel.Name;
            editEvent.Description = eventViewModel.Description;
            editEvent.ContactEmail = eventViewModel.ContactEmail;
            editEvent.Type = eventViewModel.Type;
            context.SaveChanges();
            return Redirect("/Events");
        }
        return View("edit", eventViewModel);
    }
}
