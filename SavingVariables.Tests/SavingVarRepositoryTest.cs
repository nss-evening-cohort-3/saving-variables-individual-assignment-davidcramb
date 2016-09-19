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
    }
}
