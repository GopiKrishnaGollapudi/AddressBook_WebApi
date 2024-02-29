public interface IAddressService
{
    List<ContactDetailsDto> GetContacts();
    ContactDto GetContact(int contactId);
    void AddContact(ContactDto contact);
    void EditContact(int contactId,ContactDto contact);
    void DeleteContact(int contactId);
}