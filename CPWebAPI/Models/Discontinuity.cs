namespace CPWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Discontinuity")]
    public partial class Discontinuity
    {
        public int Id { get; set; }

        public int? Student_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public virtual Student Student { get; set; }
    }
}
