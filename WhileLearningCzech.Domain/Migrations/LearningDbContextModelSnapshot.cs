﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WhileLearningCzech.Domain.Core;

namespace WhileLearningCzech.Domain.Migrations
{
    [DbContext(typeof(LearningDbContext))]
    partial class LearningDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WhileLearningCzech.Domain.Core.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WhileLearningCzech.Domain.Core.Data.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Czech");

                    b.Property<string>("English");

                    b.Property<int?>("WordGroupId");

                    b.HasKey("Id");

                    b.HasIndex("WordGroupId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("WhileLearningCzech.Domain.Core.Data.WordGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("WordGroups");
                });

            modelBuilder.Entity("WhileLearningCzech.Domain.Core.Data.Word", b =>
                {
                    b.HasOne("WhileLearningCzech.Domain.Core.Data.WordGroup", "WordGroup")
                        .WithMany("Words")
                        .HasForeignKey("WordGroupId");
                });
#pragma warning restore 612, 618
        }
    }
}
