using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IRS.Model
{
    public class IRSContext:DbContext
    {
        public IRSContext() : base() { }
        public IRSContext(DbContextOptions options) : base(options) { }

        public DbSet<Record> Records { get; set; }
        public DbSet<Permission> Actions { get; set; }
        public DbSet<ProcessingRecord> ProcessingRecords { get; set; }
        public DbSet<RolePermission> RoleAction { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CategoryInfo> CategoryInfos { get; set; }
    }
}