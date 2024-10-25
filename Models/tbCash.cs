namespace Project1_Laundry.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbCash")]
    public partial class tbCash
    {
        public int id { get; set; }

        [StringLength(200)]
        public string transno { get; set; }

        public int? cid { get; set; }

        public int? sid { get; set; }

        public int? vid { get; set; }

        [StringLength(200)]
        public string price { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        [StringLength(100)]
        public string status { get; set; }

        public virtual tbCustomer tbCustomer { get; set; }

        public virtual tbService tbService { get; set; }

        public virtual tbType tbType { get; set; }
    }
}
