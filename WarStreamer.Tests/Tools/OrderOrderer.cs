using Xunit.Abstractions;
using Xunit.Sdk;

namespace WarStreamer.Tests.Tools
{
    public class OrderOrderer : ITestCaseOrderer
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            SortedDictionary<int, List<TTestCase>> sortedMethods = new();

            foreach (TTestCase testCase in testCases)
            {
                int order = 0;

                foreach (IAttributeInfo attr in testCase.TestMethod.Method.GetCustomAttributes(typeof(TestOrderAttribute).AssemblyQualifiedName))
                {
                    order = attr.GetNamedArgument<int>("Order");
                }

                GetOrCreate(sortedMethods, order).Add(testCase);
            }

            foreach (List<TTestCase>? list in sortedMethods.Keys.Select(priority => sortedMethods[priority]))
            {
                list.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));

                foreach (TTestCase testCase in list) yield return testCase;
            }
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            if (dictionary.TryGetValue(key, out TValue? result)) return result;

            result = new TValue();
            dictionary[key] = result;

            return result;
        }
    }
}
