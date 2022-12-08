﻿// <auto-generated />
using System;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(KnowDbContext))]
    [Migration("20221102081209_ChangedSchema")]
    partial class ChangedSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.7.22376.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Model.Creator", b =>
                {
                    b.Property<long>("CreatorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("CreatorId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("SoftDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.HasKey("CreatorId");

                    b.HasAlternateKey("Email");

                    b.ToTable("Creators");

                    b.HasComment("Пользователи базы знаний");
                });

            modelBuilder.Entity("DataAccess.Model.Post", b =>
                {
                    b.Property<long>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("PostId"));

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<long>("FirstCreatorId")
                        .HasColumnType("bigint");

                    b.Property<bool>("SoftDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("State")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(64)")
                        .HasDefaultValue("Черновик");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(128)");

                    b.HasKey("PostId");

                    b.HasIndex("FirstCreatorId");

                    b.ToTable("Posts");

                    b.HasComment("Статьи в базе знаний");
                });

            modelBuilder.Entity("DataAccess.Model.Tag", b =>
                {
                    b.Property<string>("TagId")
                        .HasColumnType("varchar(124)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");

                    b.HasComment("Список тегов");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<long>("PostsPostId")
                        .HasColumnType("bigint");

                    b.Property<string>("TagsTagId")
                        .HasColumnType("varchar(124)");

                    b.HasKey("PostsPostId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("DataAccess.Model.Post", b =>
                {
                    b.HasOne("DataAccess.Model.Creator", "FirstCreator")
                        .WithMany("CreatedPosts")
                        .HasForeignKey("FirstCreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FirstCreator");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("DataAccess.Model.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Model.Creator", b =>
                {
                    b.Navigation("CreatedPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
