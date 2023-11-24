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
        string userName = "e.lekka@test.pl";
        string password = "Wiosna.123456";

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
            await loginPage.Login(userName, password);
            var isExist = await loginPage.IsMenuExist();
            Assert.IsTrue(isExist);

            //wywołanie przejścia do zakładki Użytkownicy

            var clients = new ClientsPage(Page);
            await clients.UserBtnClick();

            //wywołanie przycisku "Dodaj użytkownika"

            var addUserPg = await clients.AddUserButton();

            //dodanie usera na nowej stronie 

            AddUserPage adduser = new AddUserPage(addUserPg);
            var user = UserData();
            await adduser.FillUser(user);

            //kliknięcie chceckboxu z rolą użytkownika
            await adduser.ClickChceckBox();

            //kliknięcie Zapisz
            await adduser.SaveBtnClick();

            // sprawdzanie czy przycisk Modyfikuj istnieje
            var isModifiedExist = await adduser.isModifiedExist();
            Assert.IsTrue(isModifiedExist);


            //utworzenie imienia i nazwiska
            string name_surname = user.Name + " " + user.Surname;

            //sprawdzanie czy na liście użytkowników znajduje się nowo-dodany użytkownik
            await clients.SearchUser(name_surname);
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
