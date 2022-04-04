namespace CTF.Main
{
    public class Test
    {
        Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

        public Test()
        {
            TestMethod();
        }
        public void TestMethod()
        {
            Dictionary<string, object> keyValuePairs2 = new Dictionary<string, object>();
            keyValuePairs2.Add("bar", "foo");

            keyValuePairs.Add("john", "doe");
            keyValuePairs.Add("nestedKeyValuePairs", keyValuePairs2);

            Console.WriteLine(keyValuePairs);
        }
    }
}
