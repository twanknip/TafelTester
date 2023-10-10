using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tafel_tester
{
    public partial class MainWindow : Window
    {
        // verschillende klasse van Window ze slaan verschillende typen gegevens (int), (string), en een array (operators)
        int num01;
        private int num02;
        string ans;
        string correctAnswer;
        private int passSums = 0;
        private int diffc = 1;
        private int score = 0;
        //operators hier aangemaakt
        string[] operators = {"x","-","+",":"};
        // hier maak je een instanties van klassen tempUserInput -> Userinput random -> Random
        new UserInput tempUserInput = new UserInput();
        Random random = new Random();

        public MainWindow()
        {
            // num01 en num02 worden toegewezen aan een willekeurige int dus 0 tot 9 -> random.Next()
            // Fnumber is een string met een vermenigvuldigingsvraag op basis van de willekeurige getallen met de X oppertor 
            InitializeComponent();   
            num01 = random.Next(10);
            num02 = random.Next(10);
            Fnumber.Content = $"Wat is {num01} x {num02}";
        }


        #region begin button 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // main.visibility word verborgen (visibility.Hidden)
            // het andere deel van het venster diff word wel zichtbaar (visibility.visible)
            // de gebruikersnaam word ingevoerd in een tekstvak tb1 en deze word hierna weer weergeven in welkomtxt
            // de nextsom() word ook aangeroepen om de volgende som voor te bereiden
            main.Visibility = Visibility.Hidden;
            diff.Visibility = Visibility.Visible;
            tempUserInput.UserName = tb1.Text;
            welkomtxt.Content = "Welkom " + tempUserInput.UserName;
            nextsom();
        }
        #endregion




        #region Moeilijkheidsgraden
        //afhankelijk van welke button je klikt wordt de diffc dus moeilijkheid ingesteld op 1,3 of 5 ook word het huidige venster verborgen dat heb ik boven in de code uitgelegd
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //easy moeilijkheid
            diffc = 1;
            Game.Visibility = Visibility.Visible;
            diff.Visibility = Visibility.Hidden;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //medium moeilijkheid
            diffc = 3;
            Game.Visibility = Visibility.Visible;
            diff.Visibility = Visibility.Hidden;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //hard moeilijkheid
            diffc = 5;
            Game.Visibility = Visibility.Visible;
            diff.Visibility = Visibility.Hidden;

        }
        #endregion





        #region nextsom
        public void nextsom()
        {
            //random som generator
            //de eerste regel zorgt ervoor dat het andwoord invulvled word leegemaakt dan kan de speler natuurlijk een nieuw andwoord invullen voor de volgende som
            // op de tweede krijgt num01 een nieuw cijfer door de 1 en 10 word er een getal gemaakt tussen de 1 en de 10 
            // op de laatste regel plaatse hij de 2 cijfers en een random opperator
            //Kortom de nextsom() functie bereidt een nieuwe willekeurige som voor en laat deze aan de speler zien in een label zodat de speler kan proberen het correcte antwoord in te voeren.
            yourAnswer.Text = "";
            num01 = random.Next(1,10 * diffc);
            num02 = random.Next(1,10 * diffc);
            Fnumber.Content = $"Wat is {num01} {operators[random.Next(operators.Length)]} {num02}";


        }
        #endregion




        #region textbox
        private void textbox_yourAnswer(object sender, KeyPressEventArgs e)
        {
            //textbox is de event handler die reageert op wat de gebruiker invoert
            //hier controleert of de toetsaanslag geen controlekarakter, cijfer of decimaalteken is. Als het geen van deze is, wordt de toetsaanslag genegeerd en niet weergegeven in het tekstvak.
            //uitleg
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // accepteer alleen 1 decimaal
            // dus is maar 1 decimaalteken toegestaan
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        #endregion restart 


        #region 
        public void Check_Click(object sender, RoutedEventArgs e)
        {
            // int correctawnser;
            
            //alles was je hier invult word doorgegeven aan de variable "ans"
            ans = yourAnswer.Text;
            //
            if (Fnumber.Content.ToString().Contains("x"))
            {
                int tempAnswer = num01 * num02;
                correctAnswer = tempAnswer.ToString();

                if (ans == correctAnswer)
                {
                    //als dit goed is doet hier dingen in
                    MessageBox.Show("antwoord goed");
                    score++;
                }
                else
                {
                    MessageBox.Show("Antwoord fout");
                }
            }
            if (Fnumber.Content.ToString().Contains(":"))
            {
                    
                int tempAnswer = num01 / num02;
                correctAnswer = tempAnswer.ToString();

                if (ans == correctAnswer)
                {
                    //als dit goed is doet hier dingen in
                    MessageBox.Show("antwoord goed");
                    score++;
                }
                else
                {
                    MessageBox.Show("Antwoord fout");
                }
            }
            if (Fnumber.Content.ToString().Contains("+"))
            {
                int tempAnswer = num01 + num02;
                correctAnswer = tempAnswer.ToString();

                if (ans == correctAnswer)
                {
                    //als dit goed is doet hier dingen in
                    MessageBox.Show("antwoord goed");
                    score++;
                }
                else
                {
                    MessageBox.Show("Antwoord fout");
                }
            }
            if (Fnumber.Content.ToString().Contains("-"))
            {
                int tempAnswer = num01 - num02;
                correctAnswer = tempAnswer.ToString();

                if (ans == correctAnswer)
                {
                    //als dit goed is doet hier dingen in
                    MessageBox.Show("antwoord goed");
                    score++;
                }
                else
                {
                    MessageBox.Show("Antwoord fout");
                }
            }

            passSums++;
            nextsom();
            //live highscore
            Highscore.Content = "Je Score: " + score;
            Debug.WriteLine(score);
            if(passSums == 10) { 
            
                //einde scherm
                Game.Visibility = Visibility.Hidden;
                Endgame.Visibility = Visibility.Visible;
            }
        }
        //Check_Click haalt het antwoord van de speler op, controleert het voor de wiskundige bewerking, toont een passend bericht en werkt de score bij. Het bereidt nieuwe sommen voor en toont het eindscherm bij voltooiing.
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            //Reset alles
            Endgame.Visibility = Visibility.Hidden;
            diff.Visibility = Visibility.Visible;
            passSums = 0;
            score = 0;
        }
    }
}
#endregion