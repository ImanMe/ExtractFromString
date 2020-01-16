using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StringDeficultDemo
{
    class Program
    {
        static void Main()
        {
            var sampleStirng =
                @"+COUNTRY=CA+STATE=NT+STATE=NU+STATE=YT+STCNTY=MBR0A+STCNTY=MBR0B+STCNTY=MBR0C+STCNTY=MBR0E+STCNTY=MBR0G+STCNTY=MBR0H+STCNTY=MBR0J+STCNTY=MBR0K+STCNTY=MBR0L+STCNTY=MBR0M+STCNTY=MBR1A+STCNTY=MBR1B+STCNTY=MBR1C+STCNTY=MBR1N+STCNTY=MBR2C+STCNTY=MBR2E+STCNTY=MBR2G+STCNTY=MBR2H+STCNTY=MBR2J+STCNTY=MBR2K+STCNTY=MBR2L+STCNTY=MBR2M+STCNTY=MBR2N+STCNTY=MBR2P+STCNTY=MBR2R+STCNTY=MBR2V+STCNTY=MBR2W+STCNTY=MBR2X+STCNTY=MBR2Y+STCNTY=MBR3A+STCNTY=MBR3B+STCNTY=MBR3C+STCNTY=MBR3E+STCNTY=MBR3G+STCNTY=MBR3H+STCNTY=MBR3J+STCNTY=MBR3K+STCNTY=MBR3L+STCNTY=MBR3M+STCNTY=MBR3N+STCNTY=MBR3P+STCNTY=MBR3R+STCNTY=MBR3S+STCNTY=MBR3T+STCNTY=MBR3V+STCNTY=MBR3W+STCNTY=MBR3X+STCNTY=MBR3Y+STCNTY=MBR4A+STCNTY=MBR4G+STCNTY=MBR4H+STCNTY=MBR4J+STCNTY=MBR4K+STCNTY=MBR4L+STCNTY=MBR5A+STCNTY=MBR5G+STCNTY=MBR5H+STCNTY=MBR6M+STCNTY=MBR6W+STCNTY=MBR7A+STCNTY=MBR7B+STCNTY=MBR7C+STCNTY=MBR7N+STCNTY=MBR8A+STCNTY=MBR8N+STCNTY=MBR9A+STATE=AB+STATE=BC+STATE=SK-STCNTY=ABT3T-STCNTY=ABT6Y-STCNTY=BCV8E-STCNTY=SKS4K-STCNTY=SKS7A-STCNTY=SKS7B-STCNTY=SKS7C";

            var broken = sampleStirng.Split("+");

            var parentList = new List<Parent>();
            
            var allStates = sampleStirng.Split("+STATE=").Skip(1);
            foreach (var state in allStates)
            {
                if (state.Length >= 2)
                {
                    var parent = new Parent { ProvinceCode = state.Substring(0, 2) };
                    parentList.Add(parent);
                }               
            }

            var allStatesAndZipsIncluded = sampleStirng.Split("+STCNTY=").Skip(1);

            foreach (var statesAndZip in allStatesAndZipsIncluded)
            {
                if(statesAndZip.Length < 5) continue;
                var provinceCode = statesAndZip.Substring(0, 2);
                var postalCode = statesAndZip.Substring(2, 3);

                var parentByProvinceCode = parentList.FirstOrDefault(p => p.ProvinceCode == provinceCode);
                if (parentByProvinceCode == null)
                {
                    var parent = new Parent {ProvinceCode = provinceCode};
                    parent.Children.Add(new Child(){PostalCode = postalCode, Rule = "Add"});
                    parentList.Add(parent);
                }
                else
                {
                    parentByProvinceCode.Children.Add(new Child(){PostalCode = postalCode, Rule = "Add"});
                }
            }

            var allStatesAndZipsExcluded = sampleStirng.Split("-STCNTY=").Skip(1);

            foreach (var statesAndZip in allStatesAndZipsExcluded)
            {
                if (statesAndZip.Length < 5) continue;
                var provinceCode = statesAndZip.Substring(0, 2);
                var postalCode = statesAndZip.Substring(2, 3);

                var parentByProvinceCode = parentList.FirstOrDefault(p => p.ProvinceCode == provinceCode);
                if (parentByProvinceCode == null)
                {
                    var parent = new Parent { ProvinceCode = provinceCode };
                    parent.Children.Add(new Child() { PostalCode = postalCode, Rule = "Remove"});
                    parentList.Add(parent);
                }
                else
                {
                    parentByProvinceCode.Children.Add(new Child() { PostalCode = postalCode, Rule = "Remove"});
                }
            }           

            Console.WriteLine(sampleStirng);
        }
    }
}
