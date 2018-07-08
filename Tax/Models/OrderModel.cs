using System.Collections.Generic;

namespace Tax.Models
{
    public class OrderModel
    {
        public LocationModel FromLocation { get; set; }
        public LocationModel ToLocation { get; set; }
        public double? Amount { get; set; }
        public double? Shipping { get; set; }
        public string CustomerId { get; set; }
        public List<LocationModel> NexusAddresses { get; set; }
        public List<LineItemModel> LineItems { get; set; }
    }
}