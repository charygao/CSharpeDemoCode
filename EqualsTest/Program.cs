// ReSharper disable CompareOfFloatsByEqualityOperator

using System;
using System.Collections.Generic;
using System.Numerics;

namespace EqualsTest
{
    /// <summary>
    ///     复数类
    /// </summary>
    internal class ComplexNum : object//默认继承
    {
        #region Fields and Properties

        /// <summary>
        ///     虚部
        /// </summary>
        private readonly double _imaginaryPart;

        /// <summary>
        ///     实部
        /// </summary>
        private readonly double _realPart;

        #endregion

        #region  Constructors

        public ComplexNum(double imaginaryPart, double realPart)
        {
            _imaginaryPart = imaginaryPart;
            _realPart = realPart;
        }

        #endregion

        #region  Methods

        /// <summary>
        ///     重载“==”运算符，设计为值比较（因为复数看起来像一个值）
        /// </summary>
        /// <param name="z1"></param>
        /// <param name="z2"></param>
        /// <returns></returns>
        public static bool operator ==(ComplexNum z1, ComplexNum z2)
        {
            /**
             * 如果z1和z2都为空引用，返回true.我们用Object类的ReferenceEquals()函数比较z1的引用符和null
             */
            if (ReferenceEquals(z1, null) && ReferenceEquals(z2, null)) return true;

            /**
             * 如果z1、z2种一个为空引用，另一个不空，返回false
             * 注意，比较z1的引用符和null时，需要把z1转换为object型才能使用“==”运算符,因为Complex类的“==”还没定义好
             */
            if ((object)z1 == null || (object)z2 == null) return false;
            /**
             * 如果两个复数的实部和虚部均相等
             */
            if (z1._realPart == z2._realPart && z1._imaginaryPart == z2._imaginaryPart)
                return true;
            return false;
        }

        /// <summary>
        ///     重载“！=”运算符，值比较
        /// </summary>
        /// <param name="z1"></param>
        /// <param name="z2"></param>
        /// <returns></returns>
        public static bool operator !=(ComplexNum z1, ComplexNum z2)
        {
            return !(z1 == z2);
        }

        /// <summary>
        ///     重写实例版Equals()函数，设计为值比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            //若obj的值是null或两个对象类型不同
            if (obj == null || obj.GetType() != typeof(ComplexNum)) return false;

            var another = (ComplexNum)obj;
            //如果两个复数的实部和虚部均相等
            if (_realPart == another._realPart && _imaginaryPart == another._imaginaryPart)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_imaginaryPart.GetHashCode() * 397) ^ _realPart.GetHashCode();
            }
        }

        protected bool Equals(ComplexNum other)
        {
            return _imaginaryPart.Equals(other._imaginaryPart) && _realPart.Equals(other._realPart);
        }

        #endregion
    }

    internal class Program
    {
        #region  Methods

        private static void Main()
        {
            var a = 1;
            var b = 2;
            var c = a == b;//ceq
            var c11 = 1 == 2;//此方法直接被优化！
            b = a;
            a = 3;

            var z1 = new ComplexNum(100, 200);
            var z2 = new ComplexNum(100, 200);

            //比较引用符
            Console.WriteLine(ReferenceEquals(z1, z2) ? "引用符比较：它们是同一个复数" : "引用符比较：它们不是同一个复数");

            //静态版Equals()函数
            Console.WriteLine(Equals(z1, z2) ? "静态版比较，两个复数相等" : "静态版比较，两个复数不相等");
            //实例版Equals()函数
            Console.WriteLine(z1.Equals(z2) ? "实例版比较，两个复数相等" : "实例版比较，两个复数不相等");
            //“==”运算符
            Console.WriteLine(z1 == z2 ? "运算符比较，两个复数相等" : "运算符比较，两个复数不相等");

            z1 = z2;
            Console.WriteLine("--------");

            var c1 = new Complex(100.1, 200.2);
            var c2 = new Complex(100.1, 200.2);
            //var a = ReferenceEquals(c1, c2);//内部的 == 非下面的 == ，下面的 == 是operator重载
            //var b = c1 == c2;

            Console.WriteLine($"c1 is ValueType?:{c1 is ValueType isValueType}");
            //比较引用符
            // ReSharper disable once ReferenceEqualsWithValueType
            Console.WriteLine(ReferenceEquals(c1, c2) ? "引用符比较：它们是同一个复数" : "引用符比较：它们不是同一个复数");
            //静态版Equals()函数
            Console.WriteLine(Equals(c1, c2) ? "静态版比较，两个复数相等" : "静态版比较，两个复数不相等");
            //实例版Equals()函数
            Console.WriteLine(c1.Equals(c2) ? "实例版比较，两个复数相等" : "实例版比较，两个复数不相等");
            //“==”运算符
            Console.WriteLine(c1 == c2 ? "运算符比较，两个复数相等" : "运算符比较，两个复数不相等");


            List<ComplexNum> list = new List<ComplexNum>();



            Console.ReadKey();
        }

        #endregion
    }
}