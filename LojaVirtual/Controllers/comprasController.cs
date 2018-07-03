using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LojaVirtual.Models;

namespace LojaVirtual.Controllers
{
    public class comprasController : Controller
    {
        private LojaVirtualContext db = new LojaVirtualContext();

        // GET: compras
        public ActionResult Index()
        {
            var compras = db.compras.Include(c => c.produto);
            return View(compras.ToList());
        }

        // GET: compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compra compra = db.compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: compras/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.produtoes, "Id", "Nome");
            return View();
        }

        // POST: compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Idcompra,ID,Quantidade,ValorTotal")] compra compra)
        {
            produto produtoatual = NewMethod(compra);
            promocao promocaoatual = db.promocaos.Find(produtoatual.Id);

            switch (produtoatual.Id)
            {
                case 2 when compra.Quantidade > 3:
                    compra.ValorTotal = (compra.Quantidade / 3) * produtoatual.Valor;

                    if ((compra.Quantidade % 3) > 0)
                    {
                        compra.ValorTotal = compra.ValorTotal + produtoatual.Valor; //TODO buscar valores das tabelas
                    }

                    break;
                case 3:
                    compra.ValorTotal = compra.Quantidade * produtoatual.Valor;
                    compra.Quantidade = 2 * compra.Quantidade;
                    break;
                default:
                    compra.ValorTotal = compra.Quantidade * produtoatual.Valor;
                    break;
            }

            if (ModelState.IsValid)
            {
                db.compras.Add(compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.produtoes, "Id", "Nome", compra.ID);
            return View(compra);
        }

        private produto NewMethod(compra compra)
        {
            return db.produtoes.Find(compra.ID);
        }

        // GET: compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compra compra = db.compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.produtoes, "Id", "Nome", compra.ID);
            return View(compra);
        }

        // POST: compras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Idcompra,ID,Quantidade,ValorTotal")] compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.produtoes, "Id", "Nome", compra.ID);
            return View(compra);
        }

        // GET: compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compra compra = db.compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            compra compra = db.compras.Find(id);
            db.compras.Remove(compra);
            db.SaveChanges();
            return RedirectToAction("Index");
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
