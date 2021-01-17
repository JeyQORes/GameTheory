using System;
using System.Collections.Generic;
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
    /// Interaction logic for InfoPage.xaml
    /// </summary>
    public partial class InfoPage : Page
    {
        public enum TypeInfo
        {
            Import,
            Help
        }
        //инфо по справке
        public InfoPage(TypeInfo type)
        {
            InitializeComponent();
            if(type == TypeInfo.Import)
            {
                string[] Titles = { "Файл TXT", "Файл CSV", "Файл XML", "Файл JSON" };
                for (int i = 0; i < 4; i++)
                {
                    Button btn = new Button
                    {
                        Content = Titles[i],
                        Tag = i,
                        Width = 150,
                        Height = 50,
                        Margin = new Thickness(3)
                    };
                    btn.AddHandler(Button.ClickEvent, new RoutedEventHandler((object Sender, RoutedEventArgs e) =>
                    {
                        var b = (Button)Sender;
                        InfoRichTextBox.Document = GenerateDocuments(TypeInfo.Import, Convert.ToInt32(b.Tag));
                    }));
                    buttons.Children.Add(btn);

                }
            }
            else
            {
                string[] Titles = { "информация по методу", "получение ответа", "Подробное решение"};
                for (int i = 0; i < 3; i++)
                {
                    Button btn = new Button
                    {
                        Content = Titles[i],
                        Tag = i,
                        Width = 150,
                        Height = 50,
                        Margin = new Thickness(3)
                    };
                    btn.AddHandler(Button.ClickEvent, new RoutedEventHandler((object Sender, RoutedEventArgs e) =>
                    {
                        var b = (Button)Sender;
                        InfoRichTextBox.Document = GenerateDocuments(TypeInfo.Help, Convert.ToInt32(b.Tag));
                    }));
                    buttons.Children.Add(btn);
                }
            }
        }
        private FlowDocument GenerateDocuments(TypeInfo type, int index)
        {
            FlowDocument doc = new FlowDocument();
            Paragraph p;
            switch (type)
            {
                case TypeInfo.Import:
                    p = new Paragraph(new Run("инструкция импорта и экспорта"));
                    p.FontSize = 24;
                    p.TextAlignment = TextAlignment.Center;
                    doc.Blocks.Add(p);
                    switch (index)
                    {
                        case 0:
                            {
                                p = new Paragraph(new Run("Файл .txt"));
                                p.FontSize = 22;
                                p.TextAlignment = TextAlignment.Center;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run("Импорт"));
                                p.FontSize = 20;
                                p.TextAlignment = TextAlignment.Center;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run("формат импорта:\nМатрица чисел, знаки разделены пробелом\n" +
                                    "Пример импорта:"));
                                p.FontSize = 14;
                                p.TextAlignment = TextAlignment.Left;
                                doc.Blocks.Add(p);

                                Table t = new Table();
                                t.FontSize = 12;
                                t.TextAlignment = TextAlignment.Left;
                                for (int i = 0; i < 4; i++)
                                {
                                    TableColumn tc = new TableColumn();
                                    tc.Width = new GridLength(10);
                                    t.Columns.Add(tc);
                                }
                                TableRowGroup group = new TableRowGroup();
                                int number = 1;
                                for (int i = 0; i < 3; i++)
                                {
                                    TableRow tr = new TableRow();

                                    for (int j = 0; j < 3; j++)
                                    {
                                        TableCell cell = new TableCell(new Paragraph(new Run($"{number}")));
                                        number++;
                                        tr.Cells.Add(cell);
                                    }
                                    group.Rows.Add(tr);
                                }
                                t.RowGroups.Add(group);
                                doc.Blocks.Add(t);

                                p = new Paragraph(new Run("Экспорт"));
                                p.FontSize = 20;
                                p.TextAlignment = TextAlignment.Center;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run(@"Для экспорта нажмите кнопку ""Экспорт"" и сохраните файл в формате .txt"));
                                p.FontSize = 14;
                                p.TextAlignment = TextAlignment.Left;
                                doc.Blocks.Add(p);
                            }
                            break;
                        case 1:
                            {
                                p = new Paragraph(new Run("Файл .csv"));
                                p.FontSize = 22;
                                p.TextAlignment = TextAlignment.Center;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run("Импорт"));
                                p.FontSize = 20;
                                p.TextAlignment = TextAlignment.Center;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run("формат импорта:\nМатрица чисел, знаки разделены точкой с запятой(;)\n" +
                                    "Пример импорта:"));
                                p.FontSize = 14;
                                p.TextAlignment = TextAlignment.Left;
                                doc.Blocks.Add(p);

                                Table t = new Table();
                                t.FontSize = 12;
                                t.TextAlignment = TextAlignment.Left;
                                for (int i = 0; i < 4; i++)
                                {
                                    TableColumn tc = new TableColumn();
                                    tc.Width = new GridLength(10);
                                    t.Columns.Add(tc);
                                }
                                TableRowGroup group = new TableRowGroup();
                                int number = 1;
                                for (int i = 0; i < 3; i++)
                                {
                                    TableRow tr = new TableRow();

                                    for (int j = 0; j < 2; j++)
                                    {
                                        TableCell cell = new TableCell(new Paragraph(new Run($"{number};")));
                                        number++;
                                        tr.Cells.Add(cell);
                                    }
                                    TableCell tempCell = new TableCell(new Paragraph(new Run($"{number}")));
                                    number++;
                                    tr.Cells.Add(tempCell);
                                    group.Rows.Add(tr);
                                }
                                t.RowGroups.Add(group);
                                doc.Blocks.Add(t);

                                p = new Paragraph(new Run("Экспорт"));
                                p.FontSize = 20;
                                p.TextAlignment = TextAlignment.Center;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run(@"Для экспорта нажмите кнопку ""Экспорт"" и сохраните файл в формате .csv"));
                                p.FontSize = 14;
                                p.TextAlignment = TextAlignment.Left;
                                doc.Blocks.Add(p);
                            }
                            break;
                        case 2:
                            {
                                p = new Paragraph(new Run("Файл .xml"));
                                p.FontSize = 22;
                                p.TextAlignment = TextAlignment.Center;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run("Импорт"));
                                p.FontSize = 20;
                                p.TextAlignment = TextAlignment.Center;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run("формат импорта:\nМатрица чисел, записанная в массиве строк\n" +
                                    "Пример импорта:"));
                                p.FontSize = 14;
                                p.TextAlignment = TextAlignment.Left;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run(
                                    "<?xml version=\"1.0\" encoding=\"utf - 8\"?> \n" +
                                    "<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema- \n" +
                                    "instance\" xmlns: xsd = \"http://www.w3.org/2001/XMLSchema\" > \n" +
                                    "\t<string>1 2 3</string>\n" +
                                    "\t<string>4 5 6</string>\n" +
                                    "\t<string>7 8 9</string>\n" +
                                    "</ArrayOfString>"));
                                p.FontSize = 14;
                                p.TextAlignment = TextAlignment.Left;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run("Экспорт"));
                                p.FontSize = 20;
                                p.TextAlignment = TextAlignment.Center;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run(@"Для экспорта нажмите кнопку ""Экспорт"" и сохраните файл в формате .xml"));
                                p.FontSize = 14;
                                p.TextAlignment = TextAlignment.Left;
                                doc.Blocks.Add(p);
                            }
                            break;
                        case 3:
                            {
                                p = new Paragraph(new Run("Файл .json"));
                                p.FontSize = 22;
                                p.TextAlignment = TextAlignment.Center;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run("Импорт"));
                                p.FontSize = 20;
                                p.TextAlignment = TextAlignment.Center;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run("формат импорта:\nМатрица чисел, записанная в массиве строк\n" +
                                    "Пример импорта:"));
                                p.FontSize = 14;
                                p.TextAlignment = TextAlignment.Left;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run(@"[""1 2 3"",""4 5 6"",""7 8 9""]"));
                                p.FontSize = 14;
                                p.TextAlignment = TextAlignment.Left;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run("Экспорт"));
                                p.FontSize = 20;
                                p.TextAlignment = TextAlignment.Center;
                                doc.Blocks.Add(p);

                                p = new Paragraph(new Run(@"Для экспорта нажмите кнопку ""Экспорт"" и сохраните файл в формате .json"));
                                p.FontSize = 14;
                                p.TextAlignment = TextAlignment.Left;
                                doc.Blocks.Add(p);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case TypeInfo.Help:
                    p = new Paragraph(new Run("Справка программы"));
                    p.FontSize = 24;
                    p.TextAlignment = TextAlignment.Center;
                    doc.Blocks.Add(p);
                    switch (index)
                    {
                        case 0:
                            p = new Paragraph(new Run("Критерий максимального оптимизма"));
                            p.FontSize = 24;
                            p.TextAlignment = TextAlignment.Center;
                            doc.Blocks.Add(p);

                            p = new Paragraph(new Run("Наиболее простой критерий, основывающийся на идее, " +
                                "что ЛПР, имея возможность в некоторой степени управлять ситуцацией, " +
                                "рассчитывает, что произойдет такое развитие ситуации, которое для него " +
                                "является наиболее выгодным. В соответствии с критерием принимается " +
                                "альтернатива, соответствующая максимальному элементу матрицы выигрышей. " +
                                "Для приведенного примера это величина a33 = 9, " +
                                "поэтому выбираем альтернативу A3\nПример:"));
                            p.FontSize = 14;
                            p.TextAlignment = TextAlignment.Left;
                            doc.Blocks.Add(p);

                            Table t = new Table();
                            for (int i = 0; i < 4; i++)
                            {
                                TableColumn tc = new TableColumn();
                                tc.Width = new GridLength(25);
                                t.Columns.Add(tc);
                            }
                            TableRowGroup group = new TableRowGroup();
                            TableRow tr = new TableRow();
                            tr.Cells.Add(new TableCell(new Paragraph(new Run("Ai"))));
                            for (int i = 1; i < 4; i++)
                            {
                                tr.Cells.Add(new TableCell(new Paragraph(new Run($"П{i}"))));
                            }
                            tr.Cells.Add(new TableCell(new Paragraph(new Run("max(aij)"))));
                            group.Rows.Add(tr);
                            int number = 1;
                            for (int i = 1; i < 4; i++)
                            {
                                tr = new TableRow();
                                tr.Cells.Add(new TableCell(new Paragraph(new Run($"A{i}"))));
                                for (int j = 0; j < 3; j++)
                                {
                                    tr.Cells.Add(new TableCell(new Paragraph(new Run($"{number}"))));
                                    number++;
                                }
                                tr.Cells.Add(new TableCell(new Paragraph(new Run($"{number - 1}"))));
                                group.Rows.Add(tr);
                            }
                            t.RowGroups.Add(group);
                            doc.Blocks.Add(t);

                            
                            break;
                        case 1:
                            p = new Paragraph(new Run("Получение ответа"));
                            p.FontSize = 24;
                            p.TextAlignment = TextAlignment.Center;
                            doc.Blocks.Add(p);

                            p = new Paragraph(new Run("Для получения ответа игр с природой критерием " +
                                "максимализма необходимо запустить программу, ввести размерность и " +
                                "заполнить матрицу или импортировать файл, затем нажать на кнопку рассчитать. " +
                                "В правом поле отобразится ответ."));
                            p.FontSize = 14;
                            p.TextAlignment = TextAlignment.Left;
                            doc.Blocks.Add(p);
                            break;
                        case 2:
                            p = new Paragraph(new Run("Получение подробного ответа"));
                            p.FontSize = 24;
                            p.TextAlignment = TextAlignment.Center;
                            doc.Blocks.Add(p);

                            p = new Paragraph(new Run("Для получения подробного ответа игр с природой критерием " +
                                "максимализма необходимо запустить программу, ввести размерность и " +
                                "заполнить матрицу или импортировать файл, поставить галочку в нижнем левом углу \"Подробный ответ\" затем нажать на кнопку рассчитать. " +
                                "В правом поле отобразится ответ."));
                            p.FontSize = 14;
                            p.TextAlignment = TextAlignment.Left;
                            doc.Blocks.Add(p);
                            break;
                        case 3:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return doc;
        }
    }
}
