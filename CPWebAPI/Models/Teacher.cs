namespace CPWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Teacher")]
    public partial class Teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teacher()
        {
            Questions = new HashSet<Questions>();
        }

        public int Id { get; set; }

        public int Lesson_Id { get; set; }

        [Required]
        [StringLength(11)]
        public string TCno { get; set; }

        [Required]
        [StringLength(50)]
        public string FName { get; set; }

        [Required]
        [StringLength(50)]
        public string LName { get; set; }

        public bool Gender { get; set; }

        public DateTime? Birthday { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone1 { get; set; }

        [StringLength(15)]
        public string Phone2 { get; set; }

        [StringLength(50)]
        public string eMail { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public bool? Status { get; set; }

        public DateTime DateOfJoin { get; set; }

        public DateTime? DateOfLeave { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(250)]
        public string Photo { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public virtual Lesson Lesson { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Questions> Questions { get; set; }
    }
}
