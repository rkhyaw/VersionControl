using progtervmintak.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace progtervmintak.Entities
{
    public class CarFactory : iToyFactory
    {
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}
