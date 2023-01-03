﻿// <auto-generated />
using System;
using HackedNews.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HackedNews.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20230103203816_1.0.1")]
    partial class _101
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HackedNews.Data.Models.NewsModel.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("HackedNews.Data.Models.NewsModel.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NewsId")
                        .HasColumnType("int");

                    b.Property<int?>("ResponseToCommentId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.HasIndex("ResponseToCommentId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("HackedNews.Data.Models.NewsModel.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorNews")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImgLoad")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("NewsIntroduction")
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<string>("ShorttextCard")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<bool>("SwitchLoadImg")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("HackedNews.Data.Models.NewsModel.NewsData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImgLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImgLoad")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("NewsId")
                        .HasColumnType("int");

                    b.Property<string>("Subtitle")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<bool>("SwitchLoadImg")
                        .HasColumnType("bit");

                    b.Property<string>("Txt")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsData");
                });

            modelBuilder.Entity("HackedNews.Data.Models.NewsModel.Comment", b =>
                {
                    b.HasOne("HackedNews.Data.Models.NewsModel.News", "News")
                        .WithMany("CommentList")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HackedNews.Data.Models.NewsModel.Comment", "ResponseToComment")
                        .WithMany()
                        .HasForeignKey("ResponseToCommentId");

                    b.Navigation("News");

                    b.Navigation("ResponseToComment");
                });

            modelBuilder.Entity("HackedNews.Data.Models.NewsModel.News", b =>
                {
                    b.HasOne("HackedNews.Data.Models.NewsModel.Category", "Category")
                        .WithMany("ListNews")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("HackedNews.Data.Models.NewsModel.NewsData", b =>
                {
                    b.HasOne("HackedNews.Data.Models.NewsModel.News", "News")
                        .WithMany("ListNewsDatas")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("News");
                });

            modelBuilder.Entity("HackedNews.Data.Models.NewsModel.Category", b =>
                {
                    b.Navigation("ListNews");
                });

            modelBuilder.Entity("HackedNews.Data.Models.NewsModel.News", b =>
                {
                    b.Navigation("CommentList");

                    b.Navigation("ListNewsDatas");
                });
#pragma warning restore 612, 618
        }
    }
}
