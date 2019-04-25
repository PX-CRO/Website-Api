namespace CPWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Grade")]
    public partial class Grade
    {
        public int Id { get; set; }

        public int? Student_Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Mark { get; set; }

        [StringLength(10)]
        public string Ranking { get; set; }

        public virtual Student Student { get; set; }
    }
}
