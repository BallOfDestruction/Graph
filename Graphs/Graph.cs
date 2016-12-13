using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    /// <summary>
    /// Класс обычного графа(не взвешенный, не ориентированный)
    /// </summary>
    public class Graph : BaseGraph<Vertex, Edge>
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
        /// <summary>
        /// Добавление новой вершины в граф по имени, а также исходящие ребра из нее
        /// </summary>
        /// <param name="nextVertex">Следующие вершины</param>
        public void AddVertex(string name, params Vertex[] nextVertex)
        {
            if (this.NameVertex.ContainsKey(name))
            {
                throw new Exception("Данная вершина уже присутствует");
            }
            if (nextVertex.Where(w => nextVertex.Where(s => s == w).Count() > 1).Count() > 0)
            {
                throw new Exception("Вершины в списке повторяются, из одной вершины не может существовать два ребра в другую вершину");
            }
            this.AddVertex(name);
            Vertex vertex = this.NameVertex[name];
            foreach (Vertex ver in nextVertex)
            {
                if (ver.NextVertex.Select(w => w.NextVertex).Contains(vertex))
                {
                    throw new Exception("Ребро уже существует: " + ver.Name + " - " + vertex.Name);
                }
            }
            foreach (Vertex ver in nextVertex)
            {
                vertex.NextVertex.Add(new Edge(vertex, ver));
                ver.NextVertex.Add(new Edge(ver, vertex));
            }
        }
        #endregion

        #region Edge
        /// <summary>
        /// Добавление ребра из вершины в нее саму
        /// </summary>
        public void AddEdgeHimself(string name)
        {
            this.NameVertex[name].NextVertex.Add(new Edge(NameVertex[name], NameVertex[name]));
        }
        /// <summary>
        /// Добавляет ребро между двумя существующими вершинами
        /// </summary>
        public void AddEdge(string previousVertex, string nextVertex)
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
            this.AddEdge(new Edge(previous, next));
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public new void AddEdge(Edge e)
        {
            var ee = new Edge((Vertex)e.NextVertex, (Vertex)e.Previous);
            base.AddEdge(e);
            base.AddEdge(ee);
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
        /// Удалеяет ребро между двумя вершинами(обоюдно)
        /// </summary>
        public new void RemoveEdge(Edge edge)
        {
            this.RemoveEdge(edge);
            this.RemoveEdge(Edges.Where(w => (w.Previous == edge.NextVertex) && (w.NextVertex == edge.Previous)).ToArray()[0]);
        }
        /// <summary>
        /// Удалеяет ребро между двумя вершинами(обоюдно)
        /// </summary>
        public new void RemoveEdge(string previous, string next)
        {
            this.RemoveEdge(previous, next);
            this.RemoveEdge(next, previous);
        }
        #endregion
    }
}
