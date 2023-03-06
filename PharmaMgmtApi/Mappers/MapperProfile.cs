using AutoMapper;
using PharmaMgmtApi.Models;
using PharmaMgmtApi.ViewModels.Drugs;
using PharmaMgmtApi.ViewModels.Orders;
using PharmaMgmtApi.ViewModels.Users;

namespace PharmaMgmtApi.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserCreateViewModel>().ReverseMap();
        
        CreateMap<User, UserViewModel>()
            .ForMember(dto => dto.ImageUrl,
                expression => expression.MapFrom(entity => "https://pharmacy-app-management.herokuapp.com//" + entity.ImagePath)).ReverseMap();

        CreateMap<Drug, DrugCreateViewModel>()
            .ForMember(dto => dto.ImageUrl,
                expression => expression.MapFrom(entity => entity.ImagePath)).ReverseMap();
        
        CreateMap<Drug, DrugViewModel>()
            .ForMember(dto => dto.ImageUrl,
                expression => expression.MapFrom(entity => entity.ImagePath)).ReverseMap();

        CreateMap<Order, OrderCreateViewModel>().ReverseMap();
        
        CreateMap<Order, OrderViewModel>().ReverseMap();
        
        CreateMap<IEnumerable<OrderViewModel>, IEnumerable<Order>>();
        
        CreateMap<Order, OrderViewModel>()
            .ForMember(dto => dto.UserFullName,
                expression => expression.MapFrom(entity => entity.User.FirstName + " " + entity.User.LastName))
            .ForMember(dto => dto.DrugName,
                expression => expression.MapFrom(entity => entity.Drug.Name))
            .ForMember(dto => dto.TotalSum,
                expression => expression.MapFrom(entity => entity.Drug.Price * entity.Quantity));
    }
}