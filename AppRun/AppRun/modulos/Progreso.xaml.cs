using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microcharts;
using System.Threading.Tasks;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRun
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Progreso : ContentPage
    {
     
        private readonly List<Microcharts.ChartEntry> _entries = new List<Microcharts.ChartEntry>()
        {
            new Microcharts.ChartEntry(200)
            {
                Label = "January",
                ValueLabel = "200",
                Color = SKColor.Parse("#FF0033"),
            },
            new Microcharts.ChartEntry(400)
            {
                Label = "February",
                ValueLabel = "400",
                Color = SKColor.Parse("#FF8000"),
            },
            new Microcharts.ChartEntry(300)
            {
                Label = "March",
                ValueLabel = "300",
                Color = SKColor.Parse("#FFE600"),
            },
            new Microcharts.ChartEntry(250)
            {
                Label = "April",
                ValueLabel = "250",
                Color = SKColor.Parse("#1AB34D"),
            },
            new Microcharts.ChartEntry(650)
            {
                Label = "May",
                ValueLabel = "650",
                Color = SKColor.Parse("#1A66FF"),
            },
            new Microcharts.ChartEntry(500)
            {
                Label = "June",
                ValueLabel = "500",
                Color = SKColor.Parse("#801AB3"),
            },
        };
        public Progreso()
        {
            InitializeComponent();

            MyLineChart.Chart = new LineChart { Entries = _entries,AnimationProgress=4, LineMode= LineMode.Straight,LabelTextSize=14,IsAnimated=true};
            MyDonutChart.Chart = new DonutChart { Entries = _entries, IsAnimated = true };
            MyBarChart.Chart = new BarChart { Entries = _entries, IsAnimated = true, BarAreaAlpha=29};
            MyPointChart.Chart = new PointChart { Entries = _entries, IsAnimated = true };
            MyRadialGaugeChart.Chart = new RadialGaugeChart { Entries = _entries, IsAnimated = true};

        }


    }
}