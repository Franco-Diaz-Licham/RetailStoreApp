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
    public string AppUserId { get; set; }
    public UserEntity AppUser { get; set; }
}
