using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SavingVariables.Models
{
    public class Results 
    {
        [Key]
        public int ResultId { get; set; }
        [Required]
        public int Result { get; set; }
    }
}
