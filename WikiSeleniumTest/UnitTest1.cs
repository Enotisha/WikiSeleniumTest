
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WikiSeleniumTest;

public class WikiSeleniumTest
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        driver = new ChromeDriver(options);
    }

    [Test]
    public void SeleniumTest()
    {
        driver.Navigate().GoToUrl("https://ru.wikipedia.org/");
        IWebElement queryInput = driver.FindElement(By.Name("search"));
        IWebElement searchButton = driver.FindElement(By.Name("go"));
        queryInput.SendKeys("Selenium");
        searchButton.Click();
        
        Assert.AreEqual("Selenium — Википедия", driver.Title, "Не перешли на страницу про Selenium");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver = null;
    }
}