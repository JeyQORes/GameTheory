using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Practice.classes;
using Practice.views.pages;

namespace Practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainPage());
        }

        private void MenuItemCreate_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MainPage());
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog().GetValueOrDefault())
                {
                    string path = ofd.FileName;
                    var findFormat = path.Split('.');
                    string act = findFormat[findFormat.Length - 1];
                    IFileHelper file = null;
                    switch (act.ToLower())
                    {
                        case "csv":
                            file = new FileCSV();
                            break;
                        case "txt":
                            file = new FileTXT();
                            break;
                        case "xml":
                            file = new FileXML();
                            break;
                        case "json":
                            file = new FileJSON();
                            break;
                        case "xlsx":
                            file = new FileXLSX();
                            break;
                        default:
                            MessageBox.Show("Выбран некоректный формат файла");
                            return;
                            break;
                    }
                    var result = file.Import(path);
                    var tempStorage = result[0].Split(' ');
                    int w = tempStorage.Length;
                    int h = result.Length;
                    int[,] array = new int[h, w];
                    for (int i = 0; i < h; i++)
                    {
                        string str = (string)result[i];
                        var arr = str.Split(' ');
                        for (int j = 0; j < w; j++)
                        {
                            array[i, j] = Convert.ToInt32(arr[j]);
                        }
                    }
                    MainFrame.Navigate(new MainPage(array));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Выбран некоректный файл");
            }
        }

        private void ImportHelpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new InfoPage(InfoPage.TypeInfo.Import));
        }

        private void HelpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new InfoPage(InfoPage.TypeInfo.Help));
        }

        private void ReadmeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new InfoPage(InfoPage.TypeInfo.Readme));
        }
    }
}
