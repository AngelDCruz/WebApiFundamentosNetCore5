using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFundamentos.DTOs;
using WebApiFundamentos.Models;

namespace WebApiFundamentos.Mappers
{
    public class ComentariosMap : Profile
    {
        public ComentariosMap()
        {
            CreateMap<Comentarios, ComentariosDTO>()
                .ForMember(cmDTO => cmDTO.Id, cm => cm.MapFrom(pro => pro.Id))
                .ForMember(cmDTO => cmDTO.Contenido, cm => cm.MapFrom(pro => pro.Descripcion))
                .ReverseMap(); 
           
            CreateMap<Comentarios, ComentarioNuevoDTO>()
                .ForMember(cmDTO => cmDTO.IdLibro, cm => cm.MapFrom(pro => pro.LibroId))
                .ForMember(cmDTO => cmDTO.Contenido, cm => cm.MapFrom(pro => pro.Descripcion))
                .ReverseMap();
        }
    }
}
