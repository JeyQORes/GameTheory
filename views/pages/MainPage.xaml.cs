using Microsoft.Win32;
using Practice.classes;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Practice.views.pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private DataSet dataset;
        private int[,] Matrix;

        public MainPage()
        {
            InitializeComponent();
            heightTextBox.Text = widthTextBox.Text = "0";
        }
        public MainPage(int[,] matrix)
        {
            InitializeComponent();
            Matrix = matrix;
            heightTextBox.Text = matrix.GetLength(0).ToString();
            widthTextBox.Text = matrix.GetLength(1).ToString();
        }
        private void GenerateTable(int n, int m)
        {
            DataTable dt = new DataTable();
            DataColumn column;
            DataRow row;
            DataView view;

            for (int i = 0; i < n; i++)
            {
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                dt.Columns.Add(column);
            }
            dataset = new DataSet();
            dataset.Tables.Add(dt);
            for (int i = 0; i < m; i++)
            {
                row = dt.NewRow();
                if (Matrix != null)
                {
                    for (int j = 0; j < n; j++)
                    {
                        row[j] = Matrix[i, j];
                    }
                }
                dt.Rows.Add(row);
            }
            view = new DataView(dt);
            StartMatrixDataGrid.ItemsSource = view;
            if(heightTextBox.Text != "" && widthTextBox.Text != "")
            {
                if(heightTextBox.Text != "0" && widthTextBox.Text != "0")
                    Matrix = null;
            }
        }
        private void heightTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;

            if (Int32.TryParse(tb.Text, out int Value))
            {
                int h, w;
                if (heightTextBox.Text == "")
                    h = 0;
                else
                    h = Convert.ToInt32(heightTextBox.Text);
                if (widthTextBox.Text == "")
                    w = 0;
                else
                    w = Convert.ToInt32(widthTextBox.Text);
                GenerateTable(w, h);
            }
            else
                MessageBox.Show("Введите корректное число");
        }
        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            //получаем изначальную матрицу
            var view = (DataView)StartMatrixDataGrid.ItemsSource;
            var dt = view.Table;
            int h = Convert.ToInt32(heightTextBox.Text);
            int w = Convert.ToInt32(widthTextBox.Text);
            int[,] m = new int[h, w];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    m[i, j] = Convert.ToInt32(dt.Rows[i].ItemArray.GetValue(j));
                }
            }
            if ((h <=0) || (w <= 0))
            {
                MessageBox.Show("Введена некорректная матрица");
            }
            else
            {
                bool detail = IterationCheckBox.IsChecked.HasValue && IterationCheckBox.IsChecked.Value;
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        m[i, j] = Convert.ToInt32(dt.Rows[i].ItemArray.GetValue(j));
                    }
                }

                classes.Math math = new classes.Math(detail);
                var res = math.FindMatrix(m, out int max, out int a, out int b, out string answer);


                //вывод ответа
                FlowDocument doc = new FlowDocument();
                Paragraph par = new Paragraph(new Run("Игры с природой"));
                par.FontSize = 24;
                par.TextAlignment = TextAlignment.Center;
                doc.Blocks.Add(par);
                par = new Paragraph(new Run("Критерий максимального элемента"));
                par.FontSize = 22;
                par.TextAlignment = TextAlignment.Center;
                doc.Blocks.Add(par);
                par = new Paragraph(new Run("Исходные данные:"));
                par.FontSize = 14;
                par.TextAlignment = TextAlignment.Left;
                doc.Blocks.Add(par);
                Table matTable = new Table();
                matTable.FontSize = 14;
                matTable.TextAlignment = TextAlignment.Left;
                for (int i = 0; i < w; i++)
                {
                    TableColumn tc = new TableColumn();
                    tc.Width = new GridLength(25);
                    matTable.Columns.Add(tc);
                }
                TableRowGroup group = new TableRowGroup();
                for (int i = 0; i < h; i++)
                {
                    TableRow tr = new TableRow();
                    for (int j = 0; j < w; j++)
                    {
                        TableCell cell = new TableCell(new Paragraph(new Run(m[i, j].ToString())));
                        //cell.
                        tr.Cells.Add(cell);
                    }
                    group.Rows.Add(tr);
                }
                matTable.RowGroups.Add(group);
                doc.Blocks.Add(matTable);
                par = new Paragraph(new Run(answer));
                par.FontSize = 14;
                par.TextAlignment = TextAlignment.Left;
                doc.Blocks.Add(par);


                resultRichTextBox.Document = doc;
            }

            


        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog().GetValueOrDefault())
            {
                string path = sfd.FileName;
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
                        break;
                }
                //получаем матрицу
                var view = (DataView)StartMatrixDataGrid.ItemsSource;
                var dt = view.Table;
                int h = Convert.ToInt32(heightTextBox.Text);
                int w = Convert.ToInt32(widthTextBox.Text);
                //int[,] m = new int[h, w];
                string[] recordMatrix = new string[h];
                for (int i = 0; i < h; i++)
                {
                    recordMatrix[i] = dt.Rows[i].ItemArray.GetValue(0).ToString();
                    for (int j = 1; j < w; j++)
                    {
                        recordMatrix[i] = recordMatrix[i] + ' ' + dt.Rows[i].ItemArray.GetValue(j).ToString();
                        //m[i, j] = Convert.ToInt32(dt.Rows[i].ItemArray.GetValue(j));
                    }
                }
                if ((h <= 0) || (w <= 0))
                {
                    MessageBox.Show("Введена некорректная матрица");
                }
                else
                {
                    file.Export(recordMatrix, path);
                }
            }
        }
    }
}
