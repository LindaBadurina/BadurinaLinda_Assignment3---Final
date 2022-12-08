using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Assignment3.Models;
namespace Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        //GET : /Teacher
        public ActionResult Index()
        {
            // private static Assignment3.Models.Teacher t = new Assignment3.Models.Teacher();
            return View();
        }
        //GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController cont = new TeacherDataController();
            IEnumerable<Teacher> Teachers = cont.ListTeachers(SearchKey);
            return View(Teachers);
        }
        //GET /Teacher/Show
        //Calls exactly how the instructor does it in her code, but it doesn't work, as I explained in the TeacherDataController comment.
        public ActionResult Show(string id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewAuthor = controller.FindTeacher(id);


            return View(NewAuthor);
        }
        //GET : /Teacher/DeleteConfirm/{id}
        //Launches the deleteconfirm.cshtml page to confirm teacher deletion
        public ActionResult DeleteConfirm(string id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewAuthor = controller.FindTeacher(id);


            return View(NewAuthor);
        }
        //POST : /Teacher/Delete/{id}
        //Calls the DeleteTeacher method to delete a teacher
        [HttpPost]
        public ActionResult Delete(string id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        //GET : /Teacher/New
        //Launches the New.cshtml page
        public ActionResult New()
        {
            return View();
        }

        //POST : /Teacher/Create
        //Creates a new teacher
        [HttpPost]
        public ActionResult Create(string teacherfname, string teacherlname, string employeenumber)
        {
            return RedirectToAction("List");
            Teacher newTeacher = new Teacher();
            newTeacher.employeenumber = employeenumber;
            newTeacher.teacherfname = teacherfname;
            newTeacher.teacherlname = teacherlname;

            TeacherDataController tdc = new TeacherDataController();
            tdc.AddTeacher(newTeacher);

        }
    }
}

