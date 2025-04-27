using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;


namespace ProjectYZ
{
    public class TransportClass
    {
        public string Name;
        public List<Dot> List;

        public TransportClass(string name, List<Dot> list)
        {
            Name = name;
            List = list;
        }
    }

    public class Navigation
    {
        private static bool StopNav = false;
        public static bool NavEnded = false;
        public static bool PauseNav = false;
        public static string EndPointName { get; set; }

        public static Queue<Dot> LastCoordsQueue = new Queue<Dot>();

        private static double CalculateVectorLen(Dot currentp, Dot endp) => Math.Sqrt(Math.Pow(endp.X - currentp.X, 2) + Math.Pow(endp.Y - currentp.Y, 2));

        public static void StopNavigation()
        {
            Console.WriteLine("Stop Navigation..");
            StopNav = true;
            int checks = 0;
            while (!NavEnded && checks++ < 100) Thread.Sleep(50);
            StopNav = false;
        }

        public static void PauseNavigation()
        {
            if (!PauseNav)
            {
                PauseNav = true;
                Combat.StopAutoMovement();
                Thread.Sleep(150);
            }
        }


        public static void PauseNavigationWithoutStop()
        {
            if (!PauseNav)
            {
                PauseNav = true;
                Thread.Sleep(150);
            }
        }

        public static void ResetFlags()
        {
            StopNav = false;
            NavEnded = false;
        }

        public static double CalculateRotateAngle(int angle, Dot curcoord, Dot newcoord)
        {
            Dot OrdinateVector = new Dot(0, -1);

            double x1 = newcoord.X - curcoord.X;
            double y1 = newcoord.Y - curcoord.Y;

            double MultiplicationScalarNew = OrdinateVector.X * x1 + OrdinateVector.Y * y1;

            double ordVecLen = Math.Sqrt(Math.Pow(OrdinateVector.X, 2) + Math.Pow(OrdinateVector.Y, 2));
            double ourVecLen = Math.Sqrt(Math.Pow(x1, 2) + Math.Pow(y1, 2));

            double cosangle = MultiplicationScalarNew / (ordVecLen * ourVecLen);

            double radiansNew = Math.Acos(cosangle);
            double angleNew = radiansNew * (180 / Math.PI);

            if (curcoord.X <= newcoord.X) angleNew = 360 - angleNew;

            if (Math.Abs(angle - angleNew) <= 180) return angle - angleNew;
            else if (angle - angleNew < -180) return 360 + angle - angleNew;
            else return angle - angleNew - 360;


        }

        public static bool RotatebyAngel(double angle, double len, double accuracy)
        {
            //Console.WriteLine("Angle: " + angle + "Len: " + len);

            if (len <= 1 && Math.Abs(angle) <= 4 * accuracy) return true;
            if (len > 1 && len <= 2 && Math.Abs(angle) <= 6 * accuracy) return true;
            if (len > 2 && len <= 5 && Math.Abs(angle) <= 10 * accuracy) return true;
            if (len > 5 && len <= 10 && Math.Abs(angle) <= 13 * accuracy) return true;
            if (len > 10 && len <= int.MaxValue && Math.Abs(angle) <= 20 * accuracy) return true;

            if (angle <= 0) WowProcess.PressButton(ConfigSettings.Cfg.Commonsettings.TurnLeft.Key, -1 * (int)Math.Round(angle / 360 * 2050));
            else WowProcess.PressButton(ConfigSettings.Cfg.Commonsettings.TurnRight.Key, (int)Math.Round(angle / 360 * 2050));

            return true;
        }

        public static void Pause()
        {
            while (PauseNav) Thread.Sleep(15);
        }

        public static bool RunToPoint(Dot endpoint)
        {
            EndPointName = endpoint.Name;
           // Console.WriteLine("Endpoint: " + endpoint.X + " " + endpoint.Y);
            //Console.WriteLine("Nav ended flag: " + NavEnded);
            GameInfo.Refresh();
            Dot currpoint = new Dot(GameInfo.X, GameInfo.Y);
            while (Math.Pow(currpoint.X - endpoint.X, 2) + Math.Pow(currpoint.Y - endpoint.Y, 2) > Math.Pow(endpoint.Radius, 2) && !StopNav)
            {
                //Console.WriteLine("Here");
                Pause();
                //Console.WriteLine("Here12");
                GameInfo.Refresh();
                currpoint = new Dot(GameInfo.X, GameInfo.Y);
                CheckStuck(currpoint);
                RotatebyAngel(CalculateRotateAngle(GameInfo.Angle, currpoint, endpoint), CalculateVectorLen(currpoint, endpoint), endpoint.Accuracy);
                Thread.Sleep(endpoint.ScreenshotTimeout);
            }
            ///todo here i set coord refresch
            GlobalBotHandler.RefreshLastChatacterCoordinates();/////////////////////////////////////////////////
            return true;
        }

