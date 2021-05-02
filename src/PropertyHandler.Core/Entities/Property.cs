using PropertyHandler.Core.Enums;
using System.Collections.Generic;

namespace PropertyHandler.Core.Entities
{
    public class Property : BaseEntity
    {
        public int Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal RentPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TaxPrice { get; set; }
        public decimal CondominiumPrice { get; set; }
        public string OwnerName { get; set; }
        public bool IsSpotlight { get; set; }
        public Address Address { get; set; }
        public Details Details { get; set; }
        public IEnumerable<PropertyImage> Images { get; set; }
        public EPropertyStatus Status { get; set; }
        public EPropertyType Type { get; set; }
        public string SpecificType { get; set; }

    }
}
