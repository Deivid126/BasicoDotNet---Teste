using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class UpdateAvisoHandler : IRequestHandler<UpdateAvisoRequest, IOperationResult<UpdateAvisoResponse>>
    {
        private readonly IServiceProvider _serviceProvider;
        private IContext _context => _serviceProvider.GetRequiredService<IContext>();
        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();
        public UpdateAvisoHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<IOperationResult<UpdateAvisoResponse>> Handle(UpdateAvisoRequest request, CancellationToken cancellationToken)
        {
            var aviso = await _avisoRepository.ObterAvisoPorIdAsync(request.Id);

            if (aviso == null)
                return OperationResult<UpdateAvisoResponse>.ReturnNotFound();

            aviso.Mensagem = request.Mensagem;
            aviso.DataAtualizacao = DateTime.Now;

            _avisoRepository.Update(aviso);
            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult<UpdateAvisoResponse>.ReturnOk((UpdateAvisoResponse)aviso);
        }
    }
}
