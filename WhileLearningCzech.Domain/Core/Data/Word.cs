﻿using System.ComponentModel.DataAnnotations.Schema;
using WhileLearningCzech.Domain.Core.Abstract;

namespace WhileLearningCzech.Domain.Core.Data
{
    public class Word : IEntity
    {
        public int Id { get; set; }

        public string Czech { get; set; }

        public string English { get; set; }

        public int? WordGroupId { get; set; }

        [ForeignKey("WordGroupId")]
        public WordGroup WordGroup { get; set; }
    }
}
