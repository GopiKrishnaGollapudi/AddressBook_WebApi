using System;
using AutoMapper;
using System.Collections.Generic;

public class AddressService : IAddressService
{
    private IContactRepository contactRepository;
    private IMapper mapper;

    public AddressService(IContactRepository _contactRepository, IMapper _mapper)
    {
        this.contactRepository = _contactRepository;
        this.mapper = _mapper;
    }
    public List<ContactDetailsDto> GetContacts()
    {
        List<Contact> contacts = contactRepository.GetContacts();
        return mapper.Map<List<ContactDetailsDto>>(contacts);
    }

    public ContactDto GetContact(int contactId)
    {
        Contact contact = contactRepository.GetContact(contactId: contactId);
        return mapper.Map<ContactDto>(contact);
    }

    public void AddContact(ContactDto contactDto)
    {
        Contact contact=mapper.Map<Contact>(contactDto);
        contactRepository.AddContact(contact: contact);
    }

    public void EditContact(int contactId, ContactDto contactDto)
    {
        Contact contact=mapper.Map<Contact>(contactDto);
        contactRepository.EditContact(contactId: contactId, contact: contact);
    }

    public void DeleteContact(int contactId)
    {
        contactRepository.DeleteContact(contactId: contactId);
    }
}