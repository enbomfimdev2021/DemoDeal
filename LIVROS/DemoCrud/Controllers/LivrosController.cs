using DemoCrud.AcessoDados;
using DemoCrud.Models.Entidades;
using DemoCrud.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web.Mvc;

namespace DemoCrud.Controllers
{
    public class LivrosController : Controller
    {
        private readonly LivroContexto db = new LivroContexto();

        // GET: Livros
        public ActionResult Index()
        {
            return View();
        }
        //public JsonResult Listar(string searchPhrase, int current = 1, int rowCount = 5)
        public JsonResult Listar(ParametrosPaginacao parametrosPaginacao)
        {
            DadosFiltrados dadosFiltrados = FiltrarEPaginar(parametrosPaginacao);

            return Json(dadosFiltrados, JsonRequestBehavior.AllowGet);
        }

        private DadosFiltrados FiltrarEPaginar(ParametrosPaginacao parametrosPaginacao)
        {
            var livros = db.Livros.Include(l => l.Genero);
            int total = livros.Count();

            if (!string.IsNullOrWhiteSpace(parametrosPaginacao.SearchPhrase))
            {
                int ano = 0;
                int.TryParse(parametrosPaginacao.SearchPhrase, out ano);

                var valor = 0.0m;
                decimal.TryParse(parametrosPaginacao.SearchPhrase, out valor);

                livros = livros.Where("Titulo.Contains(@0) OR Autor.Contains(@0)" +
                    " OR AnoEdicao == @1 OR Valor = @2", parametrosPaginacao.SearchPhrase, ano, valor);
            }

            var livrosPaginados = livros.OrderBy(parametrosPaginacao.CampoOrdenado).Skip((parametrosPaginacao.Current - 1) * parametrosPaginacao.RowCount).Take(parametrosPaginacao.RowCount).ToList();

            DadosFiltrados dadosFiltrados = new DadosFiltrados(parametrosPaginacao)
            {
                rows = livrosPaginados.ToList(),
                total = total
            };
            return dadosFiltrados;
        }

        // GET: Livros/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return PartialView(livro);
        }

        // GET: Livros/Create
        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(db.Generos, "GeneroId", "Nome");
            return PartialView();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "LivroId,Titulo,AnoEdicao,Autor,Valor,GeneroId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Livros.Add(livro);
                db.SaveChanges();
                return Json(new
                {
                    resultado = true,
                    mensagem = "Livro cadastrado com sucesso"
                });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(Item => Item.Errors);
                return Json(new
                {
                    resultado = false,
                    mensage = erros
                });
            }
        }

        // GET: Livros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "GeneroId", "Nome", livro.GeneroId);
            return PartialView(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit([Bind(Include = "LivroId,Titulo,AnoEdicao,Autor,Valor,GeneroId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new
                {
                    resultado = true,
                    mensagem = "Livro editado com sucesso"
                });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(Item => Item.Errors);
                return Json(new
                {
                    resultado = false,
                    mensage = erros
                });
            }
        }

        // GET: Livros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return PartialView(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                Livro livro = db.Livros.Find(id);
                db.Livros.Remove(livro);
                db.SaveChanges();

                return Json(new { resultado = true, mensagem = "Livro excluido com sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { resultado = false, mensagem = ex.Message });
            }
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
