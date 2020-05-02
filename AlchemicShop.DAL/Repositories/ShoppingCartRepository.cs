﻿using AlchemicShop.DAL.AlchemicDbContext;
using AlchemicShop.DAL.Entities;
using AlchemicShop.DAL.Interfaces;
using System.Linq;

namespace AlchemicShop.DAL.Repositories
{
    class ShoppingCartRepository : IShoppingCart<Order>
    {
        private AlchemicShopContext _dbContext;

        public ShoppingCartRepository(AlchemicShopContext context)
        {
            _dbContext = context;
        }

        public int GetMax()
        {
            return _dbContext.Orders.Max(w => w.Id);

        }
    }
}
