using MediatorFromScratch.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace MediatorFromScratch.Sample
{
    public class Program
    {
        static void Main(string[] arg)
        {
            //Requets - response approach
            //Following represents the flow
            //request => mediator => handler => response

            //Add mediator to the Service Collection
            var serviceProvider = new ServiceCollection()
                                  .AddMediator(ServiceLifetime.Scoped, typeof(Program))
                                  .BuildServiceProvider();


            //var handlerDetails = new Dictionary<Type, Type>();
            //handlerDetails.Add(typeof(PrintToConsoleRequest), typeof(PrintToConsoleHandler));

            //Example 1
            //Request
            var request = new PrintToConsoleRequest
            {
                Text = "Hello from Mediator"
            };
                      
            //Get mediator Instance
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            //Send request via the mediator to receive the response
            var result = mediator.SendAsync(request);

            //Example 2
            //Request
            var request1 = new CheckIsEvenOddRequest
            {
                Number = 2
            };

            var result2 = mediator.SendAsync(request1);


            Console.ReadLine();

            
        }
    }
}