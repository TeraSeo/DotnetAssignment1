using DotnetAssignment1.menus;

class Program
{
    static void Main(string[] args)
    {
        LoginMenu loginMenu = new LoginMenu();
        loginMenu.LoadUsers(); // load whole users from users.txt file for login
        
        loginMenu.Authenticate(); // authenticate the id and password typed in login menu
    }
}