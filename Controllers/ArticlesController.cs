using Laborator5.Data;
using Laborator5.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laborator5.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly AppDbContext db;

        public ArticlesController(AppDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var articles = from article in db.Articles
                           orderby article.Title        //sortam dupa title
                           select article;
            ViewBag.Articles = articles;
            return View();
        }

        public IActionResult Show(int id)
        {
            Article article = db.Articles.Find(id);         ///folosim find pt a gasi articolul cu id-ul respectiv
            ViewBag.Article = article;
            return View();
        }
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        public IActionResult New(Article a)
        {
            try
            {
                db.Articles.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index");               ///dupa salvare, redirectionam catre index
            }
            catch (Exception)
            {
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            Article article = db.Articles.Find(id);                 ///gasim articolul dupa id
            ViewBag.Article = article;
            return View();
        }
        [HttpPost]              ///pentru a prelua datele modificate folosim un post
        public ActionResult Edit(int id, Article requestArticle)
        {
            Article article = db.Articles.Find(id);
            try
            {
                article.Title = requestArticle.Title;           ///modificam campurile articolului cu cele primite din formular
                article.Content = requestArticle.Content;
                article.Date = requestArticle.Date;
                db.SaveChanges();           ///salvam modificarile
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Edit", article.ArticleId);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }


}