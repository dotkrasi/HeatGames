using HeatGamesWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace HeatGames.Tests.Controllers
{
    [TestFixture]
    public class AdministratorControllerTests
    {
        private AdministratorController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new AdministratorController();
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public void AddGame_ReturnsView()
        {
            var result = _controller.AddGame() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void UploadPatch_ReturnsView()
        {
            var result = _controller.UploadPatch() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }
    }
}

