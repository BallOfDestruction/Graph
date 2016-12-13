using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class InfoGraph
    {
        /// <summary>
        /// Матрица смежности
        /// Матрица смежности графа G с конечным числом вершин n (пронумерованных числами от 1 до n) —
        /// это квадратная матрица A размера n, в которой значение элемента aij 
        /// равно числу рёбер из i-й вершины графа в j-ю вершину
        /// </summary>
        public static int[][] GetAdjacencyMatrix()
        {
            return default(int[][]);
        }
        /// <summary>
        /// Матрица инцидентности
        /// Матрицей инцидентности (инциденций) неориентированного графа называется матрица
        /// I (|V| \times |E|), для которой I_{i,j} = 1, если вершина v_i инцидентна ребру e_j,
        /// в противном случае I_{i,j} = 0.
        /// 
        /// Матрицей инцидентности (инциденций) ориентированного графа называется матрица
        /// I (|V| \times |E|), для которой I_{i,j} = 1, если вершина v_i является началом дуги e_j, I_{i,j} = -1, 
        /// если v_i является концом дуги e_j, в остальных случаях I_{i,j} = 0.
        /// </summary>
        public static int[][] GetIncidenceMatrix()
        {
            return default(int[][]);
        }
        /// <summary>
        /// Список смежности
        /// Каждой вершине графа соответствует список, состоящий из "соседей" этой вершины.
        /// </summary>
        public static List<int> GetAdjacencyList()
        {
            return default(List<int>);
        }
        /// <summary>
        /// Матрица весов
        /// вариант матрицы смежности для взвешенного графа
        /// представляет собой квадратную матрицу размером  ( n - число вершин)
        /// -й элемент которой равен весу ребра/дуги , если таковое имеется в графе;
        /// в противном случае  -й элемент полагается равным нулю
        /// </summary>
        public static int[][] GetWeightMatrix()
        {
            return default(int[][]);
        }
    }
}
