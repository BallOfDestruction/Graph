using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    /// <summary>
    /// Базовый тип для ребра
    /// </summary>
    /// <typeparam name="T">Этот же клсс, для которого будет конкретизация</typeparam>
    public class BaseEdge<T>
    {
        /// <summary>
        /// Исходящая вершина
        /// </summary>
        public BaseVertex<T> Previous { get; protected set; }
        /// <summary>
        /// Конечная вершина
        /// </summary>
        public BaseVertex<T> NextVertex { get; protected set; }

        public BaseEdge(BaseVertex<T> previousVertex, BaseVertex<T> nextVertex)
        {
            this.NextVertex = nextVertex;
            this.Previous = previousVertex;
        }

        public override string ToString()
        {
            return Previous.Name + " -> " + NextVertex.Name;
        }
    }
}
