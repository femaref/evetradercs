using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Core;
using Core.ClassExtenders;
using Core.DomainModel;
using Core.Updaters;

namespace EveTrader.Main.Characters
{
    public partial class CharactersTab : UserControl
    {
        public CharactersTab()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            this.RenderCharactersList();
        }

        private void RenderCharactersList()
        {
            this.CharactersListView.Items.Clear();

            foreach (Character character in Settings.Instance.Characters)
            {
                ListViewItem listViewItem = new ListViewItem(
                        new string[] {
                            character.Name,
                            character.Balance.FormatCurrency()
                        });
                listViewItem.UseItemStyleForSubItems = false;
                listViewItem.SubItems[1].ForeColor = character.Balance > 0 ? Color.ForestGreen : Color.IndianRed;

                this.CharactersListView.Items.Add(listViewItem);

                Dictionary<string, int> counter = new Dictionary<string, int>();

                foreach (AccountBalance ab in character.Corporation.Wallets)
                {
                    var kvp = character.Corporation.WalletDivisions.Where(k => k.Key == ab.Key).SingleOrDefault();

                    if(counter.ContainsKey(character.Corporation.Name))
                        counter[character.Corporation.Name]++;
                    else
                        counter[character.Corporation.Name] = 1;

                    string header = character.Corporation.Name + ": " +
                                    (kvp != null
                                         ? kvp.Value
                                         : "Corporation Wallet" + counter[character.Corporation.Name]);

                    ListViewItem lvi = new                   ListViewItem(
                        new string[]
                            {
                                header,
                                ab.Balance.FormatCurrency()
                            });
                    
                    lvi.UseItemStyleForSubItems = false;
                    lvi.SubItems[1].ForeColor = ab.Balance > 0 ? Color.ForestGreen : Color.IndianRed;
                    this.CharactersListView.Items.Add(lvi);
                }


            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            CharacterInfoUpdater characterInfoUpdater = new CharacterInfoUpdater();

            foreach (Character character in Settings.Instance.Characters)
            {
                (this.ParentForm as MainWindow).StatusText = string.Format("Updating character {0}", character.Name);

                try
                {
                    characterInfoUpdater.UpdateCharacter(character);
                }
                catch (WebException)
                {
                    MessageBox.Show("Unable to connect to eve server", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.RenderCharactersList();

                (this.ParentForm as MainWindow).StatusText =
                    string.Format("All characters updated {0}", DateTime.Now.ToString("HH:mm"));
            }
        }
    }
}

