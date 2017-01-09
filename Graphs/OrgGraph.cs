using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    /// <summary>
    /// Класс ориентированного не взвешенного графа
    /// </summary>
    public class OrgGraph : BaseGraph<Vertex, Edge>
    {
        #region Vertex
        /// <summary>
        /// Добавление новой вершины в граф по имени
        /// </summary>
        public void AddVertex(string name)
        {
            this.AddVertex(new Vertex(name));
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
        /// Добавляет ребро между двумя существующими вершинами
        /// </summary>
        public void AddEdge(string previousVertex, string nextVertex)
        {
            IsExistVertex(previousVertex);
            IsExistVertex(nextVertex);
            var previous = this.NameVertex[previousVertex];
            var next = this.NameVertex[nextVertex];
            this.AddEdge(new Edge(previous, next));
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public new void AddEdge(Edge e)
        {
            base.AddEdge(e);
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public void AddEdge(params Edge[] edge)
        {
            foreach (Edge ed in edge)
            {
                AddEdge(ed);
            }
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public void AddEdge(IEnumerable<Edge> edges)
        {
            this.AddEdge(edges.ToArray());
        }
        /// <summary>
        /// Удалеяет ребро между двумя вершинами
        /// </summary>
        public new void RemoveEdge(Edge edge)
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
