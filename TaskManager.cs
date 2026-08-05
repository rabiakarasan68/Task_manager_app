using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TaskManagerUI
{
    public class TaskManager
    {
        private List<TaskItem> _tasks = new();
        private readonly string _filePath = "tasks.json";

        public TaskManager()
        {
            LoadTasks();
        }

        public void AddTask(string title, Priority priority, DateTime dueDate)
        {
            int nextId = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;

            var newTask = new TaskItem 
            {
                Id = nextId,
                Title = title,
                PriorityLevel = priority,
                DueDate = dueDate,
                IsCompleted = false
            };
            _tasks.Add(newTask);
            SaveTasks();
            Console.WriteLine("--> Görev başarıyla eklendi.");
        }

        public void DeleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
                SaveTasks(); 
            }
        }

        public void ListTasks()
        {
            if(!_tasks.Any()) // Hiçbir task yoksa
            {
                Console.WriteLine("--> Henüz kayıtlı bir görev yok.");
                return;
            }

            Console.WriteLine("\n--- GÖREV LİSTESİ ---\n");
            var orderedTasks = _tasks.OrderByDescending(t => t.PriorityLevel).ThenBy(t => t.DueDate); 
            foreach(var task in orderedTasks) 
            {
                Console.WriteLine(task);
            }
        }

        public void CompleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id); 
            if(task != null)
            {
                task.IsCompleted = true;
                SaveTasks(); 
            }
            else
            {
                Console.WriteLine($"--> Belirtilen ID'de görev bulunamadı.");
            }
        }

        private void SaveTasks() 
        {
            string jsonString = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true }); 
            File.WriteAllText(_filePath, jsonString); 
        }

        private void LoadTasks()
        {
            if (File.Exists(_filePath)) //_filePath ile belirtilen dosya var mı?
            {
                string jsonString = File.ReadAllText(_filePath); 
                _tasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString) ?? new List<TaskItem>(); 
            } 
        }

        public void UncompleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                task.IsCompleted = false;
                SaveTasks();
            }
        }

        public List<TaskItem> GetTasks()
        {
            return _tasks;
        }
    }
}
