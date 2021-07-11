namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Langf")]
    public partial class Langf
    {
        [Key]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string FanpageFB { get; set; }

        [StringLength(250)]
        public string GroupFB { get; set; }

        [StringLength(250)]
        public string Address1 { get; set; }

        [StringLength(250)]
        public string Address2 { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }

        [StringLength(250)]
        public string Email { get; set; }
    }
}
