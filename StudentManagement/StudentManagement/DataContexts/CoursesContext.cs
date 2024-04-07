using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement.DataContexts
{
    public class CoursesContext
    { 
            private int nextID = 1;
            public List<Courses> Courses { get; set; }
            private readonly string coursesFilePath;

            public CoursesContext(string coursesFilePah)
            {
                this.coursesFilePath = coursesFilePath;
                Courses = ReadDataFromCsvAndUpdateId(coursesFilePath);
            }

            public void InsertCourse(Courses course)
            {
                course.ID = nextID++; // Assign the next available Id and increment the counter
                Courses.Add(course);
                WriteDataToCsv(coursesFilePath);
            }

            public void UpdateCourse(int ID, Courses updatedCourse)
            {
                Courses existingCourse = Courses.FirstOrDefault(c => c.ID == ID);

                if (existingCourse != null)
                {
                    existingCourse.CourseName = updatedCourse.CourseName;

                    WriteDataToCsv(coursesFilePath);
                }
                else
                {
                    Console.WriteLine($"Course with ID {ID} not found.");
                }
            }

            public void DeleteCourse(int ID)
            {
                Courses courseToRemove = Courses.FirstOrDefault(c => c.ID == ID);

                if (courseToRemove != null)
                {
                    Courses.Remove(courseToRemove);
                    WriteDataToCsv(coursesFilePath);
                }
                else
                {
                    Console.WriteLine($"Course with ID {ID} not found.");
                }
            }

            // Method to read data from a CSV file and populate Courses, updating nextID
            public List<Courses> ReadDataFromCsvAndUpdateId(string coursesFilePathh)
            {
                Courses = new List<Courses>();
                nextID = 1; // Reset the counter

                if (File.Exists(coursesFilePath))
                {
                    using (StreamReader reader = new StreamReader(coursesFilePath))
                    {
                        // Skip the header line
                        reader.ReadLine();

                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] values = line.Split(',');

                            if (values.Length >= 2)
                            {
                                Courses course = new Courses
                                {
                                    ID = int.Parse(values[0]),
                                    CourseName = values[1],
                                };

                                Courses.Add(course);

                                // Update nextID if needed
                                if (course.ID >= nextID)
                                {
                                    nextID = course.ID + 1;
                                }
                            }
                        }
                    }
                }

                return Courses;
            }

            private void WriteDataToCsv(string coursesFilePath)
            {
                using (StreamWriter writer = new StreamWriter(coursesFilePath))
                {
                    // Write header
                    writer.WriteLine("ID,CourseName");

                    // Write data rows
                    foreach (var course in Courses)
                    {
                        writer.WriteLine($"{course.ID},{course.CourseName}");
                    }
                }
            } 
    }
}
