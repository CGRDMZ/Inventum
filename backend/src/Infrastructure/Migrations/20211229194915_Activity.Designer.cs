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
    [Migration("20211229194915_Activity")]
    partial class Activity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Domain.Activity", b =>
                {
                    b.Property<Guid>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BelongsToBoardId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DoneByUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<DateTime>("OccuredOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ActivityId");

                    b.HasIndex("BelongsToBoardId");

                    b.HasIndex("DoneByUserId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("Domain.Board", b =>
                {
                    b.Property<Guid>("BoardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

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
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BelongsToCardGroupId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.HasKey("CardId");

                    b.HasIndex("BelongsToCardGroupId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Domain.CardGroup", b =>
                {
                    b.Property<Guid>("CardGroupId")
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

            modelBuilder.Entity("Domain.Activity", b =>
                {
                    b.HasOne("Domain.Board", "BelongsTo")
                        .WithMany("Activities")
                        .HasForeignKey("BelongsToBoardId");

                    b.HasOne("Domain.User", "DoneBy")
                        .WithMany()
                        .HasForeignKey("DoneByUserId");

                    b.Navigation("BelongsTo");

                    b.Navigation("DoneBy");
                });

            modelBuilder.Entity("Domain.Board", b =>
                {
                    b.HasOne("Domain.User", "Owner")
                        .WithMany("Boards")
                        .HasForeignKey("OwnerUserId");

                    b.OwnsOne("Domain.Color", "BgColor", b1 =>
                        {
                            b1.Property<Guid>("BoardId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Blue")
                                .HasColumnType("integer");

                            b1.Property<int>("Green")
                                .HasColumnType("integer");

                            b1.Property<int>("Red")
                                .HasColumnType("integer");

                            b1.HasKey("BoardId");

                            b1.ToTable("Boards");

                            b1.WithOwner()
                                .HasForeignKey("BoardId");
                        });

                    b.Navigation("BgColor");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Card", b =>
                {
                    b.HasOne("Domain.CardGroup", "BelongsTo")
                        .WithMany("Cards")
                        .HasForeignKey("BelongsToCardGroupId");

                    b.OwnsOne("Domain.Color", "Color", b1 =>
                        {
                            b1.Property<Guid>("CardId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Blue")
                                .HasColumnType("integer");

                            b1.Property<int>("Green")
                                .HasColumnType("integer");

                            b1.Property<int>("Red")
                                .HasColumnType("integer");

                            b1.HasKey("CardId");

                            b1.ToTable("Cards");

                            b1.WithOwner()
                                .HasForeignKey("CardId");
                        });

                    b.Navigation("BelongsTo");

                    b.Navigation("Color");
                });

            modelBuilder.Entity("Domain.CardGroup", b =>
                {
                    b.HasOne("Domain.Board", null)
                        .WithMany("CardGroups")
                        .HasForeignKey("BoardId");
                });

            modelBuilder.Entity("Domain.Board", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("CardGroups");
                });

            modelBuilder.Entity("Domain.CardGroup", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("Boards");
                });
#pragma warning restore 612, 618
        }
    }
}
