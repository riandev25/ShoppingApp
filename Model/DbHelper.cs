using ShoppingApp.Controllers;
using ShoppingApp.EfCore;


namespace ShoppingApp.Model
{
    public class DbHelper
    {
        private EF_DataContext _context;
        public DbHelper(EF_DataContext context)
        {
            _context = context;
        }
        public List<ProductModel> GetProducts()
        {
            List<ProductModel> response = new List<ProductModel>();
            var dataList = _context.Products.ToList();
            dataList.ForEach(row => response.Add(new ProductModel()
            {
                Brand = row.Brand,
                Id = row.Id,
                Name = row.Name,
                Price = row.Price,
                Size = row.Size
            }));
            return response;
        }
        public ProductModel GetProductById(int Id)
        {
            var row = _context.Products.Where(d => d.Id.Equals(Id)).FirstOrDefault();
            return new ProductModel()
            {
                Brand = row.Brand,
                Id = row.Id,
                Name = row.Name,
                Price = row.Price,
                Size = row.Size
            };
        }
        public void SaveOrder(OrderModel orderModel)
        {
            Order dbTable = new Order();
            {
                // PUT
                dbTable = _context.Orders.Where(d => d.Id.Equals(orderModel.Id)).FirstOrDefault();
                if (dbTable != null)
                {
                    dbTable.Phone = orderModel.Phone;
                    dbTable.Address = orderModel.Address;

                } else
                {
                    // POST
                    dbTable.Phone = orderModel.Phone;
                    dbTable.Address = orderModel.Address;
                    dbTable.Name = orderModel.Name;
                    dbTable.Product = _context.Products.Where(f => f.Id.Equals(orderModel.ProductId)).FirstOrDefault();
                    _context.Orders.Add(dbTable);
                }
                _context.SaveChanges();
            }
        }

        public void DeleteOrder(int Id)
        {
            var order = _context.Orders.Where(d => d.Id.Equals(Id)).FirstOrDefault();
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
