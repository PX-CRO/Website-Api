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
    public class DiscontinuitiesController : ApiController
    {
        private DBClassroomEntities1 db = new DBClassroomEntities1();

        // GET: api/Discontinuities
        public IQueryable<Discontinuity> GetDiscontinuity()
        {
            return db.Discontinuity;
        }

        // GET: api/Discontinuities/5
        [ResponseType(typeof(Discontinuity))]
        public IHttpActionResult GetDiscontinuity(int id)
        {
            Discontinuity discontinuity = db.Discontinuity.Find(id);
            if (discontinuity == null)
            {
                return NotFound();
            }

            return Ok(discontinuity);
        }

        // PUT: api/Discontinuities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiscontinuity(int id, Discontinuity discontinuity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discontinuity.Id)
            {
                return BadRequest();
            }

            db.Entry(discontinuity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscontinuityExists(id))
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

        // POST: api/Discontinuities
        [ResponseType(typeof(Discontinuity))]
        public IHttpActionResult PostDiscontinuity(Discontinuity discontinuity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Discontinuity.Add(discontinuity);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = discontinuity.Id }, discontinuity);
        }

        // DELETE: api/Discontinuities/5
        [ResponseType(typeof(Discontinuity))]
        public IHttpActionResult DeleteDiscontinuity(int id)
        {
            Discontinuity discontinuity = db.Discontinuity.Find(id);
            if (discontinuity == null)
            {
                return NotFound();
            }

            db.Discontinuity.Remove(discontinuity);
            db.SaveChanges();

            return Ok(discontinuity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscontinuityExists(int id)
        {
            return db.Discontinuity.Count(e => e.Id == id) > 0;
        }
    }
}