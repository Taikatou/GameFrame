﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCreatePokemon.World
{
    internal interface IComponent
    {
        bool Killed { get; }
    }
}
