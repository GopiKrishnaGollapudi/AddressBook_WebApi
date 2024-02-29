using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace AddressBookAPI.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactController : ControllerBase
{
    private IAddressService AddressService;
    public ContactController(IAddressService _AddressService)
    {
        this.AddressService = _AddressService;
    }

    [HttpGet]
    public List<ContactDetailsDto> Get()
    {
        List<ContactDetailsDto> contacts = AddressService.GetContacts();
        return contacts;
    }

    [HttpGet("{contactId}")]
    public ContactDto Get(int contactId)
    {
        ContactDto contact = AddressService.GetContact(contactId: contactId);
        return contact;
    }

    [HttpPost]
    public IActionResult AddContact([FromBody]ContactDto contact)
    {
        if (ModelState.IsValid)
        {
            AddressService.AddContact(contact: contact);
            return NoContent();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpPut("{contactId}")]
    public IActionResult EditContact(int contactId,[FromBody] ContactDto contact)
    {
        AddressService.EditContact(contactId: contactId,contact: contact);
        return NoContent();
    }

    [HttpDelete("{contactId}")]
    public IActionResult Delete(int contactId)
    {
        AddressService.DeleteContact(contactId: contactId);
        return NoContent();
    }
}