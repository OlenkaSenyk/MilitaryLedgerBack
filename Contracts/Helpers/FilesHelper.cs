using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Contracts.Helpers
{
    public class FilesHelper
    {
        public static byte[] GetImageBytes(string path)
        {
            Image img = Image.FromFile(path);
            MemoryStream tmpStream = new MemoryStream();
            img.Save(tmpStream, ImageFormat.Jpeg);
            tmpStream.Seek(0, SeekOrigin.Begin);
            byte[] imgBytes = new byte[tmpStream.Length];
            tmpStream.Read(imgBytes, 0, imgBytes.Length);
            return imgBytes;
        }
    }
}
