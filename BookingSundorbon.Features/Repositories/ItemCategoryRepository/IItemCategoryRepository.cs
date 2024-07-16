using BookingSundorbon.Views.DTOs.ItemCategoryView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ItemCategoryRepository
{
    public interface IItemCategoryRepository
    {
        Task<int> CreateItemCategoryAsync(ItemCategoryView itemCategory);
        Task<ItemCategoryView> GetItemCategoryAsync(int id);
        Task<IEnumerable<ItemCategoryView>> GetAllActiveItemCategoriesAsync();
        Task UpdateItemCategoryAsync(ItemCategoryView itemCategory);
        Task DeleteItemCategoryAsync(int id);
    }
}
