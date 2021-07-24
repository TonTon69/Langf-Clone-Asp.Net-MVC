using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CountView")]
    public partial class CountView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ViewCount { get; set; }
    }
}