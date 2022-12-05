using System.Collections;
using System.Collections.Generic;
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
            {
                yield return (pair.Key, pair.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public delegate bool NeighborTest((Coord p, T v) mine, (Coord p, T v) theirs);

        public static bool Yes((Coord p, T v) mine, (Coord p, T v) theirs) => true;

        public IEnumerable<(Coord p, T v)> Neighbors(Coord point, IEnumerable<GridVector> directions, NeighborTest test = null, bool defaultIfMissing = false)
        {
            if (test == null)
                test = Yes;

            var neighbors = new List<(Coord p, T v)>();
            var v = this[point];
            foreach (var dir in directions)
            {
                var location = point + dir;
                if ((_grid.ContainsKey(location) || defaultIfMissing) && test((point, v), (location, this[location])))
                    neighbors.Add((location, this[location]));
            }

            return neighbors;
        }

        public void Render(string delim = "")
            => AoCUtil.PaintGrid(_grid.ToDictionary(p => (p.Key.x, p.Key.y), p => p.Value), delim: delim);

        public void Clear() => _grid.Clear();
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

        public int GridMagnitude => dx + dy;

        public static readonly IEnumerable<GridVector> ES = new GridVector[] { new(1, 0), new(0, -1) };
        public static readonly IEnumerable<GridVector> ESWN = new GridVector[] { new(1, 0), new(0, -1), new(-1, 0), new(0, 1) }; 
        public static readonly IEnumerable<GridVector> NESW = new GridVector[] { new(0, 1), new(1, 0), new(0, -1), new(-1, 0) };
        public static readonly IEnumerable<GridVector> Diag = new GridVector[] { new(1, 1), new(1, -1), new(-1, -1), new(1, -1) };
        public static readonly IEnumerable<GridVector> AllDirs = new GridVector[] { new(-1, 1), new(0, 1), new(1, 1), new(1, 0), new(1, -1), new(0, -1), new(-1, -1), new(-1, 0) };
        public static readonly IEnumerable<GridVector> AllPlusMe = new GridVector[] { new(-1, 1), new(0, 1), new(1, 1), new(-1, 0), new(0, 0), new(1, 0), new(-1, -1), new(0, -1), new(1, -1) };
    }
}
