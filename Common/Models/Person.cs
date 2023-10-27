namespace Common.Models;

/// <summary>
/// Represents a person with a first name, last name, and unique NbkId.
/// </summary>
public class Person
{
    /// <summary>
    /// The person's first name.
    /// </summary>
    [Required(ErrorMessage = "Please enter a first name.")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters long.")]
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// The person's last name.
    /// </summary>
    [Required(ErrorMessage = "Please enter a last name.")]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long.")]
    public string LastName { get; set; } = null!;

    /// <summary>
    /// The person's unique NbkId.
    /// </summary>
    [Required(ErrorMessage = "Please enter an NbkId.")]
    public string NbkId { get; set; } = null!;

    /// <summary>
    /// The person's full name.
    /// </summary>
    public string Name => $"{FirstName} {LastName}";

    public int Bet { get; set; }
}