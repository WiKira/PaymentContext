using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            Address = null;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address? Address {get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get {return _subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription){
            
            var hasSubscriptionActive = false;
            foreach(var sub in _subscriptions){
                if(sub.Active)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract<Subscription>()
                                .Requires()
                                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa.")
                                .IsGreaterThan(subscription.Payments.Count, 0, "Student.Subscription.Payment", "Essa assinatura não possui pagamentos."));
            
            //if(hasSubscriptionActive)
            //    AddNotification("Student.Subscriptions", "Você já tem uma assinatura ativa.");
            //else
                _subscriptions.Add(subscription);
        }
    }
}