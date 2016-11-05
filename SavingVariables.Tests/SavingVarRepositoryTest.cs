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
        Mock<SavingVarContext> mock_context = new Mock<SavingVarContext>();
        Mock<DbSet<Variables>> mock_variable_table = new Mock<DbSet<Variables>>();
        //List<Variables> variable_list { get; set; }
        List<Variables> mock_variable_list = new List<Variables>();
        SavingVarRepository repo { get; set; } //?? What's really going on here?
        

        public void ConnectMoqToDatastore()
        {
            var queryable_list = mock_variable_list.AsQueryable();
            mock_variable_table.As<IQueryable<Variables>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_variable_table.As<IQueryable<Variables>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_variable_table.As<IQueryable<Variables>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_variable_table.As<IQueryable<Variables>>().Setup(m => m.GetEnumerator()).Returns(queryable_list.GetEnumerator);
            mock_context.Setup(c => c.Variables).Returns(mock_variable_table.Object);

            mock_variable_table.Setup(v => v.Add(It.IsAny<Variables>())).Callback((Variables x) => mock_variable_list.Add(x));
            mock_variable_table.Setup(v => v.Remove(It.IsAny<Variables>())).Callback((Variables x) => mock_variable_list.Remove(x));


        }
        [TestInitialize]
        public void Initialize()
        {
            ConnectMoqToDatastore();
            repo = new SavingVarRepository(mock_context.Object);
            repo.AddVariable(new Variables { Variable = "x", Value = 1 });
            repo.AddVariable(new Variables { Variable = "y", Value = 2 });
            repo.AddVariable(new Variables { Variable = "z", Value = 3 });
        }
        [TestCleanup]
        public void CleanRepo()
        {
            repo = null;
        }

        [TestMethod]
        public void EnsureCanCreateInstanceOfSavingVar()
        {
            SavingVarRepository saved = new SavingVarRepository();
            double percent = 2.5;
            Console.WriteLine(percent / 100);
            
            Assert.IsNotNull(saved);
        }
        [TestMethod]
        public void EnsureCanCreateInstanceofSavingVarWithMoq()
        {
            Assert.IsNotNull(repo);
        }
        [TestMethod]
        public void EnsureRepoHasContext()
        {
            SavingVarContext actual_context = repo.Context;
            Assert.IsInstanceOfType(actual_context, typeof(SavingVarContext));
        }
        [TestMethod]
        public void EnsureNoVariablesInDBWithMoq()
        {
            SavingVarRepository no_variables = new SavingVarRepository();
            List<Variables> actual_variables = no_variables.GetVariables();
            int expected_variable_count = 0;
            int actual_variable_count = actual_variables.Count();
            Assert.AreEqual(expected_variable_count, actual_variable_count);
        }
        [TestMethod]
        public void EnsureCanAddVariablestoDatabase()
        {
            Variables new_variable = new Variables { Variable = "x", Value = 3 };
            repo.AddVariable(new_variable);
            int expected_count = 4;
            int actual_count = repo.GetVariables().Count();
            Assert.AreEqual(expected_count, actual_count);
            mock_variable_table.Verify(x => x.Add(new_variable), Times.Once);
        }
        [TestMethod]
        public void EnsureCanFindVariableByCharacter()
        {
            string character_to_find = "y";
            Variables actual_character_result = repo.FindVariableByCharacter(character_to_find);
            string actual_character = actual_character_result.Variable;
            string expected_character = "y";
            Assert.AreEqual(expected_character, actual_character);
        }
        [TestMethod]
        public void EnsureCanFindVariableByValue()
        {
            int value_to_find = 3; //initialized z = 3
            Variables actual_value_result = repo.FindVariableByValue(value_to_find);
            int actual_value = actual_value_result.Value;
            int expected_value = 3;
            Assert.AreEqual(expected_value, actual_value);
        }
        [TestMethod]
        //delete variable from database
        public void EnsureCanRemoveVariableFromDatabase()
        {
            string variable_to_remove = "y";
            repo.RemoveVariable(variable_to_remove);
            int expected_variable_count = 2;
            int actual_variable_count = repo.GetVariables().Count();
            Assert.AreEqual(expected_variable_count, actual_variable_count);
        }
        [TestMethod]
        public void EnsureNoExceptionIfReturnResultIsNullForNoVariable()
        {
            string variable_to_remove = "a";
            Variables result = repo.RemoveVariable(variable_to_remove);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void EnsureNoExceptionIfReturnResultIsNullForNoValue()
        {
            int value_to_remove = 55;
            Variables result = repo.FindVariableByValue(value_to_remove);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void EnsureCanSaveQueryResultToDictionary()
        {
            Dictionary<string, int> expected_dictionary = new Dictionary<string, int>();
            expected_dictionary.Add("x", 1);
            expected_dictionary.Add("y", 2);
            expected_dictionary.Add("z", 3);
            Dictionary<string, int> actual_dictionary = repo.CreateDictionaryOfVariablesAndValues();
            CollectionAssert.AreEquivalent(expected_dictionary, actual_dictionary);
              
        }
        [TestMethod]
        public void EnsureCanRemoveAllDataFromTable()
        {
            repo.RemoveAllVariablesFromDatabase(repo.Context.Variables);
            int actual_count = repo.Context.Variables.Count();
            Assert.IsTrue(actual_count == 0);
        }
        [TestMethod]
        public void EnsureCanSetVariableAfterDataCleared()
        {
            repo.RemoveAllVariablesFromDatabase(repo.Context.Variables);
            int actual_count = repo.GetVariables().Count();
            Assert.IsTrue(actual_count == 0);
            Variables new_variable = new Variables { Variable = "x", Value = 1 };


        }
    }
}
