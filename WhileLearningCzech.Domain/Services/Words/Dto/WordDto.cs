using System.ComponentModel.DataAnnotations;
using WhileLearningCzech.Domain.Core.Abstract;
using WhileLearningCzech.Domain.Services.WordGroups.Dto;

namespace WhileLearningCzech.Domain.Services.Words.Dto
{
    public class WordDto : IEntityDto
    {
        public int Id { get; set; }

        [Required]
        public string Czech { get; set; }

        [Required]
        public string English { get; set; }

        public int? WordGroupId { get; set; }

        public virtual WordGroupDto WordGroup { get; set; }
    }
}
