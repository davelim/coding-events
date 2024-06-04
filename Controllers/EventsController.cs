using Microsoft.AspNetCore.Mvc;
using CodingEvents.Models;
using CodingEventsDemo.Data;

namespace CodingEvents.Controllers;

[Route("/Events")]
public class EventsController : Controller
{
    // static private List<string> Events = new List<string>();
    // static private Dictionary<string, string> Events = new Dictionary<string, string>();
    [HttpGet]
    public IActionResult Index()
    {
        // List<string> Events = new List<string>() {
        //     "Code with pride",
        //     "Strange loops",
        //     "Women who code"
        // };
        ViewBag.events = EventData.GetAll();
        return View();
    }
    [HttpGet]
    [Route("Add/")]
    public IActionResult Add()
    {
        // Any additional method code here
        return View();
    }
    [HttpPost]
    [Route("Add/")]
    public IActionResult NewEvent(Event newEvent)
    {
        // Events.Add(name, description);
        EventData.Add(newEvent);
        return Redirect("/Events");
    }
    [Route("Delete/")]
    public IActionResult Delete()
    {
        ViewBag.events = EventData.GetAll();
        return View();
    }
    [HttpPost("Delete/")]
    public IActionResult Delete(int[] eventIds)
    {
        foreach (int eventId in eventIds)
        {
            EventData.Remove(eventId);
        }

        return Redirect("/Events");
    }
    [HttpGet("Edit/{eventId}")]
    public IActionResult Edit(int eventId)
    {
        Event editEvent = EventData.GetById(eventId);
        ViewBag.editEvent = editEvent;
        ViewBag.title = "Edit Event " + editEvent.Name + "(id = " + editEvent.Id + ")";
        return View();
    }
    [HttpPost("Edit/{eventId}")]
    public IActionResult SubmitEditEventForm(int eventId, string name, string description) {
        Event editEvent = EventData.GetById(eventId);
        editEvent.Name = name;
        editEvent.Description = description;
        return Redirect("/Events");
    }
}