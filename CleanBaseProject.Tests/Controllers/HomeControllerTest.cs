using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Clean.Services.ProductItem;
using Clean.Core.Domain.ProductItem;
using Clean.Web.Controllers;
using System.Web.Mvc;

namespace CleanBaseProject.Tests.Controllers
{
    /// <summary>
    /// Summary description for HomeControllerTest
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexViewModelNotNull()
        {
            // Arrange
            var mock = new Mock<IPictureService>();

            mock.Setup(a => a.GetAllPictures()).Returns(new List<Picture>());

            //HomeController controller = new HomeController(mock.Object);

            // Act
            //ViewResult result = controller.Index() as ViewResult;

            // Assert
            //Assert.IsNotNull(result.Model);
        }
    }
}
