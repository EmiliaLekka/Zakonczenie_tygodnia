using Bogus;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zakonczenie_tygodnia.Pages;
using FluentAssertions;


namespace Zakonczenie_tygodnia.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]


    public class DeleteUserTest: PageTest

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

        public async Task DeleteUser()
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

            ////////////////////////////////////////////////////////////////////////////

            // sprawdzanie czy przycisk Modyfikuj istnieje

         //   var locator_mdf_btn = addUserPg.Locator(selector: "text='Modyfikuj'");
            
        //    await Expect(locator_mdf_btn).ToBeVisibleAsync();
            var isModifiedExist = await adduser.isModifiedExist();
            Assert.IsTrue(isModifiedExist);

            ///////////////////////////////////////////////////////////////////////////

            //utworzenie imienia i nazwiska
            string name_surname = user.Name + " " + user.Surname;
     
            //sprawdzanie czy na liście użytkowników znajduje się nowo-dodany użytkownik
            await clients.SearchUser(name_surname);
            
            
            //sprawdzanie czy to, co wyświetla się w komórce zgadza się z imieniem i nazwiskiem nowo-dodanego user
            string valueFromCell = await clients.ReturnCell();
            valueFromCell.Should().Contain(name_surname);
        
            // kliknięcie w użytkownika w tabeli
             var clickOnUser = await clients.ClickOnUserInTabel();

            // usuwamy usera
            var deletePage = new DeleteUserPage(clickOnUser);
            await deletePage.DeleteUser();  
           
            
            //sprawdzamy czy usunięty user jest w tabeli
            await clients.ReloadPage();
            string cellAfter = await clients.ReturnCell();
            cellAfter.Should().Contain("null");
          

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
