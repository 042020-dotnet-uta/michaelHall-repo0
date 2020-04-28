﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RPS_Game;

namespace RPS_Game.Migrations
{
    [DbContext(typeof(RPS_DbContext))]
    [Migration("20200427205144_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("RPS_Game.Game", b =>
                {
                    b.Property<int>("GameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("p1PlayerID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("p2PlayerID")
                        .HasColumnType("INTEGER");

                    b.HasKey("GameID");

                    b.HasIndex("p1PlayerID");

                    b.HasIndex("p2PlayerID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("RPS_Game.Player", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("PlayerID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("RPS_Game.Round", b =>
                {
                    b.Property<int>("RoundID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GameID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("WinnerPlayerID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("p1Choice")
                        .HasColumnType("TEXT");

                    b.Property<string>("p2Choice")
                        .HasColumnType("TEXT");

                    b.HasKey("RoundID");

                    b.HasIndex("GameID");

                    b.HasIndex("WinnerPlayerID");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("RPS_Game.Game", b =>
                {
                    b.HasOne("RPS_Game.Player", "p1")
                        .WithMany()
                        .HasForeignKey("p1PlayerID");

                    b.HasOne("RPS_Game.Player", "p2")
                        .WithMany()
                        .HasForeignKey("p2PlayerID");
                });

            modelBuilder.Entity("RPS_Game.Round", b =>
                {
                    b.HasOne("RPS_Game.Game", null)
                        .WithMany("Rounds")
                        .HasForeignKey("GameID");

                    b.HasOne("RPS_Game.Player", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerPlayerID");
                });
#pragma warning restore 612, 618
        }
    }
}
