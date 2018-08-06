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
        SortExtention sortExtention = new SortExtention();
        int[] arr = { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        sortExtention.MergeSortFunction(arr, sortExtention.ComparisonInt,0,arr.Length-1);
    }
}
