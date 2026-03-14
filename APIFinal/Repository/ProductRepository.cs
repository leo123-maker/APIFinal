using System;
using APIFinal.Models;
using APIFinal.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

namespace APIFinal.Repository;

public class ProductRepository : IProductRepository
{

    private readonly AplicationDBContext _db;

    public ProductRepository(AplicationDBContext db)
    {
        _db = db;
    }
    public bool BuyProduct(string name, int price)
    {
        if(string.IsNullOrWhiteSpace(name) || price == 0)
        {
            return false;
        }

        var product = _db.Products.FirstOrDefault(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
        if(product == null || product.Stock < price)
        {
            return false;
        }

        product.Stock -= price;
        _db.Products.Update(product);
        return save();
    }

    public bool CreateProduct(Product product)
    {
        if(product == null)
        {
            return false;
        }
        product.CreationDate = DateTime.Now;
        product.UpdateDate = DateTime.Now;
        _db.Products.Add(product);
        return save();
    }

    public bool DeleteProduct(Product product)
    {
        if(product == null)
        {
            return false;
        }
        _db.Products.Remove(product);
        return save();
    }

    public Product? GetProduct(int id)
    {
        if(id <= 0)
        {
            return null;
        }
        return _db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);

    }

    public ICollection<Product> GetProducts()
    {
        return _db.Products.Include(p => p.Category).OrderBy(p => p.Name).ToList();
    }

    public ICollection<Product> GetProductsForCategory(int id)
    {
        if(id <= 0)
        {
            return new List<Product>();
        }
        return _db.Products.Include(p => p.Category).Where(p => p.CategoryId == id).OrderBy(p => p.Name).ToList();
    }

    public bool ProductExists(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            return false;
        }
        return _db.Products.Any(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public bool ProductExists(int id)
    {
        if(id <= 0)
        {
            return false;
        }
        return _db.Products.Any(p => p.ProductId == id);
    }

    public bool save()
    {
        return _db.SaveChanges() >= 0;
    }

    public ICollection<Product> SearchProducts(string searchTerm)
    {
        IQueryable<Product> query = _db.Products;

        var searchTermLower = searchTerm.ToLower().Trim();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Include(p => p.Category).Where(
                p => p.Name.ToLower().Trim().Contains(searchTermLower) ||
                p.Description.ToLower().Trim().Contains(searchTermLower));
        }
        return query.OrderBy(p => p.Name).ToList();
    }

    public bool UpdateProduct(Product product)
    {
        if(product == null)
        {
            return false;
        }
        product.UpdateDate = DateTime.Now;
        _db.Products.Update(product);
        return save();
    }
}
