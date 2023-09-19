using Microsoft.AspNetCore.Mvc;
using TiendaCodeFirst.Controllers;
using TiendaCodeFirst.Models;
using TiendaCodeFirst.Services;

namespace TestTiendaCodeFirst
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class UnitTestComponentes
        {
            private readonly ComponentesController controlador = new(new FakeRepositorioComponentes());

            [TestMethod]
            public void TestComponenteIndexOk()
            {
                var result = controlador.Index() as ViewResult;
                Assert.IsNotNull(result);
                Assert.AreEqual("Index", result.ViewName);
                Assert.IsNotNull(result.ViewData.Model);
                var listaComponents = result.ViewData.Model as List<Componentes>;
                Assert.IsNotNull(listaComponents);
                Assert.AreEqual(3, listaComponents.Count);
            }

            [TestMethod]
            public void TestComponenteCreate()
            {
                Componentes componente = new()
                {
                    Serie = "797-X",
                    Descripcion = "Procesador Ryzen AMD",
                    Calor = 30,
                    Megas = 0,
                    Cores = 10,
                    Coste = 78,
                    Tipo = 1
                };
                var result = controlador.Create(componente) as RedirectToActionResult;
                var result2 = controlador.Create() as ViewResult;
                var result3 = controlador.Index() as ViewResult;

                Assert.IsNotNull(result);
                Assert.IsNotNull(result2);
                Assert.IsNotNull(result3);
                Assert.AreEqual("Create", result2.ViewName);
                Assert.AreEqual("Index", result.ActionName);

                var listaComponents = result3.ViewData.Model as List<Componentes>;
                Assert.IsNotNull(listaComponents);
                Assert.AreEqual(4, listaComponents.Count);
                Assert.AreEqual("797-X", listaComponents[3].Serie);
                Assert.AreEqual(30, listaComponents[3].Calor);
                Assert.AreEqual(10, listaComponents[3].Cores);
                Assert.AreEqual(78, listaComponents[3].Coste);
                Assert.AreEqual("Procesador Ryzen AMD", listaComponents[3].Descripcion);
                Assert.AreEqual(0, listaComponents[3].Megas);
                Assert.AreEqual(1, listaComponents[3].Tipo);
                Assert.AreEqual(4, listaComponents[3].Id);
            }

            [TestMethod]
            public void TestComponenteEditVistaEncontrada()
            {
                var result = controlador.Edit(2) as ViewResult;
                Assert.IsNotNull(result);
                Assert.AreEqual("Edit", result.ViewName);
                Assert.IsNotNull(result.ViewData.Model);
                var comp = result.ViewData.Model as Componentes;
                Assert.IsNotNull(comp);
                Assert.AreEqual("789_XCD", comp.Serie);
            }
            [TestMethod]
            public void TestComponenteEditVistaNoEncontrada()
            {
                var result = controlador.Edit(5) as NotFoundResult;
                Assert.IsNotNull(result);
                Assert.AreEqual(404, result.StatusCode);
            }

            [TestMethod]
            public void TestComponenteEdit()
            {
                Componentes componente = new()
                {
                    Serie = "789_XCT",
                    Descripcion = "Procesador Intel i7",
                    Calor = 40,
                    Megas = 0,
                    Cores = 11,
                    Coste = 100,
                    Tipo = 1
                };
                int id = 3;
                var result = controlador.Edit(id, componente) as RedirectToActionResult;
                var result2 = controlador.Index() as ViewResult;

                Assert.IsNotNull(result);
                Assert.IsNotNull(result2);
                Assert.AreEqual("Index", result.ActionName);

                var listaComponents = result2.ViewData.Model as List<Componentes>;
                Assert.IsNotNull(listaComponents);
                Assert.AreEqual(40, listaComponents[2].Calor);
                Assert.AreEqual(100, listaComponents[2].Coste);
                Assert.AreEqual(3, listaComponents[2].Id);
            }

            [TestMethod]
            public void TestComponenteDeleteVistaEncontrada()
            {
                var result = controlador.Delete(2) as ViewResult;
                Assert.IsNotNull(result);
                Assert.AreEqual("Delete", result.ViewName);
                Assert.IsNotNull(result.ViewData.Model);
                var comp = result.ViewData.Model as Componentes;
                Assert.IsNotNull(comp);
                Assert.AreEqual("789_XCD", comp.Serie);
            }
            [TestMethod]
            public void TestComponenteDeleteVistaNoEncontrada()
            {
                var result = controlador.Delete(5) as NotFoundResult;
                Assert.IsNotNull(result);
                Assert.AreEqual(404, result.StatusCode);
            }
            [TestMethod]
            public void TestComponenteDelete()
            {
                int id = 3;
                var result = controlador.DeleteConfirmed(id) as RedirectToActionResult;
                var result2 = controlador.Index() as ViewResult;

                Assert.IsNotNull(result);
                Assert.IsNotNull(result2);
                Assert.AreEqual("Index", result.ActionName);

                var listaComponents = result2.ViewData.Model as List<Componentes>;
                Assert.IsNotNull(listaComponents);
                Assert.AreEqual(2, listaComponents.Count);
            }

            [TestMethod]
            public void TestComponenteDetailsVistaEncontrada()
            {
                var result = controlador.Details(2) as ViewResult;
                Assert.IsNotNull(result);
                Assert.AreEqual("Details", result.ViewName);
                Assert.IsNotNull(result.ViewData.Model);
                var comp = result.ViewData.Model as Componentes;
                Assert.IsNotNull(comp);
                Assert.AreEqual("789_XCD", comp.Serie);
            }
            [TestMethod]
            public void TestComponenteDetailsVistaNoEncontrada()
            {
                var result = controlador.Delete(5) as NotFoundResult;
                Assert.IsNotNull(result);
                Assert.AreEqual(404, result.StatusCode);
            }



        }
    }
}