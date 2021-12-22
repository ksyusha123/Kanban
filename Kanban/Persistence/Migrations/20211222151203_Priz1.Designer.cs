﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(KanbanDbContext))]
    [Migration("20211222151203_Priz1")]
    partial class Priz1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ColumnColumn", b =>
                {
                    b.Property<Guid>("NextStatesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PrevStatesId")
                        .HasColumnType("uuid");

                    b.HasKey("NextStatesId", "PrevStatesId");

                    b.HasIndex("PrevStatesId");

                    b.ToTable("ColumnColumn");
                });

            modelBuilder.Entity("Domain.Board", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Board");
                });

            modelBuilder.Entity("Domain.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BoardId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ColumnId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<Guid?>("ExecutorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("ColumnId");

                    b.HasIndex("ExecutorId");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("Domain.Chat", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<int>("App")
                        .HasColumnType("integer");

                    b.Property<string>("BoardId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("Domain.Column", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BoardId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Column");
                });

            modelBuilder.Entity("Domain.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CardId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CardId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Domain.Executor", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("TelegramUsername")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Executor");
                });

            modelBuilder.Entity("ColumnColumn", b =>
                {
                    b.HasOne("Domain.Column", null)
                        .WithMany()
                        .HasForeignKey("NextStatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Column", null)
                        .WithMany()
                        .HasForeignKey("PrevStatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Board", b =>
                {
                    b.OwnsMany("Domain.ExecutorsWithRights", "ExecutorsWithRights", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("BoardId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("ExecutorId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Rights")
                                .HasColumnType("integer");

                            b1.HasKey("Id");

                            b1.HasIndex("BoardId");

                            b1.ToTable("ExecutorsWithRights");

                            b1.WithOwner()
                                .HasForeignKey("BoardId");
                        });

                    b.Navigation("ExecutorsWithRights");
                });

            modelBuilder.Entity("Domain.Card", b =>
                {
                    b.HasOne("Domain.Board", null)
                        .WithMany("Cards")
                        .HasForeignKey("BoardId");

                    b.HasOne("Domain.Column", "Column")
                        .WithMany()
                        .HasForeignKey("ColumnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Executor", "Executor")
                        .WithMany()
                        .HasForeignKey("ExecutorId");

                    b.Navigation("Column");

                    b.Navigation("Executor");
                });

            modelBuilder.Entity("Domain.Column", b =>
                {
                    b.HasOne("Domain.Board", null)
                        .WithMany("States")
                        .HasForeignKey("BoardId");
                });

            modelBuilder.Entity("Domain.Comment", b =>
                {
                    b.HasOne("Domain.Executor", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Card", null)
                        .WithMany("Comments")
                        .HasForeignKey("CardId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Domain.Board", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("States");
                });

            modelBuilder.Entity("Domain.Card", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
