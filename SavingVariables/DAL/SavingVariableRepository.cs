using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.Models;

namespace SavingVariables.DAL
{
    public class SavingVarRepository
    {
        public SavingVarContext Context { get; set; }
        

        public SavingVarRepository()
        {
            Context = new SavingVarContext();
        }
        public SavingVarRepository(SavingVarContext _context)
        {
            Context = _context;
        }

        public List<Variables> GetVariables()
        {
            return Context.Variables.ToList();
        }

        public void AddVariable(Variables new_variable)
        {
            Context.Variables.Add(new_variable);
            Context.SaveChanges();
        }

        public Variables FindVariableByCharacter(char character_to_find)
        {
            //List<Variables> found_characters = Context.Variables.ToList();
            IQueryable<Variables> Variable_Query =
                from data in Context.Variables
                where data.Variable == character_to_find
                select data;
            return Variable_Query.First();
            //foreach (Variables variable in Variable_Query)
            //{
            //    found_character.Add(variable.Variable);
            //}
        }

        public Variables RemoveVariable(char variable_character)
        {
            Variables character_to_remove = FindVariableByCharacter(variable_character);
            Context.Variables.Remove(character_to_remove);
            return character_to_remove;
        }
            

    }
}
