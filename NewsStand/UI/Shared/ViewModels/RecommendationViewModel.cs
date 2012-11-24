using System;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Microsoft.Practices.ServiceLocation;
using NewsStand.Infrastructure;
using NewsStand.Model;
using NewsStand.Services;

namespace NewsStand.UI.Shared.ViewModels
{
    public class RecommendationViewModel : PropertyChangedBase
    {
        private string quote;

        public string Quote
        {
            get { return quote; }
            set
            {
                if (quote == value)
                    return;
                quote = value;
                NotifyOfPropertyChange(() => Quote);
            }
        }

        private DateTime created;

        public DateTime Created
        {
            get { return created; }
            set
            {
                if (created == value)
                    return;
                created = value;
                NotifyOfPropertyChange(() => Created);
            }
        }

        private User user;

        public User User
        {
            get { return user; }
            set
            {
                if (user == value)
                    return;
                user = value;
                NotifyOfPropertyChange(() => User);
            }
        }

        private string url;

        public string Url
        {
            get { return url; }
            set
            {
                if (url == value)
                    return;
                url = value;
                NotifyOfPropertyChange(() => Url);
            }
        }

        private string articleTitle;

        public string ArticleTitle
        {
            get { return articleTitle; }
            set
            {
                if (articleTitle == value)
                    return;
                articleTitle = value;
                NotifyOfPropertyChange(() => ArticleTitle);
            }
        }

        private string websiteTitle;

        public string WebsiteTitle
        {
            get { return websiteTitle; }
            set
            {
                if (websiteTitle == value)
                    return;
                websiteTitle = value;
                NotifyOfPropertyChange(() => WebsiteTitle);
            }
        }

        private string websiteUrl;

        public string WebsiteUrl
        {
            get { return websiteUrl; }
            set
            {
                if (websiteUrl == value)
                    return;
                websiteUrl = value;
                NotifyOfPropertyChange(() => WebsiteUrl);
            }
        }

        private BitmapImage avatar;

        public BitmapImage Avatar
        {
            get { return avatar; }
            set
            {
                if (avatar == value)
                    return;
                avatar = value;
                NotifyOfPropertyChange(() => Avatar);
            }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                if (id == value)
                    return;
                id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }

        private bool isRead;

        public bool IsRead
        {
            get { return isRead; }
            set
            {
                if (isRead == value)
                    return;
                isRead = value;
                NotifyOfPropertyChange(() => IsRead);

                var readManager = ServiceLocator.Current.GetInstance<IReadManager>();
                if (IsRead)
                    readManager.MarkAsRead(Id);
                else
                {
                    readManager.MarkAsUnread(Id);
                }
            }
        }

        public static RecommendationViewModel FromModel(Recommendation model, User user, bool isRead = false)
        {
            var avatar = ServiceLocator.Current.GetInstance<IAvatarService>().LoadAvatar(user.AvatarUrl);
            return new RecommendationViewModel
            {
                Created = model.Created,
                Quote = model.Quote,
                Url = model.Url,
                User = user,
                ArticleTitle = model.ArticleTitle,
                WebsiteTitle = model.WebsiteTitle,
                WebsiteUrl = model.WebsiteUrl,
                Avatar = avatar,
                Id = model.Id,
                IsRead = isRead
            };
        }
    }
}