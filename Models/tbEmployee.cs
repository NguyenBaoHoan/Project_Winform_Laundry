namespace Project1_Laundry.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbEmployee")]
    public partial class tbEmployee
    {
        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public int? phone { get; set; }

        [Required]
        [StringLength(50)]
        public string address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dob { get; set; }

        [StringLength(50)]
        public string gender { get; set; }

        [StringLength(50)]
        public string role { get; set; }

        [StringLength(50)]
        public string salary { get; set; }

        [StringLength(50)]
        public string password { get; set; }
    }
}
