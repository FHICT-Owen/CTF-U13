﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MissionSystem.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MissionSystem.Data.Migrations
{
    [DbContext(typeof(DataStore))]
    partial class DataStoreModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GadgetMatch", b =>
                {
                    b.Property<string>("GadgetsMacAddress")
                        .HasColumnType("text");

                    b.Property<int>("MatchesId")
                        .HasColumnType("integer");

                    b.HasKey("GadgetsMacAddress", "MatchesId");

                    b.HasIndex("MatchesId");

                    b.ToTable("GadgetMatch");
                });

            modelBuilder.Entity("MissionSystem.Interface.Models.Arena", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("GadgetMacAddress")
                        .HasColumnType("text");

                    b.Property<int?>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GadgetMacAddress");

                    b.ToTable("Arenas");
                });

            modelBuilder.Entity("MissionSystem.Interface.Models.Gadget", b =>
                {
                    b.Property<string>("MacAddress")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("MacAddress");

                    b.HasIndex("MacAddress")
                        .IsUnique();

                    b.HasIndex("TypeId");

                    b.ToTable("Gadgets");
                });

            modelBuilder.Entity("MissionSystem.Interface.Models.GadgetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GadgetTypes");
                });

            modelBuilder.Entity("MissionSystem.Interface.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArenaId")
                        .HasColumnType("integer");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("GameTypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsEnglish")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArenaId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("GadgetMatch", b =>
                {
                    b.HasOne("MissionSystem.Interface.Models.Gadget", null)
                        .WithMany()
                        .HasForeignKey("GadgetsMacAddress")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MissionSystem.Interface.Models.Match", null)
                        .WithMany()
                        .HasForeignKey("MatchesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MissionSystem.Interface.Models.Arena", b =>
                {
                    b.HasOne("MissionSystem.Interface.Models.Gadget", null)
                        .WithMany("Arenas")
                        .HasForeignKey("GadgetMacAddress");
                });

            modelBuilder.Entity("MissionSystem.Interface.Models.Gadget", b =>
                {
                    b.HasOne("MissionSystem.Interface.Models.GadgetType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("MissionSystem.Interface.Models.Match", b =>
                {
                    b.HasOne("MissionSystem.Interface.Models.Arena", "Arena")
                        .WithMany()
                        .HasForeignKey("ArenaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Arena");
                });

            modelBuilder.Entity("MissionSystem.Interface.Models.Gadget", b =>
                {
                    b.Navigation("Arenas");
                });
#pragma warning restore 612, 618
        }
    }
}
