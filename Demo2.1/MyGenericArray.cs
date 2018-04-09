// ***********************************************************************
// 
// 文件名：         MyGenericArray.cs
// 
// 创建日期：       Someday
//  
// 功能说明：       泛型数组类（与类型无关的数组）
//  
// 作者：           高亚斌
// 
// ***********************************************************************

using System;

namespace Demo2._1
{
    public class MyGenericArray<T>
    {
        #region Fields and Properties

        private readonly T[] _array;

        #endregion

        #region  Constructors

        public MyGenericArray(int size)
        {
            _array = new T[size + 1];
            Console.WriteLine($"Geted T is {_array.GetType()}");
        }

        #endregion

        #region  Methods

        public T getItem(int index)
        {
            return _array[index];
        }

        public void setItem(int index, T value)
        {
            _array[index] = value;
        }

        #endregion
    }
}