namespace Tax.Models
{
    public class LineItemModel
    {
        public string Id { get; set; }
        public int? Quantity { get; set; }
        public string ProductTaxCode { get; set; }
        public double? UnitPrice { get; set; }
        public double? Discount { get; set; }
    }
}