using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVC5HW.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public List<SelectListItem> 客戶分類ItemList(string selectValue)
        {
            List<SelectListItem> 分類ItemList = new List<SelectListItem>();
            分類ItemList.AddRange(new[]{
                new SelectListItem() {Text = "請選擇", Value = ""},
                new SelectListItem() {Text = "公司", Value = "公司"},
                new SelectListItem() {Text = "學校", Value = "學校"},
                new SelectListItem() {Text = "政府", Value = "政府"},
                new SelectListItem() {Text = "私人", Value = "私人"},
                new SelectListItem() {Text = "公益", Value = "公益"},
                new SelectListItem() {Text = "其他", Value = "其他"}
             });
            //預設選擇哪一筆
            if (!String.IsNullOrEmpty(selectValue))
            {
                分類ItemList.Where(q => q.Value == selectValue).First().Selected = true;
            }
            else {
                分類ItemList.Where(q => q.Value == "").First().Selected = true;
            }
            return 分類ItemList;
        }

        public IQueryable<客戶資料> searchALL(string sortOrder, string searchString, string 客戶分類ItemList)
        {
            var 客where = this.All().Where(p => false == p.是否已刪除).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                客where = 客where.Where(s => s.客戶名稱.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(客戶分類ItemList))
            {
                客where = 客where.Where(s => s.客戶分類.Contains(客戶分類ItemList));
            }
            switch (sortOrder)
            {
                case "客戶名稱D":
                    客where = 客where.OrderByDescending(s => s.客戶名稱);
                    break;
                case "統一編號":
                    客where = 客where.OrderBy(s => s.統一編號);
                    break;
                case "統一編號D":
                    客where = 客where.OrderByDescending(s => s.統一編號);
                    break;
                case "電話":
                    客where = 客where.OrderBy(s => s.電話);
                    break;
                case "電話D":
                    客where = 客where.OrderByDescending(s => s.電話);
                    break;
                case "傳真":
                    客where = 客where.OrderBy(s => s.傳真);
                    break;
                case "傳真D":
                    客where = 客where.OrderByDescending(s => s.傳真);
                    break;
                case "地址":
                    客where = 客where.OrderBy(s => s.地址);
                    break;
                case "地址D":
                    客where = 客where.OrderByDescending(s => s.地址);
                    break;
                case "Email":
                    客where = 客where.OrderBy(s => s.Email);
                    break;
                case "EmailD":
                    客where = 客where.OrderByDescending(s => s.Email);
                    break;
                case "客戶分類":
                    客where = 客where.OrderBy(s => s.客戶分類);
                    break;
                case "客戶分類D":
                    客where = 客where.OrderByDescending(s => s.客戶分類);
                    break;
                default:
                    客where = 客where.OrderBy(s => s.客戶名稱);
                    break;
            }
            return 客where;
        }

        public 客戶資料 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        //修正delete
        public override void Delete(客戶資料 entity)
        {
            entity.是否已刪除 = true;
            this.UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
            //this.UnitOfWork.Commit();
        }

    }

    public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}