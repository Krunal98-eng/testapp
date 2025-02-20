using AutoMapper;
using Voip2.Application.Profiles;
using VoipProjectEntities.Application.Features.Categories.Commands.CreateCateogry;
using VoipProjectEntities.Application.Features.Categories.Queries.GetCategoriesList;
using VoipProjectEntities.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using VoipProjectEntities.Application.Features.Customers.Commands.CreateCustomer;
using VoipProjectEntities.Application.Features.Customers.Commands.UpdateCustomer;
using VoipProjectEntities.Application.Features.Customers.Queries.GetCustomerById;
using VoipProjectEntities.Application.Features.Customers.Queries.GetCustomerList;
using VoipProjectEntities.Application.Features.Events.Commands.CreateEvent;
using VoipProjectEntities.Application.Features.Events.Commands.UpdateEvent;
using VoipProjectEntities.Application.Features.Events.Queries.GetEventDetail;
using VoipProjectEntities.Application.Features.Events.Queries.GetEventsExport;
using VoipProjectEntities.Application.Features.Events.Queries.GetEventsList;
using VoipProjectEntities.Application.Features.Menu.Commands.CreateMenu;
using VoipProjectEntities.Application.Features.Menu.Queries.GetMenu;
using VoipProjectEntities.Application.Features.Orders.GetOrdersForMonth;
using VoipProjectEntities.Domain.Entities;

namespace VoipProjectEntities.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();
            CreateMap<Event, EventExportDto>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryEventListVm>();
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<Category, CreateCategoryDto>();

            CreateMap<Order, OrdersForMonthDto>();

            CreateMap<Event, EventListVm>().ConvertUsing<EventVmCustomMapper>();

            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
            CreateMap<Customer, CustomerDetailVm>().ReverseMap();

            CreateMap<Customer, CustomerListVm>().ConvertUsing<CustomerVmCustomMapper>();

            CreateMap<MenuAccess, CreateMenuCommand>();
            CreateMap<MenuAccess, CreateMenuDto>();

            CreateMap<MenuAccess, MenuListVm>().ConvertUsing<MenuVmCustomMapper>();
        }
    }
}
