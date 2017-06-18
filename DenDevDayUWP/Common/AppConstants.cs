using System;

namespace SM.Common
{
    public class AppConstants
    {
        public const string APP_TITLE = "DDD UWP Twitter Demo";
        public const string COMPANY_NAME = "Spilled Milk";
        public const string SPILLED_MILK_WEBSITE = "http://www.spilledmilk.com";
        public const string TWITTER_PRIVACY_WEBSITE = "http://twitter.com/privacy/";

        public const int TWEET_LEN_MAX = 140;

        public const int SECONDARY_INDENTATION = 100;

        private const string COPYRIGHT_START_YEAR = "2017";

        public static string AppTitleLower { get { return APP_TITLE.ToLower(); } }

        public static string AppTitleUpper { get { return APP_TITLE.ToUpper(); } }

        public static string AppCopyright
        {
            get
            {
                var result = DateTime.Now.Year.ToString();

                if (result != COPYRIGHT_START_YEAR)
                {
                    result = $"{COPYRIGHT_START_YEAR}-{result}";
                }

                return $"© {result} {COMPANY_NAME}";
            }
        }
    }
}