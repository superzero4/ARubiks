using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NMinos
{
    public class NMino : Matrix<bool>
    {
        public NMino() : base()
        {

        }
        public NMino(bool[,] nmino) : base(nmino) { }

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
                    val[Random.Range(0, NMino.DEFAULTTOTALSIZE)] = true;
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
                return new NMino(new bool[NMino.DEFAULTSIZE, NMino.DEFAULTSIZE]{
                    {false,false,false },
                    {false,true,false },
                    {false,false,false }
                });
            }
            public static NMino Line2Mino()
            {
                return new NMino(new bool[NMino.DEFAULTSIZE, NMino.DEFAULTSIZE]{
                    {false,false,false },
                    {true,true,false },
                    {false,false,false }
                });
            }
            public static NMino Line3Mino()
            {
                return new NMino(new bool[NMino.DEFAULTSIZE, NMino.DEFAULTSIZE]{
                    {false,false,false },
                    {true,true,true },
                    {false,false,false }
                });
            }
            public static NMino L3Mino()
            {
                return new NMino(new bool[NMino.DEFAULTSIZE, NMino.DEFAULTSIZE]{
                    {false,true,false },
                    {true,true,false },
                    {false,false,false }
                });
            }
            public static NMino L4Mino()
            {
                return new NMino(new bool[NMino.DEFAULTSIZE, NMino.DEFAULTSIZE]{
                    {false,true,false },
                    {false,true,false },
                    {true,true,false }
                });
            }
            public static NMino T4Mino()
            {
                return new NMino(new bool[NMino.DEFAULTSIZE, NMino.DEFAULTSIZE]{
                    {false,true,false },
                    {true,true,true },
                    {false,false,false }
                });
            }
            public static NMino Square4Mino()
            {
                return new NMino(new bool[NMino.DEFAULTSIZE, NMino.DEFAULTSIZE]{
                    {true,true,false },
                    {true,true,false },
                    {false,false,false }
                });
            }
            public static NMino S4Mino()
            {
                return new NMino(new bool[NMino.DEFAULTSIZE, NMino.DEFAULTSIZE]{
                    {true,true,false },
                    {false,true,true },
                    {false,false,false }
                });
            }
        }
    }
}
