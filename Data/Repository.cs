using System;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

public class ContactRepository : IContactRepository
{
    private readonly IDbConnection _dbConnection;
    public ContactRepository(IConfiguration config)
    {
        this._dbConnection = new SqlConnection(config.GetConnectionString("connection"));
    }

    public List<Contact> GetContacts()
    {
        List<Contact> contacts = this._dbConnection.Query<Contact>(Query.GetContacts).ToList();
        return contacts;
    }

    public Contact GetContact(int contactId)
    {
        string getContact = Query.GetContact + contactId;
        Contact contact = this._dbConnection.QueryFirstOrDefault<Contact>(getContact);
        return contact;
    }

    public void AddContact(Contact contact)
    {
        this._dbConnection.Query(Query.AddContact, contact);
    }

    public void EditContact(int contactId, Contact contact)
    {
        string updateContactQuery = Query.UpdateContact + contactId.ToString();
        this._dbConnection.Execute(updateContactQuery, contact);
    }

    public void DeleteContact(int contactId)
    {
        string deleteContactQuery = Query.DeleteContact + contactId;
        this._dbConnection.Execute(deleteContactQuery);
    }
}