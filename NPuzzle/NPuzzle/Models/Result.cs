using n_puzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPuzzle.Models
{
    public class Result
    {
        public List<Branch> Path { get; set; }
        public int Open { get; set; }
        public int Closed { get; set; }
    }
}
