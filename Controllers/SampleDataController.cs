using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreVueStarter.Controllers
{
    [Route("api/[controller]")] 
    public class SampleDataController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<Employee> Employees()
        {
            int i = CountOfRows("Employee.csv");
            Employee[] emp = GetEmployees(i);
            IEnumerable<Employee> employee = from item in emp select item;
            return employee.AsEnumerable();
        }
        [HttpGet("[action]/{id}")]
        public IEnumerable<Meeting> Meetings(string id)
        {
            int i = CountOfRows("Meeting.csv");
            Meeting[] meet = GetMeetings(i);
            IEnumerable<Meeting> meetings;
            
            if (id == "undefined") 
            {  
                meetings = from item in meet select item; 
            }
            else
            {
                int emp_id = Convert.ToInt32(id);
                meetings = from item in meet where item.EmployeeId == emp_id select item;
            }
            return meetings.AsEnumerable();
        }
        private Meeting[] GetMeetings (int CountRows)
        {
            Meeting[] meet = new Meeting[CountRows];
            int j = 0;

            using (var reader = new StreamReader(@"Meeting.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    var values = line.Split(';');

                    meet[j] = new Meeting
                    {
                        Id = int.Parse(values[0]),
                        Name = values[1],
                        Date = Convert.ToDateTime(values[4]),
                        EmployeeId = int.Parse(values[2]),
                        Duration = int.Parse(values[3])
                    };

                    j++;
                }
            }
            return meet;
        }
        private Employee[] GetEmployees (int CountRows)
        {
            Employee[] emp = new Employee[CountRows];
            int j = 0;
            using (var reader = new StreamReader(@"Employee.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    var values = line.Split(';');

                    emp[j] = new Employee
                    {
                        Id = int.Parse(values[0]),
                        Name = values[1]
                    };

                    j++;
                }
            }
            return emp;
        }
        private int CountOfRows(string CSVName) 
        {
            int i = 0;
            using (var reader = new StreamReader(CSVName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    i++;
                }
            }
            return i;
        }
        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
      
        }
        public class Meeting
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public int EmployeeId { get; set; }
            public int Duration { get; set; }

        }
    }
}
