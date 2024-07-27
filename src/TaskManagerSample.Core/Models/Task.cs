namespace TaskManagerSample.Core.Models;

public class Task : Entity
{
    public Guid UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DueDate { get; set; }

    public string Status { get; set; }

    /* EF Relations */
    public User User { get; set; }
}