using Google.OrTools.ConstraintSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRS.Common.SolverHelper
{
    public class SolverHelper
    {
        Solver solver = new Solver("schedule_shifts");

        public Dictionary<(int, int), IntVar> CreateShifts(int num_x, int num_y, int num_z, int type)
        {
            var shifts = new Dictionary<(int, int), IntVar>();
            if (type == 1)
            {
                foreach (var j in Enumerable.Range(0, num_x))
                {
                    foreach (var i in Enumerable.Range(0, num_y))
                    {
                        shifts[(j, i)] = solver.MakeIntVar(0, num_z - 1, string.Format("shifts({0},{1})", j, i));
                    }
                }
            }
            else
            {
                foreach (var i in Enumerable.Range(0, num_x))
                {
                    foreach (var j in Enumerable.Range(0, num_y))
                    {
                        shifts[(i, j)] = solver.MakeBoolVar(string.Format("nurse%d shift%d", i, j));
                    }
                }
            }
            return shifts;
        }


    }
}
