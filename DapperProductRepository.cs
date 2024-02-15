using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyBestPractices;
public class DapperProductRepository : IProductRepository
{
    private readonly IDbConnection _conn;
    public DapperProductRepository(IDbConnection conn) 
    { 
        _conn = conn;
    }
    public IEnumerable<Product> GetAllProducts() 
    {
        return _conn.Query<Product>("SELECT * FROM products;");
    }

    Product IProductRepository.GetProductById(int id)
    {
        return _conn.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id;",
            new { id = id });
    }

    void IProductRepository.UpdateProduct(Product product)
    {
        _conn.Execute("UPDATE products SET Name = @name, Price = @price, CAtegoryID = @catID, OnSale = @onsale, StockLevel = @stock, Where ProductID = @id;",
            new{name = product.Name, price = product.Price, catID = product.CategoryID, onsale = product.OnSale, stock = product.StockLevel, id = product.ProductID });
    
    }
}
