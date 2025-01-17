namespace RetailStore.Server.Models.Entities;

public class AddressEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zipcode { get; set; }
    public string UserEntityId { get; set; }
    public UserEntity UserEntity { get; set; }
}
