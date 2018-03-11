using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    /// <inheritdoc />
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
            AddVertex(new Vertex(name));
        }
        /// <summary>
        /// Добавление новой вершин в граф по имени
        /// </summary>
        public void AddVertex(params string[] names)
        {
            foreach (var vertexName in names)
            {
                AddVertex(vertexName);
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
            var previous = NameVertex[previousVertex];
            var next = NameVertex[nextVertex];
            AddEdge(new Edge(previous, next));
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public new void AddEdge(Edge e)
        {
            var ee = new Edge((Vertex)e.NextVertex, (Vertex)e.Previous);
            base.AddEdge(e);
            //Если вдруг добавляем ребро в эту же вершину
            if(e.NextVertex != e.Previous)
                base.AddEdge(ee);
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public void AddEdge(params Edge[] edge)
        {
            foreach (var ed in edge)
            {
                AddEdge(ed);
            }
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public void AddEdge(IEnumerable<Edge> edges)
        {
            AddEdge(edges.ToArray());
        }
        /// <summary>
        /// Удалеяет ребро между двумя вершинами(обоюдно)
        /// </summary>
        public new void RemoveEdge(Edge edge)
        {
            RemoveEdge(edge);
            RemoveEdge(Edges.Where(w => (w.Previous == edge.NextVertex) && (w.NextVertex == edge.Previous)).ToArray()[0]);
        }
        /// <summary>
        /// Удалеяет ребро между двумя вершинами(обоюдно)
        /// </summary>
        public new void RemoveEdge(string previous, string next)
        {
            base.RemoveEdge(previous, next);
            base.RemoveEdge(next, previous);
        }

        #endregion
    }
}