        public static Dot GetNearestPoint(List<Dot> pList, Dot point)
        {
            double min = double.MaxValue;
            Dot nDot = null;
            foreach (Dot p in pList)
            {
                if (CalculateVectorLen(point, p) < min)
                {
                    min = CalculateVectorLen(point, p);
                    nDot = p;
                }
            }
            return nDot;
        }

        private static List<Dot> ListNameListDots(List<string> names)
        {
            List<Dot> l = new List<Dot>();

            foreach (string name in names)
            {
                foreach (Dot d in MeshHandler.MergedSpotVendorGhost())
                {
                    if (name == d.Name)
                    {
                        l.Add(d);
                        break;
                    }
                }
            }
            return l;
        }

        public static void CheckStuck(Dot point)
        {
            Debug.AddDebugRecord("Begin unstuck..", false);
            LastCoordsQueue.Enqueue(point);
            if (LastCoordsQueue.Count > 20)
                LastCoordsQueue.Dequeue();
            else return;

            Dot[] meshPointArray = LastCoordsQueue.ToArray();

            if (SameDot(meshPointArray[18], meshPointArray[19]))
            {
                Combat.StartMovement();
                WowProcess.Delay(75);
                for (int i = 0; i < 2; i++)
                {
                    Combat.Jump();
                    WowProcess.Delay(75);
                }
            }

            bool same = true;

            for (int i = 18; i > 15; i--)
            {
                if (!SameDot(meshPointArray[19], meshPointArray[i]))
                {
                    same = false;
                    break;
                }
            }

            if (same)
            {
                WowProcess.PressButton(ConfigSettings.Cfg.Commonsettings.TurnLeft.Key, new Random().Next(1500, 1600));
            }

            bool stacked = true;

            for (int i = 1; i < 20; i++)
            {
                if (!SameInRange(meshPointArray[0], meshPointArray[i]))
                {
                    stacked = false;
                    break;
                }
            }

            if (stacked)
            {
                StopNavigation();
                GlobalBotHandler.BotStop = true;
                TelegramBot.SendMessage("Bot Stacked", true);
                TelegramBot.IsBotStacked = true;
                //WowProcess.PressButton(ConfigSettings.Cfg.Commonsettings.StopAutoMovement.Key);
                //if (WowProcess.CloseWowAndBattleNet() == 1)
                //{
                //}
                //WowProcess.Delay(30000);
                //foreach (Process process in Process.GetProcessesByName("WowClassic.exe"))
                //{
                //    process.Kill();
                //}
                //Environment.Exit(0);
            }
            Debug.AddDebugRecord("Out from unstuck", false);
        }

        private static bool SameDot(Dot p1, Dot p2) => p1.X == p2.X && p1.Y == p2.Y;
        private static bool SameInRange(Dot p1, Dot p2) => Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) < Math.Pow(0.15, 2);

