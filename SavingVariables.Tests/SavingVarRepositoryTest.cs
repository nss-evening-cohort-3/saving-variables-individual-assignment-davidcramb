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
        List<Variables> variable_list { get; set; }
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
        }
        [TestInitialize]
        public void Initialize()
        {
            ConnectMoqToDatastore();
            repo = new SavingVarRepository(mock_context.Object);
        }

        [TestMethod]
        public void EnsureCanCreateInstanceOfSavingVar()
        {
            SavingVarRepository saved = new SavingVarRepository();
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
        //[TestMethod]
        //public void EnsureNoVariablesInDatabase()
        //{
        //    SavingVarRepository var_repo = new SavingVarRepository();
        //    List<Variables> actual_variables = var_repo.GetVariables();
        //    int expected_variables = 0;
        //    int actual_variable_count = actual_variables.Count();
        //    Assert.AreEqual(expected_variables, actual_variable_count);
        //}
        [TestMethod]
        public void EnsureNoVariablesInDBWithMoq()
        {
            List<Variables> actual_variables = repo.GetVariables();
            int expected_variable_count = 0;
            int actual_variable_count = actual_variables.Count();
            Assert.AreEqual(expected_variable_count, actual_variable_count);
        }
        [TestMethod]
        public void EnsureCanAddVariablestoDatabase()
        {

        }
        //Add variable to database
        //delete variable from database
        
    }
}
