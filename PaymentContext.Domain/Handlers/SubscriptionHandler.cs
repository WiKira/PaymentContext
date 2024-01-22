using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>, 
                                       IHandler<CreateBoletoSubscriptionCommand>, 
                                       IHandler<CreatePayPalSubscriptionCommand>,
                                       IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService){
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();

            if(!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");
            }
            
            //Verificar se Documento já está cadastrado
            if(_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso.");
            
            //Verificar se Email já está cadastrado
            if(_repository.EmailExists(command.Document))
                AddNotification("Email", "Este E-mail já está em uso.");

            //Gerar os VO's
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, 
                                     command.City, command.State, command.Country,
                                     command.ZipCode, command.Complement);
            
            //Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate,
                                           command.ExpireDate, command.Total, command.TotalPaid, 
                                           command.Payer, new Document(command.PayerDocument, command.PayerDocumentType),
                                           address, email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar notificações
            if(!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");

            //Salvar as Informações
            _repository.CreateSubscription(student);

            //Enviar E-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Boas Vindas", "Seja bem vindo ");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso.");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();

            if(!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");
            }
            
            //Verificar se Documento já está cadastrado
            if(_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso.");
            
            //Verificar se Email já está cadastrado
            if(_repository.EmailExists(command.Document))
                AddNotification("Email", "Este E-mail já está em uso.");

            //Gerar os VO's
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, 
                                     command.City, command.State, command.Country,
                                     command.ZipCode, command.Complement);
            
            //Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(command.TransactionCode, command.PaidDate,
                                           command.ExpireDate, command.Total, command.TotalPaid, 
                                           command.Payer, new Document(command.PayerDocument, command.PayerDocumentType),
                                           address, email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar notificações
            if(!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");

            //Salvar as Informações
            _repository.CreateSubscription(student);

            //Enviar E-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Boas Vindas", "Seja bem vindo ");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso.");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();

            if(!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");
            }
            
            //Verificar se Documento já está cadastrado
            if(_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso.");
            
            //Verificar se Email já está cadastrado
            if(_repository.EmailExists(command.Document))
                AddNotification("Email", "Este E-mail já está em uso.");

            //Gerar os VO's
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, 
                                     command.City, command.State, command.Country,
                                     command.ZipCode, command.Complement);
            
            //Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(command.CardHolderName, command.CardNumber, command.LastTransactionNumber,
                                           command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, 
                                           command.Payer, new Document(command.PayerDocument, command.PayerDocumentType),
                                           address, email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar notificações
            if(!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");
            //Salvar as Informações
            _repository.CreateSubscription(student);

            //Enviar E-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Boas Vindas", "Seja bem vindo ");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso.");
        }
    }
}