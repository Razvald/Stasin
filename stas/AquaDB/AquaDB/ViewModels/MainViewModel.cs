using AquaDB.Database;
using AquaDB.Database.Entities;
using AquaDB.Models;
using AquaDB.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AquaDB.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly AppDbContext _context;
        public ObservableCollection<Measurement> MeasurementDatas { get; set; }
        public ObservableCollection<string> ProjectNames { get; set; }
        public ObservableCollection<string> MeasurementTypes { get; set; }

        public MainViewModel()
        {
            _context = DbContextProvider.Instance.GetDbContext();

            MeasurementDatas = new ObservableCollection<Measurement>(
                _context.Measurements.ToList());

            // Загрузка имен проектов из базы данных
            ProjectNames = new ObservableCollection<string>(
                _context.Projects.Select(p => p.Title).ToList());

            // Загрузка типов измерений из базы данных
            MeasurementTypes = new ObservableCollection<string>(
                _context.MeasurementTypes.Select(mt => mt.Name).ToList());

            AddRecordCommand = new RelayCommand(AddRecord);
            SaveToDatabaseCommand = new RelayCommand(SaveToDatabase);
            ClearFieldsCommand = new RelayCommand(ClearFields);
            GraphicOpenCommand = new RelayCommand(GraphicOpen);
        }

        public ICommand AddRecordCommand { get; private set; }
        public ICommand SaveToDatabaseCommand { get; private set; }
        public ICommand ClearFieldsCommand { get; private set; }
        public ICommand GraphicOpenCommand { get; private set; }

        private void AddRecord(object parameter)
        {
            // Находим соответствующие id в базе данных
            int measurementTypeId = _context.MeasurementTypes
                                              .FirstOrDefault(mt => mt.Name == SelectedMeasurementType)?.Id ?? 0;
            int projectId = _context.Projects
                                     .FirstOrDefault(p => p.Title == SelectedProject)?.Id ?? 0;

            // Создаем новое измерение с полученными значениями
            Measurement newMeasurement = new()
            {
                Date = Date,
                Location = Location,
                Depth = Depth,
                MeasurementTypeId = measurementTypeId,
                ProjectId = projectId
            };

            try
            {
                // Добавляем новое измерение в контекст базы данных
                _context.Measurements.Add(newMeasurement);

                // Сохраняем изменения в базе данных
                _context.SaveChanges();
                MessageBox.Show("Сохранение выполнено");

                // Добавляем новое измерение в коллекцию данных для DataGrid
                MeasurementDatas.Add(newMeasurement);

                // Обновляем отображение данных в DataGrid
                if (parameter != null && parameter is DataGrid dataGrid)
                {
                    dataGrid.Items.Refresh();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void SaveToDatabase(object parameter)
        {
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Измерения сохранены успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields(object parameter)
        {
            Date = DateTime.Now;
            Time = string.Empty;
            Location = string.Empty;
            Depth = 0;
        }

        private void GraphicOpen(object parameter)
        {
            GraphWindow graphWindow = new(_context);
            graphWindow.ShowDialog();
            //((Window)parameter).Close();
        }

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private string _time;
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private int _depth;
        public int Depth
        {
            get { return _depth; }
            set
            {
                _depth = value;
                OnPropertyChanged(nameof(Depth));
            }
        }

        private string _selectedMeasurementType;
        public string SelectedMeasurementType
        {
            get { return _selectedMeasurementType; }
            set
            {
                _selectedMeasurementType = value;
                OnPropertyChanged(nameof(SelectedMeasurementType));
            }
        }

        private string _selectedProject;
        public string SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                OnPropertyChanged(nameof(SelectedProject));
            }
        }
    }
}
