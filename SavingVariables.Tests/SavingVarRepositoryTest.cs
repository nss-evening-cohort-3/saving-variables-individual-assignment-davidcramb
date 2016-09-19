using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavingVariables.DAL;
using System.Linq;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using SavingVariables.Models;

namespace SavingVariables.Tests
{
    [TestClass]
    public class SavingVarRepositoryTest
    {
        [TestMethod]
        public void EnsureCanCreateInstanceOfSavingVar()
        {
            SavingVarRepository saved = new SavingVarRepository();
            Console.WriteLine(saved);
        }
        [TestMethod]
        public void EnsureNoVariablesInDatabase()
        {
            SavingVarRepository var_repo = new SavingVarRepository();
            List<Variables> actual_variables = var_repo.GetVariables();
            int expected_variables = 0;
            int actual_variable_count = actual_variables.Count();
            Assert.AreEqual(expected_variables, actual_variable_count);

        }
    }
}
