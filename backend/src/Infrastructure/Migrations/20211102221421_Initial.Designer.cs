﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211102221421_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Domain.Board", b =>
                {
                    b.Property<Guid>("BoardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BgColor")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("OwnerUserId")
                        .HasColumnType("uuid");

                    b.HasKey("BoardId");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Domain.Card", b =>
                {
                    b.Property<Guid>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BelongsToCardGroupId")
                        .HasColumnType("uuid");

                    b.Property<int>("Color")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.HasKey("CardId");

                    b.HasIndex("BelongsToCardGroupId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Domain.CardGroup", b =>
                {
                    b.Property<Guid>("CardGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BoardId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("CardGroupId");

                    b.HasIndex("BoardId");

                    b.ToTable("CardGroups");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Board", b =>
                {
                    b.HasOne("Domain.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerUserId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Card", b =>
                {
                    b.HasOne("Domain.CardGroup", "BelongsTo")
                        .WithMany("Cards")
                        .HasForeignKey("BelongsToCardGroupId");

                    b.Navigation("BelongsTo");
                });

            modelBuilder.Entity("Domain.CardGroup", b =>
                {
                    b.HasOne("Domain.Board", null)
                        .WithMany("CardGroups")
                        .HasForeignKey("BoardId");
                });

            modelBuilder.Entity("Domain.Board", b =>
                {
                    b.Navigation("CardGroups");
                });

            modelBuilder.Entity("Domain.CardGroup", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
