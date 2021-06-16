using MediatR;

namespace Acorisoft.Extensions.Windows.Services
{
    public partial interface IViewService
    {
        
    }
    
    public partial class ViewService : IViewService
    {
        private readonly IMediator _mediator;
        
        public ViewService(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}