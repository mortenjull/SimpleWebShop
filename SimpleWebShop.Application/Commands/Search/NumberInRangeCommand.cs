using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SimpleWebShop.Application.Commands.Search
{
    public class NumberInRangeCommand : IRequest<bool>
    {
        public NumberInRangeCommand(int number)
        {
            this.Number = number;
        }

        public int Number { get; set; }
    }

    public class NumberInRangeCommandHandler : IRequestHandler<NumberInRangeCommand, bool>
    {
        public async Task<bool> Handle(NumberInRangeCommand request, CancellationToken cancellationToken)
        {
            int min = 0;
            int max = 100;
            int number = request.Number;

            if (number >= min && number <= max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
