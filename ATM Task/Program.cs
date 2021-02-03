using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATM_Task
{
    class Card
    {
        public Card(string pIN, decimal balance)
        {
            PIN = pIN;
            Balance = balance;
            CreateRandomPAN();
            CreateRandomCVC();
            CreateExpireDate();
            Thread.Sleep(100);
        }

        public string PAN { get; set; }
        public string CVC { get; set; }
        public string PIN { get; set; }
        public string ExpireDate { get; set; }
        public decimal Balance { get; set; }

        private void CreateRandomPAN()
        {
            Random random = new Random();
            StringBuilder stringBuilderPAN = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                stringBuilderPAN.Append(random.Next(1000, 9999)).Append(" ");
            }
            PAN = stringBuilderPAN.ToString();
        }
        private void CreateRandomCVC()
        {
            Random random = new Random();
            CVC = random.Next(100, 1000).ToString();
        }
        private void CreateExpireDate()
        {
            DateTime dateTime = DateTime.Now;
            StringBuilder stringBuilderED = new StringBuilder();
            stringBuilderED.Append(dateTime.Month).Append("/").Append(dateTime.Year + 1);
            ExpireDate = stringBuilderED.ToString();
        }

        public void Withdraw(int amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                throw new Exception("Balansinizda kifayet qeder pul yoxdur!");
            }
        }

        public void Transfer(ref string pin, int amount, Card[] cards)
        {
            foreach (var item in cards)
            {
                if (item.PIN == pin)
                {
                    if (Balance >= amount)
                    {
                        Balance -= amount;
                        item.Balance += amount;
                    }
                    else
                    {
                        throw new Exception("Balansinizda kifayet qeder pul yoxdur!");
                    }
                }
            }
        }


        public void ShowRequisites()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"PAN: {PAN}");
            Console.WriteLine($"Expire Date: {ExpireDate}");
            Console.WriteLine($"CVC: {CVC}");
            Console.WriteLine($"PIN: {PIN}");
            Console.WriteLine($"Balance: {Balance}");
            Console.ResetColor();
        }

    }

    class User
    {
        public User(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public Card CreditCard { get; set; }

        public void AddCard(ref Card card)
        {
            if (card != null) CreditCard = card;
        }
        public void showUser()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Surname: {Surname}");
            Console.WriteLine($"Balance: {CreditCard.Balance}");
            Console.ResetColor();
        }
        public void showUserInfo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Surname: {Surname}");
            CreditCard.ShowRequisites();
            Console.ResetColor();
        }

    }

    class Program
    {
        public static int FindPIN(ref string pin, Card[] cards)
        {
            foreach (var item in cards)
            {
                if (item.PIN == pin)
                {
                    return 1;
                }
            }
            return 0;
        }
        public static void ShowWithdrawalError(Exception ex)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        public static void ShowTransferError(Exception ex)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        public static void ShowMenu()
        {
            Console.WriteLine("Welcome to Step Bank");
            Console.WriteLine("1.Balance");
            Console.WriteLine("2.Withdrawal");
            Console.WriteLine("3.Transfer");
            Console.WriteLine("4.Show Requisites");
            Console.WriteLine("0.Go Back");
        }
        public static void ShowWithdrawMenu()
        {
            Console.WriteLine("Select amount of money you want to withdraw:");
            Console.WriteLine("1)10\t\t\t2)20");
            Console.WriteLine("3)50\t\t\t4)100");
            Console.WriteLine("5)200\t\t\t6)Enter amount");
        }
        public static void ShowTransferMenu()
        {
            Console.WriteLine("Select amount of money you want to transfer:");
            Console.WriteLine("1)10\t\t\t2)20");
            Console.WriteLine("3)50\t\t\t4)100");
            Console.WriteLine("5)200\t\t\t6)Enter amount");
        }

        static void Main(string[] args)
        {
            Card card1 = new Card("1231", 1000);
            Card card2 = new Card("1232", 350);
            Card card3 = new Card("1233", 3200);
            Card card4 = new Card("1234", 780);
            Card card5 = new Card("1235", 2300);
            Card[] cards = new Card[5] { card1, card2, card3, card4, card5 };

            User user1 = new User("Nicola", "Reedtz");
            User user2 = new User("Russel", "Van Dulken");
            User user3 = new User("Aleksandr", "Kostyliev");
            User user4 = new User("Mathieu", "Herbaut");
            User user5 = new User("Marcelo", "David");
            user1.AddCard(ref card1);
            user2.AddCard(ref card2);
            user3.AddCard(ref card3);
            user4.AddCard(ref card4);
            user5.AddCard(ref card5);

            string transferAmount;
            string transferPin;
            int enterAmount;
            string operationMenu;
            string withdrawalAmount;
        FirstMenu:
            Console.WriteLine("Enter your PIN:");
            string enterPin = Console.ReadLine();
            if (FindPIN(ref enterPin, cards) == 1)
            {
                switch (enterPin)
                {
                    case "1231":
                        Console.Clear();
                        ShowMenu();
                    User1Menu:
                        operationMenu = Console.ReadLine();
                        switch (operationMenu)
                        {
                            case "1":
                                Console.Clear();
                                user1.showUser();
                                Thread.Sleep(3000);
                                goto User1Menu;
                                break;
                            case "2":
                                Console.Clear();
                                ShowWithdrawMenu();
                                withdrawalAmount = Console.ReadLine();
                                switch (withdrawalAmount)
                                {
                                    case "1":
                                        try
                                        {
                                            card1.Withdraw(10);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user1.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User1Menu;
                                        break;
                                    case "2":
                                        try
                                        {
                                            card1.Withdraw(20);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user1.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User1Menu;
                                        break;
                                    case "3":
                                        try
                                        {
                                            card1.Withdraw(50);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user1.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User1Menu;
                                        break;
                                    case "4":
                                        try
                                        {
                                            card1.Withdraw(100);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user1.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User1Menu;
                                        break;
                                    case "5":
                                        try
                                        {
                                            card1.Withdraw(200);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user1.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User1Menu;
                                        break;
                                    case "6":
                                        enterAmount = Convert.ToInt32(Console.ReadLine());
                                        try
                                        {
                                            card1.Withdraw(enterAmount);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user1.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User1Menu;
                                        break;
                                    default:
                                        goto User2Menu;
                                        break;
                                }
                                break;
                            case "3":
                                Console.Clear();
                                transferPin = Console.ReadLine();
                                if (FindPIN(ref transferPin, cards) == 1)
                                {
                                    ShowTransferMenu();
                                    transferAmount = Console.ReadLine();
                                    switch (transferAmount)
                                    {
                                        case "1":
                                            try
                                            {
                                                card1.Transfer(ref transferPin, 10, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User1Menu;
                                            break;
                                        case "2":
                                            try
                                            {
                                                card1.Transfer(ref transferPin, 20, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User1Menu;
                                            break;
                                        case "3":
                                            try
                                            {
                                                card1.Transfer(ref transferPin, 50, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User1Menu;
                                            break;
                                        case "4":
                                            try
                                            {
                                                card1.Transfer(ref transferPin, 100, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User1Menu;
                                            break;
                                        case "5":
                                            try
                                            {
                                                card1.Transfer(ref transferPin, 200, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User1Menu;
                                            break;
                                        case "6":
                                            int enterTransferAmount = Convert.ToInt32(Console.ReadLine());
                                            try
                                            {
                                                card1.Transfer(ref transferPin, enterTransferAmount, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User1Menu;
                                            break;
                                        default:
                                            goto User2Menu;
                                            break;
                                    }
                                }
                                break;
                            case "4":
                                user1.showUserInfo();
                                Thread.Sleep(3000);
                                goto User1Menu;
                                break;
                            case "0":
                                goto FirstMenu;
                                break;
                            default:
                                goto User1Menu;
                                break;
                        }
                        break;
                    case "1232":
                        Console.Clear();
                        ShowMenu();
                    User2Menu:
                        operationMenu = Console.ReadLine();
                        switch (operationMenu)
                        {
                            case "1":
                                Console.Clear();
                                user2.showUser();
                                Thread.Sleep(3000);
                                goto User2Menu;
                                break;
                            case "2":
                                Console.Clear();
                                ShowWithdrawMenu();
                                withdrawalAmount = Console.ReadLine();
                                switch (withdrawalAmount)
                                {
                                    case "1":
                                        try
                                        {
                                            card2.Withdraw(10);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user2.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User2Menu;
                                        break;
                                    case "2":
                                        try
                                        {
                                            card2.Withdraw(20);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user2.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User2Menu;
                                        break;
                                    case "3":
                                        try
                                        {
                                            card2.Withdraw(50);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user2.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User2Menu;
                                        break;
                                    case "4":
                                        try
                                        {
                                            card2.Withdraw(100);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user2.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User2Menu;
                                        break;
                                    case "5":
                                        try
                                        {
                                            card2.Withdraw(200);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user2.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User2Menu;
                                        break;
                                    case "6":
                                        enterAmount = Convert.ToInt32(Console.ReadLine());
                                        try
                                        {
                                            card2.Withdraw(enterAmount);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user2.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User2Menu;
                                        break;
                                    default:
                                        goto User2Menu;
                                        break;
                                }
                                break;
                            case "3":
                                Console.Clear();
                                transferPin = Console.ReadLine();
                                if (FindPIN(ref transferPin, cards) == 1)
                                {
                                    ShowTransferMenu();
                                    transferAmount = Console.ReadLine();
                                    switch (transferAmount)
                                    {
                                        case "1":
                                            try
                                            {
                                                card2.Transfer(ref transferPin, 10, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User2Menu;
                                            break;
                                        case "2":
                                            try
                                            {
                                                card2.Transfer(ref transferPin, 20, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User2Menu;
                                            break;
                                        case "3":
                                            try
                                            {
                                                card2.Transfer(ref transferPin, 50, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User2Menu;
                                            break;
                                        case "4":
                                            try
                                            {
                                                card2.Transfer(ref transferPin, 100, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User2Menu;
                                            break;
                                        case "5":
                                            try
                                            {
                                                card2.Transfer(ref transferPin, 200, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User2Menu;
                                            break;
                                        case "6":
                                            int enterTransferAmount = Convert.ToInt32(Console.ReadLine());
                                            try
                                            {
                                                card2.Transfer(ref transferPin, enterTransferAmount, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User2Menu;
                                            break;
                                        default:
                                            goto User2Menu;
                                            break;
                                    }
                                }
                                break;
                            case "4":
                                user2.showUserInfo();
                                Thread.Sleep(3000);
                                goto User2Menu;
                                break;
                            case "0":
                                goto FirstMenu;
                                break;
                            default:
                                goto User2Menu;
                                break;
                        }
                        break;
                    case "1233":
                        Console.Clear();
                        ShowMenu();
                    User3Menu:
                        operationMenu = Console.ReadLine();
                        switch (operationMenu)
                        {
                            case "1":
                                Console.Clear();
                                user3.showUser();
                                Thread.Sleep(3000);
                                goto User3Menu;
                                break;
                            case "2":
                                Console.Clear();
                                ShowWithdrawMenu();
                                withdrawalAmount = Console.ReadLine();
                                switch (withdrawalAmount)
                                {
                                    case "1":
                                        try
                                        {
                                            card3.Withdraw(10);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user3.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User3Menu;
                                        break;
                                    case "2":
                                        try
                                        {
                                            card3.Withdraw(20);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user3.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User3Menu;
                                        break;
                                    case "3":
                                        try
                                        {
                                            card3.Withdraw(50);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user3.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User3Menu;
                                        break;
                                    case "4":
                                        try
                                        {
                                            card3.Withdraw(100);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user3.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User3Menu;
                                        break;
                                    case "5":
                                        try
                                        {
                                            card3.Withdraw(200);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user3.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User3Menu;
                                        break;
                                    case "6":
                                        enterAmount = Convert.ToInt32(Console.ReadLine());
                                        try
                                        {
                                            card3.Withdraw(enterAmount);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user3.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User3Menu;
                                        break;
                                    default:
                                        goto User3Menu;
                                        break;
                                }
                                break;
                            case "3":
                                Console.Clear();
                                transferPin = Console.ReadLine();
                                if (FindPIN(ref transferPin, cards) == 1)
                                {
                                    ShowTransferMenu();
                                    transferAmount = Console.ReadLine();
                                    switch (transferAmount)
                                    {
                                        case "1":
                                            try
                                            {
                                                card3.Transfer(ref transferPin, 10, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User3Menu;
                                            break;
                                        case "2":
                                            try
                                            {
                                                card3.Transfer(ref transferPin, 20, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User3Menu;
                                            break;
                                        case "3":
                                            try
                                            {
                                                card3.Transfer(ref transferPin, 50, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User3Menu;
                                            break;
                                        case "4":
                                            try
                                            {
                                                card3.Transfer(ref transferPin, 100, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User3Menu;
                                            break;
                                        case "5":
                                            try
                                            {
                                                card3.Transfer(ref transferPin, 200, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User3Menu;
                                            break;
                                        case "6":
                                            int enterTransferAmount = Convert.ToInt32(Console.ReadLine());
                                            try
                                            {
                                                card3.Transfer(ref transferPin, enterTransferAmount, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User3Menu;
                                            break;
                                        default:
                                            goto User3Menu;
                                            break;
                                    }
                                }
                                break;
                            case "4":
                                user3.showUserInfo();
                                Thread.Sleep(3000);
                                goto User3Menu;
                                break;
                            case "0":
                                goto FirstMenu;
                                break;
                            default:
                                goto User3Menu;
                                break;
                        }
                        break;
                    case "1234":
                        Console.Clear();
                        ShowMenu();
                    User4Menu:
                        operationMenu = Console.ReadLine();
                        switch (operationMenu)
                        {
                            case "1":
                                Console.Clear();
                                user4.showUser();
                                Thread.Sleep(3000);
                                goto User4Menu;
                                break;
                            case "2":
                                Console.Clear();
                                ShowWithdrawMenu();
                                withdrawalAmount = Console.ReadLine();
                                switch (withdrawalAmount)
                                {
                                    case "1":
                                        try
                                        {
                                            card4.Withdraw(10);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user4.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User4Menu;
                                        break;
                                    case "2":
                                        try
                                        {
                                            card4.Withdraw(20);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user4.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User4Menu;
                                        break;
                                    case "3":
                                        try
                                        {
                                            card4.Withdraw(50);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user4.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User4Menu;
                                        break;
                                    case "4":
                                        try
                                        {
                                            card4.Withdraw(100);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user4.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User4Menu;
                                        break;
                                    case "5":
                                        try
                                        {
                                            card4.Withdraw(200);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user4.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User4Menu;
                                        break;
                                    case "6":
                                        enterAmount = Convert.ToInt32(Console.ReadLine());
                                        try
                                        {
                                            card4.Withdraw(enterAmount);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user4.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User4Menu;
                                        break;
                                    default:
                                        goto User4Menu;
                                        break;
                                }
                                break;
                            case "3":
                                Console.Clear();
                                transferPin = Console.ReadLine();
                                if (FindPIN(ref transferPin, cards) == 1)
                                {
                                    ShowTransferMenu();
                                    transferAmount = Console.ReadLine();
                                    switch (transferAmount)
                                    {
                                        case "1":
                                            try
                                            {
                                                card4.Transfer(ref transferPin, 10, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User4Menu;
                                            break;
                                        case "2":
                                            try
                                            {
                                                card4.Transfer(ref transferPin, 20, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User4Menu;
                                            break;
                                        case "3":
                                            try
                                            {
                                                card4.Transfer(ref transferPin, 50, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User4Menu;
                                            break;
                                        case "4":
                                            try
                                            {
                                                card4.Transfer(ref transferPin, 100, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User4Menu;
                                            break;
                                        case "5":
                                            try
                                            {
                                                card4.Transfer(ref transferPin, 200, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User4Menu;
                                            break;
                                        case "6":
                                            int enterTransferAmount = Convert.ToInt32(Console.ReadLine());
                                            try
                                            {
                                                card4.Transfer(ref transferPin, enterTransferAmount, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User4Menu;
                                            break;
                                        default:
                                            goto User4Menu;
                                            break;
                                    }
                                }
                                break;
                            case "4":
                                user4.showUserInfo();
                                Thread.Sleep(3000);
                                goto User4Menu;
                                break;
                            case "0":
                                goto FirstMenu;
                                break;
                            default:
                                goto User4Menu;
                                break;
                        }
                        break;
                    case "1235":
                        Console.Clear();
                        ShowMenu();
                    User5Menu:
                        operationMenu = Console.ReadLine();
                        switch (operationMenu)
                        {
                            case "1":
                                Console.Clear();
                                user5.showUser();
                                Thread.Sleep(3000);
                                goto User5Menu;
                                break;
                            case "2":
                                Console.Clear();
                                ShowWithdrawMenu();
                                withdrawalAmount = Console.ReadLine();
                                switch (withdrawalAmount)
                                {
                                    case "1":
                                        try
                                        {
                                            card5.Withdraw(10);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user5.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User5Menu;
                                        break;
                                    case "2":
                                        try
                                        {
                                            card5.Withdraw(20);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user5.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User5Menu;
                                        break;
                                    case "3":
                                        try
                                        {
                                            card5.Withdraw(50);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user5.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User5Menu;
                                        break;
                                    case "4":
                                        try
                                        {
                                            card5.Withdraw(100);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user5.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User5Menu;
                                        break;
                                    case "5":
                                        try
                                        {
                                            card5.Withdraw(200);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user5.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User5Menu;
                                        break;
                                    case "6":
                                        enterAmount = Convert.ToInt32(Console.ReadLine());
                                        try
                                        {
                                            card5.Withdraw(enterAmount);
                                        }
                                        catch (Exception ex)
                                        {
                                            ShowWithdrawalError(ex);
                                        }
                                        user5.showUserInfo();
                                        Thread.Sleep(1000);
                                        goto User5Menu;
                                        break;
                                    default:
                                        goto User5Menu;
                                        break;
                                }
                                break;
                            case "3":
                                Console.Clear();
                                transferPin = Console.ReadLine();
                                if (FindPIN(ref transferPin, cards) == 1)
                                {
                                    ShowTransferMenu();
                                    transferAmount = Console.ReadLine();
                                    switch (transferAmount)
                                    {
                                        case "1":
                                            try
                                            {
                                                card5.Transfer(ref transferPin, 10, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User5Menu;
                                            break;
                                        case "2":
                                            try
                                            {
                                                card5.Transfer(ref transferPin, 20, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User5Menu;
                                            break;
                                        case "3":
                                            try
                                            {
                                                card5.Transfer(ref transferPin, 50, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User5Menu;
                                            break;
                                        case "4":
                                            try
                                            {
                                                card5.Transfer(ref transferPin, 100, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User5Menu;
                                            break;
                                        case "5":
                                            try
                                            {
                                                card5.Transfer(ref transferPin, 200, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User5Menu;
                                            break;
                                        case "6":
                                            int enterTransferAmount = Convert.ToInt32(Console.ReadLine());
                                            try
                                            {
                                                card5.Transfer(ref transferPin, enterTransferAmount, cards);
                                            }
                                            catch (Exception ex)
                                            {
                                                ShowTransferError(ex);
                                            }
                                            Thread.Sleep(1000);
                                            goto User5Menu;
                                            break;
                                        default:
                                            goto User5Menu;
                                            break;
                                    }
                                }
                                break;
                            case "4":
                                user5.showUserInfo();
                                Thread.Sleep(3000);
                                goto User5Menu;
                                break;
                            case "0":
                                goto FirstMenu;
                                break;
                            default:
                                goto User5Menu;
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
