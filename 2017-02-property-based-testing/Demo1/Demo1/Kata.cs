using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FsCheck;
using Xunit;

namespace Demo1
{
    public class Diamond
    {
        private static string Padding(int i)
        {
            return new String(' ', i);
        }

        

        public static string CreateDiamond(uint diamondSize)
        {
            if (diamondSize == 1) return "A";

            var upperHalfOfDiamond = new List<string>();

            for (int currentRow = 0; currentRow < diamondSize-1; currentRow++)
            {
                if (currentRow == 0)
                {
                    var result = Padding((int) (diamondSize - 1));
                    result += "A";
                    result += Padding((int) (diamondSize - 1)) + '\n';
                    upperHalfOfDiamond.Add(result);
                }
                else
                {
                    var formattedRow = FormattedRow(diamondSize, currentRow);
                    upperHalfOfDiamond.Add(formattedRow);
                }

            }

            var middleLine = FormattedRow(diamondSize, (int) diamondSize - 1);

            var lowerHalfofDiamond = new List<string>(upperHalfOfDiamond);
            lowerHalfofDiamond.Reverse();

            return string.Join("", upperHalfOfDiamond) + middleLine + string.Join("", lowerHalfofDiamond);
        }

        private static string FormattedRow(uint i, int currentRow)
        {
            var rowPadding = i - (currentRow + 1);
            //rowPadding = rowPadding < 0 ? 0 : rowPadding;
            var formattedRow = string.Empty;
            formattedRow += Padding((int) (rowPadding));
            formattedRow += (char) (65 + currentRow); // " B..."
            formattedRow += Padding((int) (2 * currentRow - 1));
            formattedRow += (char) (65 + currentRow); // " B B "
            formattedRow += Padding((int) (rowPadding)) + '\n';
            return formattedRow;
        }
    }

    public class DiamondTests
    {
        [Fact]
        public void ZeroSizeShouldReturnEmptyString()
        {
            var diamond = Diamond.CreateDiamond(0);
            diamond.Should().Be("");
        }

        [Fact]
        public void SizeOneShouldReturnStringA()
        {
            var diamond = Diamond.CreateDiamond(3);
            diamond.Should().Be("A");
        }

        [Fact]
        public void EachRowShouldOnlyContains_SingleCharacter_AndOr_Space()
        {
            Func<uint, bool> f = (n) =>
            {
                if (n <= 1) return true;
                var rows = Diamond.CreateDiamond(n).Split(new []{'\n'}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var row in rows)
                {
                    row.Distinct().Count().Should().Be(2);
                    row.Contains(' ').Should().BeTrue();
                }

                return true;
            };
            Prop.ForAll(f).QuickCheckThrowOnFailure();
        }

        [Fact]
        public void NumberOfRowsIsAlwaysOdd()
        {
            Func<uint, bool> f = (n) =>
            {
                if (n <= 1) return true;
                var rows = Diamond.CreateDiamond(n).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                return rows.Length % 2 == 1;
            };
            Prop.ForAll(f).QuickCheckThrowOnFailure();
        }

        [Fact]
        public void NumberOfRowsIsAlways_2n_Minus_One()
        {
            Func<uint, bool> f = (n) =>
            {
                if (n <= 1) return true;
                var rows = Diamond.CreateDiamond(n).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                return rows.Length == 2 * n -1;
            };
            Prop.ForAll(f).QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Dummy()
        {
            var s = " A";
            s.Distinct().Count().Should().Be(2);

        }
    }
}