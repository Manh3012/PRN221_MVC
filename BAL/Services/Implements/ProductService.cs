using System;
using System.Linq;
using System.Text;
using DAL.Infacstucture;
using System.Threading.Tasks;
using DAL.Repositories.Interface;
using System.Collections.Generic;
using DAL.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace BAL.Services.Implements
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService()
        {
        }

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public List<Product> Filter(int id)
        {
            using (var context = new FRMDbContext())
            {
                var products = context.Product
                                .Where(p => p.Category.ID == id)
                                .ToList();
                return products;
            }
        }

        public List<Product> Search(string searchItems)
        {
            using (var context = new FRMDbContext())
            {
                var results = context.Product
                                .Where(p => p.Name.Contains(searchItems))
                                .ToList();
                return results;
            }
        }
    }
}
