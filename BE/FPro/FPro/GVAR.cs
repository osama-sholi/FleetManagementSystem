using System.Collections.Concurrent;
using System.Data;


namespace FPro
{
    public class GVAR
    {
        public ConcurrentDictionary<string, ConcurrentDictionary<string, string>> DicOfDic { get; set; } = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();

        public ConcurrentDictionary<string, DataTable> DicOfDT { get; set; } = new ConcurrentDictionary<string, DataTable>();
    }
}
