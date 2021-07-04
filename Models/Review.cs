namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        public int ReviewID { get; set; }

        public int UserID { get; set; }

        public int StoreID { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        [Column(TypeName = "text")]
        public string Image { get; set; }

        public int? Star { get; set; }

        public virtual Account Account { get; set; }

        public virtual Store Store { get; set; }
    }
}
