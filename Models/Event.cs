namespace CodingEvents.Models;

public class Event
{
    public string? Name { get; set; }
    public string? Description {get; set;}
    public string? ContactEmail {get; set;}
    // Model one-to-many (EventCategory-to-Event) in EF:
    // - Event has EventCategory property + CategoryId(i.e. foreign key)
    // - EF naming convention for foreign key: <object>Id
    public EventCategory Category { get; set; }
    public int CategoryId { get; set; }
    public int Id {get; set;}
    // Model many-to-many (Tag-to-Event) in EF:
    // - Event has ICollection<Tag> property
    public ICollection<Tag>? Tags {get;set;}

    public Event() {}
    public Event(string name, string description, string contactEmail)
      : this()
    {
        Name = name;
        Description = description;
        ContactEmail = contactEmail;
        Tags = new List<Tag>();
    }
    public override string ToString()
    {
        return Name;
    }
    public override bool Equals(object? obj)
    {
        return obj is Event @event && Id == @event.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}