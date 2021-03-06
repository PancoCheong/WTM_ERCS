﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using ERCS.Model;

namespace ERCS.DataAccess
{
    public class DataContext : FrameworkContext
    {
        public DbSet<Location> Locations { get; set; } //database table
        public DbSet<ControlCenter> ControlCenters { get; set; } //database table
        public DbSet<Hospital> Hospitals { get; set; } //database table
        public DbSet<Patient> Patients { get; set; } //database table
        public DbSet<Report> Reports { get; set; } //database table
        public DbSet<Virus> Viruses { get; set; } //database table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasIndex(x => x.IdNumber); //**key** define index for IdNumber column in Patient table

            base.OnModelCreating(modelBuilder);
        }

        public DataContext(CS cs)
             : base(cs)
        {
        }
        public DataContext(string cs, DBTypeEnum dbtype)
            : base(cs, dbtype)
        {
        }
        public DataContext(string cs, DBTypeEnum dbtype, string version=null)
             : base(cs, dbtype, version)
        {
        }

    }

    /// <summary>
    /// DesignTimeFactory for EF Migration, use your full connection string,
    /// EF will find this class and use the connection defined here to run Add-Migration and Update-Database
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            return new DataContext("your full connection string", DBTypeEnum.SqlServer);
        }
    }

}
