﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gham.Models
{
    [Flags]
    public enum UserPrivilege
    {
        Registered = 1,
        Admin = 2,
    }
}
