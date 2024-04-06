using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;

namespace StudentManagement.DataContexts
{
    public class ManagerContext
    {
        private int nextManagerId = 1;

        public List<Manager> Managers { get; set; }
        private readonly string fileManager;

        public ManagerContext(string fileManager)
        {
            this.fileManager = fileManager;
            Managers = ReadDataFromCsvAndUpdateId(fileManager);
        }

        public void InsertManager(Manager manager)
        {
            manager.Id = nextManagerId++; // Assign the next available Id and increment the counter
            Managers.Add(manager);
            WriteDataToCsv(fileManager);
        }

        public void UpdateManager(int managerId, Manager updatedManager)
        {
            Manager existingManager = Managers.FirstOrDefault(p => p.Id == managerId);

            if (existingManager != null)
            {
                existingManager.Name = updatedManager.Name;
                existingManager.Email = updatedManager.Email;
                existingManager.Address = updatedManager.Address;
                existingManager.Username = updatedManager.Username;
                existingManager.Password = updatedManager.Password;
                WriteDataToCsv(fileManager);
            }
            else
            {
                Console.WriteLine($"Manager with Id {managerId} not found.");
            }
        }

        public void DeleteManager(int managerId)
        {
            Manager managerToRemove = Managers.FirstOrDefault(p => p.Id == managerId);

            if (managerToRemove != null)
            {
                Managers.Remove(managerToRemove);
                WriteDataToCsv(fileManager);
            }
            else
            {
                Console.WriteLine($"Manager with Id {managerId} not found.");
            }
        }

        // Method to read data from a CSV file and populate Pizzas, updating nextPizzaId
        public List<Manager> ReadDataFromCsvAndUpdateId(string fileManager)
        {
            Managers = new List<Manager>();
            nextManagerId = 1; // Reset the counter

            if (File.Exists(fileManager))
            {
                using (StreamReader reader = new StreamReader(fileManager))
                {
                    // Skip the header line
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        if (values.Length >= 6)
                        {
                            Manager manager = new Manager
                            {
                                Id = int.Parse(values[0]),
                                Name = values[1],
                                Email = values[2],
                                Address = values[3],
                                Username = values[4],
                                Password = values[5]
                            };

                            Managers.Add(manager);

                            // Update nextPizzaId if needed
                            if (manager.Id >= nextManagerId)
                            {
                                nextManagerId = manager.Id + 1;
                            }

                        }
                    }
                }
            }

            return Managers;
        }

        private void WriteDataToCsv(string fileManager)
        {
            using (StreamWriter writer = new StreamWriter(fileManager))
            {
                // Write header
                writer.WriteLine("Id,Name,Email,Address,Username,Password");

                // Write data rows
                foreach (var manager in Managers)
                {
                    writer.WriteLine($"{manager.Id},{manager.Name},{manager.Email},{manager.Address},{manager.Username},{manager.Password}");
                }
            }
        }
    }
}
