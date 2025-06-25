namespace EShop.MVC.Data.Domain.Base
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IEntity
    {
        Guid Id { get; set; }
    }
}