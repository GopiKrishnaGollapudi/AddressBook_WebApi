using System;
using System.ComponentModel.DataAnnotations;
public class Contact
{
    public int ContactId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Mobile number is required")]
    [Phone(ErrorMessage = "Invalid Phone Number")]
    public string Mobile { get; set; }

    [Required(ErrorMessage = "Landline number is required")]
    [Phone(ErrorMessage = "Invalid Landline Number")]
    public string Landline { get; set; }

    [Url(ErrorMessage = "Invalid website URL")]
    public string Website { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }
}