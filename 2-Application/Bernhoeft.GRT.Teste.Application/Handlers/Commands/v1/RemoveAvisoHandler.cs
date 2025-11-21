using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    internal class RemoveAvisoHandler : IRequestHandler<RemoveAvisoRequest, Unit>
    {

        private readonly IServiceProvider _serviceProvider;
        private IContext _context => _serviceProvider.GetRequiredService<IContext>();
        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();

        public RemoveAvisoHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<Unit> Handle(RemoveAvisoRequest request, CancellationToken cancellationToken)
        {
            var entity = await _avisoRepository.ObterAvisoPorIdAsync(request.Id);
            if (entity == null)
                throw new Exception("Aviso não encontrado.");

            entity.Ativo = !entity.Ativo;
            entity.DataAtualizacao = DateTime.Now;
            _avisoRepository.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
