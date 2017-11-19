using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Drawing;
using OpenQA.Selenium.Remote;

namespace ImageService
{
    public static class ImageService
    {
        private static string UrlPart = "http://images.google.com/search?tbm=isch&q=";
        private static string DriverPath = @"C:\Users\vlad\Desktop\FSLHackaton\Services\ImageService\bin\Debug\netcoreapp2.0"; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        public static void GetPicture(string SearchWord)
        {
            string SearchUrl = UrlPart + SearchWord;

            IWebDriver driver = new ChromeDriver(DriverPath);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(SearchUrl);

            ITakesScreenshot its = (ITakesScreenshot)driver;
            Screenshot screenshot = its.GetScreenshot();

            IWebElement searchingImage = driver.FindElement(By.CssSelector(".rg_ic.rg_i"));

            Actions action = new Actions(driver);
            action.MoveToElement(searchingImage);
            action.Perform();

            int imageWidth = searchingImage.Size.Width;
            int imageHeight = searchingImage.Size.Height;

            RemoteWebElement element = (RemoteWebElement)searchingImage;

            Point imagePosition = new Point();
            imagePosition.X = element.LocationOnScreenOnceScrolledIntoView.X;
            imagePosition.Y = element.LocationOnScreenOnceScrolledIntoView.Y;

            driver.Close();

            var memoryStream = new MemoryStream(screenshot.AsByteArray);
            Bitmap imageUncut = new Bitmap(Image.FromStream(memoryStream));

            Bitmap picture = imageUncut.Clone(new Rectangle(imagePosition.X, imagePosition.Y, imageWidth, imageHeight), imageUncut.PixelFormat);
            picture.Save("picture.jpeg");
        }
    }
}
