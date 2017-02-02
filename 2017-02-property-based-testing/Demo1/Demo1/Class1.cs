using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FsCheck;
using Xunit;

namespace Demo1
{
    public class Class1
    {
        [Fact]
        public void SomeTest()
        {
            Func<int[], bool> revRevIsOrig = xs => xs.Reverse().SequenceEqual(xs);
            Prop.ForAll(revRevIsOrig).QuickCheckThrowOnFailure();
        }


        public int Add3(int i)
        {
            return i+3;
            if (i == 0) return 3;
            if (i == 1) return 4;
            return 99;
        }

        [Fact]
        public void SomeOtherTest()
        {
            Func<int, bool> f = n => Add3(n) == n + 3;
            Prop.ForAll(f).QuickCheckThrowOnFailure();

        }
    }
}
