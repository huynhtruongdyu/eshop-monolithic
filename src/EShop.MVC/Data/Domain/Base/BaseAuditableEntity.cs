﻿namespace EShop.MVC.Data.Domain.Base
{
    public abstract class BaseAuditableEntity : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}