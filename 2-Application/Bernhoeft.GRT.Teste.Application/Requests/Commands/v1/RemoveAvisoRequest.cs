using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1
{
    public class RemoveAvisoRequest: IRequest<Unit>
    {
        public int Id { get; set; }
    }
}