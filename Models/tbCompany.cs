namespace Project1_Laundry.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbCompany")]
    public partial class tbCompany
    {
        [Key]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string address { get; set; }
    }
}
