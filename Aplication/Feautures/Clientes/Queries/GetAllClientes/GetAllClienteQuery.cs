using Application.DTOs;
using Application.Interfaces;
using Application.Specification;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Application.Feautures.Clientes.Queries.GetAllClientes
{
    public class GetAllClienteQuery : IRequest<PagedResponse<List<ClienteDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
    }

    public class GetAllClienteQueryHandler : IRequestHandler<GetAllClienteQuery, PagedResponse<List<ClienteDTO>>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;

        public GetAllClienteQueryHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<PagedResponse<List<ClienteDTO>>> Handle(GetAllClienteQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"listadoClientes_{request.PageSize}_{request.PageNumber}_{request.Nombre}_{request.Apellido}";
            string serializedListardoClientes;
            var listadoClientes = new List<Cliente>();
            var redisListadoClientes = await _distributedCache.GetAsync(cacheKey);

            if(redisListadoClientes != null)
            {
                serializedListardoClientes = Encoding.UTF8.GetString(redisListadoClientes);
                listadoClientes = JsonConvert.DeserializeObject<List<Cliente>>(serializedListardoClientes);
            }
            else
            {
                listadoClientes = await _repositoryAsync.ListAsync(
                    new PagedClientesSpecification(request.PageSize, request.PageNumber, request.Nombre, request.Apellido)
                );
                serializedListardoClientes = JsonConvert.SerializeObject(listadoClientes);
                redisListadoClientes = Encoding.UTF8.GetBytes(serializedListardoClientes);

                var ops = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                await _distributedCache.SetAsync(cacheKey, redisListadoClientes, ops);
            }

            var clientesDto = _mapper.Map<List<ClienteDTO>>(listadoClientes);

            return new PagedResponse<List<ClienteDTO>>(clientesDto, request.PageNumber, request.PageSize);
        }
    }
}
