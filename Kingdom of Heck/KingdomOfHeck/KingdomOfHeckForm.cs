﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Engine;

namespace KingdomOfHeck
{
    public partial class KingdomOfHeckForm : Form
    {
        private Player _player;
        private Monster _currentMonster;

        public KingdomOfHeckForm()
        {
            InitializeComponent();

            _player = new Player(10, 10, 20, 0, 1);
            MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            _player.Inventory.Add(new InventoryItem(
                World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1));

            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        }
        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToNorth);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToEast);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToSouth);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void MoveTo(Location newLocation)
        {
            if (!_player.HasRequiredItemToEnterThisLocation(newLocation))
            {
                rtbMessages.Text += "You must have a " +
                newLocation.ItemRequiredToEnter.Name +
                " to enter this location." + Environment.NewLine;
                return;
            }

            // Update player's current location
            _player.CurrentLocation = newLocation;

            // Show/hide available movement buttons
            btnNorth.Visible = (newLocation.LocationToNorth != null);
            btnEast.Visible = (newLocation.LocationToEast != null);
            btnSouth.Visible = (newLocation.LocationToSouth != null);
            btnWest.Visible = (newLocation.LocationToWest != null);

            // Display current Location
            rtbLocation.Text = newLocation.Name + Environment.NewLine;
            rtbLocation.Text += newLocation.Description + Environment.NewLine;

            // Heal player at new location
            _player.CurrentHitPoints = _player.MaximumHitPoints;

            // Update Hit Points in UI
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();

            // Does the location have a quest?
            if (newLocation.QuestAvailableHere != null)
            {
                // See if player has quest and if its completed
                bool playerAlreadyHasQuest =
                 _player.HasThisQuest(newLocation.QuestAvailableHere);
                bool playerAlreadyCompletedQuest =
                 _player.CompletedThisQuest(newLocation.QuestAvailableHere);

                // See if player already has quest
                if (playerAlreadyHasQuest)
                {
                    // If the player has not completed the quest yet
                    if (!playerAlreadyCompletedQuest)
                    {
                        // See if the player has all the items needed to complete the quest
                        bool playerHasAllItemsToCompleteQuest =
                         _player.HasAllQuestCompletionItems(newLocation.QuestAvailableHere);
                        // Player has all items to complete quest
                        if (playerHasAllItemsToCompleteQuest)
                        {
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += "You completed the " +
                                newLocation.QuestAvailableHere.Name +
                                " quest." + Environment.NewLine;

                            // Remove quest items from inventory
                            _player.RemoveQuestCompletionItems(newLocation.QuestAvailableHere);

                            //Give quest rewards
                            rtbMessages.Text += "You receive: " + Environment.NewLine;
                            rtbMessages.Text +=
                                newLocation.QuestAvailableHere.RewardExperiencePoints.ToString() +
                                " experience points" + Environment.NewLine;
                            rtbMessages.Text +=
                                newLocation.QuestAvailableHere.RewardGold.ToString() +
                                " gold" + Environment.NewLine;
                            rtbMessages.Text +=
                                newLocation.QuestAvailableHere.RewardItem.Name +
                                    Environment.NewLine;
                            rtbMessages.Text += Environment.NewLine;

                            _player.ExperiencePoints +=
                                newLocation.QuestAvailableHere.RewardExperiencePoints;
                            _player.Gold += newLocation.QuestAvailableHere.RewardGold;

                            // Add reward item to player inventory
                            _player.AddItemToInventory(newLocation.QuestAvailableHere.RewardItem);

                            // Mark quest completed
                            // Find quest in the player's quest list
                            _player.MarkQuestCompleted(newLocation.QuestAvailableHere);
                        }
                    }
                }
                else
                {
                    // Player does not have quest

                    // Display messages
                    rtbMessages.Text += "You receive the " +
                        newLocation.QuestAvailableHere.Name +
                        " quest." + Environment.NewLine;
                    rtbMessages.Text += newLocation.QuestAvailableHere.Description +
                        Environment.NewLine;
                    rtbMessages.Text += "To complete it, return with:" +
                        Environment.NewLine;
                    foreach(QuestCompletionItem qci in
                        newLocation.QuestAvailableHere.QuestCompletionItems)
                    {
                        if(qci.Quantity == 1)
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " +
                                qci.Details.Name + Environment.NewLine;
                        }
                        else
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " +
                                qci.Details.NamePlural + Environment.NewLine;
                        }
                    }
                    rtbMessages.Text += Environment.NewLine;

                    // Add the quest to the player quest list
                    _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));
                }
            }
            // Does location have a monster?
            if(newLocation.MonsterLivingHere != null)
            {
                rtbMessages.Text += "*Gasp!* You see a " + newLocation.MonsterLivingHere.Name +
                    Environment.NewLine;

                // Make new monster from World.Monster list
                Monster standardMonster = World.MonsterByID(
                    newLocation.MonsterLivingHere.ID);

                _currentMonster = new Monster(standardMonster.ID, standardMonster.Name,
                    standardMonster.MaximumDamage, standardMonster.RewardExperiencePoints,
                    standardMonster.RewardGold, standardMonster.CurrentHitPoints,
                    standardMonster.MaximumHitPoints);

                foreach(LootItem lootItem in standardMonster.LootTable)
                {
                    _currentMonster.LootTable.Add(lootItem);
                }

                cboWeapons.Visible = true;
                cboPotions.Visible = true;
                btnUseWeapon.Visible = true;
                btnUsePotion.Visible = true;
            }
            else
            {
                _currentMonster = null;

                cboWeapons.Visible = false;
                cboPotions.Visible = false;
                btnUseWeapon.Visible = false;
                btnUsePotion.Visible = false;

            }

            // Refresh player inventory
            UpdateInventoryListInUI();

            //Refresh player quest list
            UpdateQuestListInUI();

            //Refresh weapons combobox
            UpdateWeaponListInUI();

            // Refresh player's potions combobox
            UpdatePotionListInUI();
        }

        private void UpdateInventoryListInUI()
        {
            dgvInventory.RowHeadersVisible = false;
            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";
            dgvInventory.Rows.Clear();
            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] {
                    inventoryItem.Details.Name,
                    inventoryItem.Quantity.ToString() });
                }
            }
        }
        private void UpdateQuestListInUI()
        {
            dgvQuests.RowHeadersVisible = false;
            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";
            dgvQuests.Rows.Clear();
            foreach (PlayerQuest playerQuest in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] {
                playerQuest.Details.Name,
                playerQuest.IsCompleted.ToString() });
            }
        }

        private void UpdateWeaponListInUI()
        {
            List<Weapon> weapons = new List<Weapon>();
            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is Weapon)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        weapons.Add((Weapon)inventoryItem.Details);
                    }
                }
            }
            if (weapons.Count == 0)
            {
                // The player doesn't have any weapons, so hide the weapon
                //combobox and "Use" button
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";
                cboWeapons.SelectedIndex = 0;
            }
        }

        private void UpdatePotionListInUI()
        {
            List<HealingPotion> healingPotions = new List<HealingPotion>();
            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is HealingPotion)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        healingPotions.Add(
                        (HealingPotion)inventoryItem.Details);
                    }
                }
            }
            if (healingPotions.Count == 0)
            {
                // The player doesn't have any potions, so hide the potion
               // combobox and "Use" button
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPotions;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";
                cboPotions.SelectedIndex = 0;
            }
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            Weapon currentWeapon = (Weapon)cboWeapons.SelectedItem;

            int damageToMonster = RandomNumberGenerator.NumberBetween(
                currentWeapon.MinimumDamage, currentWeapon.MaximumDamage);

            _currentMonster.CurrentHitPoints -= damageToMonster;

            rtbMessages.Text += "You hit the " + _currentMonster.Name + " for " +
                    damageToMonster.ToString() + " points." + Environment.NewLine;

            //Check if monster is dead
            if (_currentMonster.CurrentHitPoints <= 0)
            {
                //Is Dead
                rtbMessages.Text += Environment.NewLine;
                rtbMessages.Text += "You defeated the " + _currentMonster.Name +
                    Environment.NewLine;

                //Give Experience
                _player.ExperiencePoints += _currentMonster.RewardExperiencePoints;
                rtbMessages.Text += "You receive " +
                    _currentMonster.RewardExperiencePoints.ToString() +
                    " experience points" + Environment.NewLine;

                //Give player gold
                _player.Gold += _currentMonster.RewardGold;
                rtbMessages.Text += "You recieve " +
                    _currentMonster.RewardGold.ToString() + " gold" + Environment.NewLine;

                //Get random loot item
                List<InventoryItem> lootedItems = new List<InventoryItem>();

                foreach (LootItem lootItem in _currentMonster.LootTable)
                {
                    if (RandomNumberGenerator.NumberBetween(1, 100) <= lootItem.DropPercentage)
                    {
                        lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                    }
                }

                if (lootedItems.Count == 0)
                {
                    foreach (LootItem lootItem in _currentMonster.LootTable)
                    {
                        if (lootItem.IsDefaultItem)
                        {
                            lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                        }
                    }
                }

                foreach (InventoryItem inventoryItem in lootedItems)
                {
                    _player.AddItemToInventory(inventoryItem.Details);

                    if (inventoryItem.Quantity == 1)
                    {
                        rtbMessages.Text += "You loot " +
                            inventoryItem.Quantity.ToString() + " " +
                                inventoryItem.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        rtbMessages.Text += "You loot " +
                            inventoryItem.Quantity.ToString() + " " +
                                inventoryItem.Details.NamePlural + Environment.NewLine;
                    }
                }

                //Refresh player inventory info & controls
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();
                lblGold.Text = _player.Gold.ToString();
                lblExperience.Text = _player.ExperiencePoints.ToString();
                lblLevel.Text = _player.Level.ToString();

                UpdateInventoryListInUI();
                UpdateWeaponListInUI();
                UpdatePotionListInUI();

                //Blank line for space in message box
                rtbMessages.Text += Environment.NewLine;

                // Move player to current location (to heal player and create a new monster to fight)
                MoveTo(_player.CurrentLocation);
            }
            else
            {
                //Monster still alive. Monster monster attacks player.
                int damageToPlayer =
                    RandomNumberGenerator.NumberBetween(0, _currentMonster.MaximumDamage);

                rtbMessages.Text += "The " + _currentMonster.Name + " did " +
                    damageToPlayer.ToString() + " points of damage." + Environment.NewLine;

                //If no damage message
                if( damageToPlayer == 0)
                {
                    rtbMessages.Text += "The " + _currentMonster.Name + " was too slow for you!! " +
                        "You evaded the " + _currentMonster.Name + "'s attack!!" + Environment.NewLine;
                }

                //If hit subtract damage
                _player.CurrentHitPoints -= damageToPlayer;

                //Refresh UI
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();

                //If player killed
                if (_player.CurrentHitPoints <= 0)
                {
                    rtbMessages.Text += "The " + _currentMonster.Name + " killed you." +
                           Environment.NewLine;

                    MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
                }
            }
        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            // Get the currently selected potion from the combobox
            HealingPotion potion = (HealingPotion)cboPotions.SelectedItem;

            _player.CurrentHitPoints = (_player.CurrentHitPoints + potion.AmountToHeal);

            //Prevent hit points over maximum
            if (_player.CurrentHitPoints > _player.MaximumHitPoints)
            {
                _player.CurrentHitPoints = _player.MaximumHitPoints;
            }

            foreach(InventoryItem ii in _player.Inventory)
            {
                if(ii.Details.ID == potion.ID)
                {
                    ii.Quantity--;
                    break;
                }
            }

            //Display message
            rtbMessages.Text += "You drink a " + potion.Name + Environment.NewLine;


            //Monster gets chance to attack after potion use as well
            int damageToPlayer =
                RandomNumberGenerator.NumberBetween(0, _currentMonster.MaximumDamage);

            rtbMessages.Text += "The " + _currentMonster.Name + " did " +
                   damageToPlayer.ToString() + " points of damage." + Environment.NewLine;

            //If no damage message
            if (damageToPlayer == 0)
            {
                rtbMessages.Text += "The " + _currentMonster.Name + " was too slow for you!! " +
                    "You evaded the " + _currentMonster.Name + "'s attack!!" + Environment.NewLine;
            }

            //If hit subtract damage
            _player.CurrentHitPoints -= damageToPlayer;

            //Refresh UI
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();

            //If player killed
            if (_player.CurrentHitPoints <= 0)
            {
                rtbMessages.Text += "The " + _currentMonster.Name + " killed you." +
                       Environment.NewLine;

                MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            }
        }
    }
}
