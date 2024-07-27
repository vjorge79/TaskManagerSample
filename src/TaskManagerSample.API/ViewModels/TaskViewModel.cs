using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManagerSample.API.ViewModels;

public class TaskViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DefaultValue("")]
    public Guid UserId { get; set; }

    [DefaultValue("")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Title { get; set; }

    [DefaultValue("")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Description { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public DateTime DueDate { get; set; }

    [DefaultValue("")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Status { get; set; }
}