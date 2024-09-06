using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Win32;

namespace BrickstoreWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<LegoElem> LegoElemekLista;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnMegnyit_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "BSX fájlok (*.bsx)|*.bsx";
            if (openFileDialog.ShowDialog() == true)
            {
                var xaml = XDocument.Load(openFileDialog.FileName);
                LegoElemekLista = new List<LegoElem>();
                foreach (var item in xaml.Descendants("Item"))
                {
                    var legoElem = new LegoElem
                    {
                        ItemID = item.Element("ItemID")!.Value,
                        ItemName = item.Element("ItemName")!.Value,
                        CategoryName = item.Element("CategoryName")!.Value,
                        ColorName = item.Element("ColorName")!.Value,
                        Qty = item.Element("Qty")!.Value
                    };
                    LegoElemekLista.Add(legoElem);
                }
                dgLegoKeszlet.ItemsSource = LegoElemekLista;
                tbElemekSzama.Text = $"Elemek száma: {LegoElemekLista.Count}";
            }
        }



    }

    public class LegoElem
    {
        public required string ItemID { get; set; }
        public required string ItemName { get; set; }
        public required string CategoryName { get; set; }
        public required string ColorName { get; set; }
        public required string Qty { get; set; }
    }
}