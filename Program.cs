// Ruch postaci
// Plansza
// Spadajace obiekty
// Zakonczenie gry
// Zderzenia

int pozycjaGracza = 1; // Pozycja poczatkowa
Random generatorLosowy = new Random();
bool czyUderzony = false;
string[] plansza;
const string GRACZ = "^";
const string PRZESZKODA = "#";
const string NITRO = "N";
int punkty = 0;
int speed = 0;
int czasNitro = -1; // nitro jest wylaczone
bool czyPaliwoNitro = false;

NowaPlansza(10);
UstawGracza(pozycjaGracza);
PokazPlansze();

while (!czyUderzony)
{
  // Sterowanie graczem
  if (Console.KeyAvailable)
  {
    ConsoleKeyInfo klawisz = Console.ReadKey(true);
    if (klawisz.Key == ConsoleKey.RightArrow && pozycjaGracza < 2)
    {
      pozycjaGracza++;
    }
    else if (klawisz.Key == ConsoleKey.LeftArrow && pozycjaGracza > 0)
    {
      pozycjaGracza--;
    }
    else if (klawisz.Key == ConsoleKey.Spacebar && czyPaliwoNitro)
    {
      if (czasNitro == -1)
      {
        czasNitro = 10;
        czyPaliwoNitro = false;
      }
      while (Console.KeyAvailable)
      {
        Console.ReadKey(false);
      }
    }
  }
  if (czasNitro == 10)
  {
    speed += 200;
    czasNitro--;
  }
  else if (czasNitro > 0)
  {
    czasNitro--;
  }
  else if (czasNitro == 0)
  {
    speed -= 200;
    czasNitro = -1;
  }

  int pozycjaNajblizszegoNitro = plansza[plansza.Length - 2].IndexOf(NITRO);
  if (pozycjaGracza == pozycjaNajblizszegoNitro)
  {
    czyPaliwoNitro = true;
    // usuwanie nitro z planszy
    char[] linia = plansza[plansza.Length - 2].ToCharArray();
    linia[pozycjaNajblizszegoNitro] = ' ';
    plansza[plansza.Length - 2] = new string(linia);
  }
  // Sprawdzenie kolizji z przeszkodą

  // int pozycjaPrzeszkody = plansza[plansza.Length-2];
  int pozycjaPrzeszkody = plansza[^2].IndexOf(PRZESZKODA);
  if (pozycjaGracza == pozycjaPrzeszkody)
  {
    czyUderzony = true;
    break;
  }
  else
  {
    punkty++;
    // speed++;
  }


  // Dodawanie przeszkod

  int przeszkodNaPozycji = generatorLosowy.Next(3);
  string nowaPrzeszkoda = UstawPrzeszkode(przeszkodNaPozycji);

  if (generatorLosowy.Next(20) == 0) // 5% szans na nitro
  {
    int pozycjaNitro = generatorLosowy.Next(3);
    nowaPrzeszkoda = UstawNitro(pozycjaNitro, nowaPrzeszkoda);
  }
  for (int i = plansza.Length - 2; i > 0; i--)
    {
      plansza[i] = plansza[i - 1];
    }
  plansza[0] = nowaPrzeszkoda;
  

  UstawGracza(pozycjaGracza);
  speed++;
  PokazPlansze();
  Thread.Sleep(Math.Max(50, 600 - speed)); ;
}
Console.Clear();
Console.WriteLine("GAME OVER");
Console.WriteLine($"Zdobyles {punkty} punktow");
Thread.Sleep(2000);
Console.ReadKey();

//  Metody pomocnicze

void NowaPlansza(int wysokosc)
{
  plansza = new string[wysokosc];
  for (int i = 0; i < plansza.Length; i++)
  {
    plansza[i] = "   ";
  }
}

void UstawGracza(int pozycja)
{
  char[] linia = { ' ', ' ', ' ' };
  linia[pozycja] = GRACZ[0];
  plansza[^1] = new string(linia); // to samo co: plansza[plansza.Length - 1] = new string(linia);
}

void PokazPlansze()
{
  Console.Clear();
  foreach (var linia in plansza)
  {
    Console.WriteLine(linia);
  }
  Console.WriteLine($"Punkty: {punkty}");
  Console.WriteLine($"Nitro: {(czyPaliwoNitro ? "Gotowe" : "Brak")}");
  if (czasNitro > 0)
  {
    Console.WriteLine($"Aktywne nitro: {czasNitro} sek");
  }
}

string UstawPrzeszkode(int pozycja)
{
  char[] linia = { ' ', ' ', ' ' };
  linia[pozycja] = PRZESZKODA[0];
  return new string(linia);
}

string UstawNitro(int pozycja,string linia)
{
  char[] znaki = linia.ToCharArray();
  znaki[pozycja] = NITRO[0];
  return new string(znaki);
}
