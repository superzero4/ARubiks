using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NMinos
{
    public class NMino : IEnumerable<(int, int)>
    {
        public const int SIZE = 3;
        public const int TOTALSIZE = SIZE * SIZE;
        public readonly bool[,] structure;
        private NMino(bool[,] structure)
        {
            this.structure = structure;
        }
        private NMino() : this(new bool[SIZE, SIZE])
        {

        }
        public bool this[(int i, int j) index]
        {
            get => structure[index.i, index.j];
            set => structure[index.i, index.j] = value;
        }
        public bool this[int ind]
        {
            get => this[IndexToCoord(ind)];
            set => this[IndexToCoord(ind)] = value;
        }
        public IEnumerator<(int, int)> GetEnumerator() => new Indexes(SIZE);


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static (int i, int j) IndexToCoord(int _currentIndex, int _size = SIZE)
        {
            int row = _currentIndex / _size;
            int col = _currentIndex % _size;
            return (row, col);
        }
        public static int CoordToIndex((int i, int j) coord, int _size = SIZE)
        {
            return coord.i * _size + coord.j;
        }
        public class Indexes : IEnumerator<(int i, int j)>
        {
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
        public static class NMinoFactory
        {
            public static bool Rand => UnityEngine.Random.Range(0, 2) == 0;
            public static NMino RandomNMino()
            {
                var val = new NMino();
                var flag = false;
                bool rand;
                foreach (var item in val)
                {
                    rand = Rand;
                    val[item] = rand;
                    flag |= rand;
                }
                if (!flag)
                    val[Random.Range(0, NMino.TOTALSIZE)] = true;
                return val;
            }
            /*public static NMinoRandomContiguousNMino(int n)
            {
                var center = NMino.IndexToCoord(Random.Range(0, NMino.TOTALSIZE));
                var val = new NMino();
                while (n > 0)
                {
                    val[center] = true;
                    List<(int,int)> available
                    do
                    {
                        var increment = Rand ? -1 : +1;
                        if (Rand)
                            center.i += increment;
                        else
                            center.j += increment;
                    }while(center.!val[center])
                        n--;
                }
            }*/
            public static NMino Dot1Mino()
            {
                return new NMino(new bool[NMino.SIZE, NMino.SIZE]{
                    {false,false,false },
                    {false,true,false },
                    {false,false,false }
                });
            }
            public static NMino Line2Mino()
            {
                return new NMino(new bool[NMino.SIZE, NMino.SIZE]{
                    {false,false,false },
                    {true,true,false },
                    {false,false,false }
                });
            }
            public static NMino Line3Mino()
            {
                return new NMino(new bool[NMino.SIZE, NMino.SIZE]{
                    {false,false,false },
                    {true,true,true },
                    {false,false,false }
                });
            }
            public static NMino L3Mino()
            {
                return new NMino(new bool[NMino.SIZE, NMino.SIZE]{
                    {false,true,false },
                    {true,true,false },
                    {false,false,false }
                });
            }
            public static NMino L4Mino()
            {
                return new NMino(new bool[NMino.SIZE, NMino.SIZE]{
                    {false,true,false },
                    {false,true,false },
                    {true,true,false }
                });
            }
            public static NMino T4Mino()
            {
                return new NMino(new bool[NMino.SIZE, NMino.SIZE]{
                    {false,true,false },
                    {true,true,true },
                    {false,false,false }
                });
            }
            public static NMino Square4Mino()
            {
                return new NMino(new bool[NMino.SIZE, NMino.SIZE]{
                    {true,true,false },
                    {true,true,false },
                    {false,false,false }
                });
            }
            public static NMino S4Mino()
            {
                return new NMino(new bool[NMino.SIZE, NMino.SIZE]{
                    {true,true,false },
                    {false,true,true },
                    {false,false,false }
                });
            }
        }
    }
}
