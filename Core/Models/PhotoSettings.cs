using System.IO;
using System.Linq;

namespace Fire.Core.Models
{
    public class PhotoSettings
    {
        public int MAX_SIZES { get; set; }
        public string[] FILE_TYPES { get; set; }

        public bool IsSupport(string fileName){
            return FILE_TYPES.Any(s => s == Path.GetExtension(fileName).ToLower());
        }
    }
}