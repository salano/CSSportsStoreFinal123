using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models.Pages;


namespace SportsStore.Models.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }

        PagedList<Category> GetCategories(QueryOptions options);

        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