        public static void RunRoute(object info)
        {
            Console.WriteLine("Navigation start..");
            NavEnded = false;
            TransportClass transport = (TransportClass)info;
            GameInfo.Refresh();
            Dot nearestDot = GetNearestPoint(transport.List, new Dot(GameInfo.X, GameInfo.Y));
            foreach (Dot p in transport.List) Console.WriteLine(p.Name);
            List<Dot> route = ListNameListDots(Dijkstra.BuildRoute(transport.List, nearestDot.Name, transport.Name));
            foreach (Dot p in route) Console.WriteLine(p.Name);
            RotatebyAngel(CalculateRotateAngle(GameInfo.Angle, new Dot(GameInfo.X, GameInfo.Y), new Dot(route[0].X, route[0].Y)), CalculateVectorLen(new Dot(GameInfo.X, GameInfo.Y), new Dot(route[0].X, route[0].Y)), route[0].Accuracy);
            WowProcess.PressButton(ConfigSettings.Cfg.Commonsettings.StartMovement.Key);

            foreach (Dot dot in route) RunToPoint(dot);

            WowProcess.PressButton(ConfigSettings.Cfg.Commonsettings.StopAutoMovement.Key);
            Console.WriteLine("Navigation ended..");
            NavEnded = true;
        }
    }


    public class Dijkstra
    {

        readonly Graph graph;

        List<GraphVertexInfo> infos;

        public Dijkstra(Graph graph)
        {
            this.graph = graph;
        }

        void InitInfo()
        {
            infos = new List<GraphVertexInfo>();
            foreach (var v in graph.Vertices)
            {
                infos.Add(new GraphVertexInfo(v));
            }
        }

        private GraphVertexInfo GetVertexInfo(GraphVertex v)
        {
            foreach (GraphVertexInfo i in infos)
            {
                if (i.Vertex.Equals(v))
                {
                    return i;
                }
            }

            return null;
        }

        private static List<string> visitedVertices = new List<string>();

        public static bool ValidateGraph(Graph g)
        {
            visitedVertices.Clear();
            CheckDots(g.Vertices[0]);
            if (g.Vertices.Count == visitedVertices.Count) return true;
            return false;
        }

        private static void CheckDots(GraphVertex vert)
        {
            visitedVertices.Add(vert.Name);
            foreach (GraphEdge edg in vert.Edges)
            {
                if (!visitedVertices.Contains(edg.ConnectedVertex.Name))
                    CheckDots(edg.ConnectedVertex);
            }

            return;
        }

        public static List<string> BuildRoute(List<Dot> dots, string name1, string name2)
        {
            Graph g = new Graph();

            g.Vertices.Clear();

            //добавление вершин
            foreach (Dot dot in dots)
            {
                g.AddVertex(dot.Name);
            }

            //добавление ребер
            foreach (Edge edge in MeshHandler.MeshDots.EdgeList.Edge)
            {
                Dot p1 = MeshHandler.GetPointByName(edge.Dot1);
                Dot p2 = MeshHandler.GetPointByName(edge.Dot2);
                g.AddEdge(edge.Dot1, edge.Dot2, Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));
            }

            if (!ValidateGraph(g))
            {
                MessageBox.Show("Graph not validated!!!");
                return null;
            }

            Dijkstra dijkstra = new Dijkstra(g);
            List<string> path = dijkstra.FindShortestPath(name1, name2);

            path.Reverse();

            return path;
        }

        public GraphVertexInfo FindUnvisitedVertexWithMinSum()
        {
            var minValue = double.PositiveInfinity;
            GraphVertexInfo minVertexInfo = null;
            foreach (var i in infos)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }

        public List<string> FindShortestPath(string startName, string finishName)
        {
            return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName));
        }

        public List<string> FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
        {
            InitInfo();
            var first = GetVertexInfo(startVertex);
            first.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }

                SetSumToNextVertex(current);
            }

            return GetPath(startVertex, finishVertex);
        }

        void SetSumToNextVertex(GraphVertexInfo info)
        {
            info.IsUnvisited = false;
            foreach (var e in info.Vertex.Edges)
            {
                var nextInfo = GetVertexInfo(e.ConnectedVertex);
                var sum = info.EdgesWeightSum + e.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
        }

        List<string> GetPath(GraphVertex startVertex, GraphVertex endVertex)
        {
            List<string> path = new List<string>
            {
                endVertex.ToString()
            };
            while (startVertex != endVertex)
            {
                endVertex = GetVertexInfo(endVertex).PreviousVertex;
                path.Add(endVertex.ToString());
            }

            return path;
        }
    }

    public class GraphVertexInfo
    {
        public GraphVertex Vertex { get; set; }

        public bool IsUnvisited { get; set; }

        public double EdgesWeightSum { get; set; }

        public GraphVertex PreviousVertex { get; set; }

        public GraphVertexInfo(GraphVertex vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = double.MaxValue;
            PreviousVertex = null;
        }
    }
    public class Graph
    {
        public List<GraphVertex> Vertices { get; }

        public Graph()
        {
            Vertices = new List<GraphVertex>();
        }

        public void AddVertex(string vertexName)
        {
            Vertices.Add(new GraphVertex(vertexName));
        }

        public GraphVertex FindVertex(string vertexName)
        {
            foreach (var v in Vertices)
            {
                if (v.Name.Equals(vertexName))
                {
                    return v;
                }
            }

            return null;
        }

        public void AddEdge(string firstName, string secondName, double weight)
        {
            var v1 = FindVertex(firstName);
            var v2 = FindVertex(secondName);
            if (v2 != null && v1 != null)
            {
                v1.AddEdge(v2, weight);
                v2.AddEdge(v1, weight);
            }
        }
    }

    public class GraphVertex
    {
        public string Name { get; }

        public List<GraphEdge> Edges { get; }

        public GraphVertex(string vertexName)
        {
            Name = vertexName;
            Edges = new List<GraphEdge>();
        }

        public void AddEdge(GraphEdge newEdge)
        {
            Edges.Add(newEdge);
        }

        public void AddEdge(GraphVertex vertex, double edgeWeight)
        {
            AddEdge(new GraphEdge(vertex, edgeWeight));
        }

        public override string ToString() => Name;
    }

    public class GraphEdge
    {
        public GraphVertex ConnectedVertex { get; }

        public double EdgeWeight { get; }

        public GraphEdge(GraphVertex connectedVertex, double weight)
        {
            ConnectedVertex = connectedVertex;
            EdgeWeight = weight;
        }
    }
}