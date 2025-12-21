using Gymnasieskola.Data;
using Gymnasieskola.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        //remake to string list to generalize print method
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

        public Class GetClassList(int classId)
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
    }
}
