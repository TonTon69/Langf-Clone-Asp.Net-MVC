using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace langfvn.Models
{
    public class StoreView
    {
        public StoreView()
        {

        }
        public StoreView(int storeID, int placeID, String storeName, String address, String img, String note, int? star, double? averageStar, String review, String place)
        {
            this.StoreID = storeID;
            this.PlaceID = placeID;
            this.StoreName = storeName;
            this.Address = address;
            this.Image = img;
            this.NoteDiscount = note;
            this.StarOfReview = star;
            this.AverageStar = averageStar;
            this.Review = review;
            this.Place = place;
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

        public int? StarOfReview { get; set; }

        public double? AverageStar { get; set; }

        public string Review { get; set; }

        public string Place { get; set; }
    }

}
