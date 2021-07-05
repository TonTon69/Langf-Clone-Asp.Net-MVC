namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advertisement")]
    public partial class Advertisement
    {
        [Key]
        public int AdID { get; set; }

        [StringLength(250)]
        public string AdTitle { get; set; }

        public string Image { get; set; }

        public bool? Visible { get; set; }
    }
}
