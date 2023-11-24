using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakonczenie_tygodnia.Pages
{
    internal class LoginPage
    {
        private  IPage _page;
       
        private readonly ILocator _txtUserName;
        private readonly ILocator _txtPassword;
        private readonly ILocator _btnLogin;
        private readonly ILocator _Menu;

        public LoginPage(IPage page) 
        
        {

            _page = page;
            _txtUserName = _page.Locator(selector: "#Username");
            _txtPassword = _page.Locator(selector: "#Password");
            _btnLogin = _page.Locator(selector: "button", new PageLocatorOptions { HasTextString = "Zaloguj" });

            _Menu = _page.Locator(selector: "text='Menu'");
                   


        }

        public async Task Login (string userName, string password)

        {
            await _txtUserName.FillAsync(userName);
            await _txtPassword.FillAsync(password);
            await _btnLogin.ClickAsync();
            await _Menu.WaitForAsync();


        }

        public async Task<bool> IsMenuExist() => await _Menu.IsVisibleAsync();

    }
}
