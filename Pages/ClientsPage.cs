using Bogus.DataSets;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Zakonczenie_tygodnia.Pages
{
    internal class ClientsPage
    {
        private IPage _page;
        private readonly ILocator _addClientBtn;
        private readonly ILocator _UsersControllText;
        private readonly ILocator _UsersBtn;
        private readonly ILocator _addUserButton;
        private readonly ILocator _searchUserLabbel;
        private readonly ILocator _searchIcon;
        private readonly ILocator _userintable;


        public ClientsPage(IPage page)
         {
            _page = page;
            _addClientBtn = _page.Locator(selector: "text='Dodaj klienta'");
            _addUserButton = _page.GetByRole(AriaRole.Link, new() { Name = "Dodaj użytkownika" });
            _UsersBtn = _page.GetByRole(AriaRole.Link, new() { Name = "Użytkownicy i uprawnienia" });
            _UsersControllText = _page.GetByLabel("breadcrumb").GetByText("Użytkownicy i uprawnienia");
            _searchUserLabbel = _page.Locator("//input[@name='FullName']");
            _searchIcon = _page.GetByTestId("SearchIcon").First;
            _userintable = _page.Locator("//*[@id=\'user-table-row-name-1\']/a");
                 


        }

        public async Task<IPage> AddClientButton()
        {

            var page1 = await _page.RunAndWaitForPopupAsync(async () =>
            {
                await _addClientBtn.ClickAsync();
            }       
            );
            return page1;
            
        }

       // public async Task<bool> IfModifiedBtnExist()
       // {
            
      //  }

        public async Task UserBtnClick() => await _UsersBtn.ClickAsync();

        public async Task<bool> IsUsersControllTextExist() => await _UsersControllText.IsVisibleAsync();
    
        public async Task<IPage> AddUserButton()
        {

            var page2 = await _page.RunAndWaitForPopupAsync(async () =>
            {
                await _addUserButton.ClickAsync();
            }
            );
            return page2;

        }

        public async Task<IPage> ClickOnUserInTabel()
        {

            var page3 = await _page.RunAndWaitForPopupAsync(async () =>
            {
                await _userintable.ClickAsync();
            }
            );
            return page3;

        }

        public async Task ReloadPage()
        {
            await _page.ReloadAsync();
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }


        public async Task SearchUser(string username)
        {
            await _searchUserLabbel.ClickAsync();
            await _searchUserLabbel.ClearAsync();
            await _searchUserLabbel.FillAsync(username);
            await _searchIcon.ClickAsync();
            Thread.Sleep(1000);
            await _userintable.IsVisibleAsync();
         
        }

  
        public async Task<String> ReturnCell()
        {

         
            string cell_value = "null";
            bool isVisible = await _userintable.IsVisibleAsync();
           
            if ( isVisible == true)
            {
              
                cell_value = await _userintable.InnerTextAsync();
                return cell_value;
              
            }
            else { return cell_value; }
          
        }

    }
}
