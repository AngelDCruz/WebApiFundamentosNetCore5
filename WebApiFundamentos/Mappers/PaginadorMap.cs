using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFundamentos.DTOs;
using WebApiFundamentos.Models;

namespace WebApiFundamentos.Mappers
{
    public class PaginadorMap : Profile
    {
        public PaginadorMap()
        {
            CreateMap<Paginador, PaginadorDTO>()
                .ForMember(pagDTO => pagDTO.Pagina, pag => pag.MapFrom(prop => prop.Pagina))
                .ForMember(pagDTO => pagDTO.RegistrosPorPagina, pag => pag.MapFrom(prop => prop.RegistrosPagina))
                .ReverseMap();
        }
     }
  }

