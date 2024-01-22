using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class DocumentTests
{
    //Red, Green, Refactor
    [TestMethod]
    [DataTestMethod]
    [DataRow("21987654321")]
    [DataRow("123")]
    [DataRow("123456789123456")]
    [DataRow("1234567891234")]
    public void ShouldRetornErroWhenCNPJIsInvalid(string cnpj)
    {
        var doc = new Document(cnpj, EDocumentType.CNPJ);
        Assert.IsTrue(!doc.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("12345678912345")]
    [DataRow("54321987654321")]
    [DataRow("76543215432198")]
    public void ShouldRetornSuccessWhenCNPJIsValid(string cnpj)
    {
        var doc = new Document(cnpj, EDocumentType.CNPJ);
        Assert.IsTrue(doc.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("12345678912345")]
    [DataRow("123")]
    [DataRow("123456789123")]
    [DataRow("1234567891")]
    public void ShouldRetornErroWhenCPFIsInvalid(string cpf)
    {
        var doc = new Document(cpf, EDocumentType.CPF);
        Assert.IsTrue(!doc.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("21987654321")]
    [DataRow("12345678912")]
    [DataRow("15975368429")]
    public void ShouldRetornSuccessWhenCPFIsValid(string cpf)
    {
        var doc = new Document(cpf, EDocumentType.CPF);
        Assert.IsTrue(doc.IsValid);
    }
}   