﻿using AlchemicShop.BLL.DTO;
using AlchemicShop.BLL.Infrastructure;
using AlchemicShop.BLL.Interfaces;
using AlchemicShop.DAL.Interfaces;
using AlchemicShop.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using AlchemicShop.BLL.Helpers;
using AutoMapper;

namespace AlchemicShop.BLL.Services
{
    public class OrderProductService : IOrderProductService
    {
        private readonly IUnitOfWork _dbOperation;
        private readonly IMapper _mapper;
        public OrderProductService(
            IMapper mapper,
            IUnitOfWork uow)
        {
            _dbOperation = uow;
            _mapper = mapper;
        }

        public void AddOrderProduct(OrderProductDTO orderProductDTO)
        {
            var product = _dbOperation.Products.Get(orderProductDTO.ProductId);
            var order = _dbOperation.Orders.Get(orderProductDTO.OrderId);
            // валидация
            if (product == null)
            {
                throw new ValidationException("Продукт не найден", "");
            }
            if (order == null)
            {
                throw new ValidationException("Заказ не найден", "");
            }
            OrderProduct orderProduct = new OrderProduct
            {
                ProductId = product.Id,
                Product = product,
                Amount = orderProductDTO.Amount,
                OrderId = order.Id,
                Order = order
            };
            _dbOperation.OrderProducts.Create(orderProduct);
            _dbOperation.Save();
        }
        public IEnumerable<OrderProductDTO> GetOrderProducts()
        {
            return _mapper.Map<List<OrderProduct>, List<OrderProductDTO>>(_dbOperation.OrderProducts.GetAll().ToList());
        }

        public OrderProductDTO GetOrderProduct(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не установлено id заказанного продукта", "");
            }
            var orderProduct = _dbOperation.OrderProducts.Get(id.Value);
            if (orderProduct == null)
            {
                throw new ValidationException("Заказаный продукт не найден", "");
            }

            var orderProductDTO = _mapper.Map<OrderProduct, OrderProductDTO>(orderProduct);
            return orderProductDTO;
        }
        public void Dispose()
        {
            _dbOperation.Dispose();
        }
    }
}
