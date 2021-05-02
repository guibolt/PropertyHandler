using System;

namespace PropertyHandler.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool Active { get; set; }
    }
}
