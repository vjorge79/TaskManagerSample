namespace TaskManagerSample.Core.Models;

public class User : Entity
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }

    /* EF Relations */
    public IEnumerable<Core.Models.Task> Tasks { get; set; }
}