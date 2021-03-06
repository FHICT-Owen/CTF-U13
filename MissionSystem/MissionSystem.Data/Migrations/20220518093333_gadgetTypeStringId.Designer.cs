// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MissionSystem.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MissionSystem.Data.Migrations
{
    [DbContext(typeof(DataStore))]
    [Migration("20220518093333_gadgetTypeStringId")]
    partial class gadgetTypeStringId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ArenaGadget", b =>
                {
                    b.Property<int>("ArenasId")
                        .HasColumnType("integer");

                    b.Property<string>("GadgetsMacAddress")
                        .HasColumnType("text");

                    b.HasKey("ArenasId", "GadgetsMacAddress");

                    b.HasIndex("GadgetsMacAddress");

                    b.ToTable("ArenaGadget");
                });

            modelBuilder.Entity("MissionSystem.Data.Models.Arena", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Arenas");
                });

            modelBuilder.Entity("MissionSystem.Data.Models.Gadget", b =>
                {
                    b.Property<string>("MacAddress")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("MacAddress");

                    b.HasIndex("TypeId");

                    b.ToTable("Gadgets");
                });

            modelBuilder.Entity("MissionSystem.Data.Models.GadgetType", b =>
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

                    b.HasIndex("RefId")
                        .IsUnique();

                    b.ToTable("GadgetTypes");
                });

            modelBuilder.Entity("ArenaGadget", b =>
                {
                    b.HasOne("MissionSystem.Data.Models.Arena", null)
                        .WithMany()
                        .HasForeignKey("ArenasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MissionSystem.Data.Models.Gadget", null)
                        .WithMany()
                        .HasForeignKey("GadgetsMacAddress")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MissionSystem.Data.Models.Gadget", b =>
                {
                    b.HasOne("MissionSystem.Data.Models.GadgetType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });
#pragma warning restore 612, 618
        }
    }
}
