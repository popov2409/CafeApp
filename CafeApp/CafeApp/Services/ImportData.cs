using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CafeApp.Services
{
    public class ImportData
    {
        public List<string> GetData()
        {
            List<string> result=new List<string>();
            try
            {
                using (var reader = new StreamReader("/storage/emulated/0/LIST.txt", true))
                {
                    while (!reader.EndOfStream)
                    {
                        result.Add(reader.ReadLine());
                    }
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

    }
}
