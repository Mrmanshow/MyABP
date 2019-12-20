using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Game
{
    public interface ILabaOrderManger
    {
        Task<int> CreateLabaOrder(LabaOrder order, string routes);

        Task<string> LabaOrderCal(int orderId);

    }
}
