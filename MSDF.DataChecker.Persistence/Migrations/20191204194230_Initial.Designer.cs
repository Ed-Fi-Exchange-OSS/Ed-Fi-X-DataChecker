﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

// <auto-generated />
using System;
using MSDF.DataChecker.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MSDF.DataChecker.Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20191204194230_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MSDF.DataChecker.Persistence.Collections.Container", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContainerTypeId");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ParentContainerId");

                    b.HasKey("Id");

                    b.HasIndex("ContainerTypeId");

                    b.HasIndex("ParentContainerId");

                    b.ToTable("Containers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6b1c64ac-a9ce-4441-b552-c3da9a3cea0e"),
                            ContainerTypeId = 1,
                            Name = "Ed-Fi"
                        },
                        new
                        {
                            Id = new Guid("8d59b9bf-ded0-43f1-9397-3becf7720a8a"),
                            ContainerTypeId = 2,
                            Name = "General Sanity Checks",
                            ParentContainerId = new Guid("6b1c64ac-a9ce-4441-b552-c3da9a3cea0e")
                        });
                });

            modelBuilder.Entity("MSDF.DataChecker.Persistence.Collections.ContainerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ContainerTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Collection"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Folder"
                        });
                });

            modelBuilder.Entity("MSDF.DataChecker.Persistence.Collections.Rule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ContainerId");

                    b.Property<string>("JSON");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.ToTable("Rules");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a2be65cb-8f54-4694-8c23-72161e351acd"),
                            ContainerId = new Guid("8d59b9bf-ded0-43f1-9397-3becf7720a8a"),
                            JSON = @"{
  ""Id"": ""a2be65cb-8f54-4694-8c23-72161e351acd"",
  ""Name"": ""Orphaned student records"",
  ""Category"":  ""Enrollment"",
  ""Sql"": ""SELECT count(*) FROM edfi.Student s LEFT JOIN edfi.StudentSchoolAssociation ssa on s.StudentUSI = ssa.StudentUSI WHERE ssa.SchoolId is null;"",
  ""DiagnosticSql"": ""SELECT top (100) s.StudentUSI, ssa.SchoolId FROM edfi.Student s LEFT JOIN edfi.StudentSchoolAssociation ssa on s.StudentUSI = ssa.StudentUSI WHERE ssa.SchoolId is null;"",
  ""ErrorMessage"": ""You have {TestResult.Result} orphaned student records. These are students that are in your ODS but not associated with any school."",
  ""ErrorSeverityLevel"": ""Warning"",
  ""Resolution"": ""Look at your student roster and ensure you associate all students with schools."",
  ""EvaluationOperand"": ""=="",
  ""ExpectedResult"": 0,
  ""EdFiODSCompatibilityVersion"": ""3.1"",
  ""Version"": ""1.0.0"",
  ""Enabled"": 1
}"
                        });
                });

            modelBuilder.Entity("MSDF.DataChecker.Persistence.Collections.Container", b =>
                {
                    b.HasOne("MSDF.DataChecker.Persistence.Collections.ContainerType", "ContainerType")
                        .WithMany()
                        .HasForeignKey("ContainerTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MSDF.DataChecker.Persistence.Collections.Container", "ParentContainer")
                        .WithMany("ChildContainers")
                        .HasForeignKey("ParentContainerId");
                });

            modelBuilder.Entity("MSDF.DataChecker.Persistence.Collections.Rule", b =>
                {
                    b.HasOne("MSDF.DataChecker.Persistence.Collections.Container")
                        .WithMany("Rules")
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}