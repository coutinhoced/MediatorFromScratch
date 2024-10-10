using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorFromScratch.Sample
{
    public class CheckIsEvenOddHandler : IHandler<CheckIsEvenOddRequest, string>
    {
        public Task<string> HandleAsync(CheckIsEvenOddRequest request)
        {
            string _output = string.Empty;
            if (request.Number % 2 == 0)
            {
                _output = "Even";
            }
            else
            {
                _output = "Odd";
            }

            return Task.FromResult(_output);
        }
    }
}
