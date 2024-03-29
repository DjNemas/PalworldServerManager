﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PalworldServerManagerClient.Database;

#nullable disable

namespace PalworldServerManagerClient.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240201233239_PropertyNamingFix")]
    partial class PropertyNamingFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("PalworldServerManagerClient.Models.DBModel.ServerInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("IPAddresse")
                        .HasColumnType("TEXT");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ServerName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ServerInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
