using System;
using System.Collections.Generic;
using System.Text;

namespace CafeApp.Models
{
    public class AvtomatCount
    {
        public Avtomat Avtomat { get; set; }
        public int Count { get; set; }
        public AvtomatCount(Avtomat avtomat)
        {
            Avtomat = avtomat;
            Count = 0;
        }
    }
}
