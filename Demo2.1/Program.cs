using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo2._1
{
    internal static class Program
    {
        #region  Methods

        private static void Main()
        {
            #region 整型数组

            var intArray = new MyGenericArray<int>(5); // 声明一个整型数组

            for (var c = 0; c < 5; c++) // 设置值
                intArray.setItem(c, c * 5);

            for (var c = 0; c < 5; c++) // 获取值
                Console.Write(intArray.getItem(c) + " ");
            Console.WriteLine();

            #endregion

            #region 字符数组

            var charArray = new MyGenericArray<char>(5); // 声明一个字符数组

            for (var c = 0; c < 5; c++) // 设置值
                charArray.setItem(c, (char)(c + 97));

            for (var c = 0; c < 5; c++) // 获取值
                Console.Write(charArray.getItem(c) + " ");
            Console.WriteLine();

            #endregion

            Console.ReadKey();
        }

        #endregion
    }
}
