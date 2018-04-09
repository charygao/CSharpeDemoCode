using System;
using System.Reflection;
using System.Reflection.Emit;


namespace reflection_dotnet_test
{
    public class EmitTest
    {

        // Declare delegates that can be used to execute the completed 
        // SquareIt dynamic method. The OneParameter delegate can be 
        // used to execute any method with one parameter and a return
        // value, or a method with two parameters and a return value
        // if the delegate is bound to an object.
        //
        private delegate long SquareItInvoker(int input);

        private delegate TReturn OneParameter<TReturn, TParameter0>
            (TParameter0 p0);


        public static void Test()
        {

            // Example 1: A simple dynamic method.
            //
            // Create an array that specifies the parameter types for the
            // dynamic method. In this example the only parameter is an 
            // int, so the array has only one element.
            //
            Type[] methodArgs = { typeof(int) };

            // Create a DynamicMethod. In this example the method is
            // named SquareIt. It is not necessary to give dynamic 
            // methods names. They cannot be invoked by name, and two
            // dynamic methods can have the same name. However, the 
            // name appears in calls stacks and can be useful for
            // debugging. 
            //
            // In this example the return type of the dynamic method
            // is long. The method is associated with the module that 
            // contains the Example class. Any loaded module could be
            // specified. The dynamic method is like a module-level
            // static method.
            //
            DynamicMethod squareIt = new DynamicMethod(
    "SquareIt",
    typeof(long),
    methodArgs,
    typeof(EmitTest).Module);


            // Emit the method body. In this example ILGenerator is used
            // to emit the MSIL. DynamicMethod has an associated type
            // DynamicILInfo that can be used in conjunction with 
            // unmanaged code generators.
            //
            // The MSIL loads the argument, which is an int, onto the 
            // stack, converts the int to a long, duplicates the top
            // item on the stack, and multiplies the top two items on the
            // stack. This leaves the squared number on the stack, and 
            // all the method has to do is return.
            //
            ILGenerator il = squareIt.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Conv_I8);
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Mul);
            il.Emit(OpCodes.Ret);

            // Create a delegate that represents the dynamic method. 
            // Creating the delegate completes the method, and any further 
            // attempts to change the method (for example, by adding more
            // MSIL) are ignored. The following code uses a generic 
            // delegate that can produce delegate types matching any
            // single-parameter method that has a return type.
            //
            OneParameter<long, int> invokeSquareIt =
                (OneParameter<long, int>)
                squareIt.CreateDelegate(typeof(OneParameter<long, int>));

            Console.WriteLine("2 squared = {0}",
                invokeSquareIt(2));



        }


    }
}
