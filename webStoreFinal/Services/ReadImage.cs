using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webStoreFinal.Services
{
    public class ReadImage : IReadImage
    {
        public string ByteToImage(byte[] arr)
        {
            var base64 = Convert.ToBase64String(arr);
            return string.Format("data:image/gif;base64,{0}", base64);

        }
    }
    public interface IReadImage
    {
        string ByteToImage(byte[] arr);
    }
}
