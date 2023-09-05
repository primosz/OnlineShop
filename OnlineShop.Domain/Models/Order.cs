﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderRef { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
