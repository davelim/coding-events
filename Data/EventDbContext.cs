using Microsoft.AspNetCore.Identity; // IdentityUser, IdentityRole
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // IdentityDbContext
using Microsoft.EntityFrameworkCore;
using CodingEvents.Models;

namespace CodingEvents.Data;

public class EventDbContext: IdentityDbContext<IdentityUser, IdentityRole, string>

{
    public DbSet<Event> Events {get; set;}
    public DbSet<EventCategory> Categories {get; set;}
    public DbSet<Tag> Tags {get; set;}

    public EventDbContext(DbContextOptions<EventDbContext> options)
      : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // TODO: is Event-to-Category (one-to-many) needed? App seems to work
      //  without it. But LC solution contains the following...
      // modelBuilder.Entity<Event>()
      //   .HasOne(p => p.Category)
      //   .WithMany(b => b.Events);
      // Model many-to-many (Tag-to-Event) in EF:
      // - configure a join table (EventTag):
      // - join table has composite primary key consisting of
      //   - Event.Id & Tag.Id
      modelBuilder.Entity<Event>()
        .HasMany(e => e.Tags)
        .WithMany(e => e.Events)
        .UsingEntity(j => j.ToTable("EventTags"));
      base.OnModelCreating(modelBuilder);
    }
}