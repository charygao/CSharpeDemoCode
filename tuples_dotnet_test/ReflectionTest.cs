using System;
using System.Reflection;
using System.Reflection.Emit;


namespace reflection_dotnet_test
{

    public class ExampleAttribute : Attribute
    {
        private string stringVal;

        public ExampleAttribute()
        {
            stringVal = "This is the default string.";
        }

        public string StringValue
        {
            get { return stringVal; }
            set { stringVal = value; }
        }
    }

    [Example(StringValue = "This is a string.")]
    public class ReflectionTest
    {
        public static void Test()
        {
            //获取程序集和程序集内部的所有类型
            // Gets the mscorlib assembly in which the object is defined.
            Assembly a = typeof(object).Module.Assembly;
            // Gets the type names from the assembly.
            Type[] types = a.GetTypes();
            foreach (Type tt in types)
            {
                Console.WriteLine(tt.FullName);
            }

            //获取类型的构造函数
            Type type = typeof(System.String);
            Console.WriteLine("Listing all the public constructors of the {0} type", type);
            // Constructors.
            ConstructorInfo[] ci = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            Console.WriteLine("//Constructors");
            PrintMembers(ci);



            //获取类型的Members
            Console.WriteLine("\nReflection.MemberInfo");
            // Gets the Type and MemberInfo.
            Type MyType = Type.GetType("System.IO.File");
            MemberInfo[] Mymemberinfoarray = MyType.GetMembers();
            // Gets and displays the DeclaringType method.
            Console.WriteLine("\nThere are {0} members in {1}.",
                Mymemberinfoarray.Length, MyType.FullName);
            if (MyType.IsPublic)
            {
                Console.WriteLine("{0} is public.", MyType.FullName);
            }


            //输出类型的成员
            // Specifies the class.
            Type t = typeof(System.IO.BufferedStream);
            Console.WriteLine("Listing all the members (public and non public) of the {0} type", t);

            // Lists static fields first.
            FieldInfo[] fi = t.GetFields(BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public);

            Console.WriteLine("// Static Fields");
            PrintMembers(fi);

            // Static properties.
            PropertyInfo[] pi = t.GetProperties(BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("// Static Properties");
            PrintMembers(pi);

            // Static events.
            EventInfo[] ei = t.GetEvents(BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("// Static Events");
            PrintMembers(ei);

            // Static methods.
            MethodInfo[] mi = t.GetMethods(BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("// Static Methods");
            PrintMembers(mi);

            // Constructors.
            ConstructorInfo[] cinfo = t.GetConstructors(BindingFlags.Instance |
                BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("// Constructors");
            PrintMembers(cinfo);

            // Instance fields.
            fi = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public);
            Console.WriteLine("// Instance Fields");
            PrintMembers(fi);

            // Instance properites.
            pi = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public);
            Console.WriteLine("// Instance Properties");
            PrintMembers(pi);

            // Instance events.
            ei = t.GetEvents(BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public);
            Console.WriteLine("// Instance Events");
            PrintMembers(ei);

            // Instance methods.
            mi = t.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic
                | BindingFlags.Public);
            Console.WriteLine("// Instance Methods");
            PrintMembers(mi);


            //获取自定义的Attribute
            System.Reflection.MemberInfo info = typeof(ReflectionTest);
            foreach (object attrib in info.GetCustomAttributes(true))
            {
                Console.WriteLine(attrib);
            }
        }

        public static void PrintMembers(MemberInfo[] ms)
        {
            foreach (MemberInfo m in ms)
            {
                Console.WriteLine("{0}{1}", "     ", m);
            }
            Console.WriteLine();
        }
    }
}
