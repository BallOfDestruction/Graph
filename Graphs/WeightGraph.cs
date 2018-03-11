using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    /// <inheritdoc />
    /// <summary>
    /// Класс неориентированного взвешенного графа
    /// </summary>
    public class WeightGraph : BaseGraph<WeightVertex, WeightEdge>
    {
        #region Vertex
        /// <summary>
        /// Добавление новой вершины в граф по имени
        /// </summary>
        public void AddVertex(string name)
        {
            AddVertex(new WeightVertex(name));
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
        public void AddEdge(string previousVertex, string nextVertex, double weight)
        {
            IsExistVertex(nextVertex);
            IsExistVertex(previousVertex);
            var previous = NameVertex[previousVertex];
            var next = NameVertex[nextVertex];
            AddEdge(new WeightEdge(previous, next, weight));
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public new void AddEdge(WeightEdge e)
        {
            var ee = new WeightEdge((WeightVertex)e.NextVertex, (WeightVertex)e.Previous, e.Weight);
            base.AddEdge(e);
            //На случай петли
            if(e.NextVertex != e.Previous)
                base.AddEdge(ee);
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public void AddEdge(params WeightVertex[] edge)
        {
            foreach (var ed in edge)
            {
                AddEdge(ed);
            }
        }
        /// <summary>
        /// Добавляет ребро графа между двумя существующими вершинами
        /// </summary>
        public void AddEdge(IEnumerable<WeightVertex> edges)
        {
            this.AddEdge(edges.ToArray());
        }
        /// <summary>
        /// Удалеяет ребро между двумя вершинами(обоюдно)
        /// </summary>
        public new void RemoveEdge(WeightEdge edge)
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
