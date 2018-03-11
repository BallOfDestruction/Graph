using System.Collections.Generic;

namespace Graphs
{
    /// <summary>
    /// Базовый класс-дженерик для вершины
    /// </summary>
    /// <typeparam name="T">Тип ребра</typeparam>
    public class BaseVertex<T>
    {
        public string Name { get;protected set; }
        /// <summary>
        /// Метка, используется в поисках
        /// </summary>
        internal bool IsSeen { get; set; }

        public int Depth { get; set; } = 0;
        /// <summary>
        /// Степень вершины
        /// </summary>
        public List<T> NextVertex { get; protected set; }

        public BaseVertex(string name)
        {
            Name = name;
            NextVertex = new List<T>();
        }

        public BaseVertex(string name, params T[] vertex): this(name)
        {
            NextVertex.AddRange(vertex);
        }
    }
}
