

namespace Application.DTOs
{
    public sealed class ContentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
