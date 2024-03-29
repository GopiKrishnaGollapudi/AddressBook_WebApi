using System;
using System.ComponentModel.DataAnnotations;
public class ContactDetailsDto
{
    public int ContactId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }
}