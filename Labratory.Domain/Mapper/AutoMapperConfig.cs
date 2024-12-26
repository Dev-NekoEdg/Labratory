using AutoMapper;
using Labratory.Domain.Dtos;
using Labratory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Domain.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ListsDto, Lists>().ReverseMap();
            CreateMap<ListsItemsDto, ListsItems>().ReverseMap();
            CreateMap<ListsItemsDto, ListsItemsJoinSet>().ReverseMap();
        }
    }
}
