using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AoC
{
    public class Grid<T> : IEnumerable<(Coord p, T v)>
    {
        protected Dictionary<Coord, T> _grid = new();
        public int H;
        public int W;
        public T DefaultValue = default;
        public bool StoreOnMissingLookup = false;

        public Grid()
        {
        }

        public Grid(IEnumerable<IEnumerable<T>> input)
        {
            H = input.Count();
            W = input.First().Count();
            for (int y = 0; y < H; y++)
                for (int x = 0; x < W; x++)
                    _grid[new(x, y)] = input.ElementAt(y).ElementAt(x);
        }

        public Grid(IEnumerable<(int, int, T)> input)
        {
            _grid = new Dictionary<Coord, T>();
            foreach (var (x, y, val) in input)
                _grid[new(x, y)] = val;
        }


        public virtual T this[int x, int y]
        {
            get => this[new(x,y)];

            set => this[new(x,y)] = value;
        }

        public virtual T this[Coord coord]
        {
            get
            {
                if(!_grid.ContainsKey(coord))
                {
                    if (!StoreOnMissingLookup)
                        return DefaultValue;
                    _grid[coord] = DefaultValue;
                }
                return _grid[coord];
            }

            set => _grid[coord] = value;
        }

        public IEnumerator<(Coord p, T v)> GetEnumerator()
        {
            foreach (var pair in _grid)
                yield return (pair.Key, pair.Value);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public delegate bool NeighborTest((Coord p, T v) mine, (Coord p, T v) theirs);

        public static bool Yes((Coord p, T v) mine, (Coord p, T v) theirs) => true;

        public IEnumerable<(Coord p, T v)> Neighbors(Coord point, IEnumerable<GridVector> directions, NeighborTest test = null, bool defaultIfMissing = false, bool wrap = false)
        {
            if (!ContainsCoord(point))
                Console.WriteLine("yerp");
            test ??= Yes;

            var v = wrap ? this[(point.x%W + W)%W, (point.y%H+H)%H] : this[point];

            foreach (var dir in directions)
            {
                var neighbor = point + dir;
                var nValue = wrap ? this[(neighbor.x%W + W) % W, (neighbor.y%H + H) % H] : this[neighbor];
                if ((ContainsCoord(neighbor) || defaultIfMissing) && test((point, v), (neighbor, nValue)))
                    yield return (neighbor, nValue);
            }
        }

        public void Render(string delim = "")
            => AoCUtil.PaintGrid(_grid.ToDictionary(p => (p.Key.x, p.Key.y), p => p.Value), delim: delim);

        public void Clear() => _grid.Clear();

        public bool ContainsCoord(Coord c) => _grid.ContainsKey(c);

        public bool IsEdge(Coord c) => c.x == 0 || c.x == W - 1 || c.y == 0 || c.y == H - 1;
    }

    public class HistoryGrid<T> : Grid<T>
    {
        public Stack<Dictionary<Coord, T>> History { get; } = new();

        public HistoryGrid(IEnumerable<IEnumerable<T>> input) : base(input)
        {
            History = new Stack<Dictionary<Coord,T>>();
        }

        public override T this[int x, int y]
        {
            get => this[new(x, y)];

            set
            {
                this[new(x, y)] = value;
            }
        }

        public override T this[Coord p]
        {
            get => _grid[p];

            set
            {
                History.Push(_grid);
                _grid = new Dictionary<Coord, T>(_grid) { [p] = value };
            }
        }

        public void GoBack(int moves = 1)
        {
            while (moves-- > 0)
                _grid = History.Pop();
        }

    }

    public record Coord
    {
        public int x;
        public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Coord(Coord c)
        {
            x = c.x;
            y = c.y;
        }

        public static Coord operator +(Coord p, GridVector v) => new(p.x + v.dx, p.y + v.dy);

        public override string ToString()
        {
            return $"({x},{y})";
        }
    }

    public record GridVector
    {
        public int dx;
        public int dy;

        public GridVector(int dx, int dy)
        {
            this.dx = dx;
            this.dy = dy;
        }

        public GridVector(Coord p1, Coord p2)
        {
            dx = p1.x - p2.x;
            dy = p1.y - p2.y;
        }

        public static GridVector operator +(GridVector v1, GridVector v2) => new(v1.dx + v2.dx, v1.dy + v2.dy);
        public static (int x, int y) operator +((int x, int y) point, GridVector v) => (point.x + v.dx, point.y + v.dy);

        public static GridVector operator *(GridVector v, int magnitude) => new(v.dx*magnitude, v.dy*magnitude);

        public int GridMagnitude => dx + dy;

        public static readonly GridVector U = new(0, -1);
        public static readonly GridVector R = new(1, 0);
        public static readonly GridVector D = new(0, 1);
        public static readonly GridVector L = new(-1, 0);
        public static readonly GridVector UR = U + R;
        public static readonly GridVector DR = D + R;
        public static readonly GridVector DL = D + L;
        public static readonly GridVector UL = U + L;

        public static readonly GridVector N = new(0, 1);
        public static readonly GridVector E = new(1, 0);
        public static readonly GridVector S = new(0, -1);
        public static readonly GridVector W = new(-1, 0);
        public static readonly GridVector NE = N + E;
        public static readonly GridVector SE = S + E;
        public static readonly GridVector SW = S + W;
        public static readonly GridVector NW = N + W;

        public static readonly IEnumerable<GridVector> DirsES = new GridVector[] { E, S };
        public static readonly IEnumerable<GridVector> DirsESWN = new GridVector[] { E, S, W, N };
        public static readonly IEnumerable<GridVector> DirsESNW = new GridVector[] { E, S, N, W };
        public static readonly IEnumerable<GridVector> DirsNESW = new GridVector[] { N, E, S, W };
        public static readonly IEnumerable<GridVector> DirsDiag = new GridVector[] { NW, NE, SE, SW };
        public static readonly IEnumerable<GridVector> DirsAll = new GridVector[] { NW, N, NE, E, SE, S, SW, W };
        public static readonly IEnumerable<GridVector> DirsAllPlusMe = new GridVector[] { NW, N, NE, E, SE, S, SW, W, new(0,0) };

        public static readonly ImmutableDictionary<string, GridVector> Lookup = new Dictionary<string, GridVector>
        {
            ["U"] = U,
            ["R"] = R,
            ["D"] = D,
            ["L"] = L,

            ["N"] = N,
            ["E"] = E,
            ["S"] = S,
            ["W"] = W,


        }.ToImmutableDictionary();

    }
}
