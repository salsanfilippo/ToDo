using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class ToDoController : ApiController
    {
        private ToDoContext db = new ToDoContext();

        private HttpResponseMessage CreateResponse(HttpStatusCode statusCode)
        {
            HttpResponseMessage message = Request.CreateResponse(statusCode);
            message.Headers.Add("Access-Control-Allow-Origin", "*");
            return message;
        }

        private HttpResponseMessage CreateResponse<T>(HttpStatusCode statusCode, T payload)
        {
            HttpResponseMessage message = Request.CreateResponse(statusCode, payload);
            message.Headers.Add("Access-Control-Allow-Origin", "*");
            return message;
        }

        // GET api/ToDo
        public HttpResponseMessage GetToDoItems(string q = null, 
                                                string sort = null, 
                                                bool desc = false,
                                                int? limit = null, 
                                                int offset = 0)
        {
            var list = ((IObjectContextAdapter)db).ObjectContext.CreateObjectSet<ToDoItem>();

            IQueryable<ToDoItem> items = string.IsNullOrEmpty(sort) ? list.OrderBy(o => o.Id)
                                                                    : list.OrderBy(String.Format("it.{0} {1}", sort, desc ? "DESC" : "ASC"));

            if (!string.IsNullOrEmpty(q) && q != "undefined") 
                items = items.Where(t => t.Todo.Contains(q));

            if (offset > 0) 
                items = items.Skip(offset);
            
            if (limit.HasValue) 
                items = items.Take(limit.Value);

            return CreateResponse(HttpStatusCode.OK, items);
        }

        // GET api/ToDo/5
        public HttpResponseMessage GetToDoItem(int id)
        {
            ToDoItem todoitem = db.ToDoItems.Find(id);
            if (todoitem == null)
            {
                throw new HttpResponseException(CreateResponse(HttpStatusCode.NotFound));
            }

            return CreateResponse(HttpStatusCode.OK, todoitem);
        }

        // PUT api/ToDo/5
        public HttpResponseMessage PutToDoItem(int id, ToDoItem todoitem)
        {
            if (ModelState.IsValid && id == todoitem.Id)
            {
                db.Entry(todoitem).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return CreateResponse(HttpStatusCode.NotFound);
                }

                return CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/ToDo
        public HttpResponseMessage PostToDoItem(ToDoItem todoitem)
        {
            if (ModelState.IsValid)
            {
                db.ToDoItems.Add(todoitem);
                db.SaveChanges();

                HttpResponseMessage response = CreateResponse(HttpStatusCode.Created, todoitem);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = todoitem.Id }));
                return response;
            }
            else
            {
                return CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/ToDo/5
        public HttpResponseMessage DeleteToDoItem(int id)
        {
            ToDoItem todoitem = db.ToDoItems.Find(id);
            if (todoitem == null)
            {
                return CreateResponse(HttpStatusCode.NotFound);
            }

            db.ToDoItems.Remove(todoitem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return CreateResponse(HttpStatusCode.NotFound);
            }

            return CreateResponse(HttpStatusCode.OK, todoitem);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}