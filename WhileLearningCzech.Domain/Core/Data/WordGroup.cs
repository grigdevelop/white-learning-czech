using System.Collections.Generic;

namespace WhileLearningCzech.Domain.Core.Data
{
    public class WordGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }        

        public virtual ICollection<Word> Words { get; set; }
            = new List<Word>();
    }
}
