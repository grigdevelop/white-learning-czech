using System;
using System.ComponentModel.DataAnnotations;
using WhileLearningCzech.Domain.Core.Abstract;

namespace PersonalWebsite.Domain.Core.Data
{
    public class Article : IEntity, IEquatable<Article>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? DatePublished { get; set; }
      
        #region IEquatable implementation

        public bool Equals(Article other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(Title, other.Title);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Article)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (Title != null ? Title.GetHashCode() : 0);
            }
        }

        #endregion

        #region Operators

        public static bool operator ==(Article article1, Article article2) => article1?.Equals(article2) ?? false;
        public static bool operator !=(Article article1, Article article2) => !(article1 == article2);

        #endregion
    }    
}
