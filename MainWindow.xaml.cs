using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows;



namespace Mini_Save_Fiddler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void Decrypt_Save(object sender, RoutedEventArgs e)
        {
            var save_file = new OpenFileDialog();
            save_file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\LocalLow\MiniHealer\MiniHealer";
            save_file.DefaultExt = "txt";
            save_file.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            Nullable<bool> result = save_file.ShowDialog();

            if (result == true)
            {
                var save_content = File.ReadAllText(save_file.FileName);

                var decrypted_save = new Save_Fiddler().Decrypt(save_content);

                var decrypted_content = JToken.Parse(decrypted_save).ToString(Formatting.Indented);

                var decrypted_output = new SaveFileDialog();
                decrypted_output.DefaultExt = "json";
                decrypted_output.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

                var output_result = decrypted_output.ShowDialog();

                if (output_result == true)
                {
                    File.WriteAllText(save_file.FileName + System.DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".bak", save_content);
                    File.WriteAllText(decrypted_output.FileName, decrypted_content);
                }
            }
        }

        private void Encrypt_Save(object sender, RoutedEventArgs e)
        {
            var save_file = new OpenFileDialog();
            save_file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\LocalLow\MiniHealer\MiniHealer";
            save_file.DefaultExt = "json";
            save_file.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

            var result = save_file.ShowDialog();

            if (result == true)
            {
                var save_content = File.ReadAllText(save_file.FileName);
                save_content = JToken.Parse(save_content).ToString(Formatting.None);
                var encrypted_save = new Save_Fiddler().Encrypt(save_content);

                var encrypted_output = new SaveFileDialog();
                encrypted_output.DefaultExt = "txt";
                encrypted_output.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                Nullable<bool> output_result = encrypted_output.ShowDialog();

                if (output_result == true)
                {
                    File.WriteAllText(encrypted_output.FileName, encrypted_save);
                    File.WriteAllText(encrypted_output.FileName.Replace(".txt", "_CLOUD.txt"), encrypted_save);
                }
            }
        }
    }
}