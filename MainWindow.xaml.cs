using LB2.src;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LB2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TaskRepository _repository;

        private LB2.src.Task _selectedTask;

        public MainWindow()
        {
            InitializeComponent();
            _repository = new TaskRepository();
            LoadTasks();
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
        }

        private void LoadTasks()
        {
            tasksDataGrid.ItemsSource = _repository.GetAllTasks();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newTask = new LB2.src.Task
            {
                Title = titleTextBox.Text,
                Description = descriptionTextBox.Text,
                Priority = (Priority)priorityComboBox.SelectedItem,
                StartDate = startDatePicker.SelectedDate ?? DateTime.Now,
                EndDate = endDatePicker.SelectedDate ?? DateTime.Now
            };

            _repository.AddTask(newTask);
            LoadTasks();
            ClearFields();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTask == null) return;

            _selectedTask.Title = titleTextBox.Text;
            _selectedTask.Description = descriptionTextBox.Text;
            _selectedTask.Priority = (Priority)priorityComboBox.SelectedItem;
            _selectedTask.StartDate = startDatePicker.SelectedDate ?? DateTime.Now;
            _selectedTask.EndDate = endDatePicker.SelectedDate ?? DateTime.Now;

            _repository.UpdateTask(_selectedTask);
            LoadTasks();
            ClearFields();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTask == null) return;

            _repository.DeleteTask(_selectedTask.Id);
            LoadTasks();
            ClearFields();
        }

        private void TasksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tasksDataGrid.SelectedItem is LB2.src.Task task)
            {
                _selectedTask = task;
                titleTextBox.Text = task.Title;
                descriptionTextBox.Text = task.Description;
                priorityComboBox.SelectedItem = task.Priority;
                startDatePicker.SelectedDate = task.StartDate;
                endDatePicker.SelectedDate = task.EndDate;
            }
        }

        private void ClearFields()
        {
            titleTextBox.Text = string.Empty;
            descriptionTextBox.Text = string.Empty;
            priorityComboBox.SelectedIndex = 0;
            startDatePicker.SelectedDate = DateTime.Now;
            endDatePicker.SelectedDate = DateTime.Now;
            _selectedTask = null;
        }
    }
}