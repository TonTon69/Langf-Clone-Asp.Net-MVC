namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Food")]
    public partial class Food
    {
        public int FoodID { get; set; }

        public int KofID { get; set; }

        [StringLength(50)]
        public string FoodName { get; set; }

        public decimal? FoodPrice { get; set; }

        public string Description { get; set; }

        public virtual KindOfFood KindOfFood { get; set; }
    }
}
