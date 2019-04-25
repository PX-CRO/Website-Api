namespace CPWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Announcement")]
    public partial class Announcement
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public string EntireContent { get; set; }
    }
}
