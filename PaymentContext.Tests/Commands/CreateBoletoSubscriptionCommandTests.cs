using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "";

            command.Validate();
            Assert.AreEqual(false, command.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenNameIsValid()
        {
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "William";
            command.LastName = "Cesar";
            command.Validate();
            Assert.AreEqual(true, command.IsValid);
        }
    }
}