namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner
    {
        public int BannerID { get; set; }

        [Column(TypeName = "text")]
        public string Image { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }
    }
}
