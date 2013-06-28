using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gauss2DynVis
{
    public class Parameters
    {
        public int Q1Count { get; set; }
        public int Q2Count { get; set; }

        public bool Q1Inverse { get; set; }
        public bool Q2Inverse { get; set; }
    }

    public static class ParametersExtensions
    {
        private static readonly Regex ParamRegex = new Regex(@"(?<name>\w+)=(?<val>\w+)");

        private static T GetValue<T>(this Dictionary<string, string> dic, string name, T def = default(T))
        {
            string val;
            if (dic.TryGetValue(name.ToLower(), out val))
            {
                return ParseVal<T>(val);
            }
            return def;
        }

        private static T ParseVal<T>(string val)
        {
            try
            {
                return (T) Convert.ChangeType(val, typeof (T));
            }
            catch
            {
                throw new Exception(String.Format("Cannot convert '{0}' value to type '{1}'", val, typeof(T).Name));
            }
        }

        private static Dictionary<string, string> ToParamsDictionary(this IEnumerable<string> args)
        {
            return
                args.Select(arg => ParamRegex.Match(arg))
                    .Where(match => match.Success)
                    .ToDictionary(match => match.Groups["name"].Value.ToLower(), match => match.Groups["val"].Value);
        }

        public static Parameters CreateParameters(IEnumerable<string> args)
        {
            var param = new Parameters();

            var dic = args.ToParamsDictionary();

            param.Q1Count = dic.GetValue("Q1Count", param.Q1Count);
            param.Q2Count = dic.GetValue("Q2Count", param.Q2Count);

            param.Q1Inverse = dic.GetValue("Q1Inverse", param.Q1Inverse);
            param.Q2Inverse = dic.GetValue("Q2Inverse", param.Q2Inverse);

            return param;
        }

        public static void ParamsIterator(this Parameters param, Action<int> action)
        {
            for (int t2 = 0; t2 < param.Q2Count; t2++)
            {
                var q2 = param.Q1Inverse ? param.Q2Count - t2 - 1 : t2;
                bool isEven = q2 % 2 == 0;
                for (int t1 = 0; t1 < param.Q1Count; t1++)
                {
                    var q1 = param.Q2Inverse ? param.Q1Count - t1 - 1 : t1;

                    var i = q2 * param.Q1Count + (isEven ? q1 : (param.Q1Count - q1 - 1));

                    action(i);
                }
            }
        }
    }
}
