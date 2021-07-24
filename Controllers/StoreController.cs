using langfvn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace langfvn.Controllers
{
    public class StoreController : Controller
    {
        LangfvnContext db = new LangfvnContext();
        // GET: Store
        public ActionResult Index(string name)
        {
            /*lấy ra cửa hàng*/
            Store store = db.Stores.Single(s => s.StoreName == name);
            ViewData["store"] = store;

            /*lấy ra danh sách các loại món ăn cửa hàng*/
            List<KindOfFood> listKindOfFood = db.KindOfFoods.Where(k => k.Store.StoreName == name).ToList();
            ViewData["listKindOfFood"] = listKindOfFood;

            /*lấy ra các món ăn*/
            List<Food> listFood = new List<Food>();
            foreach (KindOfFood kof in listKindOfFood)
            {
                List<Food> listFoodTemp = db.Foods.Where(f => f.KofID == kof.KofID).ToList();
                foreach (Food food in listFoodTemp)
                {
                    listFood.Add(food);
                }
            }
            ViewData["listFood"] = listFood;

            /*lấy ra các review về quán ăn*/
            List<Review> listReview = db.Reviews.Where(r => r.Store.StoreName == name).ToList();
            ViewData["listReview"] = listReview;

            return View();
        }

        public ActionResult Review(Review review)
        {
            var checkStore = db.Stores.FirstOrDefault(x => x.StoreID == review.StoreID);
            Review newReview = new Review();
            newReview.UserID = review.UserID;
            newReview.StoreID = review.StoreID;
            newReview.Star = review.Star;
            newReview.Content = review.Content;
            db.Reviews.Add(newReview);
            db.SaveChanges();
            return RedirectToAction("index", "store", new { name = checkStore.StoreName });
        }
    }
}