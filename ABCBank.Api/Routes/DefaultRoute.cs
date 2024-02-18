using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBank.Api.Routes
{
    public abstract class Routes
    {
        public const String Default="[action]";
        public const String Parameter="{Id}";
        public const String ControllerParameter="{[controller]Id}";
    }
}