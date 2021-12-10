using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AoC
{
    public class Grid<T> : IEnumerable<(Point p, T v)>
    {
        private ImmutableDictionary<Point, T> grid;
        
        public Stack<ImmutableDictionary<Point, T>> History { get; } = new();
        public int Height { get; }
        public int Width { get; }

        public Grid(IEnumerable<IEnumerable<T>> input)
        {
            Height = input.Count();
            Width = input.First().Count();
            var dict = new Dictionary<Point, T>();
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    dict[new(x, y)] = input.ElementAt(y).ElementAt(x);
            grid = dict.ToImmutableDictionary();
        }

        public T this[int x, int y]
        {
            get => this[new(x,y)];

            set
            {
                this[new(x,y)] = value;
            }
        }

        public T this[Point p]
        {
            get => grid[p];

            set
            {
                History.Push(grid);
                grid = new Dictionary<Point, T>(grid) { [p] = value }.ToImmutableDictionary();
            }
        }

        public IEnumerator<(Point p, T v)> GetEnumerator()
        {
            foreach (var pair in grid)
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
            var v = grid[point];
            foreach (var dir in directions)
            {
                var location = point + dir;
                if (grid.ContainsKey(location) && test((point, v), (location, grid[location])))
                    neighbors.Add((location, grid[location]));
            }

            return neighbors;
        }

        public void Render(string delim = "")
        {
            for (int y = 0; y<Width; y++)
            {
                T[] row = new T[Width];
                for (int x = 0; x < Height; x++)
                    row[x] = grid[new(x, y)];
                Console.WriteLine(String.Join(delim, row));
            }
            Console.WriteLine();
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
