﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IEntityRep
    {
        string Id { get; set; }
        string InstitutionId { get; set; }

    }
}