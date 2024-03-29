﻿using System;
using System.Collections.Generic;
using System.Linq;
using RegExtract;

namespace AoC._Year._Day
{
    class Program : ProgramBase
    {
        static void Main()
        {
            #region input
            #region Stopwatch 
            Stopwatch.Start();
            #endregion

            var input = AoCUtil.GetAocInput(Year, Day);
            
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

