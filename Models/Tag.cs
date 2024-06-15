namespace CodingEvents.Models;
public class Tag
{
    public int Id {get; set;}
    public string? Name {get; set;}
    // Model many-to-many (Tag-to-Event) in EF:
    // - Tag has ICollection<Event> property
    public ICollection<Event>? Events {get; set;}
    public Tag() {}
    public Tag(string name)
      : this()
    {
        Name = name;
        Events = new List<Event>();
    }
}