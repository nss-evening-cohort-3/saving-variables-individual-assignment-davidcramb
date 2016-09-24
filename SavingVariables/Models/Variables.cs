using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
    
namespace SavingVariables.Models
{
    public class Variables
    {
        [Key]
        public virtual int VariableId { get; set; }
        [Required]
        public virtual string Variable { get; set; }
        [Required]
        public virtual int Value { get; set; }
    }
    
}



