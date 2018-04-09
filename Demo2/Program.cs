using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Demo2
{
    #region 3.三生万物

    #region 泛型接口

    internal interface ICanEat<TItem> where TItem : new()
    {
        #region  Methods

        TItem Eat(TItem i);

        #endregion
    }

    #endregion

    #region 泛型类

    internal class Bone
    {
        #region Fields and Properties

        public int Weight { get; set; }

        #endregion

        #region  Constructors

        public Bone()
        {
        } //

        public Bone(int weight)
        {
        }

        ~Bone()
        {
        }

        #endregion
    }

    internal class Dog<TMyFood> : ICanEat<TMyFood> where TMyFood : new()
    {
        public TMyFood Eat(TMyFood i)
        {
            Console.WriteLine($"I eat {i.GetType()}");
            return default(TMyFood);
        }
    }

    #endregion

    #region 泛型结构

    internal struct Food : ICanEat<object>
    {
        public object Eat(object i)
        {
            return null;
        }
    }

    #endregion
    
    #endregion
    public class Program
    {
        #region Fields and Properties

        #region 泛型委托#4

        public delegate TOut MyDelegate<out TOut, in TIn>(TIn op1, TIn op2) where TIn : TOut;

        #endregion

        #endregion

        #region  Methods

        public static TOut CleanTInAndInitTOut<TIn, TOut>(TIn @in) where TIn : struct where TOut : IEnumerable
        {
           // if (@in == null) throw new ArgumentNullException(nameof(@in));

            #region 要初始化该怎么办？

            //{
            //    @in = null;// null ?,0 ?;
            //    TOut result = null;// null ?,0 ?;
            //    return result;
            //}

            #endregion

            #region //default闪亮登场

            {
                // ReSharper disable once RedundantAssignment
                @in = default(TIn);
                var result = default(TOut);
                return result;
            }

            #endregion
        }

        public static TItem FindMax<TItem>(TItem[] itemsArray) where TItem : IComparable
        {
            var maxItem = default(TItem);
            if (itemsArray.Length > 1) maxItem = itemsArray[0];
            foreach (var item in itemsArray)
                if (maxItem?.CompareTo(item) < 0) //强制要求实现比较算法
                    maxItem = item;
            return maxItem;
        }
        struct MyStudentStruct
        {
            public int Age;
            public string Name;
            public double Height;
        }
        private static void Main()
        {
            var i0 = 0;
            var initializedString = CleanTInAndInitTOut<int, string>(i0); //1.多个 类型参数 ＆ 返回值为 类型参数 ＆ default
            Console.WriteLine($"'{initializedString}'");

            #region default 默认值
            var d_int = default(int);
            var d_double = default(double);
            var d_long = default(long);
            var d_float = default(float);
            var d_uint = default(uint);
            var d_ulong = default(ulong);
            var d_char = default(char);
            var d_sbyte = default(sbyte);
            var d_byte = default(byte);
            var d_short = default(short);
            var d_ushort = default(ushort);
            var d_decimal = default(decimal);
            var d_bool = default(bool);
            var d_string = default(string);
            var d_object = default(object);
            var d_ValueType = default(ValueType);
            var d_MyStudentStruct = default(MyStudentStruct);

            Console.WriteLine($"default(int)      :{d_int}");
            Console.WriteLine($"default(double)   :{d_double} ");
            Console.WriteLine($"default(long)     :{d_long} ");
            Console.WriteLine($"default(float)    :{d_float}");
            Console.WriteLine($"default(uint)     :{d_uint}");
            Console.WriteLine($"default(ulong)    :{d_ulong}");
            Console.WriteLine($"default(char)     :{d_char} ");
            Console.WriteLine($"default(sbyte)    :{d_sbyte}");
            Console.WriteLine($"default(byte)     :{d_byte} ");
            Console.WriteLine($"default(short)    :{d_short}");
            Console.WriteLine($"default(ushort)   :{d_ushort}");
            Console.WriteLine($"default(decimal)  :{d_decimal}");
            Console.WriteLine($"default(bool)     :{d_bool} ");
            Console.WriteLine($"default(string)   :{d_string}");
            Console.WriteLine($"default(object)   :{d_object}");
            Console.WriteLine($"default(object)   :{d_ValueType}");
            Console.WriteLine($"default(object)   :{d_MyStudentStruct}");
            
            #endregion

            var intParams = new[] {1, 2, 5, 6, 8, 6, 2, 5, 1, 12}; //new int[]{1, 2, 5, 6, 8, 6, 2, 5, 1, 12};

            #region 自动推断类型 ＆ where

            // ReSharper disable once RedundantTypeArgumentsOfMethod
            // ReSharper disable once BuiltInTypeReferenceStyle
            var max = FindMax(intParams); //输入 自动推断类型！
            Console.WriteLine(max);

            #endregion

            #region 泛型类 ＆ 泛型接口 #sp2 #2

            var dog = new Dog<Bone>();
            dog.Eat(new Bone(10));

            #endregion

            //int? i1 = 1, i2 = null;
            //var i3 = i1 + i2;


            Console.ReadKey();
        }

        #endregion
    }
}