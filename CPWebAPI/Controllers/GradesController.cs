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
    public class GradesController : ApiController
    {
        private MyContext db = new MyContext();

        // GET: api/Grades
        public IQueryable<Grade> GetGrade()
        {
            return db.Grade;
        }

        // GET: api/Grades/5
        [ResponseType(typeof(Grade))]
        public IHttpActionResult GetGrade(int id)
        {
            Grade grade = db.Grade.Find(id);
            if (grade == null)
            {
                return NotFound();
            }

            return Ok(grade);
        }

        // PUT: api/Grades/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGrade(int id, Grade grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grade.Id)
            {
                return BadRequest();
            }

            db.Entry(grade).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
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

        // POST: api/Grades
        [ResponseType(typeof(Grade))]
        public IHttpActionResult PostGrade(Grade grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Grade.Add(grade);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = grade.Id }, grade);
        }

        // DELETE: api/Grades/5
        [ResponseType(typeof(Grade))]
        public IHttpActionResult DeleteGrade(int id)
        {
            Grade grade = db.Grade.Find(id);
            if (grade == null)
            {
                return NotFound();
            }

            db.Grade.Remove(grade);
            db.SaveChanges();

            return Ok(grade);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GradeExists(int id)
        {
            return db.Grade.Count(e => e.Id == id) > 0;
        }
    }
}