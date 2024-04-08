using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement.DataContexts
{
    public class CoursesContext
    {
        private int nextID = 1;
        public List<modelCourses> Course { get; set; }
        private readonly string coursesFilePath;

        public CoursesContext(string coursesFilePah)
        {
            this.coursesFilePath = coursesFilePath;
            Course = ReadDataFromCsvAndUpdateId(coursesFilePath);
        }

        public void InsertCourse(modelCourses course)
        {
            course.ID = nextID++; // Assign the next available Id and increment the counter
            Course.Add(course);
            WriteDataToCsv(coursesFilePath);
        }

        public void UpdateCourse(int ID, modelCourses updatedCourse)
        {
            modelCourses existingCourse = Course.FirstOrDefault(c => c.ID == ID);

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
            modelCourses courseToRemove = Course.FirstOrDefault(c => c.ID == ID);

            if (courseToRemove != null)
            {
                Course.Remove(courseToRemove);
                WriteDataToCsv(coursesFilePath);
            }
            else
            {
                Console.WriteLine($"Course with ID {ID} not found.");
            }
        }

        // Method to read data from a CSV file and populate Courses, updating nextID
        public List<modelCourses> ReadDataFromCsvAndUpdateId(string coursesFilePathh)
        {
            Course = new List<modelCourses>();
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

                        if (values.Length >= 4)
                        {
                            modelCourses course = new modelCourses
                            {
                                ID = int.Parse(values[0]),
                                CourseName = values[1],
                                CourseDayofweek = values[2],
                                CourseTime = values[3]
                            };

                            Course.Add(course);

                            // Update nextID if needed
                            if (course.ID >= nextID)
                            {
                                nextID = course.ID + 1;
                            }
                        }
                    }
                }
            }

            return Course;
        }

        private void WriteDataToCsv(string coursesFilePath)
        {
            using (StreamWriter writer = new StreamWriter(coursesFilePath))
            {
                // Write header
                writer.WriteLine("ID,CourseName,CourseDayOfWeek, CourseTime");

                // Write data rows
                foreach (var course in Course)
                {
                    writer.WriteLine($"{course.ID},{course.CourseName},{course.CourseDayofweek},{course.CourseTime}");
                }
            }
        }
    }
}
