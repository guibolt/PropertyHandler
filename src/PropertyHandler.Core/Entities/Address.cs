using PropertyHandler.Core.Interfaces.Entities;

namespace PropertyHandler.Core.Entities
{
    public class Address : BaseEntity, IPropertyCompose
    {
        public string Street { get; set; }
        public int LocationNumber { get; set; }
        public string Cep { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PropertyId { get; set; }
    }
}