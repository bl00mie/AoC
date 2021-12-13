using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public class Grid<T> : IEnumerable<(Point p, T v)>
    {
        protected Dictionary<Point, T> _grid;
        
        public int Height { get; }
        public int Width { get; }

        public Grid(IEnumerable<IEnumerable<T>> input)
        {
            Height = input.Count();
            Width = input.First().Count();
            _grid = new Dictionary<Point, T>();
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    _grid[new(x, y)] = input.ElementAt(y).ElementAt(x);
        }

        public Grid(IEnumerable<(int, int, T)> input)
        {
            _grid = new Dictionary<Point, T>();
            foreach (var (x, y, val) in input)
            {
                _grid[new(x, y)] = val;
            }
        }


        public virtual T this[int x, int y]
        {
            get => this[new(x,y)];

            set
            {
                this[new(x,y)] = value;
            }
        }

        public virtual T this[Point p]
        {
            get
            {
                if (!_grid.ContainsKey(p))
                    return default(T);
                return _grid[p];
            }
            

            set
            {
                _grid[p] = value;
            }
        }

        public IEnumerator<(Point p, T v)> GetEnumerator()
        {
            foreach (var pair in _grid)
            {
                yield return (pair.Key, pair.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public delegate bool NeighborTest((Point p, T v) mine, (Point p, T v) theirs);

        public static bool Yes((Point p, T v) mine, (Point p, T v) theirs) => true;

        public IEnumerable<(Point p, T v)> Neighbors(Point point, IEnumerable<GridVector> directions, NeighborTest test = null)
        {
            if (test == null)
                test = Yes;

            var neighbors = new List<(Point p, T v)>();
            var v = _grid[point];
            foreach (var dir in directions)
            {
                var location = point + dir;
                if (_grid.ContainsKey(location) && test((point, v), (location, _grid[location])))
                    neighbors.Add((location, _grid[location]));
            }

            return neighbors;
        }

        public void Render(string delim = "")
        {
            for (int y = 0; y<Width; y++)
            {
                T[] row = new T[Width];
                for (int x = 0; x < Height; x++)
                    row[x] = _grid[new(x, y)];
                Console.WriteLine(String.Join(delim, row));
            }
            Console.WriteLine();
        }
    }

    public class HistoryGrid<T> : Grid<T>
    {
        public Stack<Dictionary<Point, T>> History { get; } = new();

        public HistoryGrid(IEnumerable<IEnumerable<T>> input) : base(input)
        {
            History = new Stack<Dictionary<Point,T>>();
        }

        public override T this[int x, int y]
        {
            get => this[new(x, y)];

            set
            {
                this[new(x, y)] = value;
            }
        }

        public override T this[Point p]
        {
            get => _grid[p];

            set
            {
                History.Push(_grid);
                _grid = new Dictionary<Point, T>(_grid) { [p] = value };
            }
        }

    }

    public struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point operator +(Point p, GridVector v) => new(p.x + v.dx, p.y + v.dy);

        public override string ToString()
        {
            return $"({x},{y})";
        }
    }

    public struct GridVector
    {
        public int dx;
        public int dy;

        public GridVector(int dx, int dy)
        {
            this.dx = dx;
            this.dy = dy;
        }

        public GridVector(Point p1, Point p2)
        {
            dx = p1.x - p2.x;
            dy = p1.y - p2.y;
        }

        public int GridMagnitude => dx + dy;
    }

    public static class Grid
    {
        public static readonly IEnumerable<GridVector> NESW = new GridVector[] { new(0, 1), new(1, 0), new(0, -1), new(-1, 0) };
        public static readonly IEnumerable<GridVector> Diag = new GridVector[] { new(1, 1), new(1, -1), new(-1, -1), new(1, -1) };
        public static readonly IEnumerable<GridVector> AllDirs = new GridVector[] { new(0, 1), new(1, 1), new(1, 0), new(1, -1), new(0, -1), new(-1, -1), new(-1, 0), new(-1, 1) };
    }
}
