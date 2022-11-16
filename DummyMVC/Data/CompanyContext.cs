using System;
using System.Collections.Generic;
using DummyMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DummyMVC.Data;

public partial class CompanyContext : DbContext
{
    public CompanyContext()
    {
    }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Karyawan> Karyawans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSILAPTOP;initial catalog=company; trusted_connection=yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Karyawan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Karyawan__3214EC079B9DC574");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<DummyMVC.Models.Karyawan> Karyawan { get; set; }
}
