using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIOGRAPHY_OF_PETSFAP;
using BIOGRAPHY_OF_PETSFAP.Controllers;
using BIOGRAPHY_OF_PETSFAP.Models;
using Moq;
using System.Web.Abstractions;


namespace Pruebas
{
    [TestClass]
    public class CitaControllerTest
    {
        private VeterinariaEntities db = new VeterinariaEntities();

        [TestMethod]
        public void Index()
        {
            var mockHttpContext = new Mock<System.Web.HttpContextBase>();
            mockHttpContext.SetupSet(c => c.Session["Employee"] = It.IsAny<object>());
            // Arrange
            UsuariosController usuariosController = new UsuariosController();
            Cita_MedicaController cita = new Cita_MedicaController();
            Usuarios usuario= new Usuarios{
                Usuario="admin",
                Contraseña="admin"
            };
            // Act
            usuariosController.Login(usuario);
            ViewResult result = cita.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            
        }
    }
}
