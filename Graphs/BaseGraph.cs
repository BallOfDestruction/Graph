using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Graphs
{
    /// <summary>
    /// Базовый класс для графа
    /// </summary>
    /// <typeparam name="T">Тип вершины</typeparam>
    /// <typeparam name="E">Тип ребра</typeparam>
    public abstract class BaseGraph<T, E>
        where T : BaseVertex<E>
        where E : BaseEdge<E>
    {
        /// <summary>
        /// Список ребер графа
        /// </summary>
        public List<E> Edges => Vertex.SelectMany(w => w.NextVertex).ToList();
        /// <summary>
        /// Словарь вершин графа по их именам
        /// </summary>
        public Dictionary<string, T> NameVertex => Vertex.ToDictionary(wde => wde.Name);
        public Dictionary<string, List<T>> InputVertex => Vertex.ToDictionary(w => w.Name, e => Edges.Where(ww => ww.NextVertex == e).Select(www => (T)www.Previous).ToList());
        public Dictionary<string, int> DegreeInput => Vertex.ToDictionary(w => w.Name, e => Edges.Select(p => p.NextVertex).Where(pp => pp == e).Count());
        public Dictionary<string, int> DegreeOutput => Vertex.ToDictionary(w => w.Name, e => e.NextVertex.Count);
        /// <summary>
        /// Вершины графа
        /// </summary>
        public List<T> Vertex { get; } = new List<T>();

        #region Vertex
        /// <summary>
        /// Добавление новой вершины 
        /// </summary>
        public void AddVertex(T vertex)
        {
            IsNotExistVertex(vertex.Name);
            this.Vertex.Add(vertex);
        }
        /// <summary>
        /// Добавление любого перечисляемого множества вершин в граф
        /// </summary>
        public void AddVertex(IEnumerable<T> vertex)
        {
            foreach (T vert in vertex)
                AddVertex(vert);
        }
        public void AddVertex(params T[] vertex)
        {
            AddVertex(vertex.AsEnumerable());
        }
        /// <summary>
        /// При удалении вершины также удаляются все ребра
        /// </summary>
        public void RemoveVertex(string name)
        {
            IsExistVertex(name);
            var vertex = NameVertex[name];
            this.Vertex.Remove(vertex);
            var neibour = this.Vertex.Where(w => (w.NextVertex.Select(ww => ww.NextVertex).Contains(vertex)));
            foreach (var neib in neibour)
            {
                var edge = neib.NextVertex.Where(w => w.NextVertex == vertex);
                if (edge.Count() == 1)
                    neib.NextVertex.Remove(edge.ToArray()[0]);
                else
                    throw new Exception("Error2");
            }
        }
        /// <summary>
        /// При удалении вершины также удаляются все ребра
        /// </summary>
        public void RemoveVertex(T vertex)
        {
            this.RemoveVertex(vertex.Name);
        }
        #endregion

        #region Edge
        /// <summary>
        /// Добавление вершины в одну сторону
        /// </summary>
        protected void AddEdge(E edge)
        {
            var c = (this.Edges.Where(w => (edge.Previous == w.Previous) && (w.NextVertex == edge.NextVertex))).ToArray();
            if (this.Edges.Contains(edge) || c.Count() > 0)
                throw new Exception("Ребро уже присутствует в графе " + edge.Previous.Name + " -> " + edge.NextVertex.Name);
            if (Vertex.Contains(edge.Previous) && Vertex.Contains(edge.NextVertex))
                NameVertex[edge.Previous.Name].NextVertex.Add(edge);
            else
            {
                IsExistVertex(edge.NextVertex.Name);
                IsExistVertex(edge.Previous.Name);
            }
        }
        /// <summary>
        /// удаление существующего ребра
        /// </summary>
        protected void RemoveEdge(string previous, string nextVertex)
        {
            var edges = this.Edges.Where(w => w.NextVertex.Name.Equals(nextVertex) && w.Previous.Name.Equals(previous)).ToArray();
            if (edges.Count() == 0)
                throw new Exception("Такого ребра не существует");
            else
                if (edges.Count() > 1)
                    throw new Exception("Error1");
                else
                    this.RemoveEdge(edges[0]);
        }
        /// <summary>
        /// Удаление существующего ребра
        /// </summary>
        protected void RemoveEdge(E edge)
        {
            if (!Edges.Contains(edge))
                throw new Exception("Такого ребра не существует");
            else
                edge.Previous.NextVertex.Remove(edge);
        }
        #endregion

        #region Checked
        public virtual bool IsIzomorph(BaseGraph<T, E> graph)
        {
            BaseGraph<T, E> gr = graph;
            if (gr.IsFullCount() && this.IsFullCount())
                return true;
            if (gr.Vertex.Count == this.Vertex.Count)
            {
                List<int> thisInput = this.DegreeInput.Values.ToList();
                List<int> thisOutput = this.DegreeOutput.Values.ToList();
                List<int> grInput = gr.DegreeInput.Values.ToList();
                List<int> grOutput = gr.DegreeOutput.Values.ToList();
                foreach (int i in thisInput)
                {
                    if (grInput.Contains(i))
                        grInput.Remove(i);
                }
                if (grInput.Count != 0)
                    return false;
                foreach (int i in thisOutput)
                {
                    if (grOutput.Contains(i))
                        grOutput.Remove(i);
                }
                if (grOutput.Count != 0)
                    return false;
            }
            return false;
        }
        /// <summary>
        /// Проверка на существование вершины(если не существует - плохо)
        ///Внутренняя проверка
        ///Использовать в том случае, если нужно быть точно увереным
        ///что данная вершина существует
        /// </summary>
        protected void IsExistVertex(string name)
        {
            if (!NameVertex.ContainsKey(name))
                throw new Exception("Вершины " + name + "в графе нет");
        }
        /// <summary>
        /// Проверка на существование вершины(если не существует - хорошо)
        ///Внутренняя проверка
        ///Использовать в том случае, если нужно быть точно увереным
        ///что данная отсутствует существует
        /// </summary>
        protected void IsNotExistVertex(string name)
        {
            if (NameVertex.ContainsKey(name))
                throw new Exception("Вершина " + name + "есть в графе");
        }
        /// <summary>
        /// Проверка на свзяность графа
        /// </summary>
        public bool IsConherenceOfGraph()
        {
            List<T> goodVertex = new List<T>();
            if (Vertex.Count == 0)
                return true;
            int position = 0;
            goodVertex.Add(Vertex[0]);
            while (position < goodVertex.Count)
            {
                foreach (T v in goodVertex[position].NextVertex.Select(w => w.NextVertex))
                {
                    if (!goodVertex.Contains(v))
                    {
                        goodVertex.Add(v);
                    }
                }
                var vv = Vertex.Where( w  => w.NextVertex.Select(ww => ww.NextVertex).Where( www => www == goodVertex[position]).Count() != 0);
                foreach (T v in vv)
                {
                    if (!goodVertex.Contains(v))
                    {
                        goodVertex.Add(v);
                    }
                }
                position++;
            }
            if (goodVertex.Count < Vertex.Count)
                return false;
            else
                return true;
        }
        /// <summary>
        /// Проверка на полноту
        /// </summary>
        public bool IsFullCount()
        {
            if (Edges.Count == ((Vertex.Count * (Vertex.Count - 1)) / 2))
                return true;
            else
                return false;
        }
        #endregion

        #region Searched
        /// <summary>
        /// Поиск в ширину
        /// </summary>
        /// <returns>Есть ли путь от начальной к кконечной вершине</returns>
        public bool DFS(string startVertex, string endVertex)
        {
            if (this.GetPathDFS(startVertex, endVertex) != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Поиск в глубину
        /// </summary>
        /// <returns>Путь от начальной к конечной</returns>
        public List<T> GetPathDFS(string startVertexName, string endVertexName)
        {
            NullebleNow();
            IsExistVertex(startVertexName);
            IsExistVertex(endVertexName);
            T startVertex = this.NameVertex[startVertexName];
            T endVertex = this.NameVertex[endVertexName];
            Stack<T> stack = new Stack<T>();
            stack.Push(startVertex);
            startVertex.Depth = 1;
            while (stack.Count != 0)
            {
                T bufferVertex = stack.Pop();
                if (bufferVertex.IsSeen == false)
                {
                    bufferVertex.IsSeen = true;
                    if (!bufferVertex.NextVertex.Select(w => w.NextVertex).Contains(endVertex))
                    {
                        foreach (T buf in bufferVertex.NextVertex.Select(w => w.NextVertex))
                        {
                            if ((!buf.IsSeen) && !stack.Contains(buf))
                            {
                                stack.Push(buf);
                                if ((buf.Depth == 0) || (buf.Depth > bufferVertex.Depth + 1))
                                    buf.Depth = bufferVertex.Depth + 1;
                            }
                        }
                    }
                    else
                    {
                        int depth = bufferVertex.Depth;
                        Stack<T> answer = new Stack<T>();
                        T buffer = endVertex;
                        answer.Push(endVertex);
                        while (buffer != startVertex)
                        {
                            var r = Vertex.Where(w => (w.NextVertex.Select(ww => ww.NextVertex).Contains(buffer)) && w.Depth == depth).ToArray();
                            answer.Push(r[0]);
                            depth--;
                            buffer.IsSeen = false;
                            buffer = r[0];
                        }
                        NullebleNow();
                        return answer.ToList();
                    }
                }
            }
            NullebleNow();
            return null;
        }
        /// <summary>
        /// Поиск в ширину
        /// </summary>
        /// <returns>Есть ли путь от начальной к кконечной вершине</returns>
        public bool BFS(string startVertex, string endVertex)
        {
            if (this.GetPathBFS(startVertex, endVertex) != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Обход в глубину
        /// </summary>
        public List<T> GetBFS(string startVertexName)
        {
            List<T> answer = new List<T>();
            NullebleNow();
            IsExistVertex(startVertexName);
            T startVertex = this.NameVertex[startVertexName];
            Queue<T> queue = new Queue<T>();
            queue.Enqueue(startVertex);
            startVertex.Depth = 1;
            while (queue.Count != 0)
            {
                T bufferVertex = queue.Dequeue();
                answer.Add(bufferVertex);
                if (bufferVertex.IsSeen == false)
                {
                    bufferVertex.IsSeen = true;
                    foreach (T buf in bufferVertex.NextVertex.Select(w => w.NextVertex))
                    {
                        if ((!buf.IsSeen) && !queue.Contains(buf))
                        {
                            queue.Enqueue(buf);
                            if ((buf.Depth == 0) || (buf.Depth > bufferVertex.Depth + 1))
                                buf.Depth = bufferVertex.Depth + 1;
                        }
                    }
                }
            }
            return answer;
        }
        /// <summary>
        /// Обход в ширину
        /// </summary>
        public List<T> GetDFS(string startVertexName)
        {
            List<T> answer = new List<T>();
            NullebleNow();
            IsExistVertex(startVertexName);
            T startVertex = this.NameVertex[startVertexName];
            Stack<T> queue = new Stack<T>();
            queue.Push(startVertex);
            startVertex.Depth = 1;
            while (queue.Count != 0)
            {
                T bufferVertex = queue.Pop();
                answer.Add(bufferVertex);
                if (bufferVertex.IsSeen == false)
                {
                    bufferVertex.IsSeen = true;
                    foreach (T buf in bufferVertex.NextVertex.Select(w => w.NextVertex))
                    {
                        if ((!buf.IsSeen) && !queue.Contains(buf))
                        {
                            queue.Push(buf);
                            if ((buf.Depth == 0) || (buf.Depth > bufferVertex.Depth + 1))
                                buf.Depth = bufferVertex.Depth + 1;
                        }
                    }
                }
            }
            return answer;
        }
        /// <summary>
        /// Поиск в ширину
        /// </summary>
        /// <returns>Путь от начальной к конечной</returns>
        public List<T> GetPathBFS(string startVertexName, string endVertexName)
        {
            NullebleNow();
            IsExistVertex(startVertexName);
            IsExistVertex(endVertexName);
            T startVertex = this.NameVertex[startVertexName];
            T endVertex = this.NameVertex[endVertexName];
            Queue<T> queue = new Queue<T>();
            queue.Enqueue(startVertex);
            startVertex.Depth = 1;
            while (queue.Count != 0)
            {
                T bufferVertex = queue.Dequeue();
                if (bufferVertex.IsSeen == false)
                {
                    bufferVertex.IsSeen = true;
                    if (!bufferVertex.NextVertex.Select(w => w.NextVertex).Contains(endVertex))
                    {
                        foreach (T buf in bufferVertex.NextVertex.Select(w => w.NextVertex))
                        {
                            if ((!buf.IsSeen) && !queue.Contains(buf))
                            {
                                queue.Enqueue(buf);
                                if((buf.Depth == 0) || (buf.Depth > bufferVertex.Depth + 1))
                                    buf.Depth = bufferVertex.Depth + 1;
                            }
                        }
                    }
                    else
                    {
                        int depth = bufferVertex.Depth;
                        Stack<T> answer = new Stack<T>();
                        T buffer = endVertex;
                        answer.Push(endVertex);
                        while (buffer != startVertex)
                        {
                           
                            var r = Vertex.Where(w => (w.NextVertex.Select(ww => ww.NextVertex).Contains(buffer)) && w.Depth == depth).ToArray();
                            answer.Push(r[0]);
                            depth--;
                            buffer.IsSeen = false;
                            buffer = r[0];
                        }
                        NullebleNow();
                        return answer.ToList();
                    }
                }
            }
            NullebleNow();
            return null;
        }
        /// <summary>
        /// Обнуляет все вершины
        /// </summary>
        protected void NullebleNow()
        {
            foreach (T edge in Vertex)
            {
                edge.IsSeen = false;
                edge.Depth = 0;
            }
        }
        #endregion
        /// <summary>
        /// Представляет граф в виде списка смежности
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (T vertex in Vertex)
            {
                str.Append(vertex.Name + ": ");
                if (vertex.NextVertex.Count != 0)
                {
                    foreach (E edge in vertex.NextVertex)
                    {
                        str.Append(edge.ToString() + ", ");
                    }
                    str.Remove(str.Length - 2, 1);
                }
                str.Append("\r\n");
            }
            return str.ToString();
        }
    }
}
