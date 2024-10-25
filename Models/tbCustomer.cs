namespace Project1_Laundry.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbCustomer")]
    public partial class tbCustomer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbCustomer()
        {
            tbCashes = new HashSet<tbCash>();
        }

        public int id { get; set; }

        public int? vid { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(100)]
        public string phone { get; set; }

        [StringLength(100)]
        public string no { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        [StringLength(70)]
        public string address { get; set; }

        public int points { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCash> tbCashes { get; set; }
    }
}
