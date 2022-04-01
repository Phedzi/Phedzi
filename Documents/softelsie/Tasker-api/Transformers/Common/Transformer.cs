using AutoMapper;
using Models;
using Models.Squata;
using RepModels;
using RepModels.Squata;

namespace Transformers.Common
{
    public class Transformer : Profile
    {
        public Transformer()
        {
            CreateMap<Test,TestModel>().ReverseMap();
            CreateMap<Province,ProvinceModel>().ReverseMap();
            CreateMap<City,CityModel>().ReverseMap();
            CreateMap<Site,SiteModel>().ReverseMap();
            CreateMap<Place,PlaceModel>().ReverseMap();
            CreateMap<Image,ImageModel>().ReverseMap();
            CreateMap<User,UserModel>().ReverseMap();
            CreateMap<RoomType ,RoomTypeModel>().ReverseMap();

        }
    }
}
