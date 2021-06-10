﻿// <auto-generated />
using System;
using GalaxyMerge.Client.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GalaxyMerge.Client.Data.Migrations
{
    [DbContext(typeof(AppContext))]
    [Migration("20210610142744_InitialBuild")]
    partial class InitialBuild
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10");

            modelBuilder.Entity("GalaxyMerge.Client.Data.Entities.AppLog", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Callsite")
                        .HasColumnType("TEXT");

                    b.Property<string>("Exception")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Identity")
                        .HasColumnType("TEXT");

                    b.Property<string>("Level")
                        .HasColumnType("TEXT");

                    b.Property<int>("LevelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LineNumber")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Logged")
                        .HasColumnType("TEXT");

                    b.Property<string>("Logger")
                        .HasColumnType("TEXT");

                    b.Property<string>("MachineName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<string>("Properties")
                        .HasColumnType("TEXT");

                    b.Property<string>("Stacktrace")
                        .HasColumnType("TEXT");

                    b.HasKey("LogId");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("GalaxyMerge.Client.Data.Entities.GalaxyResource", b =>
                {
                    b.Property<int>("ResourceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.Property<string>("GalaxyName")
                        .HasColumnType("TEXT");

                    b.Property<string>("NodeName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResourceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ResourceType")
                        .HasColumnType("INTEGER");

                    b.HasKey("ResourceId");

                    b.ToTable("Resource");
                });
#pragma warning restore 612, 618
        }
    }
}