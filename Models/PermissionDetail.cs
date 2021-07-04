namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PermissionDetail")]
    public partial class PermissionDetail
    {
        [Key]
        public int PerDetID { get; set; }

        public int PerID { get; set; }

        [StringLength(50)]
        public string Code_Action { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
