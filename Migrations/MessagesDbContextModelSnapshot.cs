﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PosApiJwt.Data;

namespace PosApiJwt.Migrations
{
    [DbContext(typeof(MessagesDbContext))]
    partial class MessagesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("PosApiJwt.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("From")
                        .HasColumnType("TEXT");

                    b.Property<string>("To")
                        .HasColumnType("TEXT");

                    b.HasKey("MessageId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("PosApiJwt.Models.MsgBody", b =>
                {
                    b.Property<int>("MsgBodyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Msg")
                        .HasColumnType("TEXT");

                    b.Property<string>("Stamp")
                        .HasColumnType("TEXT");

                    b.HasKey("MsgBodyId");

                    b.ToTable("MsgBody");
                });

            modelBuilder.Entity("PosApiJwt.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Blocked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TS")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PosApiJwt.Models.MsgBody", b =>
                {
                    b.HasOne("PosApiJwt.Models.Message", "Message")
                        .WithOne("MsgBody")
                        .HasForeignKey("PosApiJwt.Models.MsgBody", "MsgBodyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");
                });

            modelBuilder.Entity("PosApiJwt.Models.Message", b =>
                {
                    b.Navigation("MsgBody");
                });
#pragma warning restore 612, 618
        }
    }
}