using System;
using System.Collections;
using System.Threading.Tasks;
using Alkamous.Controller;
using Alkamous.Model;
using NUnit.Framework;

namespace Alkamous.Tests
{
    [TestFixture]
    public class InvoiceTests
    {
        private MockDataAccessLayer _mockDal;
        private ClsOperationsofInvoices _invoiceController;

        [SetUp]
        public void Setup()
        {
            _mockDal = new MockDataAccessLayer();
            _invoiceController = new ClsOperationsofInvoices(_mockDal);
        }

        [Test]
        public async Task AddNewAsync_ShouldCallRunProcedureAsync_WithCorrectParameters()
        {
            // Arrange
            var invoice = new CTB_Invoices
            {
                Invoice_Number = "INV-001",
                Invoice_product_Id = "PROD-123",
                Invoice_Unit = "PCS",
                Invoice_QTY = "10",
                Invoice_Price = "100",
                Invoice_Amount = "1000"
            };

            // Act
            var result = await _invoiceController.AddNewAsync(invoice);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _mockDal.CalledProcedures.Count);
            Assert.AreEqual("SP_TB_Invoices", _mockDal.CalledProcedures[0]);
            
            var paramsList = _mockDal.CapturedParameters[0];
            Assert.AreEqual("Add_NewInvoice", paramsList["@Check"]);
            Assert.AreEqual("INV-001", paramsList["Invoice_Number"]);
            Assert.AreEqual("PROD-123", paramsList["Invoice_product_Id"]);
        }

        [Test]
        public async Task DeleteAsync_ShouldCallRunProcedureAsync_WithCorrectParameters()
        {
            // Arrange
            string invoiceNumber = "INV-001";

            // Act
            var result = await _invoiceController.DeleteAsync(invoiceNumber);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _mockDal.CalledProcedures.Count);
            
            var paramsList = _mockDal.CapturedParameters[0];
            Assert.AreEqual("Delete_InvoiceByInvoice_Number", paramsList["@Check"]);
            Assert.AreEqual("INV-001", paramsList["Invoice_Number"]);
        }

        [Test]
        public void Add_NewInvoiceLIST_ShouldAddToInternalList()
        {
            // Arrange
            var invoice = new CTB_Invoices
            {
                Invoice_Number = "INV-BULK-001",
                Invoice_product_Id = "PROD-BULK",
                Invoice_Unit = "BOX",
                Invoice_QTY = "5",
                Invoice_Price = "50",
                Invoice_Amount = "250"
            };

            // Act
            _invoiceController.Add_NewInvoiceLIST(invoice);
            _invoiceController.InsertBulk();

            // Assert
            Assert.AreEqual(1, _mockDal.CapturedBulkParameters.Count);
            var bulkList = _mockDal.CapturedBulkParameters[0];
            Assert.AreEqual(1, bulkList.Count);
            
            var paramsList = bulkList[0];
            Assert.AreEqual("Add_NewInvoice", paramsList["@Check"]);
            Assert.AreEqual("INV-BULK-001", paramsList["Invoice_Number"]);
        }
    }
}
