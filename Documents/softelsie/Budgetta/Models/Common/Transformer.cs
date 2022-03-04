using AutoMapper;
using Models.Budget;
using Models.Model;

namespace Models.Common
{
    public class Transformer : Profile
    {
        public Transformer()
        {
           CreateMap<User, UserModel>().ReverseMap();
           CreateMap<Model.Budget, BudgetModel>().ReverseMap();
           CreateMap<Category, CategoryModel>().ReverseMap();
           CreateMap<Item, ItemModel>().ReverseMap();
           CreateMap<ItemType, ItemTypeModel>().ReverseMap();
        }
    }
}
