using EightSortAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 八大排序算法
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        int[] arr = { 6, 5, 9, 16, 45, 35, 8, 13, 25, 60, 22, 31 };
        arr.ShellSort();
    }
}
