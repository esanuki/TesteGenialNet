using AutoMapper;
using TesteGenialNet.Business.Commands.Fornecedores;
using TesteGenialNet.Business.Entitys;
using TesteGenialNet.Business.Helpers;
using TesteGenialNet.Business.Interop.ViewModels;

namespace TesteGenialNet.API.AutoMappers
{
    public class FornecedorMapper : Profile
    {
        public FornecedorMapper()
        {
            CreateMap<FornecedorViewModel, InsertProdutoFornecedorCommand>()
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(x => x.CNPJ.FormatCnpj()));
            CreateMap<FornecedorViewModel, UpdateProdutoFornecedorCommand>()
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(x => x.CNPJ.FormatCnpj()));
            CreateMap<FornecedorDeleteViewModel, DeleteProdutosFornecedorCommand>();
            
            CreateMap<InsertProdutoFornecedorCommand, Fornecedor>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone))
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => src.CNPJ))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco))
                .ForMember(dest => dest.Produtos, opt => opt.Ignore());

            CreateMap<UpdateProdutoFornecedorCommand, Fornecedor>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone))
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => src.CNPJ))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco))
                .ForMember(dest => dest.Produtos, opt => opt.Ignore());

        }
    }
}
