using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.BasketModuleDtos
{
    public class BasketDto
    {
        public string id { get; set; }
        public ICollection<BasketItemDto> items { get; set; }
    }
}
