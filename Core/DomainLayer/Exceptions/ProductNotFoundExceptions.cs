using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class ProductNotFoundExceptions(int id) : NotFoundExpceptions($"Product with Id = {id} Not Found!")
    {
    }
}
