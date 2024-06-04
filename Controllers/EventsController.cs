using Microsoft.AspNetCore.Mvc;

namespace CodingEvents.Controllers;

public class EventsController : Controller
{
    // static private List<string> Events = new List<string>();
    static private Dictionary<string, string> Events = new Dictionary<string, string>();
    [HttpGet]
    public IActionResult Index()
    {
        // List<string> Events = new List<string>() {
        //     "Code with pride",
        //     "Strange loops",
        //     "Women who code"
        // };
        ViewBag.events = Events;
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
    public IActionResult NewEvent (string name, string description)
    {
        Events.Add(name, description);
        return Redirect("/Events");
    }
}