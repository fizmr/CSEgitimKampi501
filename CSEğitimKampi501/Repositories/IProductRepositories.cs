using CSEğitimKampi501.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEğitimKampi501.Repositories
{
    public interface IProductRepositories
    {
        Task<List<ResultProductDto>> GetAllProductASync();
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(int id);
        Task GetByProductIDAsync(int id);
    }
}
