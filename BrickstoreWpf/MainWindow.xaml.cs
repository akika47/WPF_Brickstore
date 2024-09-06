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
                        ItemTypeID = Convert.ToChar(item.Element("ItemTypeID")!.Value),
                        ColorID = Convert.ToInt32(item.Element("ColorID")!.Value),
                        ItemName = item.Element("ItemName")!.Value,
                        ItemTypeName = item.Element("ItemTypeName")!.Value,
                        ColorName = item.Element("ColorName")!.Value,
                        CategoryID = Convert.ToInt32(item.Element("CategoryID")!.Value),
                        CategoryName = item.Element("CategoryName")!.Value,
                        Status = Convert.ToChar(item.Element("Status")!.Value),
                        Qty = Convert.ToInt32(item.Element("Qty")!.Value),
                        Price = Convert.ToDouble(item.Element("Price")!.Value.ToString().Replace(".",",")),
                        Condition = Convert.ToChar(item.Element("Condition")!.Value),
                        OrigQty = Convert.ToInt32(item.Element("OrigQty")?.Value),
                    };
                    LegoElemekLista.Add(legoElem);
                }
                dgLegoKeszlet.ItemsSource = LegoElemekLista;
                tbElemekSzama.Text = $"Elemek száma: {LegoElemekLista.Count}";
            }
        }

        private void txtSzuro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSzuro.Text))
            {
                var szuro = txtSzuro.Text.ToLower();
                dgLegoKeszlet.ItemsSource = LegoElemekLista.Where(x => x.ItemID.ToLower().Contains(szuro) || x.ItemName.ToLower().Contains(szuro) || x.CategoryName.ToLower().Contains(szuro));
            }
            else
            {
                dgLegoKeszlet.ItemsSource = LegoElemekLista;
            }
        }

    }

    public class LegoElem
    {
        public required string ItemID { get; set; }
        public required char ItemTypeID { get; set; }
        public required int ColorID { get; set; }
        public required string ItemName { get; set; }
        public required string ItemTypeName { get; set; }
        public required string ColorName { get; set; }
        public required int CategoryID { get; set; }
        public required string CategoryName { get; set; }
        public required char Status { get; set; }
        public required int Qty { get; set; }
        public required double Price { get; set; }
        public required char Condition { get; set; }
        public required int OrigQty { get; set; }
    }
}