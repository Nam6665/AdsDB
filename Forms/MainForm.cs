using AdsDB.Forms;
using AdsDB.Models;
using AdsDB.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AdsDB
{
    public partial class MainForm : Form
    {
        private readonly DatabaseService _dbService;
        public MainForm()
        {
            InitializeComponent();
            string connectionString = "Server=DESKTOP-GMU6PFE;Database=AdsDB;Trusted_Connection=True;";
            _dbService = new DatabaseService(connectionString);
            LoadAds();
        }
        private void LoadAds()
        {
            try
            {
                var ads = _dbService.GetAds();
                dgvAds.DataSource = ads.ToList();
                MessageBox.Show($"Antal annonser laddade: {ads.Count()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett fel uppstod vid laddning av annonser: {ex.Message}");
            }
        }

        private void btnAdd_Click_Click(object sender, EventArgs e)
        {
            var adForm = new AdForm(_dbService);
            if (adForm.ShowDialog() == DialogResult.OK)
            {
                LoadAds();
            }
        }

        private void btnEdit_Click_Click(object sender, EventArgs e)
        {
            if (dgvAds.CurrentRow?.DataBoundItem is Ad selectedAd)
            {
                var adForm = new AdForm(_dbService, selectedAd);
                if (adForm.ShowDialog() == DialogResult.OK)
                {
                    LoadAds();
                }
            }
        }

        private void btnDelete_Click_Click(object sender, EventArgs e)
        {
            if (dgvAds.CurrentRow?.DataBoundItem is Ad selectedAd)
            {
                var confirmResult = MessageBox.Show("Vill du verkligen ta bort denna annons?", "Bekräfta borttagning", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    _dbService.DeleteAd(selectedAd.Id);
                    LoadAds();
                }
            }
        }
    }
}
