using GSAExam.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Infrastructure.Configurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Students>
    {
        //public StudentConfigurations(ModelBuilder modelBuilder) : base(modelBuilder)
        //{
        //}
        //public override void Configure()
        //{
        //    _builder.Property(p => p.FirstName)
        //            .HasColumnType("varchar(256)");
        //    _builder.Property(p => p.LastName)
        //            .HasColumnType("varchar(256)");
        //    _builder.Property(p => p.Email)
        //            .HasColumnType("varchar(256)");
        //    _builder.Property(p => p.Dates)
        //            .HasColumnType("datetime2(0)");
        //    _builder.Property(p => p.Status)
        //            .HasColumnType("varchar(256)");
        //    _builder.ToTable("SystemXStudents", "dbo");
        //}
        public void Configure(EntityTypeBuilder<Students> _builder)
        {
            _builder.Property(p => p.FirstName)
                    .HasColumnType("varchar(256)");
            _builder.Property(p => p.LastName)
                    .HasColumnType("varchar(256)");
            _builder.Property(p => p.Email)
                    .HasColumnType("varchar(256)");
            _builder.Property(p => p.Dates)
                    .HasColumnType("datetime2(0)");
            _builder.Property(p => p.Status)
                    .HasColumnType("varchar(256)");
            _builder.ToTable("SystemXStudents", "dbo");
        }
    }
}
