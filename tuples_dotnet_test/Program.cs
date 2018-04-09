using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using reflection_dotnet_test;


// ref； https://docs.microsoft.com/en-us/dotnet/csharp/tuples
// Tuple VS. ValueTuple
namespace tuples_dotnet_test
{
    class Program
    {
        private static List<ToDoItem> AllItems = new List<ToDoItem>();

        static void Main(string[] args)
        {
            //解决中文输出乱码的问题
            Console.OutputEncoding = System.Text.Encoding.UTF8;//第一种方式：指定编码
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//第二种方式

            //测试reflection
            //ReflectionTest.Test();
            //EmitTest.Test();

            System.Console.WriteLine($"\r\n================元组表示一组数据=====================");
            //元组表示一组数据
            RepresentGroupedData();


            System.Console.WriteLine($"\r\n================用于单参数方法的多值传递=====================");
            //用于单参数方法的多值传递
            PassParameterViaTuple();
            PassParameterViaValueTuple();


            System.Console.WriteLine($"\r\n================值元组创建实例=====================");
            //值元组创建实例
            CreateTuple();
            CreateValueTuple();


            System.Console.WriteLine($"\r\n================返回多个值=====================");
            //返回多个值
            ReturnMultiValueViaTuple();
            ReturnMultiValueViaValueTuple();


            System.Console.WriteLine($"\r\n================序列化和反序列化=====================");
            //序列化
            TestXmlSerializer();


            System.Console.WriteLine($"\r\n================解构=====================");
            //解构和discards
            DeconstructorAndDiscards();

            System.Console.WriteLine($"\r\n================Discards（忽略/放弃）=====================");
            //test discards, ref = https://docs.microsoft.com/zh-cn/dotnet/csharp/discards
            TestDiscards();


            System.Console.WriteLine($"\r\n================关于ValueTuple元素的名称=====================");
            //unnamed tuple
            var unnamed1 = ("one", "two");
            System.Console.WriteLine($"unnamed tuple: {unnamed1.Item1}, {unnamed1.Item2}");

            //named tuple,These name are handled by the compiler,the compiler replaces the names you've defined with Item* equivalents when generating the compiled output. The compiled Microsoft Intermediate Language (MSIL) does not include the names you've given these elements.
            var named1 = (first: "one", second: "two");
            System.Console.WriteLine($"named tuple, via item: {named1.Item1}, {named1.Item2}");
            System.Console.WriteLine($"named tuple, via name: {named1.first}, {named1.second}");

            //named tuple 
            var sum = 12.5;
            var count = 5;
            var accumulation = (count, sum);
            System.Console.WriteLine($"named tuple, via name: {accumulation.sum}, {accumulation.count}");

            var localVariableOne = 5;
            var localVariableTwo = "some text";
            var tuple = (explicitFieldOne: localVariableOne, explicitFieldTwo: localVariableTwo);
            System.Console.WriteLine($"named tuple, via name: {tuple.explicitFieldOne}, {tuple.explicitFieldTwo}");

            //For any field where an explicit name is not provided, an applicable implicit name will be projected.
            var stringContent = "The answer to everything";
            var mixedTuple = (42, stringContent);
            System.Console.WriteLine($"named tuple, via name: {mixedTuple.Item1}, {mixedTuple.stringContent}");

            System.Console.WriteLine($"\r\n================关于ValueTuple元素的命名规范=====================");
            //不正确的名称命名
            //There are two conditions where candidate field names are not projected onto the tuple field: 
            //（1）When the candidate name is a reserved tuple name. Examples include Item3, ToString or Rest.
            //（2）When the candidate name is a duplicate of another tuple field name, either explicit or implicit.
            // These conditions avoid ambiguity. These names would cause an ambiguity if they were used as the field names for a field in a tuple. 
            var ToString = "This is some text";
            var one = 1;
            var Item1 = 5;
            var projections = (ToString, one, Item1);
            // Accessing the first field:
            Console.WriteLine(projections.Item1);// There is no semantic name 'ToString'

            // Accessing the second field:
            Console.WriteLine(projections.one);
            Console.WriteLine(projections.Item2);

            // Accessing the third field:
            Console.WriteLine(projections.Item3);
            // There is no semantic name 'Item1`.

            var pt1 = (X: 3, Y: 0);
            var pt2 = (X: 3, Y: 4);
            var xCoords = (pt1.X, pt2.X);  // There are no semantic names for the fields of xCoords. 

            // Accessing the first field:
            Console.WriteLine(xCoords.Item1);
            // Accessing the second field:
            Console.WriteLine(xCoords.Item2);


            System.Console.WriteLine($"\r\n================相互赋值，兼容性=====================");
            /// 相互赋值，兼容性
            // The 'arity' and 'shape' of all these tuples are compatible. 
            // The only difference is the field names being used.
            // *** Notice that the names of the tuples are not assigned. The values of the elements are assigned following the order of the elements in the tuple.
            var unnamed = (42, "The meaning of life");
            var anonymous = (16, "a perfect square");
            var named = (Answer: 42, Message: "The meaning of life");
            var differentNamed = (SecretConstant: 42, Label: "The meaning of life");

            unnamed = named;
            named = unnamed;
            // 'named' still has fields that can be referred to
            // as 'answer', and 'message':
            Console.WriteLine($"{named.Answer}, {named.Message}");

            // unnamed to unnamed:
            anonymous = unnamed;
            Console.WriteLine($"{anonymous.Item1}, {anonymous.Item2}");

            // named tuples.
            named = differentNamed;

            // The field names are not assigned. 'named' still has 
            // fields that can be referred to as 'answer' and 'message':
            Console.WriteLine($"{named.Answer}, {named.Message}");

            // With implicit conversions:
            // int can be implicitly converted to long
            (long, string) conversion = named;
            Console.WriteLine($"implicit conversions: {conversion.Item1}, {conversion.Item2}");


            //****Tuples of different types or numbers of elements are not assignable:
            // Does not compile.
            // CS0029: Cannot assign Tuple(int,int,int) to Tuple(int, string)
            var differentShape = (1, 2, 3);
            //named = differentShape;

            System.Console.WriteLine($"\r\n================Linq and Tuple=====================");
            //***** Linq and Tuple
            for (int i = 0; i < 10; i++)
            {
                AllItems.Add(new ToDoItem
                {
                    ID = i,
                    IsDone = (i % 2 == 0),
                    DueDate = DateTime.Now.AddDays(-1 * i),
                    Title = $"test_todoitem_title_{i}",
                    Notes = $"remark_balabala_{i}"
                });
            }
            var undoneList = GetCurrentItemsMobileList();
            foreach (var item in undoneList)
            {
                System.Console.WriteLine($"ToDoItem: ID = {item.ID}, Title = {item.Title}");
            }

        }



