﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public Variables FindVariableByCharacter(string character_to_find)
        {
            //List<Variables> found_characters = Context.Variables.ToList();
            IQueryable<Variables> Variable_Query =
                from data in Context.Variables
                where data.Variable == character_to_find.ToString()
                select data;
            return Variable_Query.FirstOrDefault();
            //foreach (Variables variable in Variable_Query)
            //{
            //    found_character.Add(variable.Variable);
            //}
        }

        public Variables RemoveVariable(string variable_character)
        {
            Variables character_to_remove = FindVariableByCharacter(variable_character);
            Context.Variables.Remove(character_to_remove);
            Context.SaveChanges();
            return character_to_remove;
        }

        public Variables FindVariableByValue(int value_to_find)
        {
            IQueryable<Variables> Value_Query =
                from data in Context.Variables
                where data.Value == value_to_find
                select data;
            return Value_Query.FirstOrDefault();
        }

        public Variables RemoveAllVariablesFromDatabase(DbSet<Variables> variables)
        {
            Console.Beep(800, 2);
            //Context.Database.ExecuteSqlCommand("delete from variables"); guess this doesn't work.
            IQueryable<Variables> Query_All =
                from data in Context.Variables
                select data;
            List<Variables> Query = Query_All.ToList();
            foreach (var data in Query)
            {
                Context.Variables.Remove(data);
            }

            Context.SaveChanges();
            return null;
        }

        public void WriteDictionaryKVPToConsole(Dictionary<string,int> Dictionary)
        {
            if (Dictionary != null)

            {
                Console.WriteLine(@"
                     ______________
| Name | Value |
----------------".Trim());
                foreach (KeyValuePair<string,int> entry in Dictionary)
                    Console.WriteLine("| " + entry.Key + " | " + entry.Value + " |");
                Console.WriteLine("______________");
            }

            else Console.WriteLine("Database is empty");
        }

        public Dictionary<string, int> CreateDictionaryOfVariablesAndValues()
        {
            try
            {
                IQueryable<Variables> Query_All =
                    from data in Context.Variables
                    select data;
                //List<Variables> Query = Query_All.ToList();
                Dictionary<string, int> Query_Dictionary = new Dictionary<string, int>();
                foreach (var data in Query_All)
                {
                    Query_Dictionary.Add(data.Variable, data.Value);
                }
                return Query_Dictionary;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Oh, dear.");
                Console.WriteLine(e);
                return null;
            }

        }

        public void CreateCustomResultTableofVariables()
        {
        }
    }
}
