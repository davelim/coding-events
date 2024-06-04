using Microsoft.AspNetCore.Mvc;
using CodingEvents.Models;
using CodingEventsDemo.Data;

namespace CodingEvents.Controllers;

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
    public IActionResult Add()
    {
        // Any additional method code here
        return View();
    }
    [HttpPost]
    [Route("/Events/Add")]
    public IActionResult NewEvent(Event newEvent)
    {
        // Events.Add(name, description);
        EventData.Add(newEvent);
        return Redirect("/Events");
    }
    public IActionResult Delete()
    {
        ViewBag.events = EventData.GetAll();
        return View();
    }
    [HttpPost]
    public IActionResult Delete(int[] eventIds)
    {
        foreach (int eventId in eventIds)
        {
            EventData.Remove(eventId);
        }

        return Redirect("/Events");
    }
}