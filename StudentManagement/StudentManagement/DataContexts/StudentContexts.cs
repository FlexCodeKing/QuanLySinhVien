using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace StudentManagement.DataContexts
{
    public class StudentContexts
    {
        private int nextStudentsId = 1;

        public List<Students> Student { get; set; }
        private readonly string filePath;

       public List<Students> ReadDataFromCsvAndUpdateId(string filePath)
        {
            Student = new List<Students>();
            nextStudentsId = 1;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    reader.ReadLine();

                    while (!reader.EndOfStream) 
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        if (values.Length >= 5) 
                        {
                            Students student = new Students
                            {
                                StudentsID = int.Parse(values[0]),
                                StudentsName = values[1],
                                StudentsPhone = values[2],
                                StudentsEmail = values[3],
                                StudentsAddress = values[4],
                            };

                            Student.Add(student);

                            if (student.StudentsID >= nextStudentsId)
                            {
                                nextStudentsId =  student.StudentsID +1;
                            }
                        }
                    }
                }
            }
        }
    }
}
