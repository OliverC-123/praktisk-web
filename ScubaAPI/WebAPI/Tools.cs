namespace WebAPI
{
    public static class Tools
    {
        // Images POST
        public static string ConvertBase64ToFile(string base64, string path)
        {
            if (string.IsNullOrEmpty(base64)) return null;

            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            if (Convert.TryFromBase64String(base64, buffer, out int bytesWritten))
            {
                var base64Array = Convert.FromBase64String(base64);

                string fileType = ".jpg"; // Array of chars char[4] fileType

                switch (base64[0])
                {
                    case 'i':
                        fileType = ".png";
                        break;

                    case 'R':
                        fileType = ".gif";
                        break;

                    case 'U':
                        fileType = ".webp";
                        break;

                    case 'P':
                        fileType = ".svg";
                        break;

                    default:
                        break;
                }

                string fileName = Guid.NewGuid() + fileType;
                string filePath = path + @"\" + fileName;

                File.WriteAllBytes(filePath, base64Array);

                return fileName;
            }

            return null;
        }

        // Images PUT
        public static bool IsBase64String(string base64)
        {
            if (string.IsNullOrEmpty(base64)) return false;

            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesWritten);



        }
    }
}