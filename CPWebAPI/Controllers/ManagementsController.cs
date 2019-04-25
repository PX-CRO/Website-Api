using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CPWebAPI.Models;

namespace CPWebAPI.Controllers
{
    public class ManagementsController : ApiController
    {
        private MyContext db = new MyContext();

        // GET: api/Managements
        public IQueryable<Management> GetManagement()
        {
            return db.Management;
        }

        // GET: api/Managements/5
        [ResponseType(typeof(Management))]
        public IHttpActionResult GetManagement(int id)
        {
            Management management = db.Management.Find(id);
            if (management == null)
            {
                return NotFound();
            }

            return Ok(management);
        }

        // PUT: api/Managements/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutManagement(int id, Management management)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != management.Id)
            {
                return BadRequest();
            }

            db.Entry(management).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Managements
        [ResponseType(typeof(Management))]
        public IHttpActionResult PostManagement(Management management)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Management.Add(management);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = management.Id }, management);
        }

        // DELETE: api/Managements/5
        [ResponseType(typeof(Management))]
        public IHttpActionResult DeleteManagement(int id)
        {
            Management management = db.Management.Find(id);
            if (management == null)
            {
                return NotFound();
            }

            db.Management.Remove(management);
            db.SaveChanges();

            return Ok(management);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ManagementExists(int id)
        {
            return db.Management.Count(e => e.Id == id) > 0;
        }
    }
}