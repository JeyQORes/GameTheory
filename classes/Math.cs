using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.classes
{
    public class Math
    {
        bool Detail;
        public Math(bool detail)
        {
            Detail = detail;
        }
        public int[] FindMatrix(int[,] matrix, out int MaxElement, out int a, out int b, out string answer)
        {
            MaxElement = matrix[0, 0];
            a = b = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if(matrix[i,j] > MaxElement)
                    {
                        MaxElement = matrix[i, j];
                        a = i;
                        b = j;
                    }
                }
            }
            int[] newMatrix = new int[matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                newMatrix[i] = matrix[a, i];
            }
            StringBuilder builder = new StringBuilder();
            if (Detail)
            {
                builder.Append("Критерий максимального элемента ориентирует статистику на самые благоприятные состояния природы, т.е. этот критерий выражает оптимистическую оценку ситуации.\n");
                builder.Append("Ai\t");
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    builder.AppendFormat($"П{i+1}\t");
                }
                builder.Append("max(aij)");
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    int max = matrix[i, 0];
                    builder.AppendFormat($"\nA{i + 1}\t");
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] > max)
                            max = matrix[i, j];
                        builder.AppendFormat($"{matrix[i, j]}\t");
                    }
                    builder.AppendFormat($"{max}");
                }
                builder.Append("\nВыбираем из (");

                for (int i = 0; i < newMatrix.Length; i++)
                {
                    builder.Append($"{newMatrix[i]};");
                }
                builder.Append($") максимальный элемент max = {MaxElement}\n");
                builder.Append($"Вывод: Выбираем стратегию N = {a + 1}");



            }
            else
            {
                builder.Append("Ответ: \n");
                for (int i = 0; i < newMatrix.Length; i++)
                {
                    builder.AppendFormat($"{newMatrix[i]} ");
                }
                builder.AppendLine($"\nМаксимальный элемент: {MaxElement};");
            }  
            
            answer = builder.ToString();
            return newMatrix;
        }
    }
}
