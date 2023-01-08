using Khattana.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFom.Admin.Database;
using Model.Retail.Model;
using Model.Repair.Model;
using WinFom.Common.Forms;

namespace WinFom.RepairUI.Forms
{
    public partial class AddRepItemForm : Form
    {
        public int ItemId = 0;
        private ItemCategory itemCategory = null;
        private List<ItemCategory> itemCats = null;
        private Location location = null;
        private StoreLocation storeLocation = null;
        private List<Location> locations = null;
        private List<StoreLocation> storeLocations = null;
        public AddRepItemForm()
        {
            InitializeComponent();
            
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadLocations()
        {
            try
            {
                using (Context db = new Context())
                {
                    if(locations != null)
                    {
                        locations.Clear();
                        locations = null;
                    }
                    locations = db.Locations.OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCbLocations()
        {
            cbLocations.DataSource = locations;
            cbLocations.DisplayMember = "Name";
            cbLocations.ValueMember = "Id";
        }
        private void LoadStoreLocations()
        {
            try
            {
                if(storeLocations != null)
                {
                    storeLocations.Clear();

                }
                using (Context db = new Context())
                {
                    storeLocations = db.StoreLocations.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCbStoreLocations()
        {
            cbStoreLocations.DataSource = storeLocations;
            cbStoreLocations.DisplayMember = "LocationName";
            cbStoreLocations.ValueMember = "Id";
        }
        private void LoadItemCats()
        {
            try
            {
                using (Context db = new Context())
                {
                    if(itemCats != null)
                    {
                        itemCats.Clear();
                        itemCats = null;
                    }
                    itemCats = db.ItemCategories.OrderBy(a => a.Title).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCBCats()
        {
            cbCategories.DataSource = itemCats;
            cbCategories.DisplayMember = "Title";
            cbCategories.ValueMember = "Id";
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait1 = new WaitForm(LoadItemCats);
                WaitForm wait2 = new WaitForm(LoadLocations);
                WaitForm wait3 = new WaitForm(LoadStoreLocations);
                wait1.ShowDialog();
                wait2.ShowDialog();
                wait3.ShowDialog();
                BindCBCats();
                BindCbLocations();
                BindCbStoreLocations();

                Gujjar.TB4(pMain);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                if(itemCategory ==  null)
                {
                    throw new Exception("Please select Item category");
                }
                if(location == null)
                {
                    throw new Exception("Please select item location");
                }
                if(storeLocation == null)
                {
                    throw new Exception("Please select store location");
                }
                string confMessage = string.Format("Category ({0}), item ({1}), Working Location ({2}), Storing location ({3})", 
                    itemCategory.Title, tbItemName.Text, location.Name, storeLocation.LocationName);
                DialogResult res = Gujjar.ConfirmYesNo("Please confirm... !!\n" + confMessage);
                if (res == DialogResult.No)
                    return;

                string itemName = tbItemName.Text;
                using (Context db = new Context())
                {
                    var dbObj = db.RepItems.ToList().FirstOrDefault(a => a.ItemCategoryId == itemCategory.Id 
                    && a.LocationId == location.Id 
                    && a.Name.ToLower().Equals(itemName.ToLower()));
                    if (dbObj != null)
                    {
                        throw new Exception("Item already added in database");
                    }

                    RepItem repItem = new RepItem
                    {
                        Id = 0,
                        Location = null,
                        LocationId = location.Id,
                        ItemCategory = null,
                        ItemCategoryId = itemCategory.Id,
                        RepEntries = null,
                        Name = itemName,
                        Weight = 0,
                        SACount = 0,
                        ItemPurchaseEntries = null,
                        SKU = 0,
                        TotalValue = 0,
                        UnitValue = 0,
                        UR = 0,
                        USCount = 0,
                        StoreLocationId = storeLocation.Id,
                    };
                    repItem = db.RepItems.Add(repItem);
                    db.SaveChanges();

                    ItemId = repItem.Id;
                    Gujjar.InfoMsg("Item added in database successfully");
                    Close();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategories.SelectedIndex == -1)
                return;

            itemCategory = cbCategories.SelectedItem as ItemCategory;
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            try
            {
                AddRepCategoryForm form = new AddRepCategoryForm();
                form.ShowDialog();
                if(form.CategoryId != 0)
                {
                    int id = form.CategoryId;
                    WaitForm wait = new WaitForm(LoadItemCats);
                    wait.ShowDialog();
                    BindCBCats();
                    cbCategories.SelectedItem = cbCategories.Items.OfType<ItemCategory>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddLocation_Click(object sender, EventArgs e)
        {
            try
            {
                AddItemLocationForm form = new AddItemLocationForm();
                form.ShowDialog();
                int id = form.LocationId;
                if(id != 0)
                {
                    WaitForm wait = new WaitForm(LoadLocations);
                    wait.ShowDialog();

                    BindCbLocations();
                    cbLocations.SelectedItem = cbLocations.Items.OfType<Location>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocations.SelectedIndex == -1)
                return;

            location = cbLocations.SelectedItem as Location;
        }

        private void btnAddStoreLocation_Click(object sender, EventArgs e)
        {
            try
            {
                AddItemStoreLocation form = new AddItemStoreLocation();
                form.ShowDialog();
                int id = form.LocationId;
                if (id != 0)
                {
                    WaitForm wait = new WaitForm(LoadStoreLocations);
                    wait.ShowDialog();

                    BindCbStoreLocations();
                    cbStoreLocations.SelectedItem = cbStoreLocations.Items.OfType<StoreLocation>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbStoreLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbStoreLocations.SelectedIndex == -1)
            {
                storeLocation = null;
                return;
            }
            storeLocation = cbStoreLocations.SelectedItem as StoreLocation;
        }
    }
}
