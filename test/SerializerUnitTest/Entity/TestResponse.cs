using System;
using System.Collections.Generic;
using System.Text;

namespace SerializerUnitTest
{
    public class TestResponse
    {
        public string Field { get; set; }

        public int Field2 { get; set; }

        public long Fiedl3 { get; set; }

        public byte Field4 { get; set; }

        public DateTime Field5 { get; set; }

        public float Field6 { get; set; }

        public double Field7 { get; set; }

        public List<string> Field8 { get; set; }

        public List<int> Field9 { get; set; }

        public Child Field10 { get; set; }
    }

    public class Child
    {
        public string Field { get; set; }
    }
}
