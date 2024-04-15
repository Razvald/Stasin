using AquaDB.Database;
using AquaDB.Database.Entities;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Windows;

namespace AquaDB.View
{
    public partial class GraphWindow : Window
    {
        private readonly AppDbContext _context;
        public GraphWindow(AppDbContext appDbContext)
        {
            InitializeComponent();
            _context = appDbContext;
            PlotGraph();
        }

        private void PlotGraph()
        {
            // Создаем модель для графика
            var plotModel = new PlotModel { Title = "График аквальных измерений" };

            // Создаем ось X для времени
            var dateTimeAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Время"
            };
            plotModel.Axes.Add(dateTimeAxis);

            // Создаем ось Y для значений измерений
            var linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Значение"
            };
            plotModel.Axes.Add(linearAxis);

            // Получаем данные измерений (здесь предполагается, что у вас есть список MeasurementData)
            List<Measurement> measurements = _context.Measurements.ToList();

            // Создаем серию для графика
            var series = new LineSeries
            {
                Title = "Измерения",
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Black,
                MarkerFill = OxyColors.ForestGreen
            };

            foreach (var measurement in measurements)
            {
                // Преобразуем время и значение измерения в тип double
                double xValue = measurement.Date.ToOADate();
                double yValue = Convert.ToDouble(measurement.Depth);

                // Добавляем точки данных измерений в серию
                series.Points.Add(new DataPoint(xValue, yValue));
            }

            // Добавляем серию к модели графика
            plotModel.Series.Add(series);

            // Устанавливаем модель для элемента управления PlotView
            plotView.Model = plotModel;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }
    }
}
