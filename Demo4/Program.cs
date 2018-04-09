using System;
using System.Collections.Generic;

// ReSharper disable RedundantAssignment
namespace Demo4
{
    internal class Program
    {
        #region  Methods

        private static void Main(string[] args)
        {
            var limited10ItemsList = new Limited10ItemsList<decimal>();
            for (var i = 0; i < 20; i++) limited10ItemsList.Add(i);
            limited10ItemsList.PrintListItems();
            Console.ReadKey();
        }

        #endregion
    }

    public partial class Limited10ItemsList<T> // : List<T>//部分类
    {
        #region Fields and Properties

        private readonly List<T> _List = new List<T>();
        private int _CurrentCount;
        public int MaxCount { get; } = 10;

        #endregion

        #region  Methods

        public void CleanLimitedList()
        {
            CleanCurrentListToNull(); //这个方法要调用MyMethod,但MyMethod目前还不知道如何实现
            _CurrentCount = 0;
        }

        public void PrintListItems()
        {
            foreach (var item in _List)
            {
                Console.Write(item);
                Console.Write(" - ");
            }

            Console.WriteLine();
        }

        partial void CleanCurrentListToNull(); //部分方法,partial 方法不能加其他修饰符，而且返回值只能是void

        #endregion
    }

    public partial class Limited10ItemsList<T> : List<T> //where T : new()部分类
    {
        #region  Methods

        public new void Add(T newItem)
        {
            if (_CurrentCount > MaxCount) return;
            _List.Add(newItem);
            _CurrentCount++;
        }

        public new void Remove(T oldItem)
        {
            if (_List == null || !_List.Contains(oldItem)) return;

            _List.Remove(oldItem);
            _CurrentCount--;
        }

        partial void CleanCurrentListToNull() //部分方法
        {
            _List.Clear();
        }

        #endregion
    }

}