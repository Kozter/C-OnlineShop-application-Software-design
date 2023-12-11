using OnlineShoppApp.Data;
using System;
using System.Diagnostics;

namespace OnlineShoppApp.Service
{
    public class OrderLineService : IOrderLineService
    {
        private readonly IOrderLineRepository _repository;
        public OrderLineService(IOrderLineRepository orderLine)
        {
            _repository = orderLine;
        }

        public async Task CreateAsync(OrderLine orderLine)
        {
            try
            {
               await _repository.AddAsync(orderLine);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e, $"An error occurred while persisting new order line");
            }
        }

        public async Task<OrderLine> GetOrderLineAsync(int Id)
        {
            try
            {
               return await _repository.GetOrderLineAsync(Id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e, $"An error occurred while fetching order line with id {Id}");
                return null;
            }
        }
    }
}
