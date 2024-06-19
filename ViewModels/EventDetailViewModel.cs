using System.Text;
using CodingEvents.Models;

namespace CodingEvents.ViewModels;
public class EventDetailViewModel
{
    public int EventId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ContactEmail { get; set; }
    public string CategoryName { get; set; }
    public string TagText { get; set; }

    public EventDetailViewModel(Event theEvent)
    {
        EventId = theEvent.Id;
        Name = theEvent.Name;
        Description = theEvent.Description;
        ContactEmail = theEvent.ContactEmail;
        CategoryName = theEvent.Category.Name;
        TagText = theEvent.Tags != null
                    ? String.Join(", ", theEvent.Tags
                                        .Select(elmt => $"#{elmt.Name}")
                                        .ToArray())
                    : "";
    }
}