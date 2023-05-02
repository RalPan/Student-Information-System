using MVC_SIS_Data;
using MVC_SIS_Models;
using MVC_SIS_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_SIS_UI.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            
            var viewModel = new AddStudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(AddStudentVM studentVM)
        {
            
             if (!ModelState.IsValid)
            {
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());
                return View(studentVM);
            }
             
            studentVM.Student.Courses = new List<Course>();

            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            StudentRepository.Add(studentVM.Student);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            EditStudentVM viewmodel = new EditStudentVM();
            viewmodel.SetCourseItems(CourseRepository.GetAll());
            viewmodel.SetMajorItems(MajorRepository.GetAll());
            viewmodel.SetStateItems(StateRepository.GetAll());
            viewmodel.Student = StudentRepository.Get(id);
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult EditStudent(EditStudentVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.SetCourseItems(CourseRepository.GetAll());
                viewModel.SetMajorItems(MajorRepository.GetAll());
                viewModel.SetStateItems(StateRepository.GetAll());
                viewModel.Student = StudentRepository.Get(viewModel.Student.StudentId);
                return View(viewModel);
            }
            
            viewModel.Student.Major = MajorRepository.Get(viewModel.Student.Major.MajorId);
            foreach(int id in viewModel.SelectedCourseIds)
            {
                if (!viewModel.Student.Courses.Contains(CourseRepository.Get(id)))
                {
                    viewModel.Student.Courses.Add(CourseRepository.Get(id));
                }
                
            }

            if(viewModel.Student.Address != null)
            {
                if (viewModel.Student.Address.State.StateAbbreviation != null)
                {
                    viewModel.Student.Address.State = StateRepository.Get(viewModel.Student.Address.State.StateAbbreviation);
                }
                    
            }
            StudentRepository.Edit(viewModel.Student);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult DeleteStudent(int id)
        {
            DeleteStudentVM viewModel = new DeleteStudentVM();
            viewModel.currentStudent = StudentRepository.Get(id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteStudent(DeleteStudentVM viewModel)
        {
            StudentRepository.Delete(viewModel.currentStudent.StudentId);
            return RedirectToAction("List");
        }
    }
}