using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // for session
using facebooklet.Connectors;
using facebooklet.Models;
using System.Linq;

namespace facebooklet.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbConnector SQL;
 
        public HomeController(DbConnector connect)
        {
            SQL = connect;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index(string message = null)
        {
            if(HttpContext.Session.GetString("LoggedIn?") == "Yes")
            {
                ViewBag.loggedinuser = $"{HttpContext.Session.GetString("LoggedInUserFirstName")} {HttpContext.Session.GetString("LoggedInUserLastName")}";
                ViewBag.AllMessages = SQL.Query("SELECT user_id, messages.id AS message_id, text, messages.createdat AS messagecreatedat, users.firstname, users.lastname FROM messages JOIN users ON users.id = messages.user_id ORDER BY messagecreatedat DESC");
                return View("Wall");
            }
            ViewBag.message = message;
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel input)
        {
            List<Dictionary<string,object>> alltakenemails = SQL.Query("SELECT email FROM users");
            for(int i = 0; i < alltakenemails.Count; i++)
            {
                if(input.email == (string)alltakenemails[i]["email"])
                {
                    return RedirectToAction("Index", new {message = "E-Mail already taken"});
                }
            }
            if(ModelState.IsValid)
            {
                SQL.Execute($"INSERT INTO users (firstname, lastname, email, password, createdat, updatedat) VALUES('{input.firstname}', '{input.lastname}', '{input.email}', '{input.password}', '{String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)}', '{String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)}')");
                return RedirectToAction("Index", new {message = $"Successfully registered {input.email}"});
            }
            else
            {
                ViewBag.valid = false;
                ViewBag.errors = ModelState.Values;
                return View("Index");
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string email, string password)
        {
            List<Dictionary<string,object>> login = SQL.Query($"SELECT * FROM users WHERE email = '{email}'");
            if(login.Count < 1 || (string)login[0]["password"] != password)
            {
                return RedirectToAction("Index", new {message = "Invalid Login"});
            }
            else
            {
                HttpContext.Session.SetString("LoggedIn?", "Yes");
                HttpContext.Session.SetInt32("LoggedInUserID", (int)login[0]["id"]);
                HttpContext.Session.SetString("LoggedInUserFirstName", (string)login[0]["firstname"]);
                HttpContext.Session.SetString("LoggedInUserLastName", (string)login[0]["lastname"]);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}