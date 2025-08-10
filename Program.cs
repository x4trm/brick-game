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
  }
  // Sprawdzenie kolizji z przeszkodą

  // int pozycjaPrzeszkody = plansza[plansza.Length-2];
  int pozycjaPrzeszkody = plansza[^2].IndexOf(PRZESZKODA);
  if (pozycjaGracza == pozycjaPrzeszkody)
  {
    czyUderzony = true;
    break;
  }


  // Dodawanie przeszkod

  int przeszkodNaPozycji = generatorLosowy.Next(3);
  string nowaPrzeszkoda = UstawPrzeszkode(przeszkodNaPozycji);
  for (int i = plansza.Length - 2; i > 0; i--)
  {
    plansza[i] = plansza[i - 1];
  }
  plansza[0] = nowaPrzeszkoda;
  

  UstawGracza(pozycjaGracza);
  PokazPlansze();
  Thread.Sleep(600);
}
Console.Clear();
Console.WriteLine("GAME OVER");
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
}

string UstawPrzeszkode(int pozycja)
{
  char[] linia = { ' ', ' ', ' ' };
  linia[pozycja] = PRZESZKODA[0];
  return new string(linia);
}