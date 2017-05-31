using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using facebooklet.Connectors;
using facebooklet.Models;


namespace facebooklet.Controllers
{
    public class MessageController : Controller
    {
        private readonly DbConnector SQL;
 
        public MessageController(DbConnector connect)
        {
            SQL = connect;
        }
        [HttpPost]
        [Route("CreateMessage")]
        public IActionResult CreateMessage(Message input)
        {
            SQL.Execute($"INSERT INTO messages (user_id, text, createdat, updatedat) VALUES('{HttpContext.Session.GetInt32("LoggedInUserID")}', '{input.text}', '{String.Format("{0:yyyy-MM-dd HH:mm:ss tt}", DateTime.Now)}', '{String.Format("{0:yyyy-MM-dd HH:mm:ss tt}", DateTime.Now)}')");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Route("GetMessageIDs")]
        public JsonResult GetMessageIDs()
        {
            int[] AllMessageIDs = new int[GetAllMessages().Count];
            for(int i = 0; i < GetAllMessages().Count; i++)
            {
                AllMessageIDs[i] = (int)GetAllMessages()[i]["message_id"];
            }
            return Json(AllMessageIDs);
        }
        public List<Dictionary<string,object>> GetAllMessages()
        {
            return SQL.Query("SELECT user_id, messages.id AS message_id, text, messages.createdat AS messagecreatedat, users.firstname, users.lastname FROM messages JOIN users ON users.id = messages.user_id ORDER BY messagecreatedat DESC");
        }
    }
}