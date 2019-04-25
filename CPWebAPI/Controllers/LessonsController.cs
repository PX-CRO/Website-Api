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
    public class LessonsController : ApiController
    {
        private MyContext db = new MyContext();

        // GET: api/Lessons
        public IQueryable<Lesson> GetLesson()
        {
            return db.Lesson;
        }

        // GET: api/Lessons/5
        [ResponseType(typeof(Lesson))]
        public IHttpActionResult GetLesson(int id)
        {
            Lesson lesson = db.Lesson.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            return Ok(lesson);
        }

        // PUT: api/Lessons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLesson(int id, Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lesson.Id)
            {
                return BadRequest();
            }

            db.Entry(lesson).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
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

        // POST: api/Lessons
        [ResponseType(typeof(Lesson))]
        public IHttpActionResult PostLesson(Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lesson.Add(lesson);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lesson.Id }, lesson);
        }

        // DELETE: api/Lessons/5
        [ResponseType(typeof(Lesson))]
        public IHttpActionResult DeleteLesson(int id)
        {
            Lesson lesson = db.Lesson.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            db.Lesson.Remove(lesson);
            db.SaveChanges();

            return Ok(lesson);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LessonExists(int id)
        {
            return db.Lesson.Count(e => e.Id == id) > 0;
        }
    }
}