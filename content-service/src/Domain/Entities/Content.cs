using Domain.Common;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Content: BaseEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public Guid UserId { get; set; }


        protected Content(){}

        internal Content(string title, string body, Guid userId )
        {
            Id = Guid.NewGuid();
            Title = title.Trim();
            Body = body;
            UserId = userId;
        }

        internal void Update(string title, string body, Guid userId)
        {
            Title = title.Trim();
            Body = body;
            UserId = userId;
        }

        internal void Delete()
        {
            IsDeleted = true;
            DeletedDate = DateTime.Now;
        }
    }
}
