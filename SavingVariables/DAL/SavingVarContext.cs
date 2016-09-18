using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.Models;

namespace SavingVariables.DAL
{
    public class SavingVarContext : DbContext
    {
        public virtual DbSet<Variables> Variables { get; set; }
    }
}
