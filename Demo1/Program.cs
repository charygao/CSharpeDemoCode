using System;
using Demo1Dll;

// ReSharper disable RedundantTypeArgumentsOfMethod

namespace Demo1
{
    internal class Program
    {
        #region  Methods

        private static void Main()//used by eastmoney
        {
            #region 1.原始方法

            int i1 = 1, i2 = 2;
            Console.WriteLine($"I need Swap int:i1 = {i1},i2 = {i2}");
            SwapHelper.Swap(ref i1, ref i2);
            Console.WriteLine($"After Swap int:i1 = {i1},i2 = {i2}");

            Console.WriteLine("\n========================\n");

            string s1 = "aaa", s2 = "bbb";
            Console.WriteLine($"I need Swap string:s1 = {s1},s2 = {s2}");
            SwapHelper.Swap(ref s1, ref s2);
            Console.WriteLine($"After Swap string:s1 = {s1},s2 = {s2}");

            Console.WriteLine("\n\n==========华丽的分割线==============\n\n");

            #endregion

            #region 2.泛型方法
            char c1 = 'A', c2 = '5';
            Console.WriteLine($"I can Swap everything:c1 = {c1},c2 = {c2}");
            SwapHelper.Swap<char>(ref c1, ref c2);
            Console.WriteLine($"After Swap :c1 = {c1},c2 = {c2}");

            Console.WriteLine("\n========================\n");

            decimal d1 = 1.00m, d2 = 3.00m;
            Console.WriteLine($"I can Swap everything:d1 = {d1},d2 = {d2}");
            SwapHelper.Swap<decimal>(ref d1, ref d2);
            Console.WriteLine($"After Swap :d1 = {d1},c2 = {d2}");
            #endregion

            Console.Read();
        }

        #endregion

        #region 3.一生二
        public TOut SomeMethod<T1, T2, T3, TOut>(T1 t1, T2 t2, T3 t3)
        {
            TOut @out = default(TOut);
            return @out;
        }
        #endregion
    }
}