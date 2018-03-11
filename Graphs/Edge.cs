namespace Graphs
{
    public class Edge : BaseEdge<Edge>
    {
        public Edge(Vertex previousVertex , Vertex nextVertex) : base(previousVertex, nextVertex) {}
    }
}
