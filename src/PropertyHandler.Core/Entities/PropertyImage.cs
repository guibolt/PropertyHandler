using PropertyHandler.Core.Interfaces.Entities;

namespace PropertyHandler.Core.Entities
{
    public class PropertyImage : BaseEntity, IPropertyCompose
    {
        public string Name { get; set; }
        public string FileType { get; set; }
        public int PropertyId { get; set; }
    }
}