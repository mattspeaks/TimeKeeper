using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeKeeper.Models;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        WireUpTimekeeperEvent(modelBuilder);
        WireUpTimekeeperDay(modelBuilder);
        WireUpToDoNote(modelBuilder);
        WireUpLearningNote(modelBuilder);
    }

    private void WireUpTimekeeperEvent(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TimekeeperEvent>()
            .HasKey(te => te.Id);
    }

    private void WireUpTimekeeperDay(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TimekeeperDay>()
            .HasKey(td => td.Id);
    }

    private void WireUpToDoNote(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoNote>()
            .HasKey(tdn => tdn.Id);
    }

    private void WireUpLearningNote(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LearningNote>()
            .HasKey(ln => ln.Id);
    }

    public DbSet<TimekeeperEvent> TimekeeperEvent { get; set; }
    public DbSet<TimekeeperDay> TimekeeperDay { get; set; }
    public DbSet<LearningNote> LearningNote { get; set; }
    public DbSet<ToDoNote> ToDoNote { get; set; }
}
