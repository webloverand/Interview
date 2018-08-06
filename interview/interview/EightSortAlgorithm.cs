using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightSortAlgorithm
{
    public class SortExtention
    {
        #region 冒泡排序
        /*
         * 已知一组无序数据a[1]、a[2]、……a[n]，需将其按升序排列。首先比较a[1]与a[2]的值，若a[1]大于a[2]则交换两者的值，否则不变。
         * 再比较a[2]与a[3]的值，若a[2]大于a[3]则交换两者的值，否则不变。再比较a[3]与a[4]，以此类推，最后比较a[n-1]与a[n]的值。
         * 这样处理一轮后，a[n]的值一定是这组数据中最大的。再对a[1]~a[n-1]以相同方法处理一轮，则a[n-1]的值一定是a[1]~a[n-1]中最大的。
         * 再对a[1]~a[n-2]以相同方法处理一轮，以此类推。共处理n-1轮后a[1]、a[2]、……a[n]就以升序排列了。
         * 降序排列与升序排列相类似，若a[1]小于a[2]则交换两者的值，否则不变，后面以此类推。 
         * 总的来讲，每一轮排序后最大（或最小）的数将移动到数据序列的最后，理论上总共要进行n(n-1）/2次交换。
         * 优点：稳定
         * 时间复杂度：理想情况下（数组本来就是有序的），此时最好的时间复杂度为o(n),最坏的时间复杂度(数据反序的)，此时的时间复杂度为o(n*n) 。
         * 冒泡排序的平均时间负责度为o(n*n).
         * 缺点：慢，每次只移动相邻的两个元素。
         */
        /// <summary>
        /// 冒泡排序，结果升序排列
        /// <para>调用：array.BubbleSort();</para>
        /// </summary>
        /// <param name="array">要排序的整数数组</param>
        public void BubbleSort<T>( T[] array,Comparison<T> comparison)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    //比较相邻的两个元素，如果前面的比后面的大，则交换位置
                    if (comparison(array[j] , array[j + 1]) == 1)
                    {
                        Exchange(ref array[j],ref array[j + 1]);
                    }
                }
            }
        }
        #endregion

        #region 快速排序
        /***
         * 设要排序的数组是A[0]……A[N-1]，首先任意选取一个数据（通常选用数组的第一个数）作为关键数据，
         * 然后将所有比它小的数都放到它前面，所有比它大的数都放到它后面，这个过程称为一趟快速排序。
         * 值得注意的是，快速排序不是一种稳定的排序算法，也就是说，多个相同的值的相对位置也许会在算法结束时产生变动。
         一趟快速排序的算法是：
            1）设置两个变量i、j，排序开始的时候：i=0，j=N-1；
            2）以第一个数组元素作为关键数据，赋值给key，即key=A[0]；
            3）从j开始向前搜索，即由后开始向前搜索(j–)，找到第一个小于key的值A[j]，将A[j]和A[i]互换；
            4）从i开始向后搜索，即由前开始向后搜索(i++)，找到第一个大于key的A[i]，将A[i]和A[j]互换；
            5）重复第3、4步，直到i=j； (3,4步中，没找到符合条件的值，即3中A[j]不小于key,4中A[i]不大于key的时候改变j、i的值，
         *     使得j=j-1，i=i+1，直至找到为止。找到符合条件的值，进行交换的时候i， j指针位置不变。另外，i==j这一过程一定正好是i+或j-完成的时候，此时令循环结束）。
         */
        /// <summary>
        /// 快速排序
        /// <para>调用：array.QuickSort(0, array.Length-1 );</para>
        /// </summary>
        /// <param name="array">要排序的数组</param>
        /// <param name="left">低位</param>
        /// <param name="right">高位</param>
        public void QuickSort<T>(T[] array, Comparison<T> comparison, int left, int right)
        {
            //左边小于右边说明排序还没有完成
            if (left < right)
            {
                T middle = array[(left + right) / 2];
                //注意初始化
                int j = right + 1;
                int i = left - 1;
                while (true)
                {
                    while (comparison(middle, array[++i]) > 0 && i < right) ;//左边,先加的原因是防止找到的最左边会超出界限
                    while (comparison(middle, array[--j]) < 0 && j > 0) ; //右边
                    if (i >= j)
                        break;
                    Exchange<T>(ref array[i], ref array[j]);
                }
                for (int m = 0; m < array.Length; m++)
                {
                    Console.Write(array[m] + " ");
                }
                Console.ReadLine();
                QuickSort<T>(array, comparison, left, i - 1);
                QuickSort<T>(array, comparison, j + 1, right);
            }
        }
        #endregion

        #region 直接插入排序
        /**
         * 每次从无序表中取出第一个元素，把它插入到有序表的合适位置，使有序表仍然有序。
         * 第一趟比较前两个数，然后把第二个数按大小插入到有序表中； 第二趟把第三个数据与前两个数从前向后扫描，把第三个数按大小插入到有序表中；
         * 依次进行下去，进行了(n-1)趟扫描以后就完成了整个排序过程。
         * 直接插入排序属于稳定的排序，最坏时间复杂性为O(n^2)，空间复杂度为O(1)。
         * 直接插入排序是由两层嵌套循环组成的。外层循环标识并决定待比较的数值。内层循环为待比较数值确定其最终位置。
         * 直接插入排序是将待比较的数值与它的前一个数值进行比较，所以外层循环是从第二个数值开始的。
         * 当前一数值比待比较数值大的情况下继续循环比较，直到找到比待比较数值小的并将待比较数值置入其后一位置，结束该次循环。
         * 值得注意的是，我们必需用一个存储空间来保存当前待比较的数值，因为当一趟比较完成时，
         * 我们要将待比较数值置入比它小的数值的后一位 插入排序类似玩牌时整理手中纸牌的过程。
         * 插入排序的基本方法是：每步将一个待排序的记录按其关键字的大小插到前面已经排序的序列中的适当位置，直到全部记录插入完毕为止。
         */
        /// <summary>
        /// 直接插入排序
        /// <para>调用：array.InsertSort();</para>
        /// </summary>
        /// <param name="array">要排序的数组</param>
        public void InsertSort<T>(T[] array,Comparison<T> comparison)
        {
            //直接插入排序是将待比较的数值与它的前一个数值进行比较，所以外层循环是从第二个数值开始的
            for (int i = 1; i < array.Length; i++)
            {
                T temp = array[i];
                int j = i - 1;

                // 在已排好序的数列段中找到比新数值小的数值
                while (j >= 0 && comparison(array[j] , temp) == 1)
                {
                    //将比新数值大的数值向后移
                    array[j + 1] = array[j];
                    j--;
                }
                // 如果在已排好序的数列段中找到了比新数值小的数值
                // 将数值替换到此位置
                array[j + 1] = temp;
                for (int m = 0; m < array.Length; m++)
                {
                    Console.Write(array[m] + " ");
                }
                Console.ReadLine();
            }
        }
        #endregion

        #region 希尔排序
        /**
         * 希尔排序(Shell Sort)是插入排序的一种。也称缩小增量排序，是直接插入排序算法的一种更高效的改进版本。
         * 希尔排序是非稳定排序算法。该方法因DL．Shell于1959年提出而得名。

         * 希尔排序是基于插入排序的以下两点性质而提出改进方法的：
         * 插入排序在对几乎已经排好序的数据操作时，效率高，即可以达到线性排序的效率。
         * 但插入排序一般来说是低效的，因为插入排序每次只能将数据移动一位。

        基本思想：

         * 先取一个小于n的整数d1作为第一个增量，把文件的全部记录分组。所有距离为d1的倍数的记录放在同一个组中。
         * 先在各组内进行直接插入排序；然后，取第二个增量d2<d1重复上述的分组和排序，直至所取的增量 =1( < …<d2<d1)，
         * 即所有记录放在同一组中进行直接插入排序为止。
         * 该方法实质上是一种分组插入方法
         * 比较相隔较远距离（称为增量）的数，使得数移动时能跨过多个元素，
         * 则进行一次比[2] 较就可能消除多个元素交换。D.L.shell于1959年在以他名字命名的排序算法中实现了这一思想。
         * 算法先将要排序的一组数按某个增量d分成若干组，每组中记录的下标相差d.对每组中全部元素进行排序，
         * 然后再用一个较小的增量对它进行，在每组中再进行排序。当增量减到1时，整个要排序的数被分成一组，排序完成。
         * 一般的初次取序列的一半为增量，以后每次减半，直到增量为1。
         */
        /// <summary>
        /// 希尔排序
        /// <para>调用：array.ShellSort();</para>
        /// </summary>
        /// <param name="array">待排序的数组</param>
        public void ShellSort<T>(T[] array,Comparison<T> comparison)
        {
            int Length = array.Length;
            for (int gap = Length / 2; gap > 0; gap = gap / 2) //分组，得到组数
            {
                for (int i = gap; i < gap * 2; i++)//对每一组进行插入排序
                {
                    for (int j = i; j < Length; j += gap)
                    {
                        T temp = array[j];
                        int k = j - gap;
                        while (k >= 0 && comparison(temp , array[k]) == -1)
                        {
                            array[k + gap] = array[k];
                            k = k - gap;
                        }
                        array[k + gap] = temp;

                    }
                }
                for (int k = 0; k < Length; k++)
                {
                    Console.Write(array[k] + " ");
                }
                Console.ReadLine();
            }
        }
        #endregion

        #region 简单选择排序
        /**
         * 设所排序序列的记录个数为n。i取1,2,…,n-1,从所有n-i+1个记录（Ri,Ri+1,…,Rn）中找出排序码最小的记录，
         * 与第i个记录交换。执行n-1趟 后就完成了记录序列的排序。
         * 在简单选择排序过程中，所需移动记录的次数比较少。最好情况下，
         * 即待排序记录初始状态就已经是正序排列了，则不需要移动记录。
         * 最坏情况下，即待排序记录初始状态是按逆序排列的，则需要移动记录的次数最多为3（n-1）。
         * 简单选择排序过程中需要进行的比较次数与初始状态下待排序的记录序列的排列情况无关。
         * 当i=1时，需进行n-1次比较；当i=2时，需进行n-2次比较；依次类推，
         * 共需要进行的比较次数是(n-1)+(n-2)+…+2+1=n(n-1)/2，即进行比较操作的时间复杂度为O(n^2)，
         * 进行移动操作的时间复杂度为O(n)。
         */
        /// <summary>
        /// 简单选择排序
        /// <para>调用：array.SimpleSelectSort();</para>
        /// </summary>
        /// <param name="array">待排序的数组</param>
        public void SimpleSelectSort<T>( T[] array , Comparison<T> comparison)
        {
            int k = 0;
            for (int i=0;i<array.Length;i++)
            {
                k = i;
                for (int j = i + 1;j<array.Length;j++)
                {
                    if(comparison(array[k] , array[j]) == 1)
                    {
                        k = j;
                    }
                }
                Exchange(ref array[i],ref array[k]);
                for (int m = 0;m<array.Length;m++)
                {
                    Console.Write(array[m] + " ");
                }
                Console.ReadLine();
            }
        }
        #endregion

        #region 堆排序
        /**
         * 堆排序(Heapsort)是指利用堆积树（堆）这种数据结构所设计的一种排序算法，
         * 它是选择排序的一种。可以利用数组的特点快速定位指定索引的元素。
         * 堆分为大根堆和小根堆，是完全二叉树。大根堆的要求是每个节点的值都不大于其父节点的值，
         * 即A[PARENT[i]] >= A[i]。在数组的非降序排序中，需要使用的就是大根堆，
         * 因为根据大根堆的要求可知，最大的值一定在堆顶。
         */
        /// <summary>
        /// 堆排序
        /// <para>调用：array.HeapSort(array.Length);</para>
        /// </summary>
        /// <param name="array">待排序的数组</param>、
        public void HeapSort<T>(T[] array, Comparison<T> comparison)
        {
            BuildMHeap<T>(array, comparison);
            for (int m = 0; m < array.Length; m++)
            {
                Console.Write(array[m] + " ");
            }
            Console.ReadLine();
            for (int i = array.Length - 1; i > 0; i--)
            {
                Exchange(ref array[i], ref array[0]);
                MHeapify<T>(array, 0, i, comparison);
                for (int m = 0; m < array.Length; m++)
                {
                    Console.Write(array[m] + " ");
                }
                Console.ReadLine();
            }
        }
        //计算节点的父节点和子节点
        private int Parrent(int i)
        {
            return (i - 1) / 2;
        }
        private int Left(int i)
        {
            return 2 * i + 1;
        }
        private int Right(int i)
        {
            return 2 * i + 2;
        }
        //构建最大堆/最小堆
        private void BuildMHeap<T>(T[] array, Comparison<T> comparison)
        {
            for (int i = array.Length / 2 - 1; i >= 0; i--)
            {
                MHeapify<T>(array, i, array.Length, comparison);
                
            }
        }
        private void MHeapify<T>(T[] array, int i, int heapSize, Comparison<T> comparison)
        {
            int left = Left(i);
            int right = Right(i);

            int extremumIndex = i;
            if (left < heapSize && comparison(array[left], array[i]) > 0)
            {
                extremumIndex = left;
            }

            if (right < heapSize && comparison(array[right], array[extremumIndex]) > 0)
            {
                extremumIndex = right;
            }

            if (extremumIndex != i)
            {
                Exchange<T>(ref array[extremumIndex], ref array[i]);
                MHeapify<T>(array, extremumIndex, heapSize, comparison);
            }
        }

        #endregion

        #region 归并排序
        /**
         * 归并排序是建立在归并操作上的一种有效的排序算法,该算法是采用分治法（Divide and Conquer）的一个非常典型的应用。
         * 将已有序的子序列合并，得到完全有序的序列；即先使每个子序列有序，再使子序列段间有序
         * 。若将两个有序表合并成一个有序表，称为二路归并。
         * 归并过程为：比较a[i]和a[j]的大小，若a[i]≤a[j]，则将第一个有序表中的元素a[i]复制到r[k]中，
         * 并令i和k分别加上1；否则将第二个有序表中的元素a[j]复制到r[k]中，并令j和k分别加上1，
         * 如此循环下去，直到其中一个有序表取完，然后再将另一个有序表中剩余的元素复制到r中从下标k到下标t的单元。
         * 归并排序的算法我们通常用递归实现，先把待排序区间[s,t]以中点二分，接着把左边子区间排序，再把右边子区间排序，
         * 最后把左区间和右区间用一次归并操作合并成有序的区间[s,t]。

         * 归并操作(merge)，也叫归并算法，指的是将两个顺序序列合并成一个顺序序列的方法。
         * 如　设有数列{6，202，100，301，38，8，1}
         * 初始状态：6,202,100,301,38,8，1
         * 第一次归并后：{6,202},{100,301},{8,38},{1}，比较次数：3；
         * 第二次归并后：{6,100,202,301}，{1,8,38}，比较次数：4；
         * 第三次归并后：{1,6,8,38,100,202,301},比较次数：4；
         * 总的比较次数为：3+4+4=11,；
         * 逆序数为14；

         * 归并操作的工作原理如下：
         * 第一步：申请空间，使其大小为两个已经排序序列之和，该空间用来存放合并后的序列
         * 第二步：设定两个指针，最初位置分别为两个已经排序序列的起始位置
         * 第三步：比较两个指针所指向的元素，选择相对小的元素放入到合并空间，并移动指针到下一位置
         * 重复步骤3直到某一指针超出序列尾
         * 将另一序列剩下的所有元素直接复制到合并序列尾
         */
        /// <summary>
        /// 归并排序
        /// <para>调用：array.MergeSort(0, array.Length);</para>
        /// </summary>
        /// <param name="array">待排序数组</param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        ///   //归并排序（目标数组，子表的起始位置，子表的终止位置）
        public void MergeSortFunction<T>(T[] array, Comparison<T> comparison, int first, int last)
        {
            try
            {
                if (first < last)   //子表的长度大于1，则进入下面的递归处理
                {
                    int mid = (first + last) / 2;   //子表划分的位置
                    MergeSortFunction(array,comparison, first, mid);   //对划分出来的左侧子表进行递归划分
                    MergeSortFunction(array, comparison, mid + 1, last);    //对划分出来的右侧子表进行递归划分
                    MergeSortCore(array, comparison, first, mid, last); //对左右子表进行有序的整合（归并排序的核心部分）

                    for (int m = 0; m < array.Length; m++)
                    {
                        Console.Write(array[m] + " ");
                    }
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            { }
        }

        //归并排序的核心部分：将两个有序的左右子表（以mid区分），合并成一个有序的表
        private void MergeSortCore<T>(T[] array, Comparison<T> comparison, int first, int mid, int last)
        {
            try
            {
                int indexA = first; //左侧子表的起始位置
                int indexB = mid + 1;   //右侧子表的起始位置
                T[] temp = new T[last + 1]; //声明数组（暂存左右子表的所有有序数列）：长度等于左右子表的长度之和。
                int tempIndex = 0;
                while (indexA <= mid && indexB <= last) //进行左右子表的遍历，如果其中有一个子表遍历完，则跳出循环
                {
                    if (comparison(array[indexA] , array[indexB]) <= 0) //此时左子表的数 <= 右子表的数
                    {
                        temp[tempIndex++] = array[indexA++];    //将左子表的数放入暂存数组中，遍历左子表下标++
                    }
                    else//此时左子表的数 > 右子表的数
                    {
                        temp[tempIndex++] = array[indexB++];    //将右子表的数放入暂存数组中，遍历右子表下标++
                    }
                }
                //有一侧子表遍历完后，跳出循环，将另外一侧子表剩下的数一次放入暂存数组中（有序）
                while (indexA <= mid)
                {
                    temp[tempIndex++] = array[indexA++];
                }
                while (indexB <= last)
                {
                    temp[tempIndex++] = array[indexB++];
                }

                //将暂存数组中有序的数列写入目标数组的制定位置，使进行归并的数组段有序
                tempIndex = 0;
                for (int i = first; i <= last; i++)
                {
                    array[i] = temp[tempIndex++];
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region 基数排序
        /**
         * 基数排序（radix sort）属于“分配式排序”（distribution sort），又称“桶子法”（bucket sort）或bin sort，
         * 顾名思义，它是透过键值的部份资讯，将要排序的元素分配至某些“桶”中，藉以达到排序的作用，
         * 基数排序法是属于稳定性的排序，其时间复杂度为O (nlog(r)m)，其中r为所采取的基数，
         * 而m为堆数，在某些时候，基数排序法的效率高于其它的稳定性排序法。
         */
        /// <summary>
        /// 基数排序
        /// <para>约定：待排数字中没有0,如果某桶内数字为0则表示该桶未被使用,输出时跳过即可</para>
        /// <para>调用：array.RadixSort();</para>
        /// </summary>
        /// <param name="array">待排数组</param>
        /// <param name="array_x">桶数组第一维长度</param>
        /// <param name="array_y">桶数组第二维长度</param>
        public static void RadixSort( int[] array, int array_x = 10, int array_y = 100)
        {
            /* 最大数字不超过999999999...(array_x个9) */
            for (int i = 0; i < array_x; i++)
            {
                int[,] bucket = new int[array_x, array_y];
                foreach (var item in array)
                {
                    int temp = (item / (int)Math.Pow(10, i)) % 10;
                    for (int l = 0; l < array_y; l++)
                    {
                        if (bucket[temp, l] == 0)
                        {
                            bucket[temp, l] = item;
                            break;
                        }
                    }
                }
                for (int o = 0, x = 0; x < array_x; x++)
                {
                    for (int y = 0; y < array_y; y++)
                    {
                        if (bucket[x, y] == 0) continue;
                        array[o++] = bucket[x, y];
                    }
                }
            }
        }
        #endregion

        #region 辅助计算
        #region 交换值
        public void Exchange<T>(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }
        #endregion
        #region 比较两个int的值
        public int ComparisonInt(int x,int y)
        {
            if(x > y)
            {
                return 1;
            }
            else if(x == y)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        #endregion
        #endregion
    }
}

