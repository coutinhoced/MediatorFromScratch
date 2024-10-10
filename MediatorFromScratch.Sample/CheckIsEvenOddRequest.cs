using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorFromScratch.Sample
{
    public class CheckIsEvenOddRequest : IRequest<string>
    {
        public int Number { get; set; }
    }
}
