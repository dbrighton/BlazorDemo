﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common;


public static class Extensions
{
    public static string ToJson(this object obj)
    {
        return obj == null ? string.Empty : JsonConvert.SerializeObject(obj,Formatting.Indented );
    }
}
