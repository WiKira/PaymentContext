using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Student _student;
    private readonly Subscription _subscription;
    

    public StudentTests(){
        _name = new Name("Bruce", "Wayne");
        _document = new Document("12354678912", Domain.Enums.EDocumentType.CPF);
        _email = new Email("batman@dc.com");
        _address = new Address("Billioner Street", "1000000000", "RichPersons", "Gotham", "Transilvania", "United States", "10000-000", "");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment("123213546", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayner Corp.", _document, _address, _email);
        
        _subscription.AddPayment(payment);

        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);

        Assert.IsTrue(!_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadSubscriptionHasNoPayment()
    {
        _student.AddSubscription(_subscription);
        
        Assert.IsTrue(!_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSucessWhenHadNoActiveSubscription()
    {
        var payment = new PayPalPayment("123213546", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayner Corp.", _document, _address, _email);
        
        _subscription.AddPayment(payment);

        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenHadSubscriptionHasPayment()
    {
        var payment = new PayPalPayment("123213546", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayner Corp.", _document, _address, _email);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        
        Assert.IsTrue(_student.IsValid);
    }
}   