using AdsDB.Models;
using AdsDB.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AdsDB.Forms
{
    public partial class AdForm : Form
    {
        private readonly DatabaseService _dbService;
        private Ad _ad;

        // Konstruktor för att skapa en ny annons
        public AdForm(DatabaseService dbService)
        {
            InitializeComponent();
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService)); // Säkerställ att dbService inte är null
            _ad = new Ad();
        }

        // Konstruktor som tar både DatabaseService och en Ad-instans (för att uppdatera en befintlig annons)
        public AdForm(DatabaseService dbService, Ad ad) : this(dbService)
        {
            _ad = ad;
            txtTitle.Text = _ad.Title;
            txtDescription.Text = _ad.Description;
            txtPrice.Text = _ad.Price.ToString();

            // Försök att sätta den valda kategorin från den befintliga annonsen
            var category = _dbService.GetCategories().FirstOrDefault(c => c.Id == _ad.CategoryId);
            if (category != null)
            {
                cmbCategory.SelectedItem = category;
            }
        }
        private void LoadCategories()
        {
            // Hämta kategorier från databasen
            var categories = _dbService.GetCategories().ToList();

            // Kontrollera om kategorierna verkligen hämtas
            if (categories == null || !categories.Any())
            {
                MessageBox.Show("Inga kategorier finns i databasen. Vänligen lägg till kategorier först.");
                return;
            }
            // Lägg till ett standardval "Välj kategori" i början av listan
            categories.Insert(0, new Category { Id = 0, Name = "Välj kategori" });
            // Ställ in ComboBoxens data
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "Name"; // Visa namn på kategorin i ComboBox
            cmbCategory.ValueMember = "Id";    // Använd Id som värde för varje kategori
            cmbCategory.SelectedIndex = 0;     // Sätt standardvalet till första objektet

            // Debug för att kolla om kategorierna har laddats korrekt
            Console.WriteLine("Kategorier laddade:");
            foreach (var category in categories)
            {
                Console.WriteLine($"Id: {category.Id}, Name: {category.Name}");
            }
        }

        // När formuläret laddas, fyll ComboBox med kategorier
        private void AdForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        // När användaren försöker spara annonsen
        private void btnSave_Click_Click(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedItem == null || ((Category)cmbCategory.SelectedItem).Id == 0)
            {
                MessageBox.Show("Vänligen välj en giltig kategori.");
                return;
            }
            // Validering av titel, beskrivning och pris
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Vänligen ange en titel.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Vänligen ange en beskrivning.");
                return;
            }
            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Vänligen ange ett giltigt pris.");
                return;
            }

            // Uppdatera annonsens information från formuläret
            _ad.Title = txtTitle.Text;
            _ad.Description = txtDescription.Text;
            _ad.Price = price;

            // Hämta CategoryId från den valda kategorin
            _ad.CategoryId = ((Category)cmbCategory.SelectedItem).Id;

            try
            {
                // Lägg till eller uppdatera annonsen beroende på om det är en ny eller befintlig annons
                if (_ad.Id == 0)
                {
                    _dbService.AddAd(_ad);  // Lägg till en ny annons
                }
                else
                {
                    _dbService.UpdateAd(_ad);  // Uppdatera den befintliga annonsen
                }
                // Stäng formuläret om det gick bra
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                // Hantera eventuella fel
                MessageBox.Show($"Ett fel inträffade: {ex.Message}", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
