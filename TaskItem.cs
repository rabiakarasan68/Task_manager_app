using System;

namespace TaskManagerUI
{
    public enum Priority
    {
        Dusuk = 1,
        Orta = 2,
        Yuksek = 3
    }

    public class TaskItem
    {
        public int Id { get; set;}
        public string Title { get; set;} = string.Empty;
        public Priority PriorityLevel { get; set;}
        public DateTime DueDate { get; set;}
        public bool IsCompleted { get; set;} = false;

        public override string ToString()
        {
            string status = IsCompleted ? "[X] Tamamlandı" : "[ ] Devam Ediyor";
            return $"ID: {Id} | {status} | Başlık: {Title} | Öncelik: {PriorityLevel} | Son tarih: {DueDate:dd.MM.yyyy}";
        }
    }
}
