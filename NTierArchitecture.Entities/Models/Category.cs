using NTierArchitecture.Entities.Models;

public sealed class Category
{
    //sealed ne demek
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }  //ICollection ne demek
}                                       
