using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautures.Clientes.Queries.GetClienteByIdQuery
{
    public class GetClienteByIdQuery : IRequest<Response<ClienteDTO>>
    {
        public int Id { get; set; }
    }

    public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, Response<ClienteDTO>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetClienteByIdQueryHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<ClienteDTO>> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _repositoryAsync.GetByIdAsync(request.Id);

            if (cliente == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el ID: {request.Id}");
            }
            else
            {
                var dto = _mapper.Map<ClienteDTO>(cliente);
                return new Response<ClienteDTO>(dto);
            }
        }
    }
}
