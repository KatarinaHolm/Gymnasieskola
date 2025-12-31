using Gymnasieskola.Data;
using Gymnasieskola.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Gymnasieskola
{
    internal class SchoolService
    {
        private School_dbContext context = new School_dbContext();

        public List<Student> OrderByNameAndAscOrDesc(bool sortFirstName, bool sortAsc)
        {           
            var query = context.Students.AsQueryable();

            if (sortFirstName && sortAsc)
            {
                query = query.OrderBy(s => s.FirstName);
            }

            else if (sortFirstName && !sortAsc)
            {
                query = query.OrderByDescending(s => s.FirstName);
            }

            else if(!sortFirstName && sortAsc)
            {
                query = query.OrderBy(s => s.LastName);
            }

            else
            {
                query = query.OrderByDescending(s => s.LastName);
            }
            
            return query.ToList();
        }

        public List<Class> GetClasses()
        {            
            return context.Classes                
                .OrderBy(c => c.ClassName)
                .ToList(); 
        }

        public List<string> GetClassNames()
        {
            return GetClasses().Select(c => c.ClassName).ToList();
        }

        public Class GetClass(int classId)
        {
            var selectedClass = context.Classes
                .Where(c => c.ClassId == classId)
                .Include(c => c.Students)
                .FirstOrDefault();

            return selectedClass;
        }

        public void AddStudentToDb(string firstName, string lastName, string socialSecurityNr, string homeAdress, string phoneNr, string email, int classId)
        {
            var newStudent = new Student(firstName, lastName, socialSecurityNr, homeAdress, phoneNr, email, classId);
            context.Students.Add(newStudent);
            context.SaveChanges();
        }

        public List<string> GetProfessions()
        {
            var professions = context.Staff
                .GroupBy(s => s.Profession.ToLower())
                .Select(g => g.First().Profession)
                .ToList();

            return professions;
        }
                
        public List<Staff> GetStaffByProfessions(string profession)
        {
            if (profession.Equals("Alla yrkesroller"))
            {
                return context.Staff.ToList();                    
            }

            var staffOfProfession = context.Staff
                .Where(s => s.Profession.ToLower().Equals(profession.ToLower()))
                .ToList();

            return staffOfProfession;
        }

        public void AddStaffToDb(string profession,string firstName, string lastName, string socialSecurityNr, string homeAdress, string phoneNr, string email)
        {
            var newStaff = new Staff(profession, firstName, lastName, socialSecurityNr, homeAdress, phoneNr, email);
            context.Staff.Add(newStaff);
            context.SaveChanges();
        }

        public List<Department> GetDepartments()
        {
            return context.Departments                
                .Include(d => d.Staff)
                .ToList();
        }
         
        public List<AcademicRecord> GetStudentsGrades()
        {
            var studentsGrades = context.AcademicRecords
                .Include(r => r.Student)
                    .ThenInclude(r => r.Class)
                .Include(r => r.Subject)
                .OrderBy(r => r.StudentId)
                .ThenBy(r => r.Subject)
                .ToList();
            return studentsGrades;
        }

        public List<AcademicRecord> GetActiveSubjects()
        {
            var activeSubjects = GetStudentsGrades()
                .Where(a => a.IsOngoing = true)
                .ToList();
            return activeSubjects;
        }

        public List<Subject> GetSubjects()
        {
            return context.Subjects.ToList();
        }

        public List<string> GetGradeLetters()
        {
            return context.GradeScales.Select(g => g.GradeLetter).ToList();
        }

        public void AddAcademicRecordToDb(string grade, DateOnly gradingDate, int studentId, int subjectId, int teacherId)
        {
            var transaction = context.Database.BeginTransaction();
            try
            {
                context.AcademicRecords.Add(new AcademicRecord()
                {
                    Grade = grade,

                    GradingDate = gradingDate,

                    StudentId = studentId,

                    SubjectId = subjectId,

                    TeacherId = teacherId
                });

                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }      
        }
       
    }
}
