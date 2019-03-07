﻿// <auto-generated />
using System;
using Gah.Patterns.Todo.Repository.Sql.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gah.Patterns.Todo.Repository.Sql.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190307053621_Initial")]
    partial class Initial
    {
        /// <summary>
        /// Implemented to builds the <see cref="P:Microsoft.EntityFrameworkCore.Migrations.Migration.TargetModel" />.
        /// </summary>
        /// <param name="modelBuilder">The <see cref="T:Microsoft.EntityFrameworkCore.ModelBuilder" /> to use to build the model.</param>
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Gah.Patterns.Todo.Domain.ToDoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsDone");

                    b.Property<Guid>("ListId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.ToTable("ToDoItems");
                });

            modelBuilder.Entity("Gah.Patterns.Todo.Domain.ToDoList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("TotalCompleted");

                    b.Property<int>("TotalItems");

                    b.Property<int>("TotalPending");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Title");

                    b.ToTable("ToDoLists");
                });
#pragma warning restore 612, 618
        }
    }
}
