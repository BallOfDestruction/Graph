using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    /// <summary>
    /// Класс ориентированного взвешенного графа
    /// </summary>
    public class WeightOrgGraph : BaseGraph<WeightVertex, WeightEdge>
    {
        #region Vertex
        /// <summary>
        /// Добавление новой вершины в граф по имени
        /// </summary>
        public void AddVertex(string name)
        {
            this.AddVertex(new WeightVertex(name));
        }
        /// <summary>
        /// Добавление новой вершин в граф по имени
        /// </summary>
        public void AddVertex(params string[] names)
        {
            foreach (string vertexName in names)
            {
                this.AddVertex(vertexName);
            }
        }
        #endregion

        #region Edge
        /// <summary>
        /// Добавление ребра из вершины в нее саму
        /// </summary>
        public void AddEdgeHimself(string name, double weight)
        {
            this.NameVertex[name].NextVertex.Add(new WeightEdge(NameVertex[name], NameVertex[name], weight));
        }
        /// <summary>
        /// Добавляет ребро между двумя существующими вершинами
        /// </summary>
        public void AddEdge(string previousVertex, string nextVertex, double weight)
        {
            if ((!NameVertex.ContainsKey(previousVertex) && (!NameVertex.ContainsKey(nextVertex))))
            {
                throw new Exception("Данных вершин в графе нет: " + previousVertex + " " + nextVertex);
            }
            else
            {
                if (!NameVertex.ContainsKey(previousVertex))
                {
                    throw new Exception("Вершины нет в графе: " + previousVertex);
                }
                else
                {
                    if (!NameVertex.ContainsKey(nextVertex))
                    {
                        throw new Exception("Вершины нет в графе: " + nextVertex);
                    }
                }
            }
            var previous = this.NameVertex[previousVertex];
            var next = this.NameVertex[nextVertex];
            this.AddEdge(new WeightEdge(previous, next, weight));
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public new void AddEdge(WeightEdge e)
        {
            base.AddEdge(e);
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public void AddEdge(params WeightEdge[] edge)
        {
            foreach (WeightEdge ed in edge)
            {
                AddEdge(ed);
            }
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public void AddEdge(IEnumerable<WeightEdge> edges)
        {
            this.AddEdge(edges.ToArray());
        }
        /// <summary>
        /// Удалеяет ребро между двумя вершинами
        /// </summary>
        public new void RemoveEdge(WeightEdge edge)
        {
            this.RemoveEdge(edge);
        }
        /// <summary>
        /// Удалеяет ребро между двумя вершинами
        /// </summary>
        public new void RemoveEdge(string previous, string next)
        {
            this.RemoveEdge(previous, next);
        }
        #endregion
    }
}
