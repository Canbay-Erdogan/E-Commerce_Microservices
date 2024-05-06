﻿using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.EntityFrameworks
{
    public class EFCargoOperationDal : GenericRepository<CargoOperation>, ICargoOperationDal
    {
        public EFCargoOperationDal(CargoContext context) : base(context)
        {
        }
    }
}
