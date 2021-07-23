namespace langfvn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store")]
    public partial class Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            KindOfFoods = new HashSet<KindOfFood>();
            Reviews = new HashSet<Review>();
        }

        public Store(int StoreID, int PlaceID, String StoreName, String Address, String img, String note)
        {
            this.StoreID = StoreID;
            this.PlaceID = PlaceID;
            this.StoreName = StoreName;
            this.Address = Address;
            this.Image = img;
            this.NoteDiscount = note;
        }

        public int StoreID { get; set; }

        public int PlaceID { get; set; }

        [StringLength(50)]
        public string StoreName { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public string Image { get; set; }

        [StringLength(250)]
        public string NoteDiscount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KindOfFood> KindOfFoods { get; set; }

        public virtual Place Place { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
