using System;
using APIFinal.Models;

namespace APIFinal.Repository.IRepository;

public interface IProductRepository
{
    ICollection<Product> GetProducts();

    ICollection<Product> GetProductsForCategory(int id);

    ICollection<Product> SearchProducts(string searchTerm);

    Product? GetProduct(int id);

    bool BuyProduct(string name, int price);

    bool ProductExists(string name);

    bool ProductExists(int id);

    bool CreateProduct(Product product);

    bool UpdateProduct(Product product);

    bool DeleteProduct(Product product);

    bool save();


}
