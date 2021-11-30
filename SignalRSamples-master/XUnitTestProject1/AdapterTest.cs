using SgClient1;
using SgClient1.Adapter;
using SgClient1.Builder;
using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsClient;
using Xunit;

namespace XUnitTestProject1
{
    public class AdapterTest
    {
        [Fact]
        public void TestAdapter()
        {
            FrmClient cl = new FrmClient();
            FormGame frm = new FormGame(cl.getInstance());
            frm.recreateZombies();
            frm.clearPbs();

            frm.playerInteractions.Weapon = new WeaponDirector(new PistolBuilder()).MakeWeapon().GetWeapon();
            //frm.Form1_Load(this, new EventArgs());
            Zombie zombie = frm.zm.Find("zombie1");
            frm.playerInteractions.DoDamage(zombie, new Control());
            new ZombieAdapter(zombie).DoDamage(frm.playerInteractions, new Control());
            int phealth2 = frm.playerInteractions.GetHealth();
            int zhealth2 = frm.playerInteractions.GetHealth();
            Assert.True(100 > phealth2 && 3 > zhealth2);
        }
    }
}
