using System;
using System.Windows;
using System.Windows.Input;

namespace TaskManagerUI 
    public partial class MainWindow : Window
    {
        private TaskManager _manager; 

        public MainWindow()
        {
            InitializeComponent(); 
            _manager = new TaskManager(); 
            DpDueDate.SelectedDate = DateTime.Now.AddDays(1);
            RefreshGrid(); 
        }

        private void RefreshGrid()
        {
            if (DgTasks.ItemsSource == null)
            {
                DgTasks.ItemsSource = _manager.GetTasks();
            }
            else
            {
                DgTasks.Items.Refresh(); 
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string title = TxtTitle.Text; 
            Priority priority = (Priority)(CmbPriority.SelectedIndex + 1); 
            DateTime dueDate = DpDueDate.SelectedDate ?? DateTime.Now.AddDays(1); 
            
            if (!string.IsNullOrWhiteSpace(title) && title != "Görev Başlığı...")
            {
                _manager.AddTask(title, priority, dueDate);
                RefreshGrid();
                TxtTitle.Text = "";
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir görev başlığı girin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnClearSelection_Click(object sender, RoutedEventArgs e)
        {
            if (DgTasks.SelectedItem is TaskItem selectedTask)
            {
                _manager.UncompleteTask(selectedTask.Id);
                RefreshGrid();

                DgTasks.SelectedItem = null;
                DgTasks.UnselectAll();
            }
            else
            {
                MessageBox.Show(
                    "Lütfen bir görev seçin.",
                    "Uyarı",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DgTasks.SelectedItem is TaskItem selectedTask)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"'{selectedTask.Title}' başlıklı görevi silmek istediğinize emin misiniz?",
                    "Görev Sil",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    _manager.DeleteTask(selectedTask.Id);
                    RefreshGrid();
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz görevi listeden seçin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnComplete_Click(object sender, RoutedEventArgs e)
        {
            if (DgTasks.SelectedItem is TaskItem selectedTask)
            {
                _manager.CompleteTask(selectedTask.Id);
                RefreshGrid();
            }
            else
            {
                MessageBox.Show("Lütfen listeden bir görev seçin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
