using AutoMapper;
using TesteGenialNet.Business.Commands.Produtos;
using TesteGenialNet.Business.Entity;
using TesteGenialNet.Business.Entitys;
using TesteGenialNet.Business.Helpers;
using TesteGenialNet.Business.Interop.Dtos;
using TesteGenialNet.Business.Interop.ViewModels;

namespace TesteGenialNet.API.AutoMappers
{
    public class ProdutoMapper : Profile
    {
        public ProdutoMapper()
        {
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();

            CreateMap<ProdutoViewModel, ProdutoFornecedor>()
                .ForMember(dest => dest.Produto, opt => opt.MapFrom(x => x));

            CreateMap<ProdutoDeleteViewModel, ProdutoViewModel>();

            CreateMap<ProdutoViewModel, InsertProdutoCommand>();
            CreateMap<ProdutoViewModel, UpdateProdutoCommand>();

            CreateMap<InsertProdutoCommand, Produto>();
            CreateMap<UpdateProdutoCommand, Produto>();

            CreateMap<Produto, ProdutoDto>()
                .ForMember(dest => dest.UnidadeMedida, opt => opt.MapFrom(x => x.UnidadeMedida.GetDescription()));

        }
    }
}
