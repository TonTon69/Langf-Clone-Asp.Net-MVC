using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using langfvn.Models;
using Newtonsoft.Json;

namespace langfvn.Controllers
{
    public class HomeController : Controller
    {
        LangfvnContext db = new LangfvnContext();
        public ActionResult Index()
        {
            /*lấy ra danh sách các tin tức theo thứ tự giảm dần lượt xem*/
            News[] listNews = db.News.OrderByDescending(n => n.TotalView).ToArray();
            ViewData["listNews"] = listNews;

            /*lấy ra danh sách thể loại món ăn*/
            List<CategoryFood> listCategoryFood = db.CategoryFoods.OrderBy(s => s.CFoodID).ToList();
            ViewData["listCategoryFood"] = listCategoryFood;

            /*lấy ra các review gần đây*/
            List<Review> listReview = db.Reviews.OrderByDescending(s => s.ReviewID).ToList();
            ViewData["listReview"] = listReview;

            /*lấy ra các quán mới tham gia*/
            List<Store> newStore = db.Stores.OrderByDescending(s => s.StoreID).ToList();
            ViewData["newStore"] = newStore;

            /*lấy ra các quán sử dụng khuyến mãi*/
            List<Store> saleOffStore = db.Stores.Where(s => s.NoteDiscount != null).ToList();
            ViewData["saleOffStore"] = saleOffStore;

            /*lấy ra list các place*/
            List<Place> listPlaces = db.Places.ToList();
            ViewData["listPlace"] = listPlaces;

            /*lấy ra tất cả các cửa hàng*/
            List<Store> allStore = db.Stores.ToList();
            ViewData["allStore"] = allStore;

            return View();
        }
        public JsonResult DisplayListStory(int AreaID)
        {
            /*lấy ra danh sách store theo khu vực*/
            var jsonListStore = "";
            List<Store> listStore = new List<Store>();
            List<Store> listStoreToConvert = new List<Store>();
            if (AreaID == 0)
            {
                listStore = db.Stores.ToList();
            }
            else
            {
                listStore = db.Stores.Where(sto => sto.PlaceID == AreaID).ToList();
            }
            foreach (Store sto in listStore)
            {
                listStoreToConvert.Add(new Store(sto.StoreID, sto.PlaceID, sto.StoreName, sto.Address, sto.Image, sto.NoteDiscount));
            }

            if (listStore != null)
            {
                jsonListStore = JsonConvert.SerializeObject(listStoreToConvert, new JsonSerializerSettings()
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Formatting = Formatting.Indented
                });
            }
            return Json(jsonListStore);
        }
        public List<Store> FindStoreByCategoryFoodID(string name)
        {
            KindOfFood[] listkof = new KindOfFood[100];
            List<int> listStoreID = new List<int>();
            List<Store> listStore = new List<Store>();
            listkof = db.KindOfFoods.Where(k => k.CategoryFood.CFoodName == name).ToArray();
            for (int i = 0; i < listkof.Length; i++)
            {
                if (listStoreID.Contains(listkof[i].StoreID) == false) //list.Contains(<phần tử>) : kiểm tra phần tử có tồn tại hay không.
                {
                    listStoreID.Add(listkof[i].StoreID);
                }
            }
            foreach (int i in listStoreID)
            {
                Store sto = db.Stores.Where(s => s.StoreID == i).FirstOrDefault();
                listStore.Add(sto);
            }

            return listStore;
        }
        public ActionResult FindStoreResult(string name)
        {
            List<Store> listStore = new List<Store>();
            listStore = FindStoreByCategoryFoodID(name);
            ViewData["listStore"] = listStore;
            if (listStore.FirstOrDefault() == null)
            {
                ViewBag.resultReport = "Không tìm thấy kết quả phù hợp";
            }
            return View();
        }
        public List<Store> FindStoreByKindOfkindFoodName(String kofName)
        {
            KindOfFood[] listkof = new KindOfFood[100];
            List<int> listStoreID = new List<int>();
            List<Store> listStore = new List<Store>();
            listkof = db.KindOfFoods.Where(k => k.KofName.ToLower().Contains(kofName.ToLower())).ToArray();
            for (int i = 0; i < listkof.Length; i++)
            {
                if (listStoreID.Contains(listkof[i].StoreID) == false) //list.Contains(<phần tử>) : kiểm tra phần tử có tồn tại hay không.
                {
                    listStoreID.Add(listkof[i].StoreID);
                }
            }
            foreach (int i in listStoreID)
            {
                Store sto = db.Stores.Where(s => s.StoreID == i).FirstOrDefault();
                listStore.Add(sto);
            }

            return listStore;

        }
        public ActionResult FindStoreByKOFNameResult()
        {
            List<Store> listStore = new List<Store>();
            String kindOfFoodName = Request["valueOfFindButton"];
            listStore = FindStoreByKindOfkindFoodName(kindOfFoodName);
            ViewData["listStore"] = listStore;
            if (listStore.FirstOrDefault() == null)
            {
                ViewBag.resultReport = "Không tìm thấy kết quả phù hợp";
            }
            else
            {
                ViewBag.resultReport = kindOfFoodName;
            }

            return View();
        }
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return RedirectToAction("index", "home");
        }

    }
}