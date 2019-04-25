namespace CPWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Discontinuity = new HashSet<Discontinuity>();
            Grade = new HashSet<Grade>();
            Payment = new HashSet<Payment>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public int? Class_Id { get; set; }

        public int? Group_Id { get; set; }

        [StringLength(11)]
        public string TCno { get; set; }

        [StringLength(50)]
        public string FName { get; set; }

        [StringLength(50)]
        public string LName { get; set; }

        public bool? Gender { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string eMail { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public bool? Status { get; set; }

        public DateTime? DateOfJoin { get; set; }

        public DateTime? DateOfLeave { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(50)]
        public string ParentName { get; set; }

        [StringLength(15)]
        public string ParentPhone1 { get; set; }

        [StringLength(15)]
        public string ParentPhone2 { get; set; }

        public bool? ParentGender { get; set; }

        [StringLength(250)]
        public string Photo { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discontinuity> Discontinuity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Grade> Grade { get; set; }

        public virtual Group Group { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
