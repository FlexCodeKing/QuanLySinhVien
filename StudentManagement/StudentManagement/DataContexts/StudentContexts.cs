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


        //Method read data from CSV file and populate students, update nextStudentId
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

                        if (values.Length >= 10) 
                        {
                            Students student = new Students
                            {
                                StudentsID = int.Parse(values[0]),
                                StudentsName = values[1],
                                StudentsPhone = values[2],
                                StudentsEmail = values[3],
                                StudentsAddress = values[4],
                                Studentsusername = values[5],
                                Studentspassword = values[6],
                                Studentscourse = values[7],
                                Studentsdayofweek = values[8],
                                Studentstime = values[9],
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
            return Student;
        }

        private void WriteDataToCsv(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("StudentsID, StudentsName, StudentsPhone, StudentsEmail, StudentsAddress, Studentsusername,Studentspassword,Studentscourse,Studentsdayofweek,Studentstime");

                foreach(var student in Student)
                {
                    writer.WriteLine($"{student.StudentsID},{student.StudentsName},{student.StudentsPhone},{student.StudentsEmail}," +
                        $"{student.StudentsAddress},{student.Studentsusername},{student.Studentspassword}, {student.Studentsusername},{student.Studentspassword},{student.Studentscourse},{student.Studentsdayofweek},{student.Studentstime}");
                }
            }
        }

        public StudentContexts(string filePath)
        {
            this.filePath = filePath;
            Student = ReadDataFromCsvAndUpdateId(filePath);
        }

        public void InsertStudent(Students student)
        {
            student.StudentsID = nextStudentsId++;
            Student.Add(student);
            WriteDataToCsv(filePath);    
        }

        public void UpdateStudent(int studentId, Students updatedStudent) 
        {
            Students existingStudent = Student.FirstOrDefault(s => s.StudentsID == studentId);

            if (existingStudent != null)
            {
                existingStudent.StudentsName= updatedStudent.StudentsName;
                existingStudent.StudentsPhone = updatedStudent.StudentsPhone;
                existingStudent.StudentsEmail = updatedStudent.StudentsEmail;
                existingStudent.StudentsAddress = updatedStudent.StudentsAddress;
                existingStudent.Studentsusername = updatedStudent.Studentsusername;
                existingStudent.Studentspassword = updatedStudent.Studentspassword;
                existingStudent.Studentscourse = updatedStudent.Studentscourse;
                existingStudent.Studentsdayofweek = updatedStudent.Studentsdayofweek;
                existingStudent.Studentstime = updatedStudent.Studentstime;

                WriteDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Student with Id {studentId} not found!");
            }
        }

        public void DeleteStudent(int studentId)
        {
            Students studentToRemove = Student.FirstOrDefault(s => s.StudentsID==studentId);

            if(studentToRemove != null)
            {
                Student.Remove(studentToRemove);
                WriteDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Student with Id {studentId} not found!");
            }
        }
    }
}
