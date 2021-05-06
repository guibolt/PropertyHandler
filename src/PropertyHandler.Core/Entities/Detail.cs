using PropertyHandler.Core.Interfaces.Entities;
using System;

namespace PropertyHandler.Core.Entities
{
    public class Detail : BaseEntity, IPropertyCompose
    {
        public int PropertySize { get; set; }
        public int BedRoomQuantity { get; set; }
        public int CarVacancyQuantity { get; set; }
        public int BathRoomQuantity { get; set; }
        public int PropertyId { get; set; }

        public static implicit operator Detail(Address v)
        {
            throw new NotImplementedException();
        }
    }
}
