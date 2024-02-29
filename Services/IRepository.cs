
public interface IContactRepository
{
    List<Contact> GetContacts();
    Contact GetContact(int contactId);
    void AddContact(Contact contact);
    void EditContact(int contactId, Contact contact);
    void DeleteContact(int contactId);
}