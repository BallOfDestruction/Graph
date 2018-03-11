namespace Graphs
{
    /// <summary>
    /// Базовый тип для ребра
    /// </summary>
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
            NextVertex = nextVertex;
            Previous = previousVertex;
        }

        public override string ToString()
        {
            return Previous.Name + " -> " + NextVertex.Name;
        }
    }
}
