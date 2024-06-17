# Purpose of app
App collects coding events. App provides a place for organizers of events and would-be attenders to publicize and look at/for events.
# Current state
Currently app provides following functionality:
1. create/edit/remove events
2. create event categories
3. create tags
4. add tag(s) to an event
# Future improvements
Allow a person to signup for notifications about an event. So provide following high-level functionality:
- create/edit/remove user account
- mark event(s) for which user wants update
One way of doing this:
- Person class, with properties: Id, Name, Password, EmailAddr, List<Event>
- eventdbcontext: add join table for Person & Event
- PersonsController: Index, Add(GET, POST)
- PersonViewModel: validate Name, Password, EmailAddr
- Views/Person/Index.cshtml, Add.cshtml
- EventDetailViewModel: add person (?)
