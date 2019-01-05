using System.Collections.Generic;
using WhileLearningCzech.Domain.Core.Abstract;

namespace WhileLearningCzech.Domain.Core.Data
{
    public class WordGroup : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }        

        public virtual ICollection<Word> Words { get; set; }
            = new List<Word>();
    }
}
