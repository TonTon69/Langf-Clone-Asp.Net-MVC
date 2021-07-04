namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccPer")]
    public partial class AccPer
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PerID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int AccPerID { get; set; }

        public bool? Suspended { get; set; }

        public virtual Account Account { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
