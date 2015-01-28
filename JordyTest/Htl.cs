using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTest
{
    public static class Htl
    {
        public static string AutoComplete(this string a,Action<Person> action)
        {
            Person p = new Person();
            action(p);
            return p.ToString();
        }
    }
    public class Person
    {
        public string A{get;set;}
        public string B{get;set;}
        public override string ToString()
        {
            return A + ":" + B;
        }
    }
}
