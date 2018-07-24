﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ShareYunSourse.EFCore;
using System;

namespace ShareYunSourse.EFCore.Migrations
{
    [DbContext(typeof(YunSourseContext))]
    [Migration("20180724090947_innerModel")]
    partial class innerModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShareYunSourse.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("RoleName")
                        .HasMaxLength(50);

                    b.Property<int>("status")
                        .HasMaxLength(2);

                    b.Property<int?>("yunSourseId");

                    b.HasKey("Id");

                    b.HasIndex("yunSourseId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("ShareYunSourse.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .HasMaxLength(50);

                    b.Property<string>("UserPwd")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ShareYunSourse.YunSourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Title")
                        .HasMaxLength(300);

                    b.Property<string>("URL")
                        .HasMaxLength(100);

                    b.Property<int?>("YunSourseId");

                    b.HasKey("Id");

                    b.HasIndex("YunSourseId");

                    b.ToTable("YunSourse");
                });

            modelBuilder.Entity("ShareYunSourse.Role", b =>
                {
                    b.HasOne("ShareYunSourse.YunSourse", "yunSourse")
                        .WithMany()
                        .HasForeignKey("yunSourseId");
                });

            modelBuilder.Entity("ShareYunSourse.YunSourse", b =>
                {
                    b.HasOne("ShareYunSourse.YunSourse")
                        .WithMany("yunSourses")
                        .HasForeignKey("YunSourseId");
                });
#pragma warning restore 612, 618
        }
    }
}