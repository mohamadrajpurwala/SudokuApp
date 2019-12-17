using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuApp.Services.Interfaces
{
    public interface ISudokuService
    {
        int[,] Create(string board);

        int[,] Solve(int[,] board);
    }
}
