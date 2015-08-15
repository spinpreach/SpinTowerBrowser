using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;

namespace Spinpreach.ITowerBase
{
    public static class Utilities
    {
        public static byte[] Unzip(byte[] value)
        {
            if (value == null) return new byte[0];
            if (value.Length == 0) return new byte[0];

            int offset = 2;
            var stream1 = new MemoryStream(value, offset, value.Length - offset);
            var stream2 = new MemoryStream(value.Length);
            using (var stream3 = new DeflateStream(stream1, CompressionMode.Decompress))
            {
                var buffer = new byte[0x8000];
                var count = 0;
                while ((count = stream3.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream2.Write(buffer, 0, count);
                }
            }
            return stream2.ToArray();
        }

        public static T Deserialize<T>(byte[] body)
        {
            T ret;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(Encoding.UTF8.GetString(Utilities.Unzip(body)));
            using (XmlNodeReader xnr = new XmlNodeReader(xmldoc))
            {
                var serializer = new XmlSerializer(typeof(T));
                ret = (T)serializer.Deserialize(xnr);
            }
            return ret;
        }

        public static void XmlWriter(string filename, XmlDocument doc)
        {
            try
            {
                string path = string.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), "XML");
                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
                doc.Save(string.Format(@"{0}\{1}.xml", path, filename));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public static void HistoryWriter(string filename, XmlDocument doc)
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                path = string.Format(@"{0}\{1}", path, "HISTORY");
                path = string.Format(@"{0}\{1}", path, DateTime.Now.ToString("yyyy-MM"));
                path = string.Format(@"{0}\{1}", path, DateTime.Now.ToString("yyyy-MM-dd"));
                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
                doc.Save(string.Format(@"{0}\{1}_{2}.xml", path, DateTime.Now.ToString("yyyyMMdd-HHmmss.fff"), filename));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

    }
}
