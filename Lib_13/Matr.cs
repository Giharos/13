using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Lib_13
{
    public class Table
    {
        //Параметры:
        //Входные:
        //DataGridView table - таблица
        //NumericUpDown zn,TextBox rez1,TextBox rez2
        //Выходные:
        //sum - значение функции и proizv - проиведение

        public static void Summ(DataGridView table, NumericUpDown zn,TextBox rez1,TextBox rez2)
        {
            int k, sum, proizv;
            sum = 0;
            proizv = 1;
            k = Convert.ToInt32(zn.Value) - 1;
            for (int n = 0; n < table.ColumnCount; n++)
                for (int m = 0; m < table.RowCount; m++)
                {
                    if (n == k)
                    {
                        sum = sum + Convert.ToInt32(table[k, m].Value);
                        proizv *= Convert.ToInt32(table[k, m].Value);
                    }
                }
            rez1.Text = Convert.ToString(sum);
            rez2.Text = Convert.ToString(proizv);
        }
        //Очищение массива         
        public static void ClearMas(DataGridView table,TextBox rez1,TextBox rez2)
        {
            //Очищаем ячейки таблицы
            for (int i = 0; i < table.ColumnCount; i++)
            {
                for (int j = 0; j < table.RowCount; j++)
                {
                    table[i, j].Value = " ";
                }
            }
            rez1.Clear();
            rez2.Clear();
        }

        //Заполнение массива случайными числами       
        public static void ZpMas(DataGridView table)
        {
            int RandMax = 20;
            Random Rand = new Random();
            //Заполняем случайными числами
            for (int n = 0; n < table.ColumnCount; n++)
                for (int m = 0; m < table.RowCount; m++)
                    table[n, m].Value = Rand.Next(RandMax);
        }

        //Сохранение таблицы в файл
        public static void SaveFile(DataGridView table)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";

            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(save.FileName);

                file.WriteLine(table.ColumnCount);
                file.WriteLine(table.RowCount);

                for (int i = 0; i < table.ColumnCount; i++)
                    for (int j = 0; j < table.RowCount; j++)
                        file.WriteLine(table[i, j].Value);
                file.Close();
            }
        }

        //Чтение таблицы из файла
        public static void OpenFile(DataGridView table)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";

            int columns = 0,
                rows = 0;
            if (open.ShowDialog() == DialogResult.OK)
            {
                StreamReader file = new StreamReader(open.FileName);

                if (Int32.TryParse(file.ReadLine(), out columns))
                {
                    if (Int32.TryParse(file.ReadLine(), out rows))
                    {
                        table.ColumnCount = columns;
                        table.RowCount = rows;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка");
                    return;
                }

                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        table[i, j].Value = file.ReadLine();
                    }
                }
            }
        }
    }
}


