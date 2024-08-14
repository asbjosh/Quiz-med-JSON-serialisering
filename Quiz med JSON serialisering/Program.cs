using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main(string[] args)
    {
        bool gentagQuiz = true;

        while (gentagQuiz)
        {
            // Læs og parse JSON-data
            var json = File.ReadAllText("C:\\Users\\ah\\Desktop\\Kode\\Quiz-med-JSON-serialisering-master\\Quiz med JSON serialisering\\quiz.json");
            JArray quizSpørgsmål = JArray.Parse(json);

            // Afvikl quizzen
            foreach (var item in quizSpørgsmål)
            {
                var spørgsmål = item["spørgsmål"].ToString();
                var muligheder = item["muligheder"] as JObject;
                var rigtigtSvar = item["rigtigt_svar"].ToString();

                VisSpørgsmål(spørgsmål, muligheder);

                Console.Write("Dit svar: ");
                var brugerSvar = Console.ReadLine();

                string resultat = tjekSvar(brugerSvar, rigtigtSvar);
                Console.WriteLine(resultat);
                Console.ReadKey();
            }

            Console.WriteLine("Quizzen er slut! Tak for din deltagelse. Tryk på en tast for at gå videre");
            Console.ReadKey();

            // Spørg brugeren om de vil gentage quizzen
            gentagQuiz = provIgen();
        }

        Console.WriteLine("Farvel! Tak fordi du deltog.");
    }
    static string tjekSvar(string brugerSvar, string rigtigtSvar)
    {
        if (brugerSvar.ToUpper() == rigtigtSvar.ToUpper())
        {
            return "Korrekt!";
        }
        else
        {
            return $"Forkert! Det rigtige svar var: {rigtigtSvar}";
        }

    }
    static void VisSpørgsmål(string spørgsmål, JObject muligheder)
    {
        Console.Clear();
        Console.WriteLine(spørgsmål);

        foreach (var mulighed in muligheder)
        {
            Console.WriteLine($"{mulighed.Key}: {mulighed.Value}");
        }
    }
    static bool provIgen()
    {
        string svar;
        do
        {
            Console.Clear();
            Console.Write("Vil du prøve quizzen igen? (ja/nej): ");
            svar = Console.ReadLine()?.ToLower();

            if (svar != "ja" && svar != "nej")
            {
                Console.WriteLine("Du kan kun svare 'ja' eller 'nej'. Prøv igen.");
            }
        }
        while (svar != "ja" && svar != "nej");

        return svar == "ja";
    }
}