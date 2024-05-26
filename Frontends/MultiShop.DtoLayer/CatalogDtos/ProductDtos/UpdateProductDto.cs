using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.CatalogDtos.ProductDtos
{
    public class UpdateProductDto
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public Decimal ProductPrice { get; set; }
        public string ProductImgUrl { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryID { get; set; }
    }
}
