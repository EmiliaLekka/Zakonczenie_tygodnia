using Bogus;
using FluentAssertions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Zakonczenie_tygodnia.Pages;
using static System.Net.Mime.MediaTypeNames;

namespace Zakonczenie_tygodnia.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]

    public class UserTest : PageTest
    {
        public Faker _faker;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _faker = new Faker("pl");
        }

        [SetUp]

        public async Task SetUp()
        {
            await Page.GotoAsync(url: "https://test.riskradar.pl");

        }


        [Test]

        public async Task AddUser()
        {
            //wywołanie metody logowania

            var loginPage = new LoginPage(Page);
            await loginPage.Login(userName: "e.lekka@test.pl", password: "Wiosna.123456");
            var isExist = await loginPage.IsMenuExist();
            Assert.IsTrue(isExist);

            //wywołanie przejścia do zakładki Użytkownicy

            var userclick = new ClientsPage(Page);
            var isElementUserTxtExist = userclick.UserBtnClick();
            

            //wywołanie przycisku "Dodaj użytkownika"
           
             var clients = new ClientsPage(Page);
             var addUserPg = await clients.AddUserButton();

            //dodanie usera na nowej stronie 
          
            AddUserPage adduser = new AddUserPage(addUserPg);
            var user = UserData();
            await adduser.FillUser(user);

            //kliknięcie chceckboxu z rolą użytkownika

             await adduser.ClickChceckBox();

            //kliknięcie Zapisz

            //  var locator_sv_btn = addUserPg.Locator(("//button[@data-test-id='user-form-save-button']"));
      //      ILocator valuefroncell = await clients.ReturnCell();
            ILocator saveBtn = await adduser.isModified();
            await Expect(saveBtn).ToBeEditableAsync();
            await adduser.SaveBtnClick();
            

            // sprawdzanie czy przycisk Modyfikuj istnieje
            var locator_mdf_btn = addUserPg.Locator(selector:"text='Modyfikuj'");
            await Expect(locator_mdf_btn).ToBeVisibleAsync();
            var isModifiedExist = await adduser.isModifiedExist();
            Assert.IsTrue(isModifiedExist);


            // do poprawy !!!!!!!!!!!!!!!!!!!!!!
            await userclick.UserBtnClick();
            var is_UsersControllText = Page.Locator("//*[@id='root']/div/div/form/div/div[2]/div[1]/nav/ol/li[3]/p");
            await Expect(is_UsersControllText).ToBeVisibleAsync();

            //utworzenie imienia i nazwiska
            var name_surname = user.Name +" "+ user.Surname;
            Console.WriteLine(name_surname);

            //sprawdzanie czy na liście użytkowników znajduje się nowo-dodany użytkownik
            await clients.SearchUser(name_surname);
            //   await clients.ReturnCell();
            string valuefromcell = await clients.ReturnCell();
            //ILocator valuefromcell = await 
            valuefromcell.Should().Contain(name_surname);
            //  await Expect(valuefromcell).ToContainTextAsync(name_surname);
        }




        private User UserData()
        {
            return new User()
            {
                Name = _faker.Name.FirstName(),
                Surname = _faker.Name.LastName(),
                Email = _faker.Person.Email,
                PhoneNumber = _faker.Person.Phone,
            };

        }

    }
}
