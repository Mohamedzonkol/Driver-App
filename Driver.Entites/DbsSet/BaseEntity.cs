﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Entites.DbsSet
{
    public class BaseEntity
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime UpdateDateTime{ get; set; }= DateTime.Now;
        public int Status { get; set; }
    }
}
