using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gauss2DynVis
{
    public class Geometry : List<AtomCoord>
    {
        public static Geometry CreateFormString(string str, int lineCount = 0)
        {
            return CreateFormText(new StringReader(str), lineCount);
        }

        public static Geometry CreateFormText(TextReader reader, int lineCount = 0)
        {
            var result = new Geometry();
            do
            {
                var line = reader.ReadLine();
                AtomCoord atom = AtomCoord.CreateFormString(line);

                if (atom != null)
                {
                    result.Add(atom);
                }
                else
                {
                    break;
                }
            } while (lineCount <= 0 || result.Count < lineCount);

            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var atom in this)
            {
                sb.AppendLine(atom.ToString());
            }
            return sb.ToString();
        }
    }
}
