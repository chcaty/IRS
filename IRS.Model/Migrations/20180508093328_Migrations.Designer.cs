﻿// <auto-generated />
using IRS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace IRS.Model.Migrations
{
    [DbContext(typeof(IRSContext))]
    [Migration("20180508093328_Migrations")]
    partial class Migrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IRS.Model.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IsDeleted");

                    b.Property<string>("PermissionCode");

                    b.Property<string>("PermissionName");

                    b.HasKey("PermissionId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("IRS.Model.ProcessingRecord", b =>
                {
                    b.Property<int>("ProcessingRecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProcessingDesc");

                    b.Property<int>("ProcessingPeople");

                    b.Property<int>("ProcessingResult");

                    b.Property<string>("ProcessingTime");

                    b.Property<int>("RecordId");

                    b.HasKey("ProcessingRecordId");

                    b.ToTable("ProcessingRecords");
                });

            modelBuilder.Entity("IRS.Model.Record", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DormCategory");

                    b.Property<int>("FaultCategory");

                    b.Property<int>("FaultCondition");

                    b.Property<string>("FaultDesc");

                    b.Property<int>("FaultResult");

                    b.Property<string>("RecordUser");

                    b.Property<string>("UserDorm");

                    b.Property<string>("UserPhone");

                    b.HasKey("RecordId");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("IRS.Model.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IsDeleted");

                    b.Property<string>("RoleName");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("IRS.Model.RolePermission", b =>
                {
                    b.Property<int>("RolePermissionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PermissionId");

                    b.Property<int>("RolesId");

                    b.HasKey("RolePermissionId");

                    b.ToTable("RoleAction");
                });

            modelBuilder.Entity("IRS.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsEnable");

                    b.Property<string>("UserCode");

                    b.Property<string>("UserName");

                    b.Property<string>("UserPwd");

                    b.Property<int>("isDeleted");

                    b.Property<string>("validity");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IRS.Model.UserRole", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("UserRoleId");

                    b.ToTable("UserRole");
                });
#pragma warning restore 612, 618
        }
    }
}
