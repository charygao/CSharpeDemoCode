namespace Demo1Dll
{
    public class SwapHelper//from microsoft
    {
        #region 1.原始方法

        public static void Swap(ref int lhs, ref int rhs)
        {
            var temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public static void Swap(ref string lhs, ref string rhs)
        {
            var temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public static void Swap(ref char lhs, ref char rhs)
        {
            var temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        #endregion

        #region  2.泛型方法

        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            var temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        #endregion
    }
}