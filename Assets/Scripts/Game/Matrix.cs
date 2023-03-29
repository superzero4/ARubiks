using System.Collections;
using System.Collections.Generic;
using static NMinos.Indexes;

namespace NMinos
{
    public class Matrix<T> : IEnumerable<(int, int)>
    {
        public const int DEFAULTSIZE = 3;
        public const int DEFAULTTOTALSIZE = DEFAULTSIZE * DEFAULTSIZE;
        public readonly T[,] structure;
        public Matrix(T[,] structure)
        {
            this.structure = structure;
        }
        public Matrix(int x, int y) : this(new T[x, y])
        {

        }
        public Matrix() : this(DEFAULTSIZE, DEFAULTSIZE)
        {

        }
        public Matrix(object[,] matrixToCopyFrom) : this(matrixToCopyFrom.GetLength(0), matrixToCopyFrom.GetLength(1))
        {

        }
        public T this[(int i, int j) index]
        {
            get => structure[index.i, index.j];
            set => structure[index.i, index.j] = value;
        }
        public T this[int ind]
        {
            get => this[IndexToCoord(ind)];
            set => this[IndexToCoord(ind)] = value;
        }
        public int CoordToIndex((int i, int j) coord)
        {
            return Indexes.CoordToIndex(coord, structure.GetLength(0));
        }

        public (int i, int j) IndexToCoord(int _currentIndex)
        {
            return Indexes.IndexToCoord(_currentIndex, structure.GetLength(0));
        }
        public IEnumerator<(int, int)> GetEnumerator() => new Indexes(DEFAULTSIZE);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool OutOfBound((int i, int j) coord)
        {
            return coord.i < 0 || coord.i >= structure.GetLength(0) || coord.j < 0 || coord.j >= structure.GetLength(1);
        }
    }
    public class Indexes : IEnumerator<(int i, int j)>
    {
        public static int CoordToIndex((int i, int j) coord, int sizeX)
        {
            return coord.i * sizeX + coord.j;
        }
        public static (int i, int j) IndexToCoord(int _currentIndex, int sizeX)
        {
            int row = _currentIndex / sizeX;
            int col = _currentIndex % sizeX;
            return (row, col);
        }
        public static (int i, int j) Sum((int, int) lhs, (int, int) rhs)
        {
            return (lhs.Item1 + rhs.Item1, lhs.Item2 + rhs.Item2);
        }
        private int _currentIndex;
        private int _size;

        public Indexes(int size)
        {
            _currentIndex = -1;
            _size = size;
        }

        public (int i, int j) Current
        {
            get
            {
                return IndexToCoord(_currentIndex, _size);
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            _currentIndex++;
            return _currentIndex < _size * _size;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }
    }
}