namespace Project1_Laundry.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbCostofGood")]
    public partial class tbCostofGood
    {
        public int id { get; set; }

        [StringLength(200)]
        public string costname { get; set; }

        [StringLength(200)]
        public string cost { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }
    }
}
