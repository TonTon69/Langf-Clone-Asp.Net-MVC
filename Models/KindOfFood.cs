namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KindOfFood")]
    public partial class KindOfFood
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KindOfFood()
        {
            Foods = new HashSet<Food>();
        }

        [Key]
        public int KofID { get; set; }

        public int StoreID { get; set; }

        public int CFoodID { get; set; }

        [StringLength(50)]
        public string KofName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual CategoryFood CategoryFood { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Food> Foods { get; set; }

        public virtual Store Store { get; set; }
    }
}
