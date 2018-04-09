// ReSharper disable SuggestVarOrType_BuiltInTypes

// ReSharper disable UnusedVariable
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable SuggestVarOrType_Elsewhere

using System;
using System.Collections.Generic;

#pragma warning disable 168 // local variable 'i' is never used
namespace Demo3
{
    internal class Program
    {
        #region  Methods

        private static void Main()
        {
            //List<T> | T类型对象集合
            //Dictionary<TKey, TValue> | 与K类型的键值相关的V类型的项的集合
            List<string> sList;                     
            Dictionary<string, string> ssDictionary;
            #region ? Domain zone #0

            {
                /**
                * int? 是 System.Nullable<int>的缩写;
                */
                //int? i;
                // ReSharper disable once ConvertNullableToShortForm
                Nullable<int> i;
                //System.Nullable<int> i;
            }

            #endregion

            #region Correct domain zone #1          

            {
                /**             
                * 1.运算符和可空类型
                * 对于简单类型如int，可以使用+、-等运算符来处理值。而对于可空类型，这是没有区别的：
                * 包含在可空类型中的值会隐式转换为需要的类型，使用适当的运算符。这也适用于结构和自己提供的运算符。
                */
                int? op1 = 5;   
                int? result = op1 * 2;
                //var result = op1 * 2; //注意 result也是int?，下面的代码不能被编译
            }

            #region Error domain zone #2 

            //{
            //    int? op1 = 5;
            //    int result = op1 * 2;//为了使代码正常工作，必须进行显示转换
            //}

            #endregion

            #region Resurrect domain zone #3 

            {
                int? op1 = 5;
                int result = (int)(op1 * 2);
            }

            //{
            //    int? op1 = null;
            //    int result = (int) op1 * 2;
            //}

            #endregion

            #endregion

            #region bool? zone #4

            /**
             *只要op1有一个值，上面的代码就可以正常运行，如果op1是null，就会生成System.lnvalidOperationException异常。
             * 这就引出了下一个问题：当运算等式中的一个或两个值是null时，例如，上面代码中的op1，会发生什么情况？
             * 答：对于除了bool?之外的所有简单可空类型，该操作的结果是null，可以把它解释为“不能计算”。
             * 对于结构，可以定义自己的运算符来处理这种情况。对于bool?，为 & 和|定义的运算符会得到非空返回值。
             */
            {
                bool? op1 = true, op2 = true;
                //bool? op1 = false, op2 = true;
                //bool? op1 = null, op2 = true;

                //bool? op1 = true, op2 = false;
                //bool? op1 = false, op2 = false;
                //bool? op1 = null, op2 = false;


                //bool? op1 = true, op2 = null;
                //bool? op1 = false, op2 = null;
                //bool? op1 = null, op2 = null;

                bool? result1 = op1 | op2;
                bool? result2 = op1 & op2;
                Console.WriteLine(result1 + " : " + result2); //结果看文档给的表格
            }

            #endregion

            #region ??空结合运算符

            {
                double? op1 = 1, op2 = 2;
                //double? op1 = null, op2 = 2;
                var result = op1 ?? op2; //与下一句等价
                //var result = op1 == null ? op1 : op2;
            }
            /**
            * 在上述代码中，op1可以是任意可空表达式，包括引用类型和可空类型。
            * 因此，如果可空类型是null,就可以使用??运算符提供要使用的默认值。
            */
            {
                int? op1 = null;
                int result = op1 * 2 ?? 5;
                /**
                 * 该示例中，op1是null，所以op1*2也是null。
                 * 但是??运算符检测到这个情况，并把值5赋予result.
                 * 这里特别要注意，在结果中放入int类型的变量result不需要显式转换。??运算符会自动处理这个转换。
                 * 可以把??等式的结果放在int?中：
                 */
                //int? result = op1 * 2 ?? 5;
            }

            #endregion
        }

        #endregion
    }
}