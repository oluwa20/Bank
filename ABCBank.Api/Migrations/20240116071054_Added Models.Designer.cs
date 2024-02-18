﻿// <auto-generated />
using System;
using ABCBank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ABCBank.Api.Migrations
{
    [DbContext(typeof(ABCBankDbContext))]
    [Migration("20240116071054_Added Models")]
    partial class AddedModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.15");

            modelBuilder.Entity("ABCBank.Domain.Models.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("AccountBalance")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("AccountCreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<double>("AccountDailyLimit")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("AccountExpiryDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccountStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AccountUpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("BankName")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.HasKey("AccountId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ABCBank.Domain.Models.CustomerAccount", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Bvn")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("CountryOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FamilyName")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("HashPassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("HomeAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nationality")
                        .HasColumnType("TEXT");

                    b.Property<string>("NearestLandmark")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nuban")
                        .HasColumnType("TEXT");

                    b.Property<string>("OtherNationality")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Residence")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ABCBank.Domain.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ReceivingAccount")
                        .HasColumnType("TEXT");

                    b.Property<double>("TransactionAmount")
                        .HasColumnType("REAL");

                    b.Property<int>("TransactionStatus")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TransactionTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransactionType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("TransactionUpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("TransactionId");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ABCBank.Domain.Models.Account", b =>
                {
                    b.HasOne("ABCBank.Domain.Models.CustomerAccount", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ABCBank.Domain.Models.Transaction", b =>
                {
                    b.HasOne("ABCBank.Domain.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
