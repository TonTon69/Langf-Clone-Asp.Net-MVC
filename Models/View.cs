namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("View")]
    public partial class View
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ViewCount { get; set; }
    }
}
