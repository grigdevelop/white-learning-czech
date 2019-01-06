using System.ComponentModel.DataAnnotations;
using WhileLearningCzech.Domain.Core.Abstract;

namespace WhileLearningCzech.Domain.Services.WordGroups.Dto
{
    public class WordGroupDto : IEntityDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
