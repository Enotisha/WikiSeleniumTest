
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

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
        options.AddArgument("--incognito");
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
    public void LocatorsPractice()
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

    [Test]
    public void Diadoc_BecomingPartner_SuccessMessageIsShow()
    {
        //Локатор блока “Зарабатывайте на рекомендациях”
        var orderSectionLocator = By.Id("order");
        //Локатор кнопки “Стать партнером” в блоке в конце страницы
        var becomePartnerButtonLocator = By.CssSelector("[value='Стать партнером']");
        //Локатор лайтбокса “Заявка на партнерство” (для вызова лайтбокса необходимо нажать кнопку “Стать партнером” в блоке в конце страницы)
        var becomePartnerLightboxLocator = By.CssSelector(".lightbox-window__content");
        //Локатор поля “Фамилия”
        var surnameInputLocator = By.CssSelector(".lightbox-window__content input[data-field-role='surname']");
        //Локатор поля “Имя”
        var nameInputLocator = By.CssSelector(".lightbox-window__content input[data-field-role='name']");
        //Локатор поля “Электронная почта”
        var emailInputLocator = By.CssSelector(".lightbox-window__content input[type='email']");
        //Локатор кнопки “Отправить”
        var submitButtonLocator = By.CssSelector(".lightbox-window__content button[type='submit']");
        //Локатор лайтбокса “Заявка отправлена”
        var successLightboxLocator = By.CssSelector(".form-message.form-message_success");
        //Локатор текста в лайтбоксе об успешной отправке 
        var successMessageTextLocator = By.CssSelector(".form-message.form-message_success [data-role='success-message-text']");
        //Заголовок лайтбокса об успешной отправке
        var successMessageTitleLocator = By.CssSelector(".form-message.form-message_success [data-role='success-message-title']");
        
        driver.Navigate().GoToUrl("https://konturru-master.ws.testkontur.ru/private/landing?domain=kontur.ru&path=/diadocseleniumref");
        
        // Проскроллить до блока “Зарабатывайте на рекомендациях”
        driver.FindElement(By.CssSelector("body")).SendKeys(Keys.End);
        
        //Кликнуть по кнопке “Стать партнером”
        driver.FindElement(becomePartnerButtonLocator).Click();
        
        //Дождаться появления лайтбокса
        wait.Until(ExpectedConditions.ElementIsVisible(becomePartnerLightboxLocator));
        
        //Заполнить поле “Фамилия” корректными данными
        driver.FindElement(surnameInputLocator).SendKeys("Тестов");
        
        //Заполнить поле “Имя” корректными данными
        driver.FindElement(nameInputLocator).SendKeys("Тест Тестович");
        
        //Заполнить поле “Электронная почта” корректными данными
        driver.FindElement(emailInputLocator).SendKeys("test@test.com");
        
        //Нажать кнопку “Отправить”
        driver.FindElement(submitButtonLocator).Click();
        
        //Дождаться появления сообщения об успешной отправке заявки
        wait.Until(ExpectedConditions.ElementIsVisible(successLightboxLocator));
         
        Assert.Multiple(() => 
            {
                //Проверить что сообщение об успешной отправке содержит “Спасибо за ваше обращение. В случае необходимости мы свяжемся с вами.”
                Assert.AreEqual("Спасибо за ваше обращение. В случае необходимости мы свяжемся с вами.",
                    driver.FindElement(successMessageTextLocator).Text,
                    "Неверный текст успешного сообщения об отправке заявки на партнерство");
        
                Assert.AreEqual("Заявка отправлена.", driver.FindElement(successMessageTitleLocator).Text, "Неверный текст заголовка успешного сообщения об отправке заявки на партнерство");   
            });
    }

    [Test]
    public void LocatorsHomework()
    {
        driver.Navigate()
            .GoToUrl("https://konturru-master.ws.testkontur.ru/private/landing?domain=kontur.ru&path=/diadocselenium");
        //На Странице 1 Локатор кнопки “Попробовать” в обложке tryButton
        var tryBytton = driver.FindElement(By.XPath("//a[@id='try-button']"));
        //На Странице 1  Локатор кнопки “Отправить заявку” внизу страницы sendOrderButton
        var sendOrderButton = driver.FindElement(By.XPath("//a[@data-short-text='Отправить заявку']"));
        driver.Navigate()
            .GoToUrl(
                "https://konturru-master.ws.testkontur.ru/private/landing?domain=kontur.ru&path=/diadocseleniumintegration");
        //На Странице 2 Локатор Виджета Заявки в конце страницы widgetForm
        var widgetForm = driver.FindElement(By.XPath("//section[@id='order']"));
        //На Странице 2 в Виджете Заявки нужны следующие локаторы:
        //surnameInput - поле ввода Фамилии
        var surnameInput = driver.FindElement(By.XPath("//input[@data-field-role='surname']"));
        //nameInput - поле ввода Имени
        var nameInput = driver.FindElement(By.XPath("//input[@data-field-role='name']"));
        //regionSelect - селект Региона
        var regionSelect = driver.FindElement(By.XPath("//select[@id='fw_field_d8c4659176174a3f94d70a6d9ca1284c_86874da4fcad4e43bd395579b42e649e']"));
        //emailInput - поле ввода Email
        var emailInput = driver.FindElement(By.XPath("//input[@id='fw_field_f5ae77bd069f45348c7df20a61a41afa_86874da4fcad4e43bd395579b42e649e']"));
        //emailInputValidationError - локатор ошибки, если ввели неверный Email
        var emailInputValidationError = driver.FindElement(By.XPath("//div[@data-valmsg-for='f5ae77bd-069f-4534-8c7d-f20a61a41afa']"));
        //phoneInput - поле ввода Телефона
        var phoneInput = driver.FindElement(By.XPath("//input[@type='tel']"));
        //companyNameInput - поле ввода Организации
        var companyNameInput = driver.FindElement(By.XPath("//input[@data-field-role='company-name']"));
        //organizationSuggest - саджест Организации 
        //*Чтобы увидеть саджест, например, введите СКБ и нажмите пробел
        var organizationSuggest = driver.FindElement(By.XPath("//div[@class='organization-suggest-container']"));
        //organizationSuggestFirstItem - первый элемент в саджесте Организации
        var organizationSuggestFirstItem = driver.FindElement(By.XPath("//div[@class='autocomplete-suggestion'][1]"));
        //contragentsFileUploaderInput - поле загрузки файла Список Контрагентов
        var contragentsFileUploaderInput = driver.FindElement(By.XPath("//input[@name='3860739e-5d33-440f-a23c-310b10459272.fileuploader']"));
        //fileNameLabel - лейбл с названием загруженного файла
        var fileNameLabel = driver.FindElement(By.XPath("//div[@class='file-loader__filename-wrap']"));
        //customDayCheckbox - чекбокс Связаться в определённый день
        var customDayCheckbox = driver.FindElement(By.XPath("//input[@id='fw_field_4feae0d3ddf442d695e253148603373d_86874da4fcad4e43bd395579b42e649e']"));
        //datePicker - поле ввода Даты
        var datePicker = driver.FindElement(By.XPath("//input[@id='fw_field_99cbbe31767b4ae4a90c3f2f43d83c89_86874da4fcad4e43bd395579b42e649e']"));
        //submitButton - кнопка “Отправить заявку”
        var submitButton = driver.FindElement(By.XPath("//button[@type='submit']"));
        //Заголовок об успешной отправке “Заявка отправлена” successMessageTitle 
        var successMessageTitle = driver.FindElement(By.XPath("//div[@class='form-message form-message_success']"));
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver = null;
    }
}