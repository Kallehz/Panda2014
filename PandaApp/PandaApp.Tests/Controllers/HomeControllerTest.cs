using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PandaApp;
using PandaApp.Controllers;

namespace PandaApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // I expect this(result attribute) to hold models equal to subtitles on the front page
            // Every model should have the same attributes as the subtitle on front page
            // Act
            ViewResult result = controller.Index(1) as ViewResult;
            
            
            // Assert
            Assert.IsNotNull(result);
        }
        /*
        [TestMethod]
        public void FAQ()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.FAQ() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }*/
    }
}
