
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

    [Test]
    public void Locators()
    {
        driver.Navigate().GoToUrl("https://konturru-master.ws.testkontur.ru/private/landing?domain=kontur.ru&path=/diadocseleniumref");
        //Локатор блока “Зарабатывайте на рекомендациях”
        var orderSection = driver.FindElement(By.Id("order"));
        //Локатор кнопки “Стать партнером” в блоке в конце страницы
        var becomePartnerButton = driver.FindElement(By.CssSelector("[value='Стать партнером']"));
        //Локатор лайтбокса “Заявка на партнерство” (для вызова лайтбокса необходимо нажать кнопку “Стать партнером” в блоке в конце страницы)
        var becomePartnerLightbox = driver.FindElement(By.CssSelector(".lightbox-window__content"));
        //Локатор поля “Фамилия”
        var surnameInput = driver.FindElement(By.CssSelector(".lightbox-window__content input[data-field-role='surname']"));
        //Локатор поля “Имя”
        var nameInput = driver.FindElement(By.CssSelector(".lightbox-window__content input[data-field-role='name']"));
        //Локатор поля “Электронная почта”
        var emailInput = driver.FindElement(By.CssSelector(".lightbox-window__content input[type='email']"));
        //Локатор кнопки “Отправить”
        var submitButton = driver.FindElement(By.CssSelector(".lightbox-window__content button[type='submit']"));
        //Локатор лайтбокса “Заявка отправлена”
        var successLightbox = driver.FindElement(By.CssSelector(".form-message.form-message_success"));
        //Локатор текста в лайтбоксе об успешной отправке 
        var successMessageText =
            driver.FindElement(By.CssSelector(".form-message.form-message_success [data-role='success-message-text']"));
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver = null;
    }
}