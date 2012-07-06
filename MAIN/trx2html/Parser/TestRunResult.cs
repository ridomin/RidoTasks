using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Web;

namespace trx2html.Parser
{

    class ErrorInfo
    {
        string message = string.Empty;
        public ErrorInfo()
        {

        }
        public string Message 
        {
            get
            {

                return HttpUtility.HtmlEncode(message);
            }
            set
            {
                message = value;
            }
        }
        public string StackTrace { get; set; }

        public string StdOut { get; set; }

        public object StdErr { get; set; }
    }


    class TestMethodRun : IEqualityComparer<TestMethodRun>
    {
        string description = string.Empty;
        string status = string.Empty;
        public string TestClass { get; set; }
        public string TestMethodName { get; set; }
        public string Description 
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }

        }
        public string Message { get; set; }
        public TimeSpan Duration { get; set; }
        public string StackTrace { get; set; }
        public string Status { get; set; }
        public ErrorInfo ErrorInfo { get; set; }
        public string ComputerName { get; set; }

        public bool Equals(TestMethodRun x, TestMethodRun y)
        {
            return x.TestClass == y.TestClass;
        }
        
        public int GetHashCode(TestMethodRun obj)
        {
            return base.GetHashCode();
        }
        public TestMethodRun()
        {
            ErrorInfo = new ErrorInfo();
        }
    }

    class TestClassRun : I3ValueBar
    {
        string fullname;
        public TestClassRun(string name, IEnumerable<TestMethodRun> methods)
        {
            fullname = name;
            TestMethods = new List<TestMethodRun>(methods);
        }

        public string Name 
        { 
            get 
            { 
                return fullname.Substring(0, fullname.IndexOf(','));
            } 
        }

        public AssemblyName AssemblyName
        {
            get
            {
                int pos = fullname.IndexOf(',');
                string asName = fullname.Substring(pos + 1, fullname.Length - pos -1);
                return new AssemblyName(asName);
            }
        }

        public IEnumerable<TestMethodRun> TestMethods;

        public double Percent 
        { 
            get
            {
                double total = TestMethods.Count();
                double result = Math.Round((1- Failed / (Total-Ignored)) * 100, 2);
                return result;
            }
        }

        public double Success 
        {
            get
            {
                return TestMethods.Where(m => m.Status == "Passed").Count();
            }
        }

        public double Failed 
        {
            get
            {
                return TestMethods.Where(m => m.Status == "Failed").Count();
            }
        }

        public double Ignored
        {
            get
            {
                return TestMethods.Where(m => m.Status == "Inconclusive").Count();
            }
        }


        public string Status 
        { 
            get 
            {
                string status = "Failed";
                var byStatus = TestMethods.GroupBy(m=>m.Status);
                
                int failed = byStatus.Where(k => k.Key== "Failed").Count();
                int ignored = byStatus.Where(k => k.Key == "Ignored" || k.Key == "NotRunnable" || k.Key=="Aborted").Count();

                if (ignored > 0) status = "Ignored";
                if (failed == 0 && ignored==0) status = "Succeed";
                
                return status;
            } 
        }

        public TimeSpan Duration
        {
            get
            {
                return TimeSpan.FromTicks(TestMethods.Sum(m => m.Duration.Ticks));
            }
        }

        public string FullName 
        {
            get
            {
                return fullname;            
            }
        }

        public int Total 
        {
            get
            {
                return TestMethods.Count();
            }
        }

        public double PercentIgnored
        {
            get { return Math.Round(100*(Ignored/Total),0); }
        }

        public double PercentKO
        {
            get { return Math.Round(100*(Failed/Total),0); }
        }

        public double PercentOK
        {
            get { return Math.Round(100 * (Success / Total), 0); }
        }
    }

    class TestRunResult : I3ValueBar  
    {
        public TestRunResult(string name, string runUser, IEnumerable<TestMethodRun> items)
        {
            Name = name;
            UserName = runUser;
            TestMethodRunList = new List<TestMethodRun>(items);
            TestClassList = new List<TestClassRun>();
            var clases = TestMethodRunList.Distinct(new TestMethodRun()).Select(t=>t.TestClass);
            foreach (var c in clases)
            {
                TestClassList.Add(new TestClassRun(c, items.Where(t => t.TestClass == c).Select(t => t)));
            }

        }
        public IEnumerable<TestMethodRun> TestMethodRunList;
        public List<TestClassRun> TestClassList;

        public string RunInfo { get; set; }

        public int TotalMethods 
        {
            get
            {
                return TestMethodRunList.Count();
            }
        }

        public IEnumerable<string> Computers
        {
            get
            {
                return TestMethodRunList.Select(t => t.ComputerName).Distinct();
            }
        }

        public int Passed 
        {
            get
            {
                return TestMethodRunList.Where(t => t.Status == "Passed").Count();
            }
        }

        public int Failed
        {
            get
            {
                return TestMethodRunList.Where(t => t.Status == "Failed").Count();
            }
        }

        public int Inconclusive
        {
            get
            {
                return TestMethodRunList.Where(t => t.Status == "Inconclusive" || t.Status=="NotRunnable" || t.Status=="Aborted").Count();
            }
        }

        public double TotalPercent
        {
            get
            {
                if (TestClassList.Count>0)
                    return Math.Round(TestClassList.Average(c => c.Percent));
                return 0;
            }
        }

        public double PercentOK 
        {
            get
            {
                return Math.Round(100*(Passed / (double)TotalMethods), 0);
            }
        }

        public double PercentKO
        {
            get
            {
                return Math.Round(100*(Failed / (double)TotalMethods), 0);
            }
        }

        public double PercentIgnored
        {
            get
            {
                return Math.Round(100*(Inconclusive / (double)TotalMethods), 0);
            }
        }

        public TimeSpan TimeTaken
        {
            get
            {
                return TimeSpan.FromTicks(TestClassList.Sum(c => c.Duration.Ticks));
            }
        }

        public string Name { get; set; }

        public IEnumerable<AssemblyName> Assemblies
        {
            get
            {
                return TestClassList.Select(c => c.AssemblyName).Distinct(new AssemblyNameComparer());
            }
        }

        public string UserName { get; set; }

        public IEnumerable<TestMethodRun> TopSlowerMethods
        {
            get
            {
                return TestMethodRunList.OrderByDescending(m => m.Duration).Take(5);
            }
        }
    }
}
