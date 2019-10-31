using Infrastructure.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace InfrastructureTest
{
    [TestClass]
    public class InfrastructureCommonTest
    {
        [TestMethod]
        public void Test_IsValidDomain_RegexValidCases()
        {
            bool result0 = Validations.IsValidDomain("google.com");
            bool result4 = Validations.IsValidDomain("555.123.4567");
            bool result5 = Validations.IsValidDomain("foodemo.net");
            bool result6 = Validations.IsValidDomain("bar.ba.test.co.uk");
            bool result10 = Validations.IsValidDomain("g.com");
            bool result14 = Validations.IsValidDomain("xn--d1ai6ai.xn--p1ai");
            bool result15 = Validations.IsValidDomain("xn-fsqu00a.xn-0zwm56d");
            bool result16 = Validations.IsValidDomain("xn--stackoverflow.com");
            bool result17 = Validations.IsValidDomain("stackoverflow.xn--com");
            bool result18 = Validations.IsValidDomain("stackoverflow.co.uk");
            bool result19 = Validations.IsValidDomain("google.com.au");

            var valids = new bool[] { result0, result4, result5, result6, result10, result14, result15, result16, result17, result18, result19 };

            Assert.IsTrue(valids.All(x => x));
        }

        [TestMethod]
        public void Test_IsValidDomain_RegexInvalidCases()
        {
            bool result1 = Validations.IsValidDomain("abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            bool result2 = Validations.IsValidDomain("0123456789 +-.,!@#$%^&*();\\/|<>\"\'");
            bool result3 = Validations.IsValidDomain("12345 -98.7 3.141 .6180 9,000 +42");
            bool result7 = Validations.IsValidDomain("www.demo.com");
            bool result8 = Validations.IsValidDomain("http://foo.co.uk/");
            bool result9 = Validations.IsValidDomain("http://regexr.com/foo.html?q=bar");
            bool result11 = Validations.IsValidDomain("g-.com");
            bool result12 = Validations.IsValidDomain("com.g");
            bool result13 = Validations.IsValidDomain("=-g.com");

            var invalids = new bool[] { result1, result2, result3, result7, result8, result9, result11, result12, result13 };

            Assert.IsFalse(invalids.All(x => x));
        }

        [TestMethod]
        public void Test_IsValidDomain_TooLarge()
        {
            string domain = @"themostlargedomaineverthemostlargedomaineverth
                    emostlargedomaineverthemostlargedomaineverthemostlargedo
                    maineverthemostlargedomaineverthemostlargedomaineverthem
                    ostlargedomaineverthemostlargedomaineverthemostlargedoma
                    inever.themostlargedomainever.themostlargedomainever.com";

            bool result = Validations.IsValidDomain(domain);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_IsValidDomain_InvalidLeadingContent()
        {
            bool http = Validations.IsValidDomain(@"http://themostlargedomainever.com");
            bool https = Validations.IsValidDomain(@"https://themostlargedomainever.com");
            bool httpsWww = Validations.IsValidDomain(@"https://www.themostlargedomainever.com");
            bool httpWww = Validations.IsValidDomain(@"http://www.themostlargedomainever.com");
            bool www = Validations.IsValidDomain(@"www.themostlargedomainever.com");

            var invalids = new bool[] { http, https, httpsWww, httpWww, www };

            Assert.IsFalse(invalids.All(x => x));
        }

        [TestMethod]
        public void Test_IsValidDomain_InvalidLargeParts()
        {
            bool result1 = Validations.IsValidDomain("themostlargedomaineverthemostlargedomaineverthemostlarge12345.com");
            bool result2 = Validations.IsValidDomain(@"themostlargedomaineverthemostlargedomaineverthemostlarge.themostlargedomaineverthemostlargedomaineverthemostlargeasdasdasdasd.com");
            bool result3 = Validations.IsValidDomain(@"themostlargedomaineverthemostlargedomaineverthemostlarge.themostlargedomaineverthemostlargedomaineverthemostlarge.themostlargedomaineverthemostlargedomaineverthemostlarge123456789.au");
            
            var invalids = new bool[] { result1, result2, result3 };

            Assert.IsFalse(invalids.All(x => x));
        }
    }
}
