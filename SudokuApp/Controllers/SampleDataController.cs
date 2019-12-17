using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SudokuApp.Models;
using SudokuApp.Services.Interfaces;

namespace SudokuApp.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly ISudokuService _sudokuService;
        private readonly SudokuConfiguration _sudokuConfiguration;

        public SampleDataController(ISudokuService sudokuService, IOptions<SudokuConfiguration> options)
        {
            _sudokuService = sudokuService;
            _sudokuConfiguration = options.Value;
        }

        /// <summary>
        /// Create Sudoku.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<int[,]> Create()
        {
            return await Task.FromResult(_sudokuService.Create(_sudokuConfiguration.DefaultSudokuBoard));
        }

        /// <summary>
        /// Solve Sudoku.
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<int[,]> Solve()
        {
            var board = await Task.FromResult(_sudokuService.Create(_sudokuConfiguration.DefaultSudokuBoard));
            return await Task.FromResult(_sudokuService.Solve(board));
        }

    }
}
