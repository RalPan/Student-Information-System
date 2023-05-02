using MVC_SIS_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SIS_UI.Models
{
    public class AddStudentVM : IValidatableObject
    {
        public Student Student { get; set; }
        public List<SelectListItem> CourseItems { get; set; }
        public List<SelectListItem> MajorItems { get; set; }
        public List<SelectListItem> StateItems { get; set; }
        public List<int> SelectedCourseIds { get; set; }

        public AddStudentVM()
        {
            CourseItems = new List<SelectListItem>();
            MajorItems = new List<SelectListItem>();
            StateItems = new List<SelectListItem>();
            SelectedCourseIds = new List<int>();
            Student = new Student();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Student == null || Student.FirstName == "" || Student.FirstName == null)
            {
                errors.Add(new ValidationResult("Please enter the student's first name.",
                    new[] { "Student.FirstName"}));
            }

            if (Student == null || Student.LastName == "" || Student.LastName == null)
            {
                errors.Add(new ValidationResult("Please enter the student's last name.",
                    new[] { "Student.LastName" }));
            }

            if (Student == null || string.IsNullOrEmpty(Student.GPA.ToString()))
            {
                errors.Add(new ValidationResult("Please enter the student's GPA.",
                    new[] { "Student.GPA" }));
            }

            if (Student == null || string.IsNullOrEmpty(Student.Major.MajorId.ToString()))
            {
                errors.Add(new ValidationResult("Please select the student's major.",
                    new[] { "Student.Major.MajorId" }));
            }

            if (SelectedCourseIds.Count == 0)
            {
                errors.Add(new ValidationResult("Please select the student's courses.",
                    new[] { "Student.Courses" }));
            }

            return errors;
        }

        public void SetCourseItems(IEnumerable<Course> courses)
        {
            foreach (var course in courses)
            {
                CourseItems.Add(new SelectListItem()
                {
                    Value = course.CourseId.ToString(),
                    Text = course.CourseName
                });
            }
        }

        public void SetMajorItems(IEnumerable<Major> majors)
        {
            foreach (var major in majors)
            {
                MajorItems.Add(new SelectListItem()
                {
                    Value = major.MajorId.ToString(),
                    Text = major.MajorName
                });
            }
        }

        public void SetStateItems(IEnumerable<State> states)
        {
            foreach (var state in states)
            {
                StateItems.Add(new SelectListItem()
                {
                    Value = state.StateAbbreviation,
                    Text = state.StateName
                });
            }
        }
    }
}