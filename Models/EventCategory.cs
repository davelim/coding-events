namespace CodingEvents.Models;
public class EventCategory
{
    public int Id {get; set;}
    public string? Name {get; set;}
    // Model one-to-many (Category-to-Event) in EF:
    // - EventCategory has List<Event> property
    public List<Event> Events {get; set;}
    public EventCategory() {}
    public EventCategory(string name)
      : this()
    {
        Name = name;
    }
}