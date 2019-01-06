using System.ComponentModel.DataAnnotations;
using WhileLearningCzech.Domain.Core.Abstract;

namespace WhileLearningCzech.Domain.Core.Data
{
    public class Article : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
