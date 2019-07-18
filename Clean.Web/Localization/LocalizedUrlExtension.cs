using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clean.Web.Localization
{
    public static class LocalizedUrlExtension
    {
        private static int _seoCodeLength = 2;

        public static bool IsLocalizedUrl(this string url, string applicationPath, bool IsRawPath)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            var activeLanguages = new List<string>() { "ua", "en" };

            if (IsRawPath)
            {
                url = url.RemoveApplicationPathFromUrl(applicationPath);
            }

            var firstSegment = url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

            if (String.IsNullOrEmpty(firstSegment))
                return false;

            return activeLanguages.Contains(firstSegment);
        }

        public static bool IsVirtualDirectory(this string applicationPath)
        {
            if (String.IsNullOrEmpty(applicationPath))
                throw new ArgumentException("Application path is not specified");

            return applicationPath.StartsWith("~");
        }

        public static string RemoveApplicationPathFromUrl(this string rawUrl, string applicationPath)
        {
            if (String.IsNullOrEmpty(applicationPath))
                throw new ArgumentException("Application path is not specified");

            if (rawUrl.Length == applicationPath.Length)
                return "/";

            var result = rawUrl.Substring(applicationPath.Length);

            if (!result.StartsWith("/"))
            {
                result = "/" + result;
            }

            return result;
        }

        public static string GetLanguageSeoCodeFromUrl(this string url, string applicationPath, bool IsRawPath)
        {
            if(IsRawPath)
            {
                if (applicationPath.IsVirtualDirectory())
                {
                    url = url.RemoveApplicationPathFromUrl(applicationPath);
                }
                return url.Substring(1, _seoCodeLength);
            }
            return url.Substring(2, _seoCodeLength);
        }

        public static string RemoveLanguageSeoCodeFromUrl(this string url, string applicationPath, bool IsRawPath)
        {
            if(string.IsNullOrEmpty(url))
            {
                return url;
            }

            if (IsRawPath)
                url = url.RemoveApplicationPathFromUrl(applicationPath);

            url = url.TrimStart('/');

            var result = url.Contains('/') ? url.Substring(url.IndexOf('/')) : string.Empty;

            if (IsRawPath)
                result = applicationPath + result;
            return result;
        }

        public static string AddLanguageSeoCodeToUrl(this string url, string language)
        {
            if(language == null)
            {
                throw new ArgumentNullException("language");
            }

            url = $"/{language}{url}";

            return url;
        }
    }
}