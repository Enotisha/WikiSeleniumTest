
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WikiSeleniumTest;

public class WikiSeleniumTest
{
    private IWebDriver driver;
    private WebDriverWait wait;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        driver = new ChromeDriver(options);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); //явное ожидание чего угодно.
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //неявное ожидание появления элемента на странице.
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