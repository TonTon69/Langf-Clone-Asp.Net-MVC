namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryFood")]
    public partial class CategoryFood
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategoryFood()
        {
            KindOfFoods = new HashSet<KindOfFood>();
        }

        [Key]
        public int CFoodID { get; set; }

        [StringLength(50)]
        public string CFoodName { get; set; }

        [Column(TypeName = "text")]
        public string Image { get; set; }

        public DateTime? CreatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KindOfFood> KindOfFoods { get; set; }
    }
}
