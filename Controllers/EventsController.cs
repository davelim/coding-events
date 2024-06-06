using Microsoft.AspNetCore.Mvc;
using CodingEvents.Models;
using CodingEventsDemo.Data;
using CodingEvents.ViewModels;

namespace CodingEvents.Controllers;

public class EventsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        List<Event> events = new List<Event>(EventData.GetAll());
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
            EventData.Add(newEvent);
            return Redirect("/Events");
        }
        return View(eventViewModel);
    }
    // GET: /events/delete
    [HttpGet]
    public IActionResult Delete()
    {
        ViewBag.events = EventData.GetAll();
        return View();
    }
    // POST: /events/delete
    [HttpPost]
    public IActionResult Delete(int[] eventIds)
    {
        foreach (int eventId in eventIds)
        {
            EventData.Remove(eventId);
        }

        return Redirect("/Events");
    }
    // GET: /events/edit/{id}
    [HttpGet("/events/edit/{eventId}")]
    public IActionResult Edit([FromRoute]int eventId)
    {
        Event editEvent = EventData.GetById(eventId);
        EventViewModel editEventViewModel = new EventViewModel{
            Name = editEvent.Name,
            Description = editEvent.Description,
            ContactEmail = editEvent.ContactEmail,
        };
        ViewBag.title = "Edit Event " + editEvent.Name + "(id = " + editEvent.Id + ")";
        return View(editEventViewModel);
    }
    // POST: /events/edit/{id}
    [HttpPost("/events/edit/{eventId}")]
    public IActionResult SubmitEditEventForm(int eventId, EventViewModel eventViewModel) {
        if (ModelState.IsValid)
        {
            Event editEvent = EventData.GetById(eventId);
            editEvent.Name = eventViewModel.Name;
            editEvent.Description = eventViewModel.Description;
            editEvent.ContactEmail = eventViewModel.ContactEmail;
            return Redirect("/Events");
        }
        return View("edit", eventViewModel);
    }
}