namespace EShop.MVC.Data.Domain.Base
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}