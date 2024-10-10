using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorFromScratch
{
    public class Mediator : IMediator
    {
        //We are preventing any reference to DI Container in the Mediator Library

        private readonly Func<Type, object> _serviceResolver;

        // Need to keep track of all the request we can handle
        //A request will be mapped to a single handler 
        //A request comes in; we are getting a type; I have to match to something to get the Hnadler 
        //This information will be supplied up-front
        private readonly IDictionary<Type, Type> _handlerDetails;

        public Mediator(Func<Type, object> serviceResolver, IDictionary<Type, Type> handlerDetails)
        {
            this._serviceResolver = serviceResolver;
            this._handlerDetails = handlerDetails;
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();
            if (!_handlerDetails.ContainsKey(requestType))
            {
                throw new Exception($"No handler to handle the request of type: {requestType.Name}");
            }
            _handlerDetails.TryGetValue(requestType, out var requestedHandlerType);
            var handler = _serviceResolver(requestedHandlerType);

            return await (Task<TResponse>)handler!.GetType()!.GetMethod("HandleAsync")!.Invoke(handler, new[] { request });
        }
    }
}
