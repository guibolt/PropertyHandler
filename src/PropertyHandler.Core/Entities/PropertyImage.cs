using PropertyHandler.Core.Interfaces.Entities;
using System;

namespace PropertyHandler.Core.Entities
{
    public class PropertyImage : BaseEntity, IPropertyCompose
    {
        public string Name { get; set; }
        public string FileType { get; set; }
        public int PropertyId { get; set; }
        public Guid FileId { get; set; }
    }
}