namespace Subble.Core.Func
{
    public static class BaseType
    {
        /// <summary>
        /// Funciton that do nothing
        /// </summary>
        public static void Void() { }
        public static void Void<T>(T _) { }

        /// <summary>
        /// Type without value
        /// </summary>
        /// <returns></returns>
        public static Unit Unit()
            => new Unit();
    }

    public struct Unit
    {
    }
}
