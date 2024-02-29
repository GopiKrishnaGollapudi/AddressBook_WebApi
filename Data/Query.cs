using System;

public class Query
{
    public const string GetContacts = "SELECT ContactId,Name,Email,Address FROM Contact";
    public const string GetContact = "SELECT * FROM Contact WHERE ContactId= ";
    public const string AddContact = "INSERT INTO Contact (Name,Email,Mobile,Landline,Website,Address) VALUES (@Name,@Email,@Mobile,@Landline,@Website,@Address)";
    public const string UpdateContact = "UPDATE Contact SET Name = @Name, Email = @Email, Mobile = @Mobile , Landline = @Landline, Website = @Website, Address= @Address WHERE ContactId = ";
    public const string DeleteContact = "DELETE FROM Contact WHERE ContactId=";

}