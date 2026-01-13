using NUnitLite;
using System.Reflection;

namespace Alkamous.Tests
{
    class Program
    {
        static int Main(string[] args)
        {
            return new AutoRun(Assembly.GetExecutingAssembly()).Execute(args);
        }
    }
}
