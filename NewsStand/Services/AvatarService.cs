using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace NewsStand.Services
{
    public class AvatarService : IAvatarService
    {
        private Dictionary<string, BitmapImage> cachedImages;

        public AvatarService()
        {
            cachedImages = new Dictionary<string, BitmapImage>();
        }

        public BitmapImage LoadAvatar(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            if (cachedImages.ContainsKey(url))
                return cachedImages[url];

            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = httpWebReponse.GetResponseStream();
            if (stream == null)
                return null;

            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            cachedImages.Add(url, image);
            return image;
        }
    }
}
