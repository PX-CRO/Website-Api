namespace CPWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questions
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Question { get; set; }

        [StringLength(250)]
        public string A { get; set; }

        [StringLength(250)]
        public string B { get; set; }

        [StringLength(250)]
        public string C { get; set; }

        [StringLength(250)]
        public string D { get; set; }

        [StringLength(250)]
        public string E { get; set; }

        [StringLength(1)]
        public string Answer { get; set; }

        public string Solution { get; set; }

        public int? LessonID { get; set; }

        public int? TeacherID { get; set; }

        public virtual Lesson Lesson { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
