using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables.DAL
{
    public class SavingVarRepository
    {
        public SavingVarContext Context { get; set; }
        public SavingVarRepository()
        {
            Context = new SavingVarContext();
        }
            
    }
}
