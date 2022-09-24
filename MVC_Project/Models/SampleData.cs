using System.Collections.Generic;
using System.Linq;

namespace MVC_Project.Models
{
    //Model Business logic For Class
    public class SampleData
    {
        List<Product> products =new List<Product>();
        public SampleData()
        {
            products.Add(new Product(){ Id = 1, Name = "IPhone1", Price = 1000, Description = "Is Good1" ,img="1.png"});
            products.Add(new Product() { Id = 2, Name = "IPhone2", Price = 2000, Description = "Is Good2", img = "2.png" });
            products.Add(new Product() { Id = 3, Name = "IPhone3", Price = 3000, Description = "Is Good3" , img = "3.png" });
        }
        public List<Product> GetAll()
        {
            return products;
        }
        public Product getById(int id)
        {
            return products.FirstOrDefault(d => d.Id == id);
        }
    }
}
