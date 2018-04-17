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
    }
}