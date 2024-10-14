﻿using AutoMapper;
using KidsFashion.Dominio;
using KidsFashion.Models;

namespace KidsFashion.AutoMapper
{
    public class EntidadeParaViewModelMappingProfile : Profile
    {
        public EntidadeParaViewModelMappingProfile()
        {
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Municipio, MunicipioViewModel>().ReverseMap();
        }
    }
}