        #region

        //创建元组实例（Tuple)
        private static void CreateTuple()
        {

            //创建元组：构造函数 
            var testTuple6 = new Tuple<int, int, int, int, int, int>(1, 2, 3, 4, 5, 6);
            Console.WriteLine($"Item 1: {testTuple6.Item1}, Item 6: {testTuple6.Item6}");

            var testTuple10 = new Tuple<int, int, int, int, int, int, int, Tuple<int, int, int>>(1, 2, 3, 4, 5, 6, 7, new Tuple<int, int, int>(8, 9, 10));
            Console.WriteLine($"Item 1: {testTuple10.Item1}, Item 10: {testTuple10.Rest.Item3}");

            //创建值元组：Tuple静态方法创建元组
            testTuple6 = Tuple.Create<int, int, int, int, int, int>(1, 2, 3, 4, 5, 6);
            Console.WriteLine($"Item 1: {testTuple6.Item1}, Item 6: {testTuple6.Item6}");

            var testTuple8 = Tuple.Create<int, int, int, int, int, int, int, int>(1, 2, 3, 4, 5, 6, 7, 8);
            Console.WriteLine($"Item 1: {testTuple8.Item1}, Item 8: {testTuple8.Rest.Item1}");
            //这里构建出来的Tuple类型其实是Tuple<int, int, int, int, int, int, int, Tuple<int>>，因此testTuple8.Rest取到的数据类型是Tuple<int>，因此要想获取准确值需要取Item1属性

        }


