using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using facebooklet.Connectors;
using facebooklet.Models;


namespace facebooklet.Controllers
{
    public class CommentController : Controller
    {
        private readonly DbConnector SQL;
 
        public CommentController(DbConnector connect)
        {
            SQL = connect;
        }
        [HttpPost]
        [Route("CreateComment")]
        public void CreateComment(int message_id, string text)
        {
            SQL.Execute($"INSERT INTO comments (user_id, message_id, text, createdat, updatedat) VALUES ('{HttpContext.Session.GetInt32("LoggedInUserID")}', '{message_id}', '{text}', '{String.Format("{0:yyyy-MM-dd HH:mm:ss tt}", DateTime.Now)}', '{String.Format("{0:yyyy-MM-dd HH:mm:ss tt}", DateTime.Now)}')");
        }
        [HttpGet]
        [Route("GetComments")]
        public JsonResult GetComments(int message_id)
        {   
            return Json(SQL.Query("SELECT comments.message_id, comments.text, comments.createdat AS commentcreatedat, users.firstname, users.lastname FROM comments JOIN users ON users.id = comments.user_id").Where( comment => (int)comment["message_id"] == message_id).OrderBy(comment => comment["commentcreatedat"]));
        }
    }
}