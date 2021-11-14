﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(KanbanDbContext))]
    partial class KanbanDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Domain.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<Guid?>("TaskId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("TaskId");

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

            modelBuilder.Entity("Domain.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Domain.State", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("TableId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("State");
                });

            modelBuilder.Entity("Domain.Table", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ProjectId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectId1");

                    b.ToTable("Table");
                });

            modelBuilder.Entity("Domain.Task", b =>
                {
                    b.Property<Guid>("Id")
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

                    b.Property<Guid>("StateId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TableId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ExecutorId");

                    b.HasIndex("StateId");

                    b.HasIndex("TableId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("StateState", b =>
                {
                    b.Property<Guid>("NextStatesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PrevStatesId")
                        .HasColumnType("uuid");

                    b.HasKey("NextStatesId", "PrevStatesId");

                    b.HasIndex("PrevStatesId");

                    b.ToTable("StateState");
                });

            modelBuilder.Entity("Domain.Comment", b =>
                {
                    b.HasOne("Domain.Executor", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Task", null)
                        .WithMany("Comments")
                        .HasForeignKey("TaskId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Domain.State", b =>
                {
                    b.HasOne("Domain.Table", null)
                        .WithMany("States")
                        .HasForeignKey("TableId");
                });

            modelBuilder.Entity("Domain.Table", b =>
                {
                    b.HasOne("Domain.Project", null)
                        .WithMany("Tables")
                        .HasForeignKey("ProjectId");

                    b.HasOne("Domain.Project", null)
                        .WithMany("_tables")
                        .HasForeignKey("ProjectId1");

                    b.OwnsMany("Domain.ExecutorsWithRights", "ExecutorsWithRights", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("ExecutorId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Rights")
                                .HasColumnType("integer");

                            b1.Property<Guid>("TableId")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("TableId");

                            b1.ToTable("ExecutorsWithRights");

                            b1.WithOwner()
                                .HasForeignKey("TableId");
                        });

                    b.Navigation("ExecutorsWithRights");
                });

            modelBuilder.Entity("Domain.Task", b =>
                {
                    b.HasOne("Domain.Executor", "Executor")
                        .WithMany()
                        .HasForeignKey("ExecutorId");

                    b.HasOne("Domain.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Table", null)
                        .WithMany("Tasks")
                        .HasForeignKey("TableId");

                    b.Navigation("Executor");

                    b.Navigation("State");
                });

            modelBuilder.Entity("StateState", b =>
                {
                    b.HasOne("Domain.State", null)
                        .WithMany()
                        .HasForeignKey("NextStatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.State", null)
                        .WithMany()
                        .HasForeignKey("PrevStatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Project", b =>
                {
                    b.Navigation("_tables");

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("Domain.Table", b =>
                {
                    b.Navigation("States");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Domain.Task", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
