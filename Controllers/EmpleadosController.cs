using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Perfiles_S.A.Models;

namespace Perfiles_S.A.Controllers
{
    public class EmpleadosController : Controller
    {
        private Empresa_PerfilesEntities db = new Empresa_PerfilesEntities();

        // GET: Empleados
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.Departamentos);
            return View(empleados.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "NombreDepartamento");
            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpleadoID,Nombres,DPI,FechaNacimiento,Genero,FechaIngreso,Edad,Direccion,NIT,DepartamentoID")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "NombreDepartamento", empleados.DepartamentoID);
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "NombreDepartamento", empleados.DepartamentoID);
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpleadoID,Nombres,DPI,FechaNacimiento,Genero,FechaIngreso,Edad,Direccion,NIT,DepartamentoID")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "NombreDepartamento", empleados.DepartamentoID);
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Empleados/Reporte
        public ActionResult Reporte(DateTime? fechaInicio, DateTime? fechaFin)
        {
            // Obtener todos los empleados con sus departamentos
            var empleados = db.Empleados.Include(e => e.Departamentos).AsQueryable();

            // Aplicar filtros si se proporcionan fechas
            if (fechaInicio.HasValue)
            {
                empleados = empleados.Where(e => e.FechaIngreso >= fechaInicio.Value);
            }

            if (fechaFin.HasValue)
            {
                empleados = empleados.Where(e => e.FechaIngreso <= fechaFin.Value);
            }

            // Mapear los empleados al modelo ReporteEmpleado
            var empleadosReporte = empleados.ToList().Select(e => new ReporteEmpleado
            {
                EmpleadoID = e.EmpleadoID,
                Nombres = e.Nombres,
                DPI = e.DPI,
                FechaNacimiento = e.FechaNacimiento,
                Genero = e.Genero,
                FechaIngreso = e.FechaIngreso,
                Edad = DateTime.Today.Year - e.FechaNacimiento.Year, // Calcular la edad
                Direccion = e.Direccion,
                NIT = e.NIT,
                NombreDepartamento = e.Departamentos.NombreDepartamento,
                Estatus = e.FechaIngreso <= DateTime.Today ? "Activo" : "Inactivo" // Definir el estatus
            }).ToList();

            // Agrupar empleados por departamento
            var empleadosPorDepartamento = empleadosReporte
                .GroupBy(e => e.NombreDepartamento)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Pasar los empleados agrupados a la vista
            return View(empleadosPorDepartamento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}