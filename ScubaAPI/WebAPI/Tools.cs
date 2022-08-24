namespace ScubaAPI
{
    public static class Tools
    {
        public static string ConvertBase64tofile(string Base64, string path)
        {
            if (string.IsNullOrEmpty(Base64)) return null;

            Span<byte> buffer = new Span<byte>(new byte[Base64.Length]);
            if (Convert.TryFromBase64String(Base64, buffer, out int bytesWritten))
            {
                var base64Array = Convert.FromBase64String(Base64);
                string fileType = ".jpg";

                switch (Base64[0])
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
                    default:
                        break;
                }
                string fileName = Guid.NewGuid() + fileType;
                string filePath = path + fileName;

                File.WriteAllBytes(filePath, base64Array);

                return fileName;

            }
            return null;
        }
        public static bool IsBase64String(string Base64)
        {
            if (string.IsNullOrEmpty(Base64)) return false;
            Span<byte> buffer = new Span<byte>(new byte[Base64.Length]);
            return Convert.TryFromBase64String(Base64, buffer, out int bytesWritten);
        }
    }

}
