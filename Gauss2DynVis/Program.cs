using System;
using System.IO;
using System.Linq;

namespace Gauss2DynVis
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Error: illiegal parameters string.");
                Console.WriteLine();

                PrintHelp();
                return;
            }

            var @params = ParametersExtensions.CreateParameters(args.Skip(1));

            var filePath = args[0];
            using (var inputFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var iterations = new OutputFileReader().ReadFile(inputFile);

                var optimals = iterations.Where(iter => iter.IsOptimal).ToList();

                var fileName = Path.GetFileNameWithoutExtension(filePath);
                var ouputFiletemplate = Path.Combine(Path.GetDirectoryName(filePath), fileName);

                using (var file = new FileStream(Path.ChangeExtension(ouputFiletemplate, "sam"), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    new SamFileCreater(optimals, @params).WriteTo(file);
                }

                using (var file = new FileStream(Path.ChangeExtension(ouputFiletemplate, "srs"), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    new SrsFileCreater(optimals, @params).WriteTo(file);
                }
            }
        }


        static private void PrintHelp()
        {
            Console.WriteLine("Parameters string format:");
            Console.WriteLine("gauss2dynvis <input file> q1count=<int> q2count=<int> [q1inverse=(true|false)] [q2inverse=(true|false)]");

            Console.WriteLine();
            Console.WriteLine("q1count - count of steps by first axe in gaussian's file. Required.");
            Console.WriteLine("q2count - count of steps by second axe in gaussian's file. Required.");
            Console.WriteLine("q1inverse - invers first axes in dynvis' files. Default: false");
            Console.WriteLine("q2inverse - invers second axes in dynvis' files. Default: false");
            Console.WriteLine();
        }
    }
}
