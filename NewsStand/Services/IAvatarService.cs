using System.Drawing;
using System.Windows.Media.Imaging;

namespace NewsStand.Services
{
    public interface IAvatarService
    {
        BitmapImage LoadAvatar(string url);
    }
}