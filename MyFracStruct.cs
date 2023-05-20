using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb5_1_8
{
    public struct MyFrac
    {
        public long nom;
        public long denom;

        public MyFrac(long nom_, long denom_)
        {
            nom = 0;
            denom = 0;
            long firstnom = nom_;
            long secdenom = denom_;

            if (denom_ < 0)
            {
                firstnom = -nom_;
                secdenom = -denom_;
                nom_ = -nom_;
                denom_ = -denom_;
            }

            while (firstnom != 0 && secdenom != 0)
            {
                if (firstnom > secdenom)
                {
                    firstnom = firstnom % secdenom;
                }
                else
                {
                    secdenom = secdenom % firstnom;
                }
            }

            long sum = firstnom + secdenom;

            if (sum > 0)
            {
                nom = nom_ / sum;
                denom = denom_ / sum;
            }
            else
            {
                nom = nom_;
                denom = denom_;
            }
        }

        public override string ToString()
        {
            return $"{nom}/{denom}";
        }
    }
}
