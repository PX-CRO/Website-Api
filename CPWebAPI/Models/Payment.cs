namespace CPWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        public int Id { get; set; }

        public int? Student_Id { get; set; }

        [StringLength(50)]
        public string PayerNameSurname { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Total { get; set; }

        public virtual Student Student { get; set; }
    }
}
