using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Timers;


namespace ProjectWeekMohamed
{
    class Program
    {

        static string bestandsnaam = "users.txt";
        public static char[] line1 = new char[10];
        public static char[] anonyms=new char[] {'0','0','0','0','0','0','0','0','0','0'};

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool logged = false;
            while (!logged)
            {
                Console.Clear();
                Console.WriteLine("1.Gebruiker toevoegen\n2.Gebruiker bewerken\n3.Gebruiker verwijderen\n4.Inloggen");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        NewUsername();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Username : ");
                        string username = Console.ReadLine().ToLower();
                        if (UsernameExist(username) != "")
                        {
                            Console.WriteLine("Wachtwoord : ");
                            string password = Console.ReadLine();
                            if (UsernameExist(username) == password)
                            {
                                Console.WriteLine("New username : ");
                                string newusername = Console.ReadLine();
                                if (IsAllLettersOrDigits(newusername))
                                {
                                    Console.WriteLine("New wachtwoord : ");
                                    string newpassword = Console.ReadLine();
                                    string old = "";
                                    using (StreamReader reader = new StreamReader(bestandsnaam))
                                    {
                                        while (!reader.EndOfStream)
                                        {
                                            string line = reader.ReadLine();
                                            if (Decript(line).Substring(Decript(line).IndexOf(' ') + 1, Decript(line).IndexOf('!')-username.Length-1) != password)
                                                old += line + "\n";
                                            else
                                            {
                                                old += Encript(newusername + " " + newpassword+"!"+GetKredit(username)) + "\n";
                                            }
                                        }
                                    }
                                    using (StreamWriter write = new StreamWriter(bestandsnaam))
                                        write.WriteLine(old);
                                }
                                else
                                {
                                    Console.WriteLine("Username moet alleen letters en cijfers zijn !!");
                                    Console.WriteLine("Druk op Enter om door te gaan");
                                    Console.ReadLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Verkeerd wachtwoord !!");
                                Console.WriteLine("Druk op Enter om door te gaan");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Username bestaat niet !!");
                            Console.WriteLine("Druk op Enter om door te gaan");
                            Console.ReadLine();
                        }
                            break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Username : ");
                        string username1 = Console.ReadLine().ToLower();
                        if (UsernameExist(username1) != "")
                        {
                            Console.WriteLine("Wachtwoord : ");
                            string password = Console.ReadLine();
                            if (UsernameExist(username1) == password)
                            {
                                string old = "";
                                using (StreamReader reader = new StreamReader(bestandsnaam))
                                {
                                    while (!reader.EndOfStream)
                                    {
                                        string line = reader.ReadLine();
                                        if (Decript(line).Substring(Decript(line).IndexOf(' ') + 1,Decript(line).IndexOf("!") - username1.Length - 1) != password)
                                            old += line + "\n";
                                    }
                                }
                                using (StreamWriter write = new StreamWriter(bestandsnaam))
                                    write.WriteLine(old);
                                Console.WriteLine($"Username {username1} is verwijderd");
                            }
                            else
                            {
                                Console.WriteLine("Verkeerd password !!");
                                Console.WriteLine("Druk op Enter om door te gaan");
                                Console.ReadLine();
                            }
                        }
                        else
                        { 
                            Console.WriteLine("Username bestaat niet !!");
                            Console.WriteLine("Druk op Enter om door te gaan");
                            Console.ReadLine();
                        }
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("Username : ");
                        string user = Console.ReadLine();
                        if (UsernameExist(user) != "")
                        {
                            Console.WriteLine("Wachtwoord : ");
                            string pass = Console.ReadLine();
                            if (UsernameExist(user) == pass)
                            {
                                logged = true;
                                Console.Clear();
                                DateTime dt = DateTime.Now;
                                Console.WriteLine(dt.ToString("dd/MM/yyyy HH:mm:ss"));
                                Console.WriteLine("WelKome " + user);
                                Console.WriteLine("je kredit is : 200$");

                                
                                while (logged)
                                {
                                    Console.Clear();
                                    Console.WriteLine("1 : Blackjack.\n2 : Slot machine.\n3 : Memory.\n4 : Uit te loggen.");

                                    switch (Console.ReadLine())
                                    {
                                        case "1":
                                            Console.Clear();
                                            Console.WriteLine($"Je bent al {(DateTime.Now - dt).Duration().ToString().Substring(0, 8)} uur.");
                                            Console.WriteLine("***Blackjack***");
                                            Console.WriteLine("Je inzet is : 10 $");
                                            SetKredit(GetKredit(user) - 10,user);
                                            char[] kaarten = new char[52];
                                            for (int i = 0; i < 52; i += 13)
                                            {
                                                kaarten[i] = 'A';
                                                kaarten[i + 1] = '2';
                                                kaarten[i + 2] = '3';
                                                kaarten[i + 3] = '4';
                                                kaarten[i + 4] = '5';
                                                kaarten[i + 5] = '6';
                                                kaarten[i + 6] = '7';
                                                kaarten[i + 7] = '8';
                                                kaarten[i + 8] = '9';
                                                kaarten[i + 9] = '0';
                                                kaarten[i + 10] = 'J';
                                                kaarten[i + 11] = 'Q';
                                                kaarten[i + 12] = 'K';
                                            }
                                            Random rdm = new Random();
                                            char[] dealer = new char[11];
                                            dealer[0] = kaarten[rdm.Next(51)];
                                            dealer[1] = kaarten[rdm.Next(51)];
                                            int dealerkaarten = 2;
                                            int dealersum = sum(dealer, dealerkaarten);

                                            char[] player = new char[11];
                                            player[0] = kaarten[rdm.Next(51)];
                                            player[1] = kaarten[rdm.Next(51)];
                                            int playerkaarten = 2;
                                            int playersum = sum(player, playerkaarten);
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            string playerhand = $"Je    :  [{player[0]}] + [{player[1]}]";
                                            Console.WriteLine(playerhand);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            string dealerhand = $"Dealer :  [{dealer[0]}] + [x]";
                                            Console.WriteLine(dealerhand);
                                            Console.ForegroundColor = ConsoleColor.White;
                                            if (player[0] == 'A' && player[1] == 'J' || player[0] == 'A' && player[1] == 'Q' || player[0] == 'A' && player[1] == 'K' || player[0] == 'A' && player[1] == '0')
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Blackjack ! Je krijgt 25$");
                                                SetKredit(GetKredit(user) + 25, user);
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.WriteLine($"Je kredit is : {GetKredit(user)} $.");
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.WriteLine("Druk op Enter om door te gaan");
                                                Console.ReadLine();
                                            }
                                            else if (player[1] == 'A' && player[0] == 'J' || player[1] == 'A' && player[0] == 'Q' || player[1] == 'A' && player[0] == 'K' || player[1] == 'A' && player[0] == '0')
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Blackjack ! Je krijgt 25$");
                                                SetKredit(GetKredit(user)+25,user);
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.WriteLine($"Je kredit is : {GetKredit(user)} $.");
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.WriteLine("Druk op Enter om door te gaan");
                                                Console.ReadLine();
                                            }
                                            else
                                            {
                                                bool trekorstop = true;
                                                while (trekorstop)
                                                {
                                                    Console.WriteLine("1:Trekken\t2:stoppen");
                                                    switch (Console.ReadLine())
                                                    {
                                                        case "1":
                                                            playerkaarten++;
                                                            player[playerkaarten - 1] = kaarten[rdm.Next(51)];
                                                            playerhand += " + [" + player[playerkaarten - 1] + "]";
                                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                                            Console.WriteLine(playerhand);
                                                            Console.ForegroundColor = ConsoleColor.White;
                                                            playersum = sum(player, playerkaarten);
                                                            if (playersum > 21)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("Je verlies je inzet");
                                                                SetKredit(GetKredit(user)-10,user);
                                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                                Console.WriteLine($"Je kredit is : {GetKredit(user)} $.");
                                                                trekorstop = false;
                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                Console.WriteLine("Druk op Enter om door te gaan");
                                                                Console.ReadLine();
                                                            }

                                                            break;
                                                        case "2":
                                                            trekorstop = false;
                                                            dealerhand = $"Dealer :  [{dealer[0]}] + [{dealer[1]}]";
                                                            Console.ForegroundColor = ConsoleColor.Blue;
                                                            Console.WriteLine(dealerhand);
                                                            Console.ForegroundColor = ConsoleColor.White;
                                                            while (dealersum < 17)
                                                            {
                                                                Console.WriteLine("Dealer trekt nog een kaartje!");
                                                                dealerkaarten++;
                                                                dealer[dealerkaarten - 1] = kaarten[rdm.Next(51)];
                                                                dealerhand += " + [" + dealer[dealerkaarten - 1] + "]";
                                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                                Console.WriteLine(dealerhand);
                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                dealersum = sum(dealer, dealerkaarten);
                                                            }
                                                            if (dealersum > 21 || playersum < 22 && dealersum < playersum)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Green;
                                                                Console.WriteLine("Je wint 20$ !");
                                                                SetKredit(GetKredit(user)+20,user);
                                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                                Console.WriteLine($"Je kredit is : {GetKredit(user)} $.");
                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                Console.WriteLine("Druk op een knop om door te gaan");
                                                                Console.ReadLine();
                                                            }
                                                            else if (dealersum < 22 && dealersum > playersum)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;

                                                                Console.WriteLine("Je verliest je inzet !");
                                                                SetKredit(GetKredit(user)-10,user);
                                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                                Console.WriteLine($"Je kredit is : {GetKredit(user)} $.");
                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                Console.WriteLine("Druk op Enter om door te gaan");
                                                                Console.ReadLine();
                                                            }
                                                            else if (dealersum == playersum)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                                Console.WriteLine("de dealer heeft evenveel dan je! Je krijgt je 10$ trug!");
                                                                SetKredit(GetKredit(user)+10,user);
                                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                                Console.WriteLine($"Je kredit is : {GetKredit(user)} $.");
                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                Console.WriteLine("Druk op Enter om door te gaan");
                                                                Console.ReadLine();
                                                            }
                                                            break;
                                                        default:
                                                            Console.WriteLine("Verkeerd Keuze!");
                                                            Console.WriteLine("Druk op Enter om door te gaan");
                                                            Console.ReadLine();
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        default:
                                            Console.WriteLine("Verkeerd keuze !!");
                                            Console.WriteLine("Druk op Enter om door te gaan");
                                            Console.ReadLine(); break;
                                        case "2":
                                            SetKredit(GetKredit(user)-5,user);
                                            Random rd = new Random();
                                            Random rdclr = new Random();
                                            char[] chars = new char[] { '☺', '♠', '♣', '♦', '♥', 'A', '7' };
                                            char[,] table = new char[3, 3];
                                            Console.Clear();
                                            Console.WriteLine($"Je bent al {(DateTime.Now - dt).Duration().ToString().Substring(0, 8)} uur.");
                                            Console.WriteLine("***Slot machine***");
                                            Console.WriteLine("Je inzet is : 5 $");
                                            Console.WriteLine("Druk op Enter om te starten");
                                            Console.ReadLine();
                                            for (int k = 0; k < 20; k++)
                                            {
                                                Console.Clear();
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int j = 0; j < 3; j++)
                                                    {
                                                        table[i, j] = chars[rd.Next(1, 7)];
                                                        Console.ForegroundColor = ConsoleColor.Black + rdclr.Next(1, 10);
                                                        Console.Write($"[{table[i, j]}] ");
                                                    }
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    Console.WriteLine();
                                                }
                                                System.Threading.Thread.Sleep(100);
                                            }
                                            
                                            Console.WriteLine("Finished !");
                                            SetKredit(GetKredit(user)+Slot(table),user);
                                            Console.WriteLine($"you winned {Slot(table)} $");
                                            Console.WriteLine($"Je kredit is : {GetKredit(user)} $");
                                            Console.WriteLine("Druk op Enter om door te gaan");
                                            Console.ReadLine();
                                            break;
                                        case "3":
                                            Console.Clear();
                                            Console.WriteLine($"Je bent al {(DateTime.Now - dt).Duration().ToString().Substring(0, 8)} uur.");
                                            Console.WriteLine("***Memory***");
                                            Console.WriteLine("Je inzet is : 20 $");
                                            SetKredit(GetKredit(user)-20,user);
                                            Random r = new Random();
                                            char[] list = new char[] { '♠', '♣', '♦', '♥', 'A', '7' };

                                            for (int i = 0; i < 10; i++)
                                            {
                                                Console.Write((i) + " \t");
                                            }
                                            Console.WriteLine();
                                            for (int i = 0; i < 10; i++)
                                            {
                                                Console.OutputEncoding = System.Text.Encoding.UTF8;

                                                Console.ForegroundColor = ConsoleColor.Black + i + 1;
                                                char str = list[r.Next(0, 5)];
                                                Console.Write(str + "\t");
                                                line1[i] += str;
                                            }
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.WriteLine();
                                            System.Threading.Thread.Sleep(5500);
                                            //timer.Elapsed += ontime;
                                            ontime();
                                            Console.BackgroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("♠=P \t♣=C\t♦=T\t♥=H\tA=A\t7=7");
                                            Console.ForegroundColor = ConsoleColor.White;
                                            int cnt = 0;
                                            for (int i = 0; i < 10; i++)
                                            {
                                                if (anonyms[i] != '0')
                                                {
                                                    Console.WriteLine($"Card {i} is : ");
                                                    if (!check(Convert.ToChar(Console.ReadLine()), anonyms[i]))
                                                    {
                                                        Console.WriteLine("Verkeerd antwoord !\nJe verliest je inzet !");
                                                        SetKredit(GetKredit(user)-20,user);
                                                        Console.WriteLine($"Je kredit is : {GetKredit(user)} $.");
                                                        Console.WriteLine("Druk op Enter om door te gaan");
                                                        Console.ReadLine();
                                                        break;
                                                    }
                                                    else
                                                        cnt++;
                                                }
                                                if (cnt == 6)
                                                {
                                                    Console.WriteLine("Je antwoorden zijn juist ! Je wint 10 $ !");
                                                    SetKredit(GetKredit(user)+10,user);
                                                    Console.WriteLine($"Je kredit is : {GetKredit(user)} $.");
                                                    Console.WriteLine("Druk op Enter om door te gaan");
                                                    Console.ReadLine();
                                                    break;
                                                }

                                            }

                                            break;
                                        case "4":
                                            logged = false;
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Verkeerd password");
                                Console.WriteLine("Druk op Enter om door te gaan");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Verkeerd username !!");
                            Console.WriteLine("Druk op Enter om door te gaan");
                            Console.ReadLine();
                        }
                        break;
                    default:
                        Console.WriteLine("Verkeerd keuze !!");
                        Console.WriteLine("Druk op Enter om door te gaan");
                        Console.ReadLine();
                        break;

                }

            }
        }
        public static bool IsAllLettersOrDigits(string s)
        {
            bool x = true;
            foreach (char c in s)
            {
                if (!Char.IsLetterOrDigit(c))
                    x= false;
            }
            return x;
        }
        public static void NewUsername()
        {
            Console.WriteLine("Username (alleen letters en cijfers) : ");
            string username = Console.ReadLine().ToLower();
            if (UsernameExist(username) == "")
            {
                if (IsAllLettersOrDigits(username))
                {
                    Console.WriteLine("Geef een wachtwoord :\n (Minstens 1 hoofdletter, 1 kleine letter, 1 cijfer, 1 vreemd teken en een lengte van 8-20 characters)");
                    string password = Console.ReadLine();
                    Regex reg = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?\"\'()@$%^&*+./.,-]).{8,20}$");
                    if (reg.IsMatch(password))
                    {
                        NewPassword(username, password,200);
                    }
                    else
                    {
                        Console.WriteLine("Wachtwoord moet Minstens 1 hoofdletter, 1 kleine letter, 1 cijfer, 1 vreemd teken en een lengte van 8-20 characters zijn !!");
                        Console.WriteLine("Druk op Enter om door te gaan");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Username moet alleen letters en cijfers zijn !!");
                    Console.WriteLine("Druk op Enter om door te gaan");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("deze username bestaat al !!");
                Console.WriteLine("Druk op Enter om door te gaan");
                Console.ReadLine();
            }
        }
        public static void NewPassword(string username, string password,int kredit)
        {
            using (StreamWriter writer = new StreamWriter(bestandsnaam, true))
            {

                writer.WriteLine(Encript(username + " " + password+"!"+kredit));

                writer.Close();
            }
        }
        public static string UsernameExist(string username)
        {
            string pass = "";
            if (File.Exists(bestandsnaam))
            {
                using (StreamReader reader = new StreamReader(bestandsnaam))
                {
                    while (!reader.EndOfStream)
                    {
                        string orig = Decript(reader.ReadLine());
                        if (orig != "" && orig.Substring(0, orig.IndexOf(' ')) == username)
                        {
                            pass = orig.Substring(orig.IndexOf(' ') + 1,orig.IndexOf('!')-username.Length-1);
                        }
                    }
                }
                return pass;
            }
            else
                return pass;

        }
        public static string Encript(string input)
        {
            string encryptie = "";
            foreach (char c in input)
            {
                if (Convert.ToInt32(c) > 33 && Convert.ToInt32(c) < 127)
                    encryptie += Convert.ToChar(159 - Convert.ToInt32(c));
                else
                    encryptie += c;
            }
            return encryptie;
        }
        public static string Decript(string input)
        {
            string orig = "";
            foreach (char c in input)
            {
                if (Convert.ToInt32(c) > 33 && Convert.ToInt32(c) < 127)
                    orig += Convert.ToChar(159 - Convert.ToInt32(c));
                else
                    orig += c;
            }
            return orig;
        }
        public static int sum(char[] tbl, int countkaarten)
        {
            int playersum = 0;
            for (int i = 0; i < countkaarten; i++)
            {
                if (tbl[i] == 'A' && i < countkaarten - 1)
                {
                    char var;
                    var = tbl[countkaarten - 1];
                    tbl[countkaarten - 1] = tbl[i];
                    tbl[i] = var;
                    if (tbl[i] == '0' || tbl[i] == 'J' || tbl[i] == 'Q' || tbl[i] == 'K')
                    {
                        playersum += 10;
                    }
                    else if (char.IsDigit(tbl[i]) && tbl[i] != '0')
                    {
                        playersum += Convert.ToInt32(tbl[i].ToString());
                    }
                }

                else if (tbl[i] == '0' || tbl[i] == 'J' || tbl[i] == 'Q' || tbl[i] == 'K')
                {
                    playersum += 10;
                }
                else if (char.IsDigit(tbl[i]) && tbl[i] != '0')
                {

                    playersum += Convert.ToInt32(tbl[i].ToString());
                }
                else if (tbl[i] == 'A' && playersum > 10)
                {
                    playersum += 1;
                }
                else if (tbl[i] == 'A' && playersum < 11)
                {
                    playersum += 11;
                }
            }
            return playersum;
        }
        public static int Slot(char[,] table)
        {
            int win = 0;
            for (int i = 0; i < 3; i++)
            {
                if (table[i,0]==table[i,1] && table[i,0] == table[i,2])
                {
                    win += Winnen(table[i, 0]);
                }
                if (table[0, i] == table[1, i] && table[0, i] == table[2, i])
                {
                    win += Winnen(table[0, i]);
                }
            }
            return win;
        }
        public static int Winnen(char value)
        {
            if (value == '☺')
                return 3;
            if (value == '♠')
                return 5;
            if (value == '♣')
                return 7;
            if (value == '♦')
                return 10;
            if (value == '♥')
                return 20;
            if (value == 'A')
                return 50;
            if (value == '7')
                return 100;
            else
                return 0;

        }
        public static char[] Action( char[] line)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Random r = new Random();
            
            for (int i = 0; i < 10; i++)
            {
                anonyms[i] = '0';
            }

            for (int i = 0; i < 6; i++)
            {
                int index = r.Next(0, 9);
                while (line[index]=='?')
                {
                    index= r.Next(0, 9);
                }                
                anonyms[index] = line[index]; 
                line[index] = '?';
            }
            for (int i = 0; i < 10; i++)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.ForegroundColor = ConsoleColor.Black + i + 1;
                if (line[i]=='?')
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write( line[i]+"\t");
            }
            Console.WriteLine();           
            return anonyms;
        }
        public static void ontime()
        {
            Console.Clear();
            for (int i = 0; i < 10; i++)
            {
                Console.Write((i) + " \t");
            }
            Console.WriteLine();
            anonyms= Action(line1);
            Console.WriteLine();
        }
        public static bool check(char input, char answer)
        {
            if (input.ToString().ToUpper() == "P" && answer == '♠')
                return true;
            else if (input.ToString().ToUpper() == "C" && answer == '♣')
                return true;
            else if (input.ToString().ToUpper() == "T" && answer == '♦')
                return true;
            else if (input.ToString().ToUpper() == "H" && answer == '♥')
                return true;
            else if (input.ToString().ToUpper() == "A" && answer == 'A')
                return true;
            else if (input.ToString().ToUpper() == "7" && answer == '7')
                return true;
            else
                return false;
        }
        public static void SetKredit(int kredit,string user)
        {
            string old = "";
            using (StreamReader reader = new StreamReader(bestandsnaam))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (Decript(line).Substring(0,Decript(line).IndexOf(' ')) != user)
                        old += line + "\n";
                    else
                    {
                        old += Encript(user + " " + UsernameExist(user))+"!"+kredit + "\n";
                    }
                }
            }
            using (StreamWriter write = new StreamWriter(bestandsnaam))
                write.WriteLine(old);
        }
        public static int GetKredit(string user)
        {
            int kredit = 0;
            using (StreamReader reader = new StreamReader(bestandsnaam))
            {
                while (!reader.EndOfStream)
                {
                    string orig = Decript(reader.ReadLine());
                    if (orig != "" && orig.Substring(0, orig.IndexOf(' ')) == user)
                    {
                        kredit = Convert.ToInt32(orig.Substring(orig.IndexOf('!') + 1, orig.Length- orig.IndexOf('!')));
                    }
                }
            }
            return kredit;
        }
    }
}
