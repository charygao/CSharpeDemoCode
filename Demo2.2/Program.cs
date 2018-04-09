using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo2._2
{
    internal static class Program
    {
        #region Fields and Properties

        /// <summary>
        ///     泛型委托（与类型无关的委托）
        /// </summary>
        /// <typeparam name="T">委托用到的类型</typeparam>
        /// <param name="n">委托只调用一个参数</param>
        /// <returns></returns>
        private delegate T Changer<T>(T n);

        #endregion

        #region  Methods

        private static void Main()
        {
            // 创建委托实例
            Changer<int> numChange1 = AddNum; //new Changer<int>(AddNum);
            Changer<int> numChange2 = MultNum; //new Changer<int>(MultNum);
            // 使用委托对象调用方法
            Console.WriteLine("Value of AddNum: {0}", numChange1(25));
            Console.WriteLine("Value of MultNum: {0}", numChange2(5));

            Console.WriteLine();

            /*************************华丽的分割线****************************/
            

            // 创建委托实例
            Changer<string> strChange1 = AddString;
            Changer<string> strChange2 = ReplaceString;
            // 使用委托对象调用方法

            Console.WriteLine("Value of AddString: {0}", strChange1("123"));
            Console.WriteLine("Value of ReplaceString: {0}", strChange2("Str"));

            Console.ReadKey();
        }

        #endregion

        #region 数字操作

        private static int AddNum(int p)
        {
            var num = 10;
            var resualt = num + p;
            return resualt;
        }

        private static int MultNum(int q)
        {
            var num = 10;
            num *= q;
            return num;
        }

        #endregion


        #region 字符串操作

        private static string AddString(string p)
        {
            var str = "orgStr";
            str += p;
            return str;
        }

        private static string ReplaceString(string q)
        {
            var str = "orgStr";
            var replaced = str.Replace(q, "[Replaced String]");
            return replaced;
        }

        #endregion
    }
}
