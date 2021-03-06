﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps.module.IDAL
{
    public interface IDbSession
    {
        IUserDal UserDal { get; }

        IAdjustDal AdjustDal { get; }

        int SaveChanges();
    }
}
