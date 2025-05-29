using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR1
{
    internal class Freezed
    {
        static int getLen(int nRow, int nColl, int row, out int error, out int deltaerr)
        {
            //int error = 0;
            //int deltaerr = (nRow + 1);
            int diry = 1; // nRow;
                          //int y = 0;

            //if (diry > 0)
            //    diry = 1;
            //if (diry < 0)
            //    diry = -1;

            //for x from x0 to x1
            //    plot(x, y)
            error += deltaerr;
            if (error >= (nColl + 1))
            {
                row += diry;
                error -= (nColl + 1);
            }

            return row;
        }

        static int myArr(int nRow, int nColl)
        {
            int AllElements = nRow * nColl;
            int countElem = 0;
            int centr = (nRow / 2 > 0) ? nRow / 2 : 1;

            //int cx = (nRow % 2);
            //int cy = -(nColl % 2);
            //cx += (cx != 0) ? cy : 0;

            int[][] Array2DStep = new int[nRow][];
            //int len =
            for (int i = 0, col = AllElements; i < nRow; i++)
            {
                // (centr - i) инверсия относительно центра
                // (nColl/centr) длина ступеньки 
                int currCol = nColl - (nColl / centr) * (centr - i);

                Array2DStep[i] = new int[currCol];

                for (int j = 0; j < Array2DStep[i].Count(); j++)
                {
                    Array2DStep[i][j] = j + 1;
                    countElem++;
                }

                col -= currCol;
            }

            //if (countElem != AllElements) 
            //{
            //}

            foreach (var arr in Array2DStep)
            {
                Console.WriteLine();
                foreach (var item in arr)
                {
                    Console.Write(item + " ");
                }
            }


            Console.WriteLine("\n\n --------- " + countElem + "/" + AllElements + " --------- " +
                ((AllElements == countElem) ? "YES" : "NO"));

            return countElem;
        }

        static int plotLine(int nRow, int nColl)
        {
            int dx = nColl;
            var dy = -nRow;

            int sx = 1;
            var sy = 1;

            var error = dx + dy;

            int[][] Array2DStep = new int[nRow][];

            int row = 0, col = 0;
            int iter = 0;

            try
            {
                while (iter < 1000)
                {
                    if (row == nRow) //&& col == nColl
                        break;

                    Array2DStep[row] = new int[col];

                    for (int j = 0; j < Array2DStep[row].Count(); j++)
                    {
                        Array2DStep[row][j] = j + 1;
                    }

                    var e2 = 2 * error;
                    if (e2 >= dy)
                    {
                        if (col == nColl)
                            break;

                        error = error + dy;
                        col += sx;
                    }
                    if (e2 <= dx)
                    {
                        if (row == nRow) break;
                        error = error + dx;
                        row += sy;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            var countElem = 0;

            foreach (var arr in Array2DStep)
            {
                countElem += arr.Count();
                Console.WriteLine();
                foreach (var item in arr)
                {
                    Console.Write(item + " ");
                }
            }

            return countElem;
        }
    }
}
