﻿using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._2023._16
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(2023, 16);
            
            #region Stopwatch
            Stopwatch.Stop();
            WL($"Input processed in {Stopwatch.ElapsedMilliseconds} ms");
            Stopwatch.Restart();
            #endregion
            #endregion
            
            #region Part 1

            Ans("");
            #endregion Part 1

            #region Part 2

            //Ans("", 2);
            #endregion
        }
    }
}
