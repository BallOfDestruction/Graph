#define PI 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public abstract class BaseGraph<T, E>
        where T : BaseVertex<E>
        where E : BaseEdge<E>
    {
        public Dictionary<string, T> NameVertex => Vertex.ToDictionary(wde=> wde.Name);

        public List<T> Vertex { get; } = new List<T>();

        #region Vertex
        protected abstract void AddVertex(T vertex);

        public void AddVertex(IEnumerable<T> vertex)
        {
            foreach (T vert in vertex)
            {
                AddVertex(vert);
            }
        }

        public void AddVertex(params T[] vertex)
        {
            AddVertex(vertex.AsEnumerable());
        }
        #endregion

        #region Edge

        protected void AddEdge(string nameOutputVertex, E edge)
        {
            if (NameVertex.ContainsKey(nameOutputVertex))
            {
                NameVertex[nameOutputVertex].NextVertex.Add(edge);
            }
            else
            {
                //Фабричный класс для вершины
                Vertex.Add(new T(nameOutputVertex, edge));
            }
        }

        protected void AddEdge(T outputVertex, E edge)
        {
        }

        #endregion
    }
}
