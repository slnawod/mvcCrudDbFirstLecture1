using mvcCrudDbFirstLecture1.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcCrudDbFirstLecture1.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        db_testEntities dbObj = new db_testEntities();
        public ActionResult Student(tbl_Student obj)
        {
                return View(obj);
          
        }
       [HttpPost]
        public ActionResult Index(String search)
        {
            var model = dbObj.tbl_Student.Where(x => x.name.Contains(search) || search == null).ToList();

            foreach (var item in model)
            {

            }


            return View(model);

        }
       
   

        [HttpPost]
        public ActionResult AddStudent(tbl_Student model)
   
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            tbl_Student obj = new tbl_Student();
            if (ModelState.IsValid)
            {

                
                obj.id = model.id;
                obj.name = model.name;
                obj.discription = model.discription;
                obj.purches_price = model.purches_price;
                obj.selling_price = model.selling_price;
                obj.quantity = model.quantity;

                if(model.id==0)
                {
                 dbObj.tbl_Student.Add(obj);
                 dbObj.SaveChanges();
                }
                else 
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }

                
            }

            ModelState.Clear();

            return View("Student");
        }

        public ActionResult ProductList()
        {
            var res = dbObj.tbl_Student.ToList();
            return View(res);
        }

        public ActionResult Delete(int id)
        {
            var res = dbObj.tbl_Student.Where(x => x.id == id).First();
            dbObj.tbl_Student.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.tbl_Student.ToList();

            return View("ProductList",list);
        }

    }
}