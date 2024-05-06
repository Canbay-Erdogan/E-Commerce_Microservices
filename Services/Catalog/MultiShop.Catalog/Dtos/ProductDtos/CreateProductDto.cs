namespace MultiShop.Catalog.Dtos.ProductDtos
{
    public class CreateProductDto
    {
       
        public string ProductName { get; set; }
        public Decimal ProductPrice { get; set; }
        public string ProductImgUrl { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryID { get; set; }
    }
}
