using Application.DTOs;
using Application.Feautures.Clientes.Commands.CreateClienteCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region DTOs

            CreateMap<Cliente, ClienteDTO>();

            #endregion

            #region Commands

            CreateMap<CreateClienteCommand, Cliente>();

            #endregion
        }
    }
}
