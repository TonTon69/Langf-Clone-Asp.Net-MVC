namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KindOfNew
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KindOfNew()
        {
            News = new HashSet<News>();
        }

        [Key]
        public int KonID { get; set; }

        public int CNewsID { get; set; }

        [StringLength(50)]
        public string KonName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual CategoryNew CategoryNew { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }
    }
}
