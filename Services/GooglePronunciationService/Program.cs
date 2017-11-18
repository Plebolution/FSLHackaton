using System.Net;

namespace GooglePronunciationService
{
    public static class GooglePronunciationService
    {
        private static string UrlPartOne = "https://translate.google.com.vn/translate_tts?ie=UTF-8&q=";
        private static string UrlPartTwo = "&tl=";
        private static string UrlPartThree = "&client=tw-ob";
        private static string DownLoadedFileName = "Pronunciation.mp3";

        public static void GetPronunciation(string PronunciationWord, string PronunciationLanguage)
        {
            string DownloadUrl = UrlPartOne + WebUtility.UrlEncode(PronunciationWord) + UrlPartTwo + PronunciationLanguage + UrlPartThree;

            WebClient PronunciationWebClient = new WebClient();
            PronunciationWebClient.DownloadFile(DownloadUrl, DownLoadedFileName);
        }
    }
}
