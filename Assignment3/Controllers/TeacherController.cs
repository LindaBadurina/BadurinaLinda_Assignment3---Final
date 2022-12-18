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
        public ActionResult List(string SearchKey)
        {
            TeacherDataController cont = new TeacherDataController();
            IEnumerable<Teacher> Teachers = cont.ListTeachers(SearchKey);
            return View(Teachers);
        }
        //GET /Teacher/Show
        //Calls exactly how the instructor does it in her code, but it doesn't work, as I explained in the TeacherDataController comment.
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);


            return View(SelectedTeacher);
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
        public ActionResult Create(string teacherfname, string teacherlname, string employeenumber, DateTime hireDate, decimal salary)
        { 
            Teacher newTeacher = new Teacher();
            newTeacher.hiredate = hireDate;
            newTeacher.salary = salary;
            newTeacher.employeenumber = employeenumber;
            newTeacher.teacherfname = teacherfname;
            newTeacher.teacherlname = teacherlname;

            TeacherDataController tdc = new TeacherDataController();
            tdc.AddTeacher(newTeacher);
            return RedirectToAction("List");
        }

        //Routes to a dynamically generated "Teacher Update" page. Gathers information from the database.
        //GET: /Author/Update/{id}
        public ActionResult Update(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);


            return View(SelectedTeacher);
        }
        //Receives a POST request containing info about an existing teacher in the system, with new values. Conveys this info to the API, and
        //redirects to the "Teacher Show" page of our updated teacher.
        //As an example, 
        //POST : Teacher/Update/1
        //FORM DATA / POST DATA / REQUEST BODY
        /*
        {
        "Teacherfname" : "Alexander",
        "Teacherlname" : "Bennett",
        "employeenumber" : "T378",
        "hiredate" : "2016-08-05 00:00:00",
        "salary" : "55.30"
        }
        */
        [HttpPost]
        public ActionResult Update(int id, string teacherfname, string teacherlname, string employeenumber, DateTime hireDate, decimal salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.hiredate = hireDate;
            TeacherInfo.salary = salary;
            TeacherInfo.employeenumber = employeenumber;
            TeacherInfo.teacherfname = teacherfname;
            TeacherInfo.teacherlname = teacherlname;

            TeacherDataController tdc = new TeacherDataController();
            tdc.UpdateTeacher(id, TeacherInfo);
            return RedirectToAction("Show/" + id);
        }
    }
}

