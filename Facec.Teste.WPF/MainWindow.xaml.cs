using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Facec.Teste.WPF
{
    public partial class MainWindow : Window
    {

        public ObservableCollection<Cliente> Clientes { get; set; } = new ObservableCollection<Cliente>();
        public Cliente Cliente { get; set; } = new Cliente(string.Empty, string.Empty);
        public MainWindow()
        {
            InitializeComponent();
            SetDataContext();
        }
        private void SetDataContext()
        {
            DataContext = null;
            DataContext = this;
        }

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://facec-webapi-2022.herokuapp.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(10);

                    var json = new JavaScriptSerializer().Serialize(new Cliente(txtDocumento.Text, txtNome.Text));
                    var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = client.PostAsync("clientes", conteudo).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Erro ao gravar cliente!" +
                            $"\n {response.Content.ReadAsStringAsync().Result}");
                        return;
                    }

                    MessageBox.Show("Sucesso ao gravar cliente!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }    

        private void btnIncluir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://facec-webapi-2022.herokuapp.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(10);

                    var response = client.GetAsync("clientes").Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Erro ao listar cliente!" +
                            $"\n {response.Content.ReadAsStringAsync().Result}");
                        return;
                    }
                    var result = response.Content.ReadAsStringAsync().Result;
                    
                    Clientes = new JavaScriptSerializer().Deserialize<ObservableCollection<Cliente>>(result);
                    SetDataContext();
                    MessageBox.Show("Sucesso ao listar cliente!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            private void btnExcluir_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://facec-webapi-2022.herokuapp.com/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.Timeout = TimeSpan.FromSeconds(10);

                        var response = client.DeleteAsync($"clientes/{Cliente.Id}").Result;
                        btnListar_Click(null, null);

                        if (!response.IsSuccessStatusCode)
                        {
                            MessageBox.Show($"Erro ao Excluir cliente!" +
                                $"\n {response.Content.ReadAsStringAsync().Result}");
                            return;
                        }
                        MessageBox.Show("Sucesso ao Excluir cliente!");

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
}