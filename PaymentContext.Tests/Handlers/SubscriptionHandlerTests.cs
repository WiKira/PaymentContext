using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            
            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "teste@email.com.br";
            command.BarCode = "123456789";
            command.BoletoNumber = "123456789";
            command.PaymentNumber = "12321";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 10;
            command.TotalPaid = 10;
            command.Payer = "Wayne Corp";
            command.PayerDocument = "12312312311";
            command.PayerDocumentType = Domain.Enums.EDocumentType.CPF;
            command.PayerEmail = "batman@dc.com";
            command.Street = "aas";
            command.Number = "as";
            command.Neighborhood = "as";
            command.City = "as";
            command.State = "as";
            command.Country = "as";
            command.ZipCode = "as";
            command.Complement = "as";

            handler.Handle(command);

            Assert.AreEqual(false, handler.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenEmailExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            
            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999911";
            command.Email = "williamcamargo@email.com.br";
            command.BarCode = "123456789";
            command.BoletoNumber = "123456789";
            command.PaymentNumber = "12321";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 10;
            command.TotalPaid = 10;
            command.Payer = "Wayne Corp";
            command.PayerDocument = "12312312311";
            command.PayerDocumentType = Domain.Enums.EDocumentType.CPF;
            command.PayerEmail = "batman@dc.com";
            command.Street = "aas";
            command.Number = "as";
            command.Neighborhood = "as";
            command.City = "as";
            command.State = "as";
            command.Country = "as";
            command.ZipCode = "as";
            command.Complement = "as";

            handler.Handle(command);

            Assert.AreEqual(false, handler.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenEmailAndDocumentDoesntExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            
            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999911";
            command.Email = "teste@email.com.br";
            command.BarCode = "123456789";
            command.BoletoNumber = "123456789";
            command.PaymentNumber = "12321";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 10;
            command.TotalPaid = 10;
            command.Payer = "Wayne Corp";
            command.PayerDocument = "12312312311";
            command.PayerDocumentType = Domain.Enums.EDocumentType.CPF;
            command.PayerEmail = "batman@dc.com";
            command.Street = "aas";
            command.Number = "aas";
            command.Neighborhood = "aas";
            command.City = "aas";
            command.State = "asa";
            command.Country = "aas";
            command.ZipCode = "aas";
            command.Complement = "aas";

            handler.Handle(command);

            Assert.AreEqual(true, handler.IsValid);
        }
    }
}