        // 创建元组实例（ValueTuple）
        private static void CreateValueTuple()
        {
            //创建值元组：构造函数 
            var testTuple6 = new ValueTuple<int, int, int, int, int, int>(1, 2, 3, 4, 5, 6);
            Console.WriteLine($"Item 1: {testTuple6.Item1}, Item 6: {testTuple6.Item6}");

            var testTuple10 = new ValueTuple<int, int, int, int, int, int, int, ValueTuple<int, int, int>>(1, 2, 3, 4, 5, 6, 7, new ValueTuple<int, int, int>(8, 9, 10));
            Console.WriteLine($"Item 1: {testTuple10.Item1}, Item 10 via Rest.Item3: {testTuple10.Rest.Item3}, Item 10 via Item10: {testTuple10.Item10}");

            //创建值元组：ValueTuple静态方法创建元组
            testTuple6 = ValueTuple.Create<int, int, int, int, int, int>(1, 2, 3, 4, 5, 6);
            Console.WriteLine($"Item 1: {testTuple6.Item1}, Item 6: {testTuple6.Item6}");

            var testTuple8 = ValueTuple.Create<int, int, int, int, int, int, int, int>(1, 2, 3, 4, 5, 6, 7, 8);
            Console.WriteLine($"Item 1: {testTuple8.Item1}, Item 8: {testTuple8.Rest.Item1}");

            //新语法
            var t = (firstName: "firstName", lastName: "lastName");
            Console.WriteLine($"Hello, {t.firstName}!");
        }

        //一个参数是传递多个值(Tuple)
        private static void WriteStudentInfo1(Object student)
        {
            var studentInfo = student as Tuple<string, int, uint>;
            Console.WriteLine($"Student Information: Name [{studentInfo.Item1}], Age [{studentInfo.Item2}], Height [{studentInfo.Item3}]");
        }

        private static void PassParameterViaTuple()
        {
            var t = new Thread(new ParameterizedThreadStart(WriteStudentInfo1));
            t.Start(new Tuple<string, int, uint>("Bob", 28, 175));
            while (t.IsAlive)
            {
                Thread.Sleep(50);
            }
        }


        //一个参数是传递多个值(ValueTuple)
        private static void WriteStudentInfo2(Object student)
        {
            //var studentInfo = student as (string, int, uint);//error
            var studentInfo = ((string, int, int))student;//ok
            Console.WriteLine($"Student Information: Name [{studentInfo.Item1}], Age [{studentInfo.Item2}], Height [{studentInfo.Item3}]");
        }

        private static void PassParameterViaValueTuple()
        {
            var t = new Thread(new ParameterizedThreadStart(WriteStudentInfo2));
            t.Start(("Bob", 28, 175));
            while (t.IsAlive)
            {
                Thread.Sleep(50);
            }
        }


        //返回多个值（Tuple）
        static Tuple<string, int, uint> GetStudentInfo1(string name)
        {
            return new Tuple<string, int, uint>("Bob", 28, 175);
        }

        static void ReturnMultiValueViaTuple()
        {
            var studentInfo = GetStudentInfo1("Bob");
            Console.WriteLine($"Student Information: Name [{studentInfo.Item1}], Age [{studentInfo.Item2}], Height [{studentInfo.Item3}]");
        }

        //返回多个值（ValueTuple）
        private static (string, int, uint) GetStudentInfo2(string name)
        {
            return ("Bob", 28, 175); //新语法
        }

        // It's recommended that you provide semantic names to the elements of tuples returned from methods.
        private static (string name, int age, uint height) GetStudentInfo3(string name)
        {
            return ("Bob", 28, 175);
        }

        private static void ReturnMultiValueViaValueTuple()
        {
            var studentInfo = GetStudentInfo2("Bob");
            Console.WriteLine($"Student Information: Name [{studentInfo.Item1}], Age [{studentInfo.Item2}], Height [{studentInfo.Item3}]");

            var studentInfo2 = GetStudentInfo3("Bob");
            Console.WriteLine($"Student Information: Name [{studentInfo2.name}], Age [{studentInfo2.age}], Height [{studentInfo2.height}]");

            var result = GetCityAndState();
            System.Console.WriteLine($"City is {result.city}, State is {result.state}");
        }


        //解构
        private static void DeconstructorAndDiscards()
        {
            /*
            You can unpackage all the items in a tuple by deconstructing the tuple returned by a method. There are two different approaches to deconstructing tuples. 
            (1) you can explicitly declare the type of each field inside parentheses to create discrete variables for each of the elements in the tuple. 
            (2) You can also declare implicitly typed variables for each field in a tuple by using the var keyword outside the parentheses.
            */

            // 使用两个明确定义的返回值
            (string city1, string state1) = GetCityAndState();

            // 使用var而不是现实类型定义
            (var city2, var state2) = GetCityAndState();

            // 对多个变量使用单个var声明
            var (city3, state3) = GetCityAndState();

            // 使用现有变量
            (city3, state3) = GetCityAndState();


            //Deconstructing user defined types
            var p = new Person("Althea", "Goodwin");
            var (first, last) = p;

            var s1 = new Student("Cary", "Totten", 4.5);
            var (fName, lName, gpa) = s1;
        }


