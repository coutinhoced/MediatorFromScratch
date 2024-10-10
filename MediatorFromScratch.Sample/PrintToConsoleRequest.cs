﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorFromScratch.Sample
{
    public class PrintToConsoleRequest : IRequest<bool>
    {
        public string Text { get; init; }

    }
}
