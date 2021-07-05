namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        public int NewsID { get; set; }

        public int KonID { get; set; }

        public int UserID { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public int? TotalView { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Account Account { get; set; }

        public virtual KindOfNew KindOfNew { get; set; }
    }
}
