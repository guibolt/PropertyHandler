using PropertyHandler.Core.Interfaces.Entities;

namespace PropertyHandler.Core.Entities
{
    public class Details : BaseEntity, IPropertyCompose
    {
        public int PropertySize { get; set; }
        public int BedRoomQuantity { get; set; }
        public int CarVacancyQuantity { get; set; }
        public int BathRoomQuantity { get; set; }
        public int PropertyId { get; set; }
    }
}