        //测试忽略
        private static void TestDiscards()
        {
            //忽略元组中部分元素
            var (_, age, _) = GetStudentInfo3("Bob");
            Console.WriteLine($"Student Information: Age [{age}]");

            var (city, _) = GetCityAndState();
            System.Console.WriteLine($"test discards: city is {city}");

            // //放弃模式可通过 is 和 switch 关键字用于模式匹配。 每个表达式始终匹配放弃模式。
            // object[] objects = { CultureInfo.CurrentCulture,
            //                CultureInfo.CurrentCulture.DateTimeFormat,
            //                CultureInfo.CurrentCulture.NumberFormat,
            //                new ArgumentException(), null };
            // foreach (var obj in objects)
            //     ProvidesFormatInfo(obj);
        }

        //元组序列化成xml字符串
        private static void TestXmlSerializer()
        {
            var tempValueTuple = GetStudentInfo3("Bob");

            //序列化
            StringBuilder output = new StringBuilder();
            using (StringWriter writer = new StringWriter(output))
            {
                XmlSerializer xs = new XmlSerializer(typeof(ValueTuple<string, int, uint>));
                xs.Serialize(writer, tempValueTuple);
            }
            Console.WriteLine(output.ToString());

            //反序列化
            var serializer = new XmlSerializer(typeof(ValueTuple<string, int, uint>));
            using (var s = new StringReader(output.ToString()))
            {
                var (name, age, height) = (ValueTuple<string, int, uint>)serializer.Deserialize(s);
                Console.WriteLine($"反序列化后结果：{name}_{age}_{height}");
            }
        }



        #endregion

        //表示一组数据
        private static void RepresentGroupedData()
        {
            //Tuple表示一组数据
            var studentInfo1 = Tuple.Create<string, int, uint>("Bob", 28, 175);
            Console.WriteLine($"Student Information: Name [{studentInfo1.Item1}], Age [{studentInfo1.Item2}], Height [{studentInfo1.Item3}]");
            //studentInfo1.Item1 = "zzw"; //read-only

            //ValueTuple表示一组数据
            var studentInfo2 = ValueTuple.Create<string, int, uint>("Bob", 28, 175);
            Console.WriteLine($"Student Information: Name [{studentInfo2.Item1}], Age [{studentInfo2.Item2}], Height [{studentInfo2.Item3}]");
            studentInfo2.Item1 = "zzw"; //not editable
            Console.WriteLine($"Student Information(after modified): Name [{studentInfo2.Item1}], Age [{studentInfo2.Item2}], Height [{studentInfo2.Item3}]");


            var valuetuple = ("1", "2", "3", "4", "5", "6", "7", "8", "9", "10");
            Console.WriteLine($"test valuetuple: item1={valuetuple.Item1}, item10={valuetuple.Item10}");
        }


        private static void ProvidesFormatInfo(object obj)
        {
            if (obj is IFormatProvider fmt)
                Console.WriteLine($"{fmt} object");
            else if (obj is null)
            {
                Console.Write("A null object reference: ");
                Console.WriteLine("Its use could result in a NullReferenceException");
            }
            else if (obj is var _) //表示其他任何类型
                Console.WriteLine($"{obj.GetType()}: Some object type without format information");
        }


        public static (string city, string state) GetCityAndState()
        {
            return ("Lake Charles", "Louisiana");
        }

        private static IEnumerable<(int ID, string Title)> GetCurrentItemsMobileList()
        {
            return from item in AllItems
                   where !item.IsDone
                   orderby item.DueDate
                   select (item.ID, item.Title);
        }

    }


    public class ToDoItem
    {
        public int ID { get; set; }
        public bool IsDone { get; set; }
        public DateTime DueDate { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
    }

    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Person(string first, string last)
        {
            FirstName = first;
            LastName = last;
        }

        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }
    }

    public class Student : Person
    {
        public double GPA { get; }
        public Student(string first, string last, double gpa) :
            base(first, last)
        {
            GPA = gpa;
        }
    }

    public static class Extensions
    {
        public static void Deconstruct(this Student s, out string first, out string last, out double gpa)
        {
            first = s.FirstName;
            last = s.LastName;
            gpa = s.GPA;
        }
    }
}
