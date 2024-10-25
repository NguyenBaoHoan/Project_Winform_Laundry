namespace Project1_Laundry.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbType")]
    public partial class tbType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbType()
        {
            tbCashes = new HashSet<tbCash>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [Column("class")]
        [StringLength(50)]
        public string _class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCash> tbCashes { get; set; }
    }
}
