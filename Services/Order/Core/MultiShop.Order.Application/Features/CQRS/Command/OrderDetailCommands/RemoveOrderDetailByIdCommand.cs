using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Command.OrderDetailCommands
{
    public class RemoveOrderDetailByIdCommand
    {
        public int Id { get; set; }

        public RemoveOrderDetailByIdCommand(int id)
        {
            Id = id;
        }
    }
}
