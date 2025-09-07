using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace _5LettersHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WordFilterService _filterService;

        public MainWindow()
        {
            InitializeComponent();
            _filterService = new WordFilterService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string mask = TextBox1_Mask.Text.ToLowerInvariant();
            if (mask.Length != 5)
            {
                System.Windows.MessageBox.Show("Маска слова должна состоять из 5 символов.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string excludedLetters = TextBox2_ExcludedLetters.Text.ToLowerInvariant();
            string includedLetters = TextBox3_IncludedLetters.Text.ToLowerInvariant();

            List<string> finalWords = _filterService.GetFilteredWords(mask, excludedLetters, includedLetters);

            Border border = (Border)FinalWordsList.Template.FindName("TextureBorder", FinalWordsList);
            if (border != null)
            {
                ImageBrush? backgroundTexture = border.Background as ImageBrush;
                if (backgroundTexture != null)
                {
                    if (finalWords.Count > 30)
                    {
                        FinalWordsList.FontSize = 26;
                        backgroundTexture.Viewport = new Rect(0, 0, 52, 52);
                    }
                    else
                    {
                        FinalWordsList.FontSize = 38;
                        backgroundTexture.Viewport = new Rect(55, 0, 78, 78);
                    }
                }
            }

            FinalWordsList.ItemsSource = finalWords;
        }

        private void TextBox_Mask_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, 0) && e.Text != "*")
                e.Handled = true;
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, 0))
                e.Handled = true;
        }
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }
}