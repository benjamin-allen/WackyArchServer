﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WackyArchServer.Model;

#nullable disable

namespace WackyArchServer.Migrations
{
    [DbContext(typeof(WAContext))]
    [Migration("20220919013433_UpdateKeys")]
    partial class UpdateKeys
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("WackyArchServer.Model.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Passwordhash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WackyArchServer.Model.AlphaChallenge", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Flag")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("InputTextJson")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OutputTextJson")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PredecessorId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PredecessorId");

                    b.ToTable("AlphaChallenges");
                });

            modelBuilder.Entity("WackyArchServer.Model.AlphaChallengeTest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AlphaChallengeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("InputTextJson")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OutputTextJson")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AlphaChallengeId");

                    b.ToTable("AlphaChallengeTests");
                });

            modelBuilder.Entity("WackyArchServer.Model.RunLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ChallengeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CompletedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SubmittedTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubmitterAccountId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SubmitterAccountId");

                    b.ToTable("RunLogs");
                });

            modelBuilder.Entity("WackyArchServer.Model.AlphaChallenge", b =>
                {
                    b.HasOne("WackyArchServer.Model.AlphaChallenge", "Predecessor")
                        .WithMany()
                        .HasForeignKey("PredecessorId");

                    b.Navigation("Predecessor");
                });

            modelBuilder.Entity("WackyArchServer.Model.AlphaChallengeTest", b =>
                {
                    b.HasOne("WackyArchServer.Model.AlphaChallenge", "AlphaChallenge")
                        .WithMany()
                        .HasForeignKey("AlphaChallengeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AlphaChallenge");
                });

            modelBuilder.Entity("WackyArchServer.Model.RunLog", b =>
                {
                    b.HasOne("WackyArchServer.Model.Account", "SubmitterAccount")
                        .WithMany()
                        .HasForeignKey("SubmitterAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubmitterAccount");
                });
#pragma warning restore 612, 618
        }
    }
}
