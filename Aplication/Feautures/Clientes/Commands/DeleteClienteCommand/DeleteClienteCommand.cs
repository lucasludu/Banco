using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautures.Clientes.Commands.DeleteClienteCommand
{
    public class DeleteClienteCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;

        public DeleteClienteCommandHandler(IRepositoryAsync<Cliente> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repositoryAsync.GetByIdAsync(request.Id);

            if (cliente == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el ID: {request.Id}");
            }
            else
            {
                await _repositoryAsync.UpdateAsync(cliente);
                return new Response<int>(cliente.Id);
            }
        }
    }
}
