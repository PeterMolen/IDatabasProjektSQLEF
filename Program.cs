using IDatabasProjektSQLEF.Models;

namespace IDatabasProjektSQLEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HogwartzContext run = new HogwartzContext();
            run.RunTheShow();
        }
    }
}
