using System;
using System.ComponentModel.DataAnnotations;
using WhileLearningCzech.Domain.Core.Abstract;

namespace WhileLearningCzech.Domain.Services.Articles.Dto
{
    public class ArticleDto : IEntityDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? DatePublished { get; set; }

        public ArticleDto()
        {
            DatePublished = DateTime.UtcNow;
        }
    }
}
