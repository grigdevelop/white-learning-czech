using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhileLearningCzech.Domain.Core.Abstract;

namespace WhileLearningCzech.Domain.Core.Data
{
    public class Image : IEntity,  IEquatable<Image>, IHasId
    {
        public int Id { get; set; }       

        public byte[] Data { get; set; }

        public string DataType { get; set; }

        public int? ArticleId { get; set; }


        #region IEquatable implementation

        public bool Equals(Image other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Image)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        #endregion

        #region Operators

        public static bool operator ==(Image image1, Image image2) => image1?.Equals(image2) ?? false;
        public static bool operator !=(Image image1, Image image2) => !(image1 == image2);

        #endregion       
    }

    internal class ImageMapper : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);            
        }
    }
}